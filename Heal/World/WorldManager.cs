#region Usings

using System;
using System.Collections.Generic;
using System.Threading;
using Heal.Core.AI;
using Heal.Core.Entities;
using Heal.Core.Entities.Enemies;
using Heal.Core.GameData;
using Heal.Core.Sence;
using Heal.Sprites;
using Heal.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Heal.Component;
using Heal.Data;
using Heal.Core.Utilities;
using Heal.Levels;
using System.Text;
using Heal.GameState;

#endregion

namespace Heal.World
{
    public class WorldManager : GameClassManager, ISprite, IUpdateable
    {
        #region Instance

        private static WorldManager m_instance;

        public WorldManager()
        {
            m_instance = this;
        }

        public static WorldManager GetInstance()
        {
            return m_instance;
        }

        #endregion

        #region Private Functions

        /// <summary>
        ///   Makes the color array to byte.
        /// </summary>
        /// <param name="colors">The colors.</param>
        /// <returns></returns>
        internal byte[] MakeColorArrayToByte(IList<Color> colors)
        {
            byte[] bytes;
            int n = WorldPart.ImageSize;
            bytes = new byte[(n * n) >> 3];
            for (int i = 0; i < n * n; i++)
            {
                if (colors[i].A < 100)
                {
                    //bytes[i >> 3] |= (byte) ( 0x0 << ( i & 0x07 ))
                    colors[i] = Color.Transparent;
                }
                else
                {
                    bytes[i >> 3] |= (byte)(0x1 << (i & 0x07));
                    colors[i] = Color.Black;
                }
            }
            return bytes;
        }

        /// <summary>
        ///   Check is the location in the area.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        private Vector2 CheckLocation(Vector2 value)
        {
            Vector2 t = value;
            t.X = MathHelper.Clamp(t.X, Space.X / 2 / Scale, m_worldSize.X - Space.X / 2 / Scale);
            t.Y = MathHelper.Clamp(t.Y, Space.Y / 2 / Scale, m_worldSize.Y - Space.Y / 2 / Scale);
            return t;
        }

        #endregion

        private MapManager m_level;
        private GraphicsManager m_effect;
        private SenceManager m_senceManager;
        private EntityCreator m_entityCreator;
        private DialogManager m_dialog;
        private CameraManager m_camera;

        private SplashTools m_splash;
        private HandbookTools m_handbook;
        internal MapDrawer[, ,] Pieces;
        private WorldLayer[] m_layers;
        private WorldPart[,] m_part;

        private List<Unit> m_units;
        private List<Item> m_item;
        private List<Region> m_regions;
        public Player Player;

        /// <summary>
        ///   Gets or sets the display space.
        /// </summary>
        /// <value>Display space.</value>
        public Vector2 Space;

        private Vector2 m_destination;
        private Vector2 m_locate;

        private int[] m_drawItems = new int[4];
        private int m_layerCount;
        private Point m_size;
        private Vector2 m_speed;
        private int m_collusionLayer;

        private Point m_worldSize;

        private bool m_lastFreeze;
        private float m_freezeTime;

        private float m_loadingTimer;
        public float Scale = 1f;

        public RunningState State = RunningState.Loading;
        private RunningState m_lastState = RunningState.Loading;

        private MapManager.MapLoadingData m_data;

        private Texture2D m_loadingImage;
        private RenderTarget2D m_resolveTexture;

        public enum RunningState
        {
            Running,
            Loading,
            Speaking,
            Viewing,
            Splash,
            Handbook
        }

        /// <summary>
        ///   Gets or sets the size of the world.
        /// </summary>
        /// <value>
        ///   The size of the world.
        /// </value>
        public Point WorldSize
        {
            get { return m_worldSize; }
            set { m_worldSize = value; }
        }

        /// <summary>
        ///   Gets or sets the locate.
        /// </summary>
        /// <value>The locate.</value>
        public Vector2 Locate
        {
            get { return m_locate; }
            set { m_locate = CheckLocation(value); }
        }

        /// <summary>
        ///   Gets or sets the destination.
        /// </summary>
        /// <value>The destination.</value>
        public Vector2 Destination
        {
            get { return m_destination; }
            set
            {
                m_destination = CheckLocation(value);
                m_senceManager.CameraFollow = m_destination;
            }
        }

        public WorldPart this[int x, int y]
        {
            get { return (m_part[x, y]); }
            set { m_part[x, y] = value; }
        }

        public void UpdateDraw(GameTime gameTime)
        {
            // Calcuate which locate should camera move.
            m_destination = m_senceManager.CameraFollow;
            m_locate = m_senceManager.CameraLocate;
            m_speed = m_destination - m_locate;
            m_locate += m_speed * (float)gameTime.ElapsedGameTime.TotalMilliseconds / 1000 * 5;
            m_locate = CheckLocation(m_locate);
            m_senceManager.CameraLocate = m_locate;
            // Calculate which block should be shown in screen.
            m_drawItems[0] = Math.Max((int)(m_locate.X - (Space.X / Scale) / 2 / 0.8f) / WorldPart.ImageSize, 0);
            m_drawItems[2] = Math.Max((int)(m_locate.Y - (Space.Y / Scale) / 2 / 0.8f) / WorldPart.ImageSize, 0);
            m_drawItems[1] = Math.Min((int)(m_locate.X + (Space.X / Scale) / 2 / 0.8f) / WorldPart.ImageSize,
                                       m_size.X - 1);
            m_drawItems[3] = Math.Min((int)(m_locate.Y + (Space.Y / Scale) / 2 / 0.8f) / WorldPart.ImageSize,
                                       m_size.Y - 1);
            m_effect.Parameters("Wave", "Intensity").SetValue(
                                                                   MathHelper.WrapAngle(
                                                                                           (float)
                                                                                           gameTime.TotalGameTime.
                                                                                               TotalSeconds));

            foreach (WorldLayer layer in m_layers)
            {
                layer.UpdateDraw(gameTime);
            }
            // Update draw information.
            for (int i = m_drawItems[0]; i <= m_drawItems[1]; i++)
            {
                for (int j = m_drawItems[2]; j <= m_drawItems[3]; j++)
                {
                    this[i, j].UpdateDraw(gameTime);
                }
            }

            // Layer
            for (int k = 0; k < m_layerCount; k++)
            {
                // X
                for (int i = m_drawItems[0]; i <= m_drawItems[1]; i++)
                {
                    // Y
                    for (int j = m_drawItems[2]; j <= m_drawItems[3]; j++)
                    {
                        Pieces[k, i, j].UpdateDraw(gameTime);
                    }
                }
            }
        }

        public override void Initialize()
        {
            Space = new Vector2(HealGame.Game.GraphicsDevice.Viewport.Width,
                                 HealGame.Game.GraphicsDevice.Viewport.Height);
            m_level = MapManager.GetInstance();
            m_senceManager = SenceManager.GetInstance();
            m_effect = GraphicsManager.GetInstance();
            m_entityCreator = EntityCreator.GetInstance();
            m_dialog = DialogManager.GetInstance();
            m_camera = CameraManager.GetInstance();

            m_splash = new SplashTools();
            m_splash.Initialize();

            m_handbook = new HandbookTools();
            m_handbook.Initialize();
        }

        public override void PostInitialize()
        {
            InstallStatusDialog();

            m_loadingImage = DataReader.Load<Texture2D>("Texture/Running/Loading");

            m_units = m_entityCreator.Units;
            Player = m_entityCreator.Player;
            m_regions = m_entityCreator.Regions;
            m_item = m_entityCreator.Items;

            m_resolveTexture = new RenderTarget2D(SpriteManager.SpriteBatch.GraphicsDevice, 800, 600, false,
                                                     SpriteManager.SpriteBatch.GraphicsDevice.PresentationParameters.
                                                         BackBufferFormat, DepthFormat.Depth24Stencil8);
        }

        private void DrawLayer(int a, int b, GameTime gameTime, SpriteBatch batch)
        {
            // Layer
            for (int k = a; k <= b; k++)
            {
                // X
                for (int i = m_drawItems[0]; i <= m_drawItems[1]; i++)
                {
                    // Y
                    for (int j = m_drawItems[2]; j <= m_drawItems[3]; j++)
                    {
                        Pieces[k, i, j].Draw(gameTime, batch);
                    }
                }
            }
        }

        private void DrawEntities(GameTime gameTime, SpriteBatch batch)
        {
            Item[] list1 = new Item[m_item.Count];
            m_item.CopyTo(list1);
            foreach (var list in list1)
            {
                list.Component().Draw(gameTime, batch);
            }
            Unit[] list2 = new Unit[m_units.Count];
            m_units.CopyTo(list2);
            foreach (var list in list2)
            {
                list.Component().Draw(gameTime, batch);
            }
        }

        public void Draw(GameTime gameTime, SpriteBatch batch)
        {
            switch (m_lastState)
            {
                case RunningState.Loading:
                    m_effect.Draw(b => b.Draw(m_loadingImage, Vector2.Zero, Color.White));
                    break;
                case RunningState.Running:
                    DrawRunning(gameTime, batch);
                    break;
                case RunningState.Speaking:
                    DrawDialog(gameTime, batch);
                    break;
                case RunningState.Viewing:
                    DrawRunning(gameTime, batch);
                    break;
                case RunningState.Splash:
                    DrawSplash( gameTime, batch );
                    break;
                case RunningState.Handbook:
                    m_handbook.Draw( gameTime, batch );
                    break;
            }
        }

        private void DrawSplash(GameTime gameTime, SpriteBatch batch)
        {
            m_splash.Draw(gameTime, batch);
        }

        private void DrawDialog(GameTime gameTime, SpriteBatch batch)
        {
            m_dialog.Draw(gameTime, batch);
            //m_effect.Draw(b => b.Draw(m_resolveTexture, Vector2.Zero, Color.LightGray));
            //
        }

        private void DrawRunning(GameTime gameTime, SpriteBatch batch)
        {
            m_effect.Draw(b => DrawLayer(0, m_collusionLayer, gameTime, b));

            //m_effect.Draw(b => DrawLayer(m_layerCount, m_layerCount, gameTime, b), new[]{
            //    new GraphicsManager.EffectDrawParameters(){Effect = "Wave",Pass = 1,Technique = 0},
            //    new GraphicsManager.EffectDrawParameters(){Effect = "Blur",Pass = 0,Technique = 0}
            //                   }, Color.Transparent);

            m_effect.Draw(b => DrawLayer(m_collusionLayer, m_collusionLayer, gameTime, b));

            m_effect.Draw(b => DrawEntities(gameTime, b));

            // Layer
            m_effect.Draw(b => DrawLayer(m_collusionLayer + 1, m_layerCount - 1, gameTime, b));

            if(m_freezeTime > .5)
            {
                m_freezeTime = -1f;
            }
            else if (m_freezeTime > 0)
            {
                m_freezeTime += (float) gameTime.ElapsedGameTime.TotalSeconds;
                m_effect.Parameters("Mosaic", "Intensity").SetValue((float)Math.Sin(m_freezeTime * Math.PI * 2) * .03f);
                SpriteManager.SpriteBatch.GraphicsDevice.SetRenderTarget(m_resolveTexture);
                m_effect.Draw(b => b.Draw(m_resolveTexture, Vector2.Zero, Color.White),
                               new GraphicsManager.EffectDrawParameters() {Effect = "Mosaic", Pass = 0, Technique = 0},
                               Color.White );
            }
            else if(!m_lastFreeze && Player.Status == AIBase.Status.Freeze)
            {
                m_freezeTime = .01f;
            }
            m_lastFreeze = ( Player.Status == AIBase.Status.Freeze );
        }

        private void SwitchTo(RunningState runningState, object param)
        {
            if (runningState == RunningState.Running && State == RunningState.Loading)
            {
                InternalLoad(m_data);
            }
            else if (runningState == RunningState.Loading)
            {
                m_data = (MapManager.MapLoadingData)param;
                m_loadingTimer = 0;
            }
            else if (runningState == RunningState.Speaking)
            {
                this.UpdateDraw(new GameTime());
                AIControler.Update(new GameTime(), m_units, m_item, Player);
                DrawRunning(new GameTime(), SpriteManager.SpriteBatch);
                SpriteManager.SpriteBatch.GraphicsDevice.SetRenderTarget(m_resolveTexture);
                m_dialog.Load(param.ToString(), m_resolveTexture);
            }
            else if (runningState == RunningState.Viewing)
            {
                m_camera.Load((string)param);
            }
            else if(runningState == RunningState.Splash)
            {
            }
            State = runningState;
        }

        internal Texture2D ResolveDraw()
        {
            this.UpdateDraw(new GameTime());
            AIControler.Update(new GameTime(), m_units, m_item, Player);
            DrawRunning(new GameTime(), SpriteManager.SpriteBatch);
            SpriteManager.SpriteBatch.GraphicsDevice.SetRenderTarget(m_resolveTexture);
            return m_resolveTexture;
        }

        internal void Load()
        {
            GameItemState.Load();
            Vector2 locate = (Vector2)GameItemState.Get( "locate" );
            m_level.GotoXY( GameItemState.Get( "currentmap" ).ToString(), (int)locate.X, (int)locate.Y );
        }

        /// <summary>
        /// Loads the specified level from data.
        /// </summary>
        internal void Load(MapManager.MapLoadingData data, int x, int y)
        {
            SwitchTo(RunningState.Loading, data);

            data.Player.X = x;
            data.Player.Y = y;
        }

        private void InternalLoad(MapManager.MapLoadingData data)
        {
            m_senceManager.Initialize(data.Size.X, data.Size.Y, WorldPart.ImageSize);

            EntityCreator.Load(data.Name);

            foreach (SencePart sencePart in data.Sence)
            {
                m_senceManager[sencePart.X, sencePart.Y] = sencePart;
            }
            Pieces = data.Pieces;
            m_part = data.Parts;
            m_layers = data.Layers;
            Player = m_entityCreator.Player;

            m_collusionLayer = data.CollusionLayer;
            m_layerCount = data.LayerCount;
            m_size = data.Size;
            m_worldSize = data.WorldSize;

            m_senceManager.CameraLocate = CheckLocation(data.Player);
            m_senceManager.CameraFollow = CheckLocation(data.Player);
            if(Player!=null)Player.Locate = data.Player;
            GameItemState.Set("currentmap", data.Name);
            GameItemState.Set( "locate", data.Player );
            GameCommands.Enqueue(data.PostLoading);
            GameItemState.Save();
        }

        private void InstallStatusDialog()
        {
            StatusDialog.InstallTexture(DataReader.Load<Texture2D>("Texture/Entities/StatusDialog/FallToGround"));
            StatusDialog.InstallTexture(DataReader.Load<Texture2D>("Texture/Entities/StatusDialog/Alarm"));
            StatusDialog.InstallTexture(DataReader.Load<Texture2D>("Texture/Entities/StatusDialog/Attack"));
            StatusDialog.InstallTexture(DataReader.Load<Texture2D>("Texture/Entities/StatusDialog/StrongerThanPlayer"));
            StatusDialog.InstallTexture(DataReader.Load<Texture2D>("Texture/Entities/StatusDialog/WeakerThanPlayer"));
            StatusDialog.InstallTexture(DataReader.Load<Texture2D>("Texture/Entities/StatusDialog/Strike"));
            StatusDialog.InstallTexture(DataReader.Load<Texture2D>("Texture/Entities/StatusDialog/Ahhh"));
            StatusDialog.InstallTexture(DataReader.Load<Texture2D>("Texture/Entities/StatusDialog/Kakaka"));
            StatusDialog.InstallTexture(DataReader.Load<Texture2D>("Texture/Entities/StatusDialog/Soughhh"));
        }

        public void Update(GameTime gameTime)
        {
            m_lastState = State;
            switch (this.State)
            {
                case RunningState.Running:
                    {
                        Destination = Player.Locate;
                        this.UpdateDraw(gameTime);
                        AIControler.Update(gameTime, m_units, m_item, Player);
                        foreach (var mRegion in m_regions)
                        {
                            mRegion.Update(gameTime);
                        }
                    }
                    break;
                case RunningState.Loading:
                    {
                        if (m_loadingTimer > 0.5f && m_data.LoadingLeft == 0)
                        {
                            SwitchTo(RunningState.Running, null);
                        }
                        m_loadingTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
                    }
                    break;
                case RunningState.Speaking:
                    m_dialog.Update(gameTime);
                    break;
                case RunningState.Viewing:
                    m_camera.Update(gameTime);
                    this.UpdateDraw(gameTime);
                    break;
                case RunningState.Splash:
                    m_splash.Update( gameTime );
                    break;
                case RunningState.Handbook:
                    m_handbook.Update( gameTime );
                    break;
            }
            string cmd;
            string[] cmds;
            while (GameCommands.Count() > 0)
            {
                cmd = GameCommands.Dequeue();
                if(cmd == null)continue;
                cmds = cmd.Split(';');
                foreach (string s in cmds)
                {
                    CommandResolver(s.Trim());
                }
            }
        }

        private void CommandResolver(string cmd)
        {
            string[] param;
            cmd = cmd.Replace("  ", " ").Trim();
            string[] info = cmd.Split(':');
            if (info.Length == 1)
            {
                param = info[0].Split(' ');
            }
            else
            {
                string[] items = info[0].Trim().Split('&');
                foreach (string item in items)
                {
                    string[] tmp = item.Split('=');
                    if (GameItemState.Get(tmp[0].Trim()) == null)
                    {
                        return;
                    }
                    if (GameItemState.Get(tmp[0].Trim()).ToString().ToLower() != tmp[1].Trim().ToLower())
                    {
                        return;
                    }
                }
                param = info[1].Trim().Split(' ');
            }
            switch (param[0].ToLower())
            {
                case "state":
                    {
                        object item;
                        switch (param[2].ToLower())
                        {
                            case "bool":
                                {
                                    bool obj;
                                    item = bool.Parse(param[3]);
                                }
                                break;
                            case "string":
                                item = param[3];
                                break;
                            case "int":
                                item = int.Parse(param[3]);
                                break;
                            default:
                                item = null;
                                break;
                        }
                        GameItemState.Set(param[1], item);
                    }
                    break;
                case "save":
                    {
                        GameItemState.Save();
                    }
                    break;
                case "goto":
                    {
                        m_level.Goto(param[1]);
                    }
                    break;
                case "gotoxy":
                    {
                        m_level.GotoXY(param[1], Convert.ToInt32(param[2]), Convert.ToInt32(param[3]));
                    }
                    break;
                case "read":
                    {
                        new Thread(() => m_level.Load(param[1])).Start();
                    }
                    break;
                case "dialog":
                    {
                        SwitchTo(RunningState.Speaking, param[1]);
                    }
                    break;
                case "camera":
                    {
                        SwitchTo(RunningState.Viewing, param[1]);
                    }
                    break;
                case "resume":
                    {
                        SwitchTo(RunningState.Running, null);
                    }
                    break;
                case "splash":
                    {
                        string[] str = (string[])param;
                        StringBuilder sb = new StringBuilder();
                        for (int i = 2; i < str.Length; i++)
                        {
                            sb.Append(str[i]);
                            sb.Append(' ');
                        }
                        m_splash.Load(str[1], sb.ToString(), ResolveDraw());
                        SwitchTo( RunningState.Splash, null );
                    }
                    break;
                case "handbook":
                    {
                        string[] str = (string[])param;
                        StringBuilder sb = new StringBuilder();
                        for (int i = 2; i < str.Length; i++)
                        {
                            sb.Append(str[i]);
                            sb.Append(' ');
                        }
                        m_handbook.Load(str[1], sb.ToString(), ResolveDraw());
                        SwitchTo(RunningState.Handbook, null);
                    }
                    break;
                case "splashtimer":
                    {
                        StringBuilder sb = new StringBuilder();
                        float n = float.Parse( param[2] );
                        for (int i = 3; i < param.Length; i++)
                        {
                            sb.Append(param[i]);
                            sb.Append(' ');
                        }
                        m_splash.Load(param[1], sb.ToString(), ResolveDraw(), n);
                        SwitchTo(RunningState.Splash, null);
                    }
                    break;
                case "clear":
                    {
                        m_splash.Load( "clear", "credit", ResolveDraw(), 5f, false );
                        SwitchTo( RunningState.Splash, null );
                    }
                    break;
                case "credit":
                    {
                        StateManager.GetInstance().GotoState( StateManager.States.CreditShowMenuState, null );
                    }
                    break;
                default:
                    break;
            }
        }

        #region Unused Interface inherits

        public bool Enabled
        {
            get { return true; }
        }

        public int UpdateOrder
        {
            get { return 0; }
        }
        event EventHandler<EventArgs> IUpdateable.EnabledChanged
        { add { } remove { } }
        event EventHandler<EventArgs> IUpdateable.UpdateOrderChanged
        { add { } remove { } }


        #endregion
    }
}
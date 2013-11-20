using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Heal.Core.Sence;
using Heal.Core.Utilities;
using Heal.GameState;
using Heal.Levels;
using Heal.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;
using Heal.Sprites;
using Heal.Data;
using Heal.Utilities;
using System.Threading;

namespace Heal
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class HealGame : Microsoft.Xna.Framework.Game
    {
        public static Game Game
        {
            get;
            set;
        }
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        private StateManager m_state;
        private DateTime m_time;

        public HealGame()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            m_time = DateTime.Now;
            //Core.Utilities.ScriptTools.RunScript("");
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            Components.Add( new GamerServicesComponent( this ) );
            Content = new DataManager(Content);
            DataReader.Manager = (DataManager)Content;
            DataReader.Initialize(Thread.CurrentThread);

            InstanceManager.Initialize();
            InstanceManager.RegisterInstance( typeof( MapManager ), typeof( WorldManager ), typeof( SenceManager ),
                                              typeof( SpriteManager ), typeof( GraphicsManager ), typeof( Config ),
                                              typeof( EntityCreator ), typeof( DialogManager ), typeof( CameraManager ),
                                              typeof( AudioManager ), typeof( GameItemState ) );

            this.IsFixedTimeStep = true;
            this.TargetElapsedTime = new TimeSpan( 0, 0, 0, 0, 16 );

            base.Initialize();
        }


        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            SpriteManager.SpriteBatch = spriteBatch;
            InstanceManager.InitializeManagers();
            m_state = StateManager.GetInstance();
            m_state.Initialize();

            Console.WriteLine(DateTime.Now - m_time);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            //if (!this.IsActive) return;
            Input.Update( gameTime );
            m_state.Update( gameTime );
            //KeyboardState ks = Keyboard.GetState();
            //Guide.ShowPlayers(PlayerIndex.One);

            base.Update(gameTime);
        }

        private float m_timer;
        private int m_frame;

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            //if (!this.IsActive) return;
            if((float)gameTime.TotalGameTime.TotalSeconds - m_timer > 1)
            {
                m_frame++;
                m_timer = m_timer + 1;
                Console.WriteLine( "FPS: {0}", m_frame );
                m_frame = 0;
            }
            else
            {
                m_frame++;
            }
            GraphicsDevice.Clear(Color.Black);
            m_state.Draw( gameTime );

            //base.Draw(gameTime);
            //this.GraphicsDevice();
            //GraphicsDevice.Present();
        }
    }
}

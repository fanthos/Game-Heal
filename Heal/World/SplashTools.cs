using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Heal.Core.Utilities;
using Heal.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Heal.Data;
using Heal.Utilities;

namespace Heal.World
{
    internal class SplashTools : IUpdateable, ISprite
    {
        private Texture2D m_backGround;
        private Texture2D m_image;
        private WorldManager m_manager;
        private Vector2 m_origin;
        private bool m_lastState;
        private string m_command;
        private GraphicsManager m_graphics;
        private float m_timer;
        private float m_now;
        private bool m_canBreak;

        public void Initialize()
        {
            m_manager = WorldManager.GetInstance();
            m_graphics = GraphicsManager.GetInstance();
        }

        public void Load(string path, string cmd, Texture2D backGround)
        {
            Load(path, cmd, backGround, -1);
        }

        public void Load(string path, string cmd, Texture2D backGround, float timer)
        {
            Load( path, cmd, backGround, timer, true );
        }

        public void Load(string path, string cmd, Texture2D backGround, float timer, bool canBreak)
        {
            m_canBreak = canBreak;
            m_timer = timer;
            m_command = cmd;
            m_backGround = backGround;
            m_image = DataReader.Load<Texture2D>( "Texture/Splash/" + path );
            m_origin = new Vector2((float)m_image.Width / 2, (float)m_image.Height / 2 );
            m_now = 0;
            m_lastState = true;
        }

        public void Update( GameTime gameTime )
        {
            bool spaceState = Input.IsActionKeyDown();
            if (!m_lastState && spaceState && m_canBreak)
            {
                GameCommands.Enqueue( m_command );
                return;
            }
            if (m_timer > 0)
            {
                if (m_now >= m_timer)
                {
                    m_now = 0;
                    GameCommands.Enqueue( m_command );
                }
                else
                {
                    m_now += (float)gameTime.ElapsedGameTime.TotalSeconds;
                }
            }
            m_lastState = spaceState;
        }

        public void Draw( GameTime gameTime, SpriteBatch batch )
        {
            m_graphics.Draw( InternalDraw );
        }

        private void InternalDraw( SpriteBatch batch )
        {
            batch.Draw( m_backGround, Vector2.Zero, null, Color.DarkGray );
            batch.Draw( m_image, m_manager.Space / 2, null, Color.White, 0, m_origin, 1, SpriteEffects.None, 0 );
        }

        #region Unused
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

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
    internal class HandbookTools : IUpdateable, ISprite
    {
        private Texture2D m_bookLF;
        private Texture2D m_bookLB;
        private Texture2D m_bookRF;
        private Texture2D m_bookRB;
        private Texture2D m_bookFG;
        private Texture2D m_bookBG;
        private Texture2D m_image;
        private Texture2D m_backGround;
        private WorldManager m_manager;
        private Vector2 m_origin;
        private bool m_lastState;
        private string m_command;
        private GraphicsManager m_graphics;

        private float m_timer;

        private ShowingState m_state;

        private enum ShowingState
        {
            Showing,
            Display,
            Disapper 
        }

        public void Initialize()
        {
            m_manager = WorldManager.GetInstance();
            m_graphics = GraphicsManager.GetInstance();
            m_bookLF = DataReader.Load<Texture2D>("Texture/Handbook/book_l_fg");
            m_bookLB = DataReader.Load<Texture2D>("Texture/Handbook/book_l_bg");
            m_bookRF = DataReader.Load<Texture2D>("Texture/Handbook/book_r_fg");
            m_bookRB = DataReader.Load<Texture2D>("Texture/Handbook/book_r_bg");
            m_bookFG = DataReader.Load<Texture2D>("Texture/Handbook/glass_fg");
            m_bookBG = DataReader.Load<Texture2D>("Texture/Handbook/glass_fg");
        }

        public void Load(string path, string cmd, Texture2D backGround)
        {
            m_command = cmd;
            m_backGround = backGround;
            m_image = DataReader.Load<Texture2D>( "Texture/handbook/" + path );
            m_origin = new Vector2((float)m_image.Width / 2, (float)m_image.Height / 2 );
            m_lastState = true;
            m_timer = 0;
        }

        public void Update( GameTime gameTime )
        {
            bool spaceState = Input.IsActionKeyDown();
            switch( m_state )
            {
                case ShowingState.Showing:
                    m_timer += (float) gameTime.ElapsedGameTime.TotalSeconds;
                    if( m_timer > 1 )
                    {
                        m_state = ShowingState.Display;
                    }
                    break;
                case ShowingState.Display:
                    if( !m_lastState && spaceState )
                    {
                        m_state = ShowingState.Disapper;
                    }
                    break;
                case ShowingState.Disapper:
                    m_timer -= (float)gameTime.ElapsedGameTime.TotalSeconds;
                    if (m_timer < 0)
                    {
                        GameCommands.Enqueue( m_command );
                    }
                    break;
            }
            m_lastState = spaceState;
        }

        public void Draw( GameTime gameTime, SpriteBatch batch )
        {
            m_graphics.Draw( InternalDraw );
        }

        private void InternalDraw( SpriteBatch batch )
        {
            float offset = (float) Math.Sin( m_timer * Math.PI / 2 ) * 165f;
            batch.Draw(m_backGround, Vector2.Zero, null, Color.DarkGray);
            batch.Draw(m_bookLB, m_manager.Space / 2 - new Vector2(offset, 0), null, Color.White, 0, m_origin, 0.3f, SpriteEffects.None, 0);
            batch.Draw(m_bookRB, m_manager.Space / 2 + new Vector2(offset, 0), null, Color.White, 0, m_origin, 0.3f, SpriteEffects.None, 0);
            batch.Draw(m_bookBG, m_manager.Space / 2, null, Color.White, 0, m_origin, 0.33f, SpriteEffects.None, 0);
            batch.Draw(m_bookBG, m_manager.Space / 2, null, Color.White, 0, m_origin, 0.33f, SpriteEffects.None, 0);
            batch.Draw(m_bookBG, m_manager.Space / 2, null, Color.White, 0, m_origin, 0.33f, SpriteEffects.None, 0);

            batch.Draw(m_image, m_manager.Space / 2, null, Color.White, 0, m_origin, 0.3f, SpriteEffects.None, 0);

            batch.Draw(m_bookFG, m_manager.Space / 2, null, Color.White, 0, m_origin, 0.33f, SpriteEffects.None, 0);
            batch.Draw(m_bookLF, m_manager.Space / 2 - new Vector2(offset, 0), null, Color.White, 0, m_origin, 0.3f, SpriteEffects.None, 0);
            batch.Draw(m_bookRF, m_manager.Space / 2 + new Vector2(offset, 0), null, Color.White, 0, m_origin, 0.3f, SpriteEffects.None, 0);
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

        public event EventHandler EnabledChanged;
        public event EventHandler UpdateOrderChanged;
        #endregion
    }
}

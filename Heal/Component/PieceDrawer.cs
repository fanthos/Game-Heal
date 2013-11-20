using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Heal.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Heal.Component
{
    internal class PieceDrawer : IUpdateableSprite
    {
        private Texture2D m_texture;
        private Point m_size;
        private long m_updateMillisecond;
        private EntityComponent m_entity;
        private long m_updateElapse;
        private int m_imageCount;
        private int m_playingImage;
        private int m_drawOrder;
        private Rectangle m_srcRect;
        private SpriteBatch m_spriteBatch;

        internal PieceDrawer(EntityComponent entity, Texture2D texture2D, int updateMillisecond, int drawOrder):this(entity, texture2D, (long)updateMillisecond, drawOrder)
        {
            
        }
        internal PieceDrawer(EntityComponent entity, Texture2D texture2D, long updateMillisecond, int drawOrder)
        {
            m_updateElapse = 0;
            m_playingImage = 0;
            m_texture = texture2D;
            m_size = new Point( texture2D.Height, texture2D.Height );
            m_updateMillisecond = updateMillisecond;
            m_imageCount = texture2D.Width / m_size.X;
            m_entity = entity;
            m_drawOrder = drawOrder;
            m_srcRect = new Rectangle(0, 0, m_size.X, m_size.Y);
            m_spriteBatch = SpriteManager.SpriteBatch;
        }

        public void Initialize()
        {
            
        }

        public void Draw( GameTime gameTime, SpriteBatch batch )
        {
            batch.Draw(m_texture, m_entity.Locate, m_srcRect, Color.White, m_entity.Rotation, m_entity.Size / 2,
                              m_entity.Scale, m_entity.SpriteEffect, 0 );
        }

        internal bool Visible
        {
            get { return true; }
        }

        internal int DrawOrder
        {
            get { return m_drawOrder; }
        }

        internal event EventHandler VisibleChanged;
        internal event EventHandler DrawOrderChanged;

        public void UpdateDraw( GameTime gameTime )
        {
            m_updateElapse += (long) gameTime.ElapsedGameTime.TotalMilliseconds;
            if (m_updateElapse < m_updateMillisecond)
            {
                return;
            }
            m_playingImage += (int) ( m_updateElapse / m_updateMillisecond );
            m_updateElapse %= m_updateMillisecond;
            m_playingImage %= m_imageCount;
            m_srcRect.X = m_playingImage * m_size.X;
        }

        public bool Palse
        {
            get { return false; }
        }
    }
}

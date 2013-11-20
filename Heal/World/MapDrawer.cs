using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Heal.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Heal.World
{
    internal class MapDrawer : IUpdateableSprite
    {
        private Texture2D m_texture;
        private Point m_size;
        private int m_layerCount;
        private WorldPart m_mapPart;
        private Rectangle m_srcRect;
        private Vector2 m_offset;
        private float m_alpha;
        private WorldManager m_manager;
        private WorldLayer m_layer;
        private float m_scale;

        internal MapDrawer(WorldPart entity, WorldLayer layer, Texture2D texture2D, int layerCount)
            : this(entity, layer, texture2D, layerCount, 1f)
        { }

        internal MapDrawer(WorldPart entity, WorldLayer layer, Texture2D texture2D, int layerCount, float alpha)
            : this(entity, layer, texture2D, layerCount, alpha, 1f)
        {

        }

        internal MapDrawer(WorldPart entity, WorldLayer layer, Texture2D texture2D, int layerCount, float alpha, float scale)
            : this(entity, layer, texture2D, layerCount, alpha, scale, new Vector2(0))
        {

        }
        internal MapDrawer(WorldPart entity, WorldLayer layer, Texture2D texture2D, int layerCount, float alpha, float scale, Vector2 offset)
        {
            m_manager = WorldManager.GetInstance();
            m_texture = texture2D;
            m_size = new Point(texture2D.Height, texture2D.Height);
            m_layerCount = layerCount;
            m_mapPart = entity;
            m_srcRect = new Rectangle(0, 0, m_size.X, m_size.Y);
            m_offset = offset;
            m_alpha = alpha;
            m_scale = scale;
            m_layer = layer;
        }

        public void Initialize()
        {

        }

        public void Draw(GameTime gameTime, SpriteBatch batch)
        {
            batch.Draw(m_texture, m_mapPart.Locate * m_scale + m_manager.Space * .5f * m_manager.Scale * (1 - m_scale) + m_offset * m_manager.Scale * m_scale, m_srcRect, new Color(1f, 1f, 1f, m_alpha * m_layer.Alpha), 0, new Vector2(0), WorldManager.GetInstance().Scale * m_scale, SpriteEffects.None, 0);
            if(m_scale<1)
            {}
        }

        public bool Visible
        {
            get { return true; }
        }

        public int DrawOrder
        {
            get { return 0; }
        }

        public event EventHandler VisibleChanged;
        public event EventHandler DrawOrderChanged;

        public void UpdateDraw(GameTime gameTime)
        {
            /*
            m_updateElapse += (long)gameTime.ElapsedGameTime.TotalMilliseconds;
            if (m_updateElapse < m_updateMillisecond)
            {
                return;
            }
            m_playingImage += (int)(m_updateElapse / m_updateMillisecond);
            m_updateElapse %= m_updateMillisecond;
            m_playingImage %= m_imageCount;
            m_srcRect.X = m_playingImage * m_size.X;
            */
        }

        public bool Palse
        {
            get { return false; }
        }
    }
}

using System;
using System.Collections.Generic;
using Heal.Core.Entities;
using Heal.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Heal.World;

namespace Heal.Component
{
    internal abstract class EntityComponent : ISprite, IDisposable
    {
        protected SpriteEffects m_rotate;
        protected float m_scale = 0.6f;
        protected Entity m_entity;
        protected WorldManager m_worldManager = WorldManager.GetInstance();

        public virtual void Initialize()
        {
            
        }

        //internal void AddLayer(Texture2D texture2D, int updateMillisecond, int drawOrder)
        //{
        //    m_usprite.Add( new PieceDrawer( this, texture2D, updateMillisecond, drawOrder ) );
        //}
        internal virtual void AddLayer(Texture2D texture2D, long updateMillisecond, int drawOrder)
        {
            //m_usprite.Add( new PieceDrawer( this, texture2D, updateMillisecond, drawOrder ) );
            //Unit
        }

        public void Draw( GameTime gameTime, SpriteBatch batch )
        {
            Rotation = Unit.Rotation;
            Locate = (Unit.Locate - m_worldManager.Locate)*m_worldManager.Scale + m_worldManager.Space / 2;
            m_rotate = SpriteEffects.None;
            if (Rotation < 0)
            {
                //Rotation += MathHelper.Pi;
                //m_rotate = SpriteEffects.FlipVertically;
            }
            InternalDraw(gameTime, batch);
        }

        protected abstract void InternalDraw( GameTime gameTime, SpriteBatch batch );

        internal virtual bool Visible
        {
            get { return true; }
        }

        internal virtual int DrawOrder
        {
            get { return 0; }
        }

        internal event EventHandler VisibleChanged;

        internal event EventHandler DrawOrderChanged;

        #region Implementation of IUpdateableSprite

        internal virtual bool Palse { get; private set; }

        #endregion

        internal virtual Entity Unit
        {
            get{ return m_entity;}
            set{ m_entity = value;}
        }

        internal Vector2 Locate;

        internal Vector2 Size = new Vector2(64,64);

        internal float Rotation;

        internal float Scale
        {
            get { return m_scale; }
        }

        internal SpriteEffects SpriteEffect
        {
            get { return m_rotate; }
        }

        #region IDisposable Members

        public void Dispose()
        {
            
        }

        #endregion
    }
}



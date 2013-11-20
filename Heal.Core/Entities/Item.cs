using System;
using Microsoft.Xna.Framework;

namespace Heal.Core.Entities
{
    /// <summary>
    /// Items can gain your energy and do some other things like this.
    /// </summary>
    public abstract class Item : Entity
    {
        public Item(object sprite):base(sprite)
        {
        }

        #region Implementation of IGameComponent

        public override void Initialize( )
        {
            throw new NotImplementedException( );
        }

        #endregion

        #region Implementation of IEntity

        public override byte[] CollisionTexture
        {
            get { throw new NotImplementedException( ); }
        }

        public override float Rotation
        {
            get { return 0; }
        }

        private Vector2 m_locate;
        public override Vector2 Locate
        {
            get { return m_locate; }
            set { m_locate = value; }
        }

        public override void Update( GameTime gameTime )
        {
            
        }

        public override Rectangle GetDrawingRectangle( )
        {
            throw new NotImplementedException( );
        }

        #endregion
    }
}



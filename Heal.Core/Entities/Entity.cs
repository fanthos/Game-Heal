using Microsoft.Xna.Framework;

namespace Heal.Core.Entities
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class Entity : IGameComponent
    {
        protected object m_sprite;

        protected Entity(object sprite)
        {
            m_sprite = sprite;
        }

        public object Sprite { get { return m_sprite; } }
        /// <summary>
        /// Gets or sets the collision texture.
        /// </summary>
        /// <value>The collision texture.</value>
        public abstract byte[] CollisionTexture { get; }

        /// <summary>
        /// Gets or sets the rotation.
        /// </summary>
        /// <value>The rotation.</value>
        public abstract float Rotation { get; }

        /// <summary>
        /// Gets or sets the locate.
        /// </summary>
        /// <value>The locate.</value>
        public abstract Vector2 Locate { get; set; }

        /// <summary>
        /// Gets or sets the size.
        /// </summary>
        /// <value>The size.</value>
        //public abstract Vector2 Size { get; set; }

        /// <summary>
        /// Update component info.
        /// </summary>
        /// <param name="gameTime">The game time.</param>
        public abstract void Update(GameTime gameTime);

        /// <summary>
        /// Gets the drawing rectangle.
        /// </summary>
        /// <returns></returns>
        public abstract Rectangle GetDrawingRectangle();

        public abstract void Initialize( );
    }
}



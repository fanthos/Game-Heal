using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Heal.Sprites
{
    public class ScenceSprite : ISprite
    {
        private float m_beginingFlashInterval;

        private State m_curState;

        public enum State
        {
            Common,
            On,
            Selected
        }

        public State CurState
        {
            get
            {
                return m_curState;
            }

            set
            {
                m_curState = value;
            }
        }

        public const float MaxAlpha = 1.0f;
        public const float MinAlpha = 0.0f;

        public Texture2D TextureImage { get; set; }
        public Vector2 Position { get; set; }
        public float Rotation { get; set; }
        public Vector2 Origin { get; set; }
        public Rectangle SourceRect { get; set; }
        public Rectangle DestRect { get; set; }
        public Vector2 TargetOffset { get; set; }
        public string Name{get;set;}
        public float BeginingFlashInterval
        {
            get
            {
                return m_beginingFlashInterval;
            }
            set
            {
                m_beginingFlashInterval = value;
                FlashInterval = value;
            }
        }
        public float FlashInterval { get; set; }
        public float CurAlpha { get; set; }
// ReSharper disable InconsistentNaming
        public Color TColor { get; set; }
// ReSharper restore InconsistentNaming

        #region IDrawable Members

        public virtual void Draw( GameTime gameTime, SpriteBatch batch )
        {
            batch.Draw(TextureImage, DestRect, SourceRect, TColor, Rotation, Origin, SpriteEffects.None, 1);
        }

        public virtual void DrawWithDestRectangle(GameTime gameTime, SpriteBatch batch)
        {
            batch.Draw( TextureImage, DestRect, TColor );
        }

        private int m_drawOrder;
        public int DrawOrder
        {
            get
            {
                return m_drawOrder;
            }
            set
            {
                m_drawOrder = value;
                if( DrawOrderChanged != null )
                {
                    DrawOrderChanged( this, new EventArgs() );
                }
            }
        }

        public event EventHandler DrawOrderChanged;

        private bool m_visible;
        public bool Visible
        {
            get
            {
                return m_visible;
            }
            set
            {
                m_visible = value;
                if( VisibleChanged != null )
                {
                    VisibleChanged( this, new EventArgs() );
                }
            }
        }

        public event EventHandler VisibleChanged;

        #endregion

        #region IGameComponent Members

        public void Initialize()
        {

        }

        #endregion
    }
}

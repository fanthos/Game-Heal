using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Heal.Data;

namespace Heal.Sprites
{
    /// <summary>
    /// The type of all the buttons, appearing in the game
    /// </summary>
    public class DButton : ISprite
    {
        private SpriteBatch m_spriteBatch;
        private DButtonState m_buttonState;
        private readonly string m_buttonName;

        private const float m_maxAlpha = 1.0f;
        private const float m_minAlpha = 0.0f;

        public Texture2D ButtonTex{ get; set;}
        public Rectangle DestRect
        {
            get; set;
        }
        private string m_buttonPath;
        private Color m_buttonColor;
        private float m_flashInterval;
        private float m_curAlpha;
        private float changes = 0.1f;

        public Vector2 Location{ get; set; }
        public Vector2 Size{ get; set; }

        public Color ButtonColor
        {
            get
            {
                return m_buttonColor;
            }
            set{ m_buttonColor = value;}
        }

        public string ButtonName
        {
            get
            {
                return m_buttonName;
            }
        }

        public DButtonState ButtonState
        {
            get
            {
                return m_buttonState;
            }
            set{ m_buttonState = value;}
        }

        public enum DButtonState
        {
            Idle,
            Foused,
            Pressed
        }

        public DButton( DButtonState dButtonState,string texPath, SpriteBatch spriteBatch, string buttonName)
        {
            m_buttonState = dButtonState;
            m_spriteBatch = spriteBatch;
            m_buttonColor = Color.White;
            m_curAlpha = 0.0f;
            m_flashInterval = 0.005f;
            m_buttonName = buttonName;
            InitButtonTex( texPath );
            Visible = true;
        }

        public void Update(GameTime gameTime)
        {
            switch( m_buttonState )
            {
                case DButtonState.Idle:
                    {
                        this.OnLeave();
                    }
                    break;

                case DButtonState.Foused:
                    {
                        this.OnFocused();
                    }
                    break;

                case DButtonState.Pressed:
                    {
                        this.OnPressed();
                    }
                    break;
            }
        }


        public void OnLeave()
        {
            m_buttonColor = Color.White;
            m_curAlpha = 0;
        }

        public void OnFocused()
        {
            //m_buttonColor = Color.Blue;
            m_buttonColor = new Color( 1, 1, 1, (float)Math.Sin( m_curAlpha * Math.PI ) );
            m_curAlpha += 1.5f*m_flashInterval;
            if( m_curAlpha >= m_maxAlpha )
                m_curAlpha = 0.0f;
        }

        public void OnPressed()
        {
            m_buttonColor = new Color( 1, 1, 1, (float)Math.Sin( m_minAlpha * Math.PI ) );
        }

        private void InitButtonTex( string buttonTexPath )
        {
            m_buttonPath = buttonTexPath;
            ButtonTex = DataReader.Load<Texture2D>( buttonTexPath );
        }

        public void Draw( GameTime gameTime, SpriteBatch batch )
        {
            if( !Visible )
                return;
            batch.Draw(ButtonTex, DestRect, m_buttonColor);
        }



        public void Initialize()
        {
        }



        public bool Visible
        {
            get; set;
        }

        public int DrawOrder
        {
            get; set;
        }

        public event EventHandler VisibleChanged;
        public event EventHandler DrawOrderChanged;
    }
}

using System.Collections.Generic;
using Heal.Core.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Heal.Sprites.Packagings
{
    public class OptionMenuButtonPackaging : IGameComponent, ISprite
    {
        private SpriteBatch m_spriteBatch;

        private DButton m_volumnButton;
        private DButton m_controlButton;
        private DButton m_backButton;

        private static string m_mateButtonName;

        private int m_count;
        private const int m_maxCount = 6;
        private float m_timer;
        private float m_totalTimer;

        private List<DButton> m_buttonList;

        public static string MateButtonName
        {
            get
            {
                return m_mateButtonName;
            }
        }

        public OptionMenuButtonPackaging( SpriteBatch spriteBatch)
        {
            m_mateButtonName = "VolumnButton";
            m_spriteBatch = spriteBatch;
            m_count = 0;
            m_timer = 0.3f;
            m_totalTimer = 0.3f;
            
        }

        public void Load()
        {
            m_volumnButton = new DButton( DButton.DButtonState.Foused, "Texture/Menu/Buttons/VolumnButton", m_spriteBatch, "VolumnButton" );
            m_controlButton = new DButton( DButton.DButtonState.Idle, "Texture/Menu/Buttons/control", m_spriteBatch, "ControlButton" );
            m_backButton = new DButton( DButton.DButtonState.Idle, "Texture/Menu/Buttons/BackButton", m_spriteBatch, "BackButton" );
            
            m_buttonList = new List<DButton>();

            m_buttonList.Add( m_volumnButton );
            m_buttonList.Add(m_controlButton);
            m_buttonList.Add( m_backButton );

            for( int i = 0; i < m_buttonList.Count; i++ )
            {
                m_buttonList[i].Size = new Vector2( 440, 40 );
                m_buttonList[i].DestRect = new Rectangle( 40, 20 + i * 35, (int)( m_buttonList[i].Size.X ), (int)( m_buttonList[i].Size.Y ) );
            }

        }

        private void ResetButtonState()
        {
            foreach( var dButton in m_buttonList )
            {
                dButton.ButtonState = DButton.DButtonState.Idle;
            }
        }

        public void Update( GameTime gameTime )
        {


            
            if( Input.IsDownKeyDown() )
            {
                m_totalTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
                if( m_totalTimer >= m_timer )
                {
                    m_totalTimer -= m_timer;
                    m_count++;
                    if( m_count >= m_buttonList.Count )
                        m_count = 0;

                    ResetButtonState();
                    m_buttonList[m_count].ButtonState = DButton.DButtonState.Foused;

                    m_mateButtonName = m_buttonList[m_count].ButtonName;
                }

            }

            else if( Input.IsUpKeyDown() )
            {
                m_totalTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
                if( m_totalTimer >= m_timer )
                {
                    m_totalTimer -= m_timer;
                    m_count--;
                    if( m_count == -1 )
                        m_count = m_buttonList.Count - 1;

                    ResetButtonState();
                    m_buttonList[m_count].ButtonState = DButton.DButtonState.Foused;

                    m_mateButtonName = m_buttonList[m_count].ButtonName;
                }

            }
            else
            {
                m_totalTimer = m_timer;
            }




            foreach( var dButton in m_buttonList )
            {
                dButton.Update( gameTime );
            }

        }

        public void Draw(GameTime gameTime, SpriteBatch batch)
        {
            foreach( var dButton in m_buttonList )
            {
                dButton.Draw( gameTime, batch );
            }
        }

        public void Initialize()
        {

        }
    }
}



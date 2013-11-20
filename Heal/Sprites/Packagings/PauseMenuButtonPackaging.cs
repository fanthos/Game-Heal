using System.Collections.Generic;
using Heal.Core.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Heal.Sprites.Packagings
{
    public class PauseMenuButtonPackaging : IGameComponent, ISprite
    {
        private SpriteBatch m_spriteBatch;

        private DButton m_resumeGameButton;
        private DButton m_optionButton;
        private DButton m_exitButton;

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

        public PauseMenuButtonPackaging( SpriteBatch spriteBatch )
        {
            m_mateButtonName = "ResumeGameButton";
            m_spriteBatch = spriteBatch;
            m_count = 0;
            m_timer = 0.3f;
            m_totalTimer = 0.3f;
        }

        public void Load()
        {
            m_resumeGameButton = new DButton( DButton.DButtonState.Foused, "Texture/Menu/Buttons/resume_1", m_spriteBatch, "ResumeGameButton" );
            m_optionButton = new DButton( DButton.DButtonState.Idle, "Texture/Menu/Buttons/option_1", m_spriteBatch, "OptionButton" );
            m_exitButton = new DButton( DButton.DButtonState.Idle, "Texture/Menu/Buttons/menu_1", m_spriteBatch, "BacktoStartButton" );
           
            m_buttonList = new List<DButton>();

            m_buttonList.Add( m_resumeGameButton );
            m_buttonList.Add( m_optionButton );
            m_buttonList.Add( m_exitButton );


            for( int i = 0; i < m_buttonList.Count; i++ )
            {
                m_buttonList[i].Size = new Vector2( 440, 40 );
                m_buttonList[i].DestRect = new Rectangle(30, 20 + i*35, (int) (m_buttonList[i].Size.X),
                                                         (int) (m_buttonList[i].Size.Y));
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

        public void Draw( GameTime gameTime, SpriteBatch batch )
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



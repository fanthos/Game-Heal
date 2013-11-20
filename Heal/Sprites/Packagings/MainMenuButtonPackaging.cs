using System.Collections.Generic;
using Heal.Core.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Heal.Sprites.Packagings
{
    public class MainMenuButtonPackaging : IGameComponent, ISprite
    {
        private SpriteBatch m_spriteBatch;

        private DButton m_newGameButton;
        private DButton m_continueButton;
        private DButton m_optionButton;
        private DButton m_howToPlayButton;
        private DButton m_achievementsButton;
        private DButton m_creditButton;
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

        public MainMenuButtonPackaging( SpriteBatch spriteBatch)
        {
            m_mateButtonName = "NewGameButton";
            m_spriteBatch = spriteBatch;
            m_count = 0;
            m_timer = 0.3f;
            m_totalTimer = 0.3f;
            
        }

        public void Load()
        {
            m_newGameButton = new DButton( DButton.DButtonState.Foused, "Texture/Menu/Buttons/new game_0", m_spriteBatch,"NewGameButton");
            m_continueButton = new DButton( DButton.DButtonState.Idle, "Texture/Menu/Buttons/continue_0", m_spriteBatch, "ContinueButton");
            m_optionButton = new DButton( DButton.DButtonState.Idle, "Texture/Menu/Buttons/option_0", m_spriteBatch, "OptionButton");
            m_howToPlayButton = new DButton( DButton.DButtonState.Idle, "Texture/Menu/Buttons/howtoplay", m_spriteBatch, "HowToPlay" );
            m_achievementsButton = new DButton( DButton.DButtonState.Idle, "Texture/Menu/Buttons/achievements_0", m_spriteBatch, "AchievementsButton");
            m_creditButton = new DButton( DButton.DButtonState.Idle, "Texture/Menu/Buttons/credit_0", m_spriteBatch, "CreditButton");
            m_exitButton = new DButton( DButton.DButtonState.Idle, "Texture/Menu/Buttons/exit_0", m_spriteBatch, "ExitButton");
            m_buttonList = new List<DButton>();

            m_buttonList.Add( m_newGameButton );
            if( CoreUtilities.IsTrue( "Continue" ) )
                m_buttonList.Add(m_continueButton);
            m_buttonList.Add( m_optionButton );
            m_buttonList.Add(m_howToPlayButton);
            m_buttonList.Add( m_achievementsButton );
            m_buttonList.Add( m_creditButton );
            m_buttonList.Add( m_exitButton );


            for( int i = 0; i < m_buttonList.Count; i++ )
            {
                m_buttonList[i].Size = new Vector2( 440 , 40 );
                m_buttonList[i].DestRect = new Rectangle( 40, 20 + i * 35, (int)( m_buttonList[i].Size.X ), (int)( m_buttonList[i].Size.Y) );
            }

        }

        private void ResetButtonState()
        {
            foreach( var dButton in m_buttonList )
            {
                dButton.ButtonState = DButton.DButtonState.Idle;
            }
        }

        public void Update(GameTime gameTime)
        {
            if( Input.IsDownKeyDown() )
            {
                m_totalTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
                if( m_totalTimer >= m_timer )
                {
                    m_totalTimer -= m_timer;
                    m_count++;
                    if (m_count >= m_buttonList.Count)
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
                dButton.Draw(gameTime, batch);
            }
        }

        public void Initialize()
        {
           
        }
    }
}



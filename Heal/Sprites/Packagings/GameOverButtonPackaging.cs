using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Heal.Core.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Heal.Sprites.Packagings
{
    public class GameOverButtonPackaging : IGameComponent, ISprite
    {
        private SpriteBatch m_spriteBatch;

        private DButton m_giveUpButton;
        private DButton m_retryButton;

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

        public GameOverButtonPackaging( SpriteBatch spriteBatch )
        {
            m_mateButtonName = "GiveUpButton";
            m_spriteBatch = spriteBatch;
            m_count = 0;
            m_timer = 0.3f;
            m_totalTimer = 0.3f;
        }

        public void Load()
        {
            m_giveUpButton = new DButton( DButton.DButtonState.Foused, "Texture/Menu/Buttons/GiveUpButton", m_spriteBatch, "GiveUpButton" );
            m_retryButton = new DButton( DButton.DButtonState.Idle, "Texture/Menu/Buttons/RetryButton", m_spriteBatch, "RetryButton" );

            m_buttonList = new List<DButton>();

            m_buttonList.Add( m_giveUpButton );
            m_buttonList.Add( m_retryButton );


            for( int i = 0; i < m_buttonList.Count; i++ )
            {
                m_buttonList[i].Size = new Vector2( 440, 40 );
                m_buttonList[i].DestRect = new Rectangle( 30, 20 + i * 35, (int)( m_buttonList[i].Size.X ),
                                                         (int)( m_buttonList[i].Size.Y ) );

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

        public void Initialize()
        {
            
        }

        public void Draw(GameTime gameTime, SpriteBatch batch)
        {
            foreach( var dButton in m_buttonList )
            {
                dButton.Draw( gameTime, batch );
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Heal.Core.Utilities;
using Heal.Utilities;
using Microsoft.Xna.Framework;
using Heal.Core;
using Microsoft.Xna.Framework.Graphics;
using Heal.Sprites;
using Heal.Data;
using Heal.Sprites.Packagings;

namespace Heal.GameState
{
    internal class PauseGameState : GameState
    {
        internal override StateManager.States GetState()
        {
            return StateManager.States.PauseGameState;
        }

        #region PauseMenu Private Field

        private Texture2D m_insteadTex;

        private StateManager m_stateManager;
        private PauseMenuButtonPackaging m_buttonPackaging;
        private PauseMenuTexPackaging m_texPackaging;
        private float m_timer;
        private bool m_isEnterPressed;

        #endregion

        #region Overrides of GameState

        public override void Initialize()
        {
            this.GameStateChanged += new GameStateEventHandler( PauseGameState_GameStateChanged );
            m_stateManager = StateManager.GetInstance();

            new Thread( Init )
            {
                Priority = ThreadPriority.BelowNormal
            }.Start();
        }

        private void Init()
        {
            m_buttonPackaging = new PauseMenuButtonPackaging( SpriteManager.SpriteBatch );
            m_buttonPackaging.Load();

            m_texPackaging = new PauseMenuTexPackaging();
            m_texPackaging.Initialize( m_insteadTex );

            
        }

        void PauseGameState_GameStateChanged( object sender, GameState.GameStateEventArgs args )
        {
            m_insteadTex = (Texture2D)args.Param;
            m_texPackaging.Initialize( m_insteadTex );
        }

        public override void Update(GameTime gameTime)
        {
            OptionMenuGameState.IsFromPauseGameState = true;

            float count = (float)gameTime.ElapsedGameTime.TotalSeconds;
            m_timer += count;

            if( m_timer >= 0.5f )
            {
                m_buttonPackaging.Update( gameTime );

                if( !m_isEnterPressed && Input.IsConfirmKeyDown() )
                {
                    switch( PauseMenuButtonPackaging.MateButtonName )
                    {
                        case "ResumeGameButton":
                            {
                                m_stateManager.GotoState( StateManager.States.RunningGameState, null );
                            }
                            break;

                        case "OptionButton":
                            {
                                m_stateManager.GotoState( StateManager.States.OptionMenuGameState, null );
                            }
                            break;

                        case "BacktoStartButton":
                            {
                                m_stateManager.GotoState( StateManager.States.MainMenuState, "MainMenu" );
                            }
                            break;
                    }
                }
                m_isEnterPressed = Input.IsConfirmKeyDown();
            }

        }

        public override void Draw(GameTime gameTime)
        {
            SpriteManager.SpriteBatch.Begin( SpriteSortMode.Immediate, BlendState.AlphaBlend );
            m_texPackaging.Draw( gameTime, SpriteManager.SpriteBatch );
            m_buttonPackaging.Draw( gameTime, SpriteManager.SpriteBatch );
            SpriteManager.SpriteBatch.End();
        }

        #endregion
    }
}

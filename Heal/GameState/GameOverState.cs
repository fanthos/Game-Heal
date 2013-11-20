using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Heal.Core.Utilities;
using Heal.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Heal.Sprites.Packagings;
using Heal.World;
using Heal.Core.AI;
using Heal.Data;
using Microsoft.Xna.Framework.Media;

namespace Heal.GameState
{
    internal class GameOverState : GameState
    {
        private AudioManager m_audioManager;

        internal override StateManager.States GetState()
        {
            return StateManager.States.GameOverState;
        }

        #region PauseMenu Private Field

        private Texture2D m_insteadTex;

        private StateManager m_stateManager;
        private GameOverButtonPackaging m_buttonPackaging;
        private GameOverTexPackaging m_texPackaging;
        private float m_timer;
        private bool m_isEnterPressed;

        private bool m_loaded;

        #endregion

        public override void Initialize()
        {
            this.GameStateChanged += new GameStateEventHandler( GamOverState_GameStateChanged );
            m_stateManager = StateManager.GetInstance();

            new Thread( Init )
            {
                Priority = ThreadPriority.BelowNormal
            }.Start();
        }

        private void Init()
        {
            m_audioManager = AudioManager.GetInstance();
            m_audioManager.LoadSong("SongOfGameOver", DataReader.Load<Song>("MusicAndSoundEffect/Song/MusicOfGameOverState"));


            m_buttonPackaging = new GameOverButtonPackaging( SpriteManager.SpriteBatch );
            m_buttonPackaging.Load();

            m_texPackaging = new GameOverTexPackaging();
            m_texPackaging.Initialize( m_insteadTex );


        }

        void GamOverState_GameStateChanged( object sender, GameState.GameStateEventArgs args )
        {
            if( !m_loaded )return;
            m_insteadTex = (Texture2D)args.Param;
            m_texPackaging.Initialize( m_insteadTex );
        }

        public override void Update(GameTime gameTime)
        {
            m_audioManager.PlaySong( "SongOfGameOver",true );

            float count = (float)gameTime.ElapsedGameTime.TotalSeconds;
            m_timer += count;

            if( m_timer >= 0.5f )
            {
                m_buttonPackaging.Update( gameTime );

                if( !m_isEnterPressed && Input.IsConfirmKeyDown() )
                {
                    switch( GameOverButtonPackaging.MateButtonName )
                    {
                        case "GiveUpButton":
                            {
                                m_stateManager.GotoState( StateManager.States.MainMenuState, null );
                            }
                            break;

                        case "RetryButton":
                            {
                                AIControler.IsDead = false;
                                WorldManager.GetInstance().Load();
                                m_stateManager.GotoState( StateManager.States.RunningGameState, null );
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
    }
}

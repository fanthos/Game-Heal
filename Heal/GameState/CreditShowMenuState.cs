using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Heal.Sprites;
using Microsoft.Xna.Framework;
using Heal.Sprites.Packagings;
using Heal.Core.Utilities;
using Heal.Data;
using Microsoft.Xna.Framework.Media;

namespace Heal.GameState
{
    internal class CreditShowMenuState : GameState
    {
        private AudioManager m_audioManager;
        private StateManager m_stateManager;
        private CreditMenuTexPackaging m_creditPackaging;
        private float m_timer;
        private bool m_isSpacePressed;

        internal override StateManager.States GetState()
        {
            return StateManager.States.CreditShowMenuState;
        }

        public override void Initialize()
        {
           m_stateManager = StateManager.GetInstance();
           new Thread( Init )
           {
               Priority = ThreadPriority.BelowNormal
           }.Start();
        }

        private void Init()
        {
            m_audioManager = AudioManager.GetInstance();
            m_audioManager.LoadSong("MusicInCreditShowState",DataReader.Load<Song>("MusicAndSoundEffect/Song/MusicInCreditShowState"));


            m_creditPackaging = new CreditMenuTexPackaging();
            m_creditPackaging.Initialize();

        }

        public override void Update(GameTime gameTime)
        {
            m_audioManager.PlaySong( "MusicInCreditShowState",true );

            if( Input.IsPauseKeyDown() || m_creditPackaging.IsFinished )
            {
                m_creditPackaging.Reset();
                m_stateManager.GotoState( StateManager.States.MainMenuState, null );
            }
            else
            {
                m_creditPackaging.Update(gameTime);
            }
        }

        

        public override void Draw(GameTime gameTime)
        {
            SpriteManager.SpriteBatch.Begin();
            m_creditPackaging.Draw( gameTime, SpriteManager.SpriteBatch );
            SpriteManager.SpriteBatch.End();
        }
    }
}

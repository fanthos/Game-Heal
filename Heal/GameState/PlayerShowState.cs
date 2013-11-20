using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Heal.Core.Utilities;
using Heal.Sprites;
using Microsoft.Xna.Framework;
using Heal.Sprites.Packagings;
using Microsoft.Xna.Framework.Graphics;

namespace Heal.GameState
{
    internal class PlayerShowState : GameState
    {
        private StateManager m_stateManager;
        private PlayerStateTexPackaging m_playerStatePackaging;
        private float m_timer;
        private bool m_isSpacePressed;
        private Texture2D m_insteadTex;

        internal override StateManager.States GetState()
        {
            return StateManager.States.PlayerState;
        }

        public override void Initialize()
        {
            m_stateManager = StateManager.GetInstance();
            new Thread( Init )
            {
                Priority = ThreadPriority.BelowNormal
            }.Start();

            this.GameStateChanged += PlayerState_GameStateChanged;
        }

        private void Init()
        {
            m_playerStatePackaging = new PlayerStateTexPackaging();
            m_playerStatePackaging.Initialize( m_insteadTex );
        }

        void PlayerState_GameStateChanged( object sender, GameState.GameStateEventArgs args )
        {
            m_insteadTex = (Texture2D)args.Param;
            m_playerStatePackaging.Initialize( m_insteadTex );
        }
        

        public override void Update( GameTime gameTime )
        {
            m_playerStatePackaging.Update();

            var count = (float) gameTime.ElapsedGameTime.TotalSeconds;
            m_timer += count;
            if (m_timer > 0.5f)
            {
                if (!m_isSpacePressed && Input.IsStatusKeyDown())
                {
                    m_stateManager.GotoState(StateManager.States.RunningGameState, null);
                }
            }
            m_isSpacePressed = Input.IsStatusKeyDown();
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteManager.SpriteBatch.Begin();
            m_playerStatePackaging.Draw( gameTime, SpriteManager.SpriteBatch);
            SpriteManager.SpriteBatch.End();
        }
    }
}

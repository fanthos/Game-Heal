using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Heal.Core.Utilities;
using Heal.Sprites.Packagings;
using Microsoft.Xna.Framework;
using Heal.Sprites;
using Microsoft.Xna.Framework.Input;
using Heal.Data;
using Microsoft.Xna.Framework.Graphics;
using Heal.Core;
using Heal.Utilities;

namespace Heal.GameState
{
    internal class StartMenuGameState : GameState
    {

        private ScenceSprite m_teamlogo;
        private ScenceSprite m_respectBackground;
        private ScenceSprite m_word_1;
        private ScenceSprite m_word_2;

        private ShowStoryTexPackaging m_showStoryTexPackaging;

        private List<ISprite> m_showTeamlogoCollection;
        private List<ISprite> m_showRespectCollection;
        private List<ISprite> m_showStoryCollection;

        private ChildState m_curChildState;
        private ChildState m_drawingChildState;
        private StateManager m_stateManager;
        private float m_timerInRespect;

        private enum ChildState
        {
            ShowTeamlogoState,
            ShowRespectState,
            ShowStoryState
        }

        #region GameStateEnum

        internal override StateManager.States GetState()
        {
            return StateManager.States.StartMenuGameState;
        }

        #endregion

        public override void Initialize()
        {
            m_curChildState = ChildState.ShowTeamlogoState;
            m_stateManager = StateManager.GetInstance();
            m_showStoryTexPackaging = new ShowStoryTexPackaging();
            m_showStoryTexPackaging.Initialize();

            #region Initialize ScenceSprites

            #region Initialize the teamlogo

            m_teamlogo = new ScenceSprite()
            {
                TextureImage = DataReader.Load<Texture2D>( "Texture/Menu/Teamlogo/teamlogo" ),
                DestRect = new Rectangle( 0, 0, HealGame.Game.GraphicsDevice.Viewport.Width,
                                         HealGame.Game.GraphicsDevice.Viewport.Height ),
                CurAlpha = 0.1f,
                FlashInterval = 0.008f,
                Visible = true,
            };
            m_teamlogo.TColor = new Color( new Vector4( 255, 255, 255, m_teamlogo.CurAlpha ) );
            m_teamlogo.SourceRect = new Rectangle( 0, 0, m_teamlogo.TextureImage.Width, m_teamlogo.TextureImage.Height );
            #endregion

            #region Initialize the respectBackground

            m_respectBackground = new ScenceSprite()
            {
                TextureImage = DataReader.Load<Texture2D>( "Texture/Menu/RespectShow/b_0" ),
                DestRect = new Rectangle( 0, 0, HealGame.Game.GraphicsDevice.Viewport.Width,
                                         HealGame.Game.GraphicsDevice.Viewport.Height ),
                CurAlpha = 0.0f,
                FlashInterval = 0.005f,
                Visible = true,
            };
            m_respectBackground.TColor = new Color( new Vector4( 255, 255, 255, m_respectBackground.CurAlpha ) );
            m_respectBackground.SourceRect = new Rectangle( 0, 0, m_respectBackground.TextureImage.Width, m_respectBackground.TextureImage.Height );
            #endregion

            #region Initialize the respect word_1

            m_word_1 = new ScenceSprite()
            {
                TextureImage = DataReader.Load<Texture2D>( "Texture/Menu/RespectShow/b_1" ),
                DestRect = new Rectangle( 0, 0, HealGame.Game.GraphicsDevice.Viewport.Width,
                                         HealGame.Game.GraphicsDevice.Viewport.Height ),
                CurAlpha = 0.2f,
                FlashInterval = 0.005f,
                Visible = true,
            };
            m_word_1.TColor = new Color( new Vector4( 255, 255, 255, m_word_1.CurAlpha ) );
            m_word_1.SourceRect = new Rectangle( 0, 0, m_word_1.TextureImage.Width, m_word_1.TextureImage.Height );
            #endregion

            #region Initialize the respect word_2

            m_word_2 = new ScenceSprite()
            {
                TextureImage = DataReader.Load<Texture2D>( "Texture/Menu/RespectShow/b_2" ),
                DestRect = new Rectangle( 0, 0, HealGame.Game.GraphicsDevice.Viewport.Width,
                                         HealGame.Game.GraphicsDevice.Viewport.Height ),
                CurAlpha = 0.005f,
                FlashInterval = 0.005f,
                Visible = true,
            };
            m_word_2.TColor = new Color( new Vector4( 255, 255, 255, m_word_2.CurAlpha ) );
            m_word_2.SourceRect = new Rectangle( 0, 0, m_word_2.TextureImage.Width, m_word_2.TextureImage.Height );
            #endregion

            

            m_showTeamlogoCollection = new List<ISprite>();
            m_showRespectCollection = new List<ISprite>();
            
            #region Add the components to the m_showTeamlogoCollection

            m_showTeamlogoCollection.Add(m_teamlogo);
            #endregion

            #region Add the components to the m_showRespectCollection

            m_showRespectCollection.Add( m_respectBackground );
            m_showRespectCollection.Add( m_word_1);
            m_showRespectCollection.Add( m_word_2 );
            #endregion

            #endregion

        }

        public override void Update( GameTime gameTime )
        {
            m_drawingChildState = m_curChildState;
            switch( m_curChildState )
            {
                    #region Case ChildState.ShowTeamlogoState

                case ChildState.ShowTeamlogoState:
                    {
                        m_teamlogo.TColor = new Color(1, 1, 1, (float) Math.Sin(0.3*m_teamlogo.CurAlpha*Math.PI));
                        m_teamlogo.CurAlpha += m_teamlogo.FlashInterval;
                        if (m_teamlogo.TColor.A <= 0.01)
                            m_curChildState = ChildState.ShowRespectState;
                    }
                    break;

                    #endregion

                    #region Case ChildState.ShowRespectState

                case ChildState.ShowRespectState:
                    {
                        //m_audioManager.LoadSong( "MusicAndSoundEffect/Music/MusicInRespectShowState", );

                        float count = (float) gameTime.ElapsedGameTime.TotalSeconds;
                        m_timerInRespect += count;

                        m_respectBackground.TColor = new Color(1, 1, 1,
                                                               (float)
                                                               Math.Sin(0.18*m_respectBackground.CurAlpha*Math.PI));
                        m_respectBackground.CurAlpha += m_respectBackground.FlashInterval;

                        m_word_1.TColor = new Color(1, 1, 1, (float) Math.Sin(0.36*m_word_1.CurAlpha*Math.PI));
                        m_word_1.CurAlpha += m_word_1.FlashInterval;

                        if (m_word_1.TColor.A <= 0.05f)
                        {
                            m_word_1.CurAlpha = 0;
                            if (m_timerInRespect >= 2.0f)
                            {
                                m_word_2.TColor = new Color(1, 1, 1, (float) Math.Sin(0.36*m_word_2.CurAlpha*Math.PI));
                                m_word_2.CurAlpha += m_word_2.FlashInterval;

                                if (m_word_2.TColor.A <= 0.01f)
                                {
                                    m_curChildState = ChildState.ShowStoryState;
                                }
                            }
                        }
                    }
                    break;

                    #endregion

                    #region Case ChildState.ShowStoryState

                case ChildState.ShowStoryState:
                    if (m_showStoryTexPackaging.IsFinished)
                        m_stateManager.GotoState( StateManager.States.MainMenuState, null );
                    m_showStoryTexPackaging.Update();
                    break;

                    #endregion
            }
        }

        public override void Draw( GameTime gameTime )
        {
            #region Draw ChildState.ShowTeamlogoState

            if( m_drawingChildState == ChildState.ShowTeamlogoState)
            {
                SpriteManager.SpriteBatch.Begin();
                foreach( ISprite sprite in m_showTeamlogoCollection )
                {
                    sprite.Draw( gameTime, SpriteManager.SpriteBatch );
                }
                SpriteManager.SpriteBatch.End();
            }

            #endregion

            #region Draw ChildState.ShowRespectState

            if( m_drawingChildState == ChildState.ShowRespectState )
            {
                SpriteManager.SpriteBatch.Begin();
                foreach( ISprite sprite in m_showRespectCollection )
                {
                    sprite.Draw( gameTime, SpriteManager.SpriteBatch );
                }
                SpriteManager.SpriteBatch.End();
            }

            #endregion

            #region Draw ChildState.ShowRespectState

            SpriteManager.SpriteBatch.Begin();
            if(m_curChildState == ChildState.ShowStoryState)
            {
                m_showStoryTexPackaging.Draw(gameTime,SpriteManager.SpriteBatch);
            }
            SpriteManager.SpriteBatch.End();
            #endregion

        }
    }
}

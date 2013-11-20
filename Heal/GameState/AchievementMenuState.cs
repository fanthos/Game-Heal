using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Heal.Core.Utilities;
using Heal.Data;
using Heal.Sprites;
using Heal.Utilities;
using Microsoft.Xna.Framework;
using Heal.Sprites.Packagings;
using Microsoft.Xna.Framework.Graphics;

namespace Heal.GameState
{
    internal class AchievementMenuState : GameState
    {
        #region ScenceSprite variable

        private ScenceSprite m_background;
        private ScenceSprite m_foreground;
        private ScenceSprite m_backgroundMist;
        private ScenceSprite m_staticsDarkPlayer;
        private ScenceSprite m_ufoLight;
        private List<ISprite> m_achievementMenuCollection;
        #endregion

        private StateManager m_stateManager;
        private AchievementMenuButtonPackaging m_buttonPackaging;
        private AchievementTexPackaging m_achievementTexPackaging;
        private float m_timer;
        private bool m_isSpacePressed;


        internal override StateManager.States GetState()
        {
            return StateManager.States.AchievementMenuState;
        }

        public override void Initialize()
        {        
            m_achievementMenuCollection = new List<ISprite>();
            m_stateManager = StateManager.GetInstance();
            new Thread( Init )
            {
                Priority = ThreadPriority.BelowNormal
            }.Start();
        }

        private void Init()
        {
            #region Initialize the packagings

            m_buttonPackaging = new AchievementMenuButtonPackaging( SpriteManager.SpriteBatch );
            m_buttonPackaging.Load();
            m_achievementTexPackaging = new AchievementTexPackaging();
            m_achievementTexPackaging.Initialize();
            #endregion

            #region Initialize background Textures

            #region Initialize the background

            m_background = new ScenceSprite()
            {
                TextureImage = DataReader.Load<Texture2D>( "Texture/Menu/CommonScene/Background" ),
                TargetOffset = new Vector2( 2000, 3000 ),
                DestRect =
                    new Rectangle( 0, 0, HealGame.Game.GraphicsDevice.Viewport.Width,
                                  HealGame.Game.GraphicsDevice.Viewport.Height ),
                Visible = true,
                TColor = Color.White
            };
            m_background.SourceRect = new Rectangle( 0, 0, m_background.TextureImage.Width, m_background.TextureImage.Height );


            #endregion

            #region Initialize the foreground

            m_foreground = new ScenceSprite()
            {
                TextureImage = DataReader.Load<Texture2D>( "Texture/Menu/CommonScene/Foreground" ),
                TargetOffset = new Vector2( 2000, 3000 ),
                DestRect =
                    new Rectangle( 0, 0, HealGame.Game.GraphicsDevice.Viewport.Width,
                                   HealGame.Game.GraphicsDevice.Viewport.Height ),
                Visible = true,
                TColor = Color.White
            };
            m_foreground.SourceRect = new Rectangle( 0, 0, m_foreground.TextureImage.Width, m_foreground.TextureImage.Height );
            #endregion

            #region Initialize the mist of background

            m_backgroundMist = new ScenceSprite()
            {
                TextureImage = DataReader.Load<Texture2D>( "Texture/Menu/CommonScene/Mist" ),
                DestRect = new Rectangle( 0, 0, HealGame.Game.GraphicsDevice.Viewport.Width,
                                         HealGame.Game.GraphicsDevice.Viewport.Height ),
                BeginingFlashInterval = 0.003f,
                CurAlpha = ScenceSprite.MinAlpha,
                Visible = true,
            };
            m_backgroundMist.TColor = new Color( new Vector4( 255, 255, 255, m_backgroundMist.CurAlpha ) );
            m_backgroundMist.SourceRect = new Rectangle( 0, 0, m_backgroundMist.TextureImage.Width, m_backgroundMist.TextureImage.Height );
            #endregion

            #region Initialize the UFO light

            m_ufoLight = new ScenceSprite()
            {
                TextureImage = DataReader.Load<Texture2D>( "Texture/Menu/CommonScene/UFOLight" ),
                DestRect = new Rectangle( 0, 0, HealGame.Game.GraphicsDevice.Viewport.Width,
                                         HealGame.Game.GraphicsDevice.Viewport.Height ),
                BeginingFlashInterval = 0.008f,
                CurAlpha = ScenceSprite.MinAlpha,
                Visible = true,
            };
            m_ufoLight.TColor = new Color( new Vector4( 255, 255, 255, m_ufoLight.CurAlpha ) );
            m_ufoLight.SourceRect = new Rectangle( 0, 0, m_ufoLight.TextureImage.Width, m_ufoLight.TextureImage.Height );
            #endregion

            #region Initialize the dark player

            m_staticsDarkPlayer = new ScenceSprite()
            {
                TextureImage = DataReader.Load<Texture2D>( "Texture/Menu/CommonScene/DarkPlayer" ),
                TargetOffset = new Vector2( 2000, 3000 ),
                DestRect =
                    new Rectangle( 0, 0, HealGame.Game.GraphicsDevice.Viewport.Width,
                                   HealGame.Game.GraphicsDevice.Viewport.Height ),
                Visible = true,
                TColor = Color.White
            };
            m_staticsDarkPlayer.SourceRect = new Rectangle( 0, 0, m_staticsDarkPlayer.TextureImage.Width, m_staticsDarkPlayer.TextureImage.Height );
            #endregion

            #endregion

            #region Add the components to the m_optionMenuCollection

            m_achievementMenuCollection.Add( m_background );
            m_achievementMenuCollection.Add( m_foreground );
            m_achievementMenuCollection.Add( m_buttonPackaging );
            m_achievementMenuCollection.Add( m_ufoLight );
            m_achievementMenuCollection.Add( m_staticsDarkPlayer );
            m_achievementMenuCollection.Add( m_backgroundMist );
            #endregion
        }

        internal void Twinkle( ScenceSprite scenceSprite )
        {
            if( scenceSprite.CurAlpha <= ScenceSprite.MaxAlpha )
            {
                scenceSprite.TColor = new Color( 1f, 1f, 1f, (float)Math.Sin( scenceSprite.CurAlpha * Math.PI ) );
                scenceSprite.CurAlpha += scenceSprite.FlashInterval;
                if( scenceSprite.CurAlpha >= ScenceSprite.MaxAlpha )
                    scenceSprite.CurAlpha = 0.0f;

            }
        }


        public override void Update(GameTime gameTime)
        {
            Twinkle( m_backgroundMist );
            Twinkle( m_ufoLight );

            var count = (float)gameTime.ElapsedGameTime.TotalSeconds;
            m_timer += count;
            if( m_timer > 0.5f )
            {
                m_buttonPackaging.Update( gameTime );
                if( !m_isSpacePressed && Input.IsConfirmKeyDown() )
                {

                    if( m_timer > 0.5f )
                    {
                        m_timer = 0;
                        switch( AchievementMenuButtonPackaging.MateButtonName )
                        {
                            case "TestYesButton":
                                {
                                    //if( !m_isSpacePressed && Input.IsActionKeyDown() )
                                        m_achievementTexPackaging.IsAttainedPressed();
                                }
                                break;

                            case "TestNoButton":
                                {
                                    //if( !m_isSpacePressed && Input.IsActionKeyDown() )
                                        m_achievementTexPackaging.IsNotAttainedPressed();
                                }
                                break;

                            case "BackButton":
                                {
                                    //if( !m_isSpacePressed && Input.IsActionKeyDown() )
                                        m_stateManager.GotoState( StateManager.States.MainMenuState, null );
                                }
                                break;
                        }
                    }
                }
            }
            m_isSpacePressed = Input.IsConfirmKeyDown();
        }

     

        public override void Draw(GameTime gameTime)
        {
            SpriteManager.SpriteBatch.GraphicsDevice.Clear( Color.CornflowerBlue );
            SpriteManager.SpriteBatch.Begin();
            foreach( ISprite sprite in m_achievementMenuCollection )
            {
                sprite.Draw( gameTime, SpriteManager.SpriteBatch );
            }
            m_buttonPackaging.Draw( gameTime, SpriteManager.SpriteBatch );
            m_achievementTexPackaging.Draw( gameTime, SpriteManager.SpriteBatch );
            SpriteManager.SpriteBatch.End();
        }
    }
}

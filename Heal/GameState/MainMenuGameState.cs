using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Heal.Core.AI;
using Heal.Core.Utilities;
using Heal.Data;
using Heal.Sprites;
using Heal.Sprites.Packagings;
using Heal.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Heal.Levels;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;

namespace Heal.GameState
{
    internal class MainMenuGameState : GameState
    {
        private AudioManager m_audioManager;

        #region Button packaging variables

        private MainMenuButtonPackaging m_buttonPackaging;
        #endregion

        private ScenceSprite m_background;
        private ScenceSprite m_foreground;
        private ScenceSprite m_backgroundMist;
        private ScenceSprite m_staticsDarkPlayer;
        private ScenceSprite m_staticsGlowingPlayer;
        private ScenceSprite m_ufoLight;
        private ScenceSprite m_tipsButton;
        private ScenceSprite m_logo;
        private ScenceSprite m_startString;

        private DButton m_tvMessey_1;
        private DButton m_tvMessey_2;
        private DButton m_tvMessey_3;
        private DButton m_tvPicture_1;
        private DButton m_tvPicture_2;
        private DButton m_tvPicture_3;
        private DButton m_tvPicture_4;
        private DButton m_tvPicture_5;
        private DButton m_tvPicture_6;
        private DButton m_tvPicture_7;


        private StateManager m_stateManager;

        private List<ISprite> m_beginStateCollection;
        private List<ISprite> m_gameLogoDisappearStateCollection;
        private List<ISprite> m_startMenuShowStateCollection;
        private List<ISprite> m_zoomInBackgroundStateCollection;
        private List<ISprite> m_capsuleOnTargetStateCollection;
        private List<ISprite> m_distortionStateCollection;

        private ChildState m_curChildState;
        private ChildState m_drawingChildState;
        private float m_timer;
        private int m_intTimer;
        private bool m_isSpacePressed;
        private float m_scaleCount;
        private float m_count;
        private int m_messeyNum;
        private int m_picNum = 0;
        private List<DButton> m_tvMesseyList;
        private List<DButton> m_tvPictureList;
        private float m_scale;
        private GraphicsManager m_effect;

        private enum ChildState
        {
            BeginState,
            GameLogoDisappearState,
            StartMenuShowState,
            ZoomInBackgroundState,
            CapsuleOnTargetState,
            DistortionState
        }

        internal override StateManager.States GetState()
        {
            return StateManager.States.MainMenuState;
        }

        public override void Initialize()
        {
            m_audioManager = AudioManager.GetInstance();
            m_audioManager.LoadSong( "SongOfMainMenuGameState", DataReader.Load<Song>( "MusicAndSoundEffect/Song/MusicOfMenuState" ) );
            
            //m_audioManager.PlaySong( "SongOfMainMenuGameState", true );
            //m_audioManager.LoadSound("def", DataReader.Load<SoundEffect>(""));

            //m_audioManager.PlaySong( "SongOfMainMenuGameState", true );
            //m_audioManager.StopSong();
            //m_audioManager.FadeSong(1,new TimeSpan(0,0,1));

            m_curChildState = ChildState.BeginState;

            m_beginStateCollection = new List<ISprite>();
            m_gameLogoDisappearStateCollection = new List<ISprite>();
            m_startMenuShowStateCollection = new List<ISprite>();
            m_zoomInBackgroundStateCollection = new List<ISprite>();
            m_capsuleOnTargetStateCollection = new List<ISprite>();
            m_distortionStateCollection = new List<ISprite>();

            m_effect = GraphicsManager.GetInstance();
            m_stateManager = StateManager.GetInstance();

            #region Initialize the tv pictures




            m_tvPicture_1 = new DButton( DButton.DButtonState.Idle, "Texture/Menu/TVPicture/TVPicture_1", SpriteManager.SpriteBatch, "TVPicture_1" );
            m_tvPicture_2 = new DButton( DButton.DButtonState.Idle, "Texture/Menu/TVPicture/TVMessy _3", SpriteManager.SpriteBatch, "TVPicture_2" );
            m_tvPicture_3 = new DButton( DButton.DButtonState.Idle, "Texture/Menu/TVPicture/TVPicture_3", SpriteManager.SpriteBatch, "TVPicture_3" );
            m_tvPicture_4 = new DButton( DButton.DButtonState.Idle, "Texture/Menu/TVPicture/TVMessy _2", SpriteManager.SpriteBatch, "TVPicture_4" );
            m_tvPicture_5 = new DButton( DButton.DButtonState.Idle, "Texture/Menu/TVPicture/TVMessy _1", SpriteManager.SpriteBatch, "TVPicture_5" );
            m_tvPicture_6 = new DButton( DButton.DButtonState.Idle, "Texture/Menu/TVPicture/TVPicture_6", SpriteManager.SpriteBatch, "TVPicture_6" );
            m_tvPicture_7 = new DButton( DButton.DButtonState.Idle, "Texture/Menu/TVPicture/TVPicture_7", SpriteManager.SpriteBatch, "TVPicture_7" );
            m_tvMessey_1 = new DButton( DButton.DButtonState.Idle, "Texture/Menu/TVPicture/TVMessy _1", SpriteManager.SpriteBatch, "TVMessy _1" );
            m_tvMessey_2 = new DButton( DButton.DButtonState.Idle, "Texture/Menu/TVPicture/TVMessy _2", SpriteManager.SpriteBatch, "TVMessy _2" );
            m_tvMessey_3 = new DButton( DButton.DButtonState.Idle, "Texture/Menu/TVPicture/TVMessy _3", SpriteManager.SpriteBatch, "TVMessy _3" );

            m_tvPictureList = new List<DButton>();
            //m_tvMesseyList = new List<DButton>();

            m_tvPictureList.Add( m_tvPicture_1 );
            m_tvPictureList.Add( m_tvPicture_2 );
            m_tvPictureList.Add( m_tvPicture_3 );
            m_tvPictureList.Add( m_tvPicture_4 );
            m_tvPictureList.Add( m_tvPicture_5 );
            m_tvPictureList.Add( m_tvPicture_6 );
            m_tvPictureList.Add( m_tvPicture_7 );
            m_tvPictureList.Add( m_tvMessey_1 );
            m_tvPictureList.Add( m_tvMessey_2 );
            m_tvPictureList.Add( m_tvMessey_3 );

            #endregion

            m_logo = new ScenceSprite();
            m_logo.TextureImage = DataReader.Load<Texture2D>( "Texture/Menu/CommonScene/GameLogo" );

            m_startString = new ScenceSprite();
            m_startString.TextureImage = DataReader.Load<Texture2D>( "Texture/Menu/CommonScene/press_0" );

            m_buttonPackaging = new MainMenuButtonPackaging( SpriteManager.SpriteBatch );

            m_background = new ScenceSprite();
            m_background.TextureImage = DataReader.Load<Texture2D>( "Texture/Menu/CommonScene/Background" );

            m_foreground = new ScenceSprite();
            m_foreground.TextureImage = DataReader.Load<Texture2D>( "Texture/Menu/CommonScene/Foreground" );

            m_backgroundMist = new ScenceSprite();
            m_backgroundMist.TextureImage = DataReader.Load<Texture2D>( "Texture/Menu/CommonScene/Mist" );

            m_ufoLight = new ScenceSprite();
            m_ufoLight.TextureImage = DataReader.Load<Texture2D>( "Texture/Menu/CommonScene/UFOLight" );

            #region Initialize the statics player

            m_staticsDarkPlayer = new ScenceSprite();
            m_staticsDarkPlayer.TextureImage = DataReader.Load<Texture2D>( "Texture/Menu/CommonScene/DarkPlayer" );

            #region Initial the glowing player

            m_staticsGlowingPlayer = new ScenceSprite()
            {
                TextureImage = DataReader.Load<Texture2D>( "Texture/Menu/CommonScene/GlowingPlayer" ),
                TargetOffset = new Vector2( 2000, 3000 ),
                DestRect = new Rectangle( 0, 0, HealGame.Game.GraphicsDevice.Viewport.Width,
                                   HealGame.Game.GraphicsDevice.Viewport.Height ),
                Visible = true,
                TColor = Color.White
            };
            m_staticsGlowingPlayer.SourceRect = new Rectangle( 0, 0, m_staticsGlowingPlayer.TextureImage.Width, m_staticsGlowingPlayer.TextureImage.Height );
            #endregion

            #endregion

            #region Initialize the tips button "Enter"

            m_tipsButton = new ScenceSprite()
            {
                TextureImage = DataReader.Load<Texture2D>( "Texture/Menu/CommonScene/press_1" ),
                Position = new Vector2( 420, 125 ),
                Visible = true,
                TColor = Color.White
            };
            m_tipsButton.DestRect = new Rectangle( (int)m_tipsButton.Position.X, (int)m_tipsButton.Position.Y,
                                                  (int)( m_tipsButton.TextureImage.Width * 0.25 ),
                                                  (int)( m_tipsButton.TextureImage.Height * 0.25 ) );
            m_tipsButton.SourceRect = new Rectangle( 0, 0, m_tipsButton.TextureImage.Width,


                   m_tipsButton.TextureImage.Height );
            #endregion
            
            InitLogic();

            #region Add the components to the m_beginStateCollection

            m_beginStateCollection.Add( m_background );
            m_beginStateCollection.Add( m_foreground );
            m_beginStateCollection.Add( m_backgroundMist );
            m_beginStateCollection.Add( m_staticsDarkPlayer );
            m_beginStateCollection.Add( m_logo );
            m_beginStateCollection.Add( m_startString );
            #endregion

            #region Add the components to the m_gameLogoDisappearStateCollection

            m_gameLogoDisappearStateCollection.Add( m_background );
            m_gameLogoDisappearStateCollection.Add( m_foreground );
            m_gameLogoDisappearStateCollection.Add( m_staticsDarkPlayer );
            m_gameLogoDisappearStateCollection.Add( m_backgroundMist );
            #endregion

            #region Add the components to the m_startMenuShowStateCollection

            m_startMenuShowStateCollection.Add( m_background );
            m_startMenuShowStateCollection.Add( m_foreground );
            m_startMenuShowStateCollection.Add( m_buttonPackaging );
            m_startMenuShowStateCollection.Add( m_ufoLight );
            m_startMenuShowStateCollection.Add( m_staticsDarkPlayer );
            m_startMenuShowStateCollection.Add( m_backgroundMist );
            #endregion

            #region Add the components to the m_zoomInBackgroundStateCollection

            m_zoomInBackgroundStateCollection.Add( m_background );
            m_zoomInBackgroundStateCollection.Add( m_foreground );
            m_zoomInBackgroundStateCollection.Add( m_ufoLight );
            m_zoomInBackgroundStateCollection.Add( m_staticsDarkPlayer );
            m_zoomInBackgroundStateCollection.Add( m_backgroundMist );
            #endregion

            #region Add the components to the m_capsuleOnTargetStateCollection

            m_capsuleOnTargetStateCollection.Add( m_background );

            foreach( var a in m_tvPictureList )
            {
                m_capsuleOnTargetStateCollection.Add( a );
            }

            m_capsuleOnTargetStateCollection.Add( m_foreground );
            m_capsuleOnTargetStateCollection.Add( m_tipsButton );
            m_capsuleOnTargetStateCollection.Add( m_ufoLight );
            m_capsuleOnTargetStateCollection.Add( m_staticsGlowingPlayer );
            m_capsuleOnTargetStateCollection.Add( m_backgroundMist );
            #endregion

            #region Add the components to the m_distortionStateCollection

            m_distortionStateCollection.Add( m_background );
            foreach( var a in m_tvPictureList )
            {
                m_distortionStateCollection.Add( a );
            }
            m_distortionStateCollection.Add( m_foreground );
            m_distortionStateCollection.Add( m_backgroundMist );
            m_distortionStateCollection.Add( m_staticsGlowingPlayer );
            

            #endregion

            this.GameStateChanged += new GameStateEventHandler( MainMenuGameState_GameStateChanged );
        }

        void MainMenuGameState_GameStateChanged( object sender, GameState.GameStateEventArgs args )
        {
            m_curChildState = ChildState.BeginState;

        }

        internal void InitLogic()
        {
            #region Background

            m_background.Visible = true;
            m_background.FlashInterval = 0.005f;
            m_background.CurAlpha = 0.1f;
            m_background.DestRect = new Rectangle( 0, 0, HealGame.Game.GraphicsDevice.Viewport.Width,
                                                  HealGame.Game.GraphicsDevice.Viewport.Height );
            m_background.SourceRect = new Rectangle( 0, 0, m_background.TextureImage.Width,
                                                    m_background.TextureImage.Height );
            #endregion

            #region Foreground

            m_foreground.DestRect = new Rectangle( 0, 0, HealGame.Game.GraphicsDevice.Viewport.Width,
                                                  HealGame.Game.GraphicsDevice.Viewport.Height );
            m_foreground.CurAlpha = 0.1f;
            m_foreground.FlashInterval = 0.005f;
            m_foreground.Visible = true;
            m_foreground.SourceRect = new Rectangle( 0, 0, m_foreground.TextureImage.Width, m_foreground.TextureImage.Height );
            #endregion

            #region Mist

            m_backgroundMist.DestRect = new Rectangle( 0, 0, HealGame.Game.GraphicsDevice.Viewport.Width,
                                                      HealGame.Game.GraphicsDevice.Viewport.Height );
            m_backgroundMist.BeginingFlashInterval = 0.003f;
            m_backgroundMist.CurAlpha = ScenceSprite.MinAlpha;
            m_backgroundMist.Visible = true;
            m_backgroundMist.TColor = new Color( new Vector4( 255, 255, 255, m_backgroundMist.CurAlpha ) );
            m_backgroundMist.SourceRect = new Rectangle( 0, 0, m_backgroundMist.TextureImage.Width, m_backgroundMist.TextureImage.Height );
            #endregion

            #region UFO light

            m_ufoLight.DestRect = new Rectangle( 0, 0, HealGame.Game.GraphicsDevice.Viewport.Width,
                                                HealGame.Game.GraphicsDevice.Viewport.Height );
            m_ufoLight.BeginingFlashInterval = 0.008f;
            m_ufoLight.CurAlpha = ScenceSprite.MinAlpha;
            m_ufoLight.Visible = true;
            m_ufoLight.TColor = new Color( new Vector4( 255, 255, 255, m_ufoLight.CurAlpha ) );
            m_ufoLight.SourceRect = new Rectangle( 0, 0, m_ufoLight.TextureImage.Width, m_ufoLight.TextureImage.Height );
            #endregion

            #region Dark player

            m_staticsDarkPlayer.DestRect = new Rectangle( 0, 0, HealGame.Game.GraphicsDevice.Viewport.Width,
                                                         HealGame.Game.GraphicsDevice.Viewport.Height );
            m_staticsDarkPlayer.CurAlpha = 0.1f;
            m_staticsDarkPlayer.FlashInterval = 0.005f;
            m_staticsDarkPlayer.Visible = true;
            m_staticsDarkPlayer.SourceRect = new Rectangle( 0, 0, m_staticsDarkPlayer.TextureImage.Width, m_staticsDarkPlayer.TextureImage.Height );
            #endregion

            #region Buttons

            m_buttonPackaging.Load();
            #endregion

            #region Start string

            m_startString.CurAlpha = ScenceSprite.MinAlpha;
            m_startString.Position = new Vector2( 200, 470 );
            m_startString.Visible = true;
            m_startString.BeginingFlashInterval = 0.005f;
            m_startString.TColor = new Color( new Vector4( 255, 255, 255, m_startString.CurAlpha ) );
            m_startString.DestRect = new Rectangle( (int)m_startString.Position.X, (int)m_startString.Position.Y, m_startString.TextureImage.Width, m_startString.TextureImage.Height );
            m_startString.SourceRect = new Rectangle( 0, 0, m_startString.TextureImage.Width, m_startString.TextureImage.Height );
            #endregion

            #region Logo

            m_logo.CurAlpha = ScenceSprite.MinAlpha;
            m_logo.Position = Vector2.Zero;
            m_logo.Visible = true;
            m_logo.BeginingFlashInterval = 0.01f;
            m_logo.TColor = new Color( new Vector4( 255, 255, 255, m_logo.CurAlpha ) );
            m_logo.DestRect = new Rectangle( (int)m_logo.Position.X, (int)m_logo.Position.Y, HealGame.Game.GraphicsDevice.Viewport.Width, HealGame.Game.GraphicsDevice.Viewport.Height );
            m_logo.SourceRect = new Rectangle( 0, 0, m_logo.TextureImage.Width, m_logo.TextureImage.Height );
            #endregion

            #region TV picture

            foreach( var a in m_tvPictureList )
            {
                a.Size = new Vector2( 624, 432 );
                a.Location = new Vector2( 199, 220 );
                a.DestRect = new Rectangle( (int)a.Location.X, (int)a.Location.Y,
                                        (int)a.Size.X, (int)a.Size.Y );
            }

            #endregion


        }

        internal void ResetTVPictureState()
        {
            foreach( var a in m_tvPictureList )
            {
                a.Visible = false;
            }
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

        public override void Update( GameTime gameTime )
        {
            m_audioManager.PlaySong( "SongOfMainMenuGameState", true );
            
            m_drawingChildState = m_curChildState;

            Twinkle( m_backgroundMist );
            Twinkle( m_ufoLight );

            switch( m_curChildState )
            {
                #region Case ChildState.BeginState

                case ChildState.BeginState:
                    {
                        m_background.CurAlpha += m_background.FlashInterval;
                        m_background.TColor = new Color( 1, 1, 1, (float)m_background.CurAlpha );
                        m_foreground.CurAlpha += m_foreground.FlashInterval;
                        m_foreground.TColor = new Color( 1, 1, 1, (float)m_foreground.CurAlpha );
                        m_staticsDarkPlayer.CurAlpha += m_staticsDarkPlayer.FlashInterval;
                        m_staticsDarkPlayer.TColor = new Color( 1, 1, 1, (float)m_staticsDarkPlayer.CurAlpha );
                        if( m_background.CurAlpha >= ScenceSprite.MaxAlpha )
                        {
                            m_background.TColor = Color.White;
                            m_logo.CurAlpha += m_logo.FlashInterval;
                            m_logo.TColor = new Color( 1, 1, 1, (float)m_logo.CurAlpha );
                            if( m_logo.CurAlpha >= ScenceSprite.MaxAlpha )
                            {
                                m_startString.TColor = new Color( 1, 1, 1,
                                                                 (float)Math.Sin( m_startString.CurAlpha * Math.PI ) );
                                m_startString.CurAlpha += m_startString.FlashInterval;
                                if( m_startString.CurAlpha >= ScenceSprite.MaxAlpha )
                                    m_startString.CurAlpha = 0.0f;
                            }
                            if( Input.IsConfirmKeyDown() )
                            {
                                m_curChildState = ChildState.GameLogoDisappearState;
                            }
                        }
                    }
                    break;

                #endregion

                #region Case ChildState.GameLogoDisappearState

                case ChildState.GameLogoDisappearState:
                    {
                        m_startString.CurAlpha = Math.Abs( m_startString.FlashInterval );
                        m_logo.CurAlpha = Math.Abs( m_logo.FlashInterval );
                        m_startString.CurAlpha = 0.0f;
                        m_logo.TColor = new Color( new Vector4( 255, 255, 255, m_logo.CurAlpha ) );
                        m_startString.TColor = new Color( new Vector4( 255, 255, 255, m_startString.CurAlpha ) );
                        if( m_logo.CurAlpha <= 0.02 )
                        {
                            m_curChildState = ChildState.StartMenuShowState;
                        }
                    }
                    break;

                #endregion

                #region Case ChildState.StartMenuShowState

                case ChildState.StartMenuShowState:
                    {
                        float count = (float)gameTime.ElapsedGameTime.TotalSeconds;
                        m_timer += count;
                        if( m_timer >= 0.5f )
                        {
                            m_buttonPackaging.Update( gameTime );
                            if( !m_isSpacePressed && Input.IsConfirmKeyDown() )
                            {
                                switch( MainMenuButtonPackaging.MateButtonName )
                                {
                                    case "NewGameButton":
                                        {
                                            m_curChildState = ChildState.ZoomInBackgroundState;
                                        }

                                        break;

                                    case "ContinueButton":
                                        {
                                            m_curChildState = ChildState.DistortionState;
                                            //逻辑需要完善，需要读取前一记录的地图位置，人物状态
                                        }

                                        break;

                                    case "OptionButton":
                                        {
                                            m_stateManager.GotoState( StateManager.States.OptionMenuGameState, null );
                                        }
                                        break;

                                    case "HowToPlay":
                                        {
                                            m_stateManager.GotoState( StateManager.States.HelpGameState, null );
                                        }
                                        break;
                                    case "AchievementsButton":
                                        {
                                            m_stateManager.GotoState( StateManager.States.AchievementMenuState, null );
                                        }
                                        break;

                                    case "CreditButton":
                                        {
                                            m_stateManager.GotoState( StateManager.States.CreditShowMenuState, null );
                                        }
                                        break;

                                    case "ExitButton":
                                        {
                                            HealGame.Game.Exit();
                                        }
                                        break;
                                }
                            }

                        }
                        m_isSpacePressed = Input.IsConfirmKeyDown();
                    }
                    break;

                #endregion

                #region Case ChildState.ZoomInBackgroundState

                case ChildState.ZoomInBackgroundState:
                    {
                        float runningTime = m_count + (float)gameTime.ElapsedGameTime.TotalSeconds;
                        m_count = runningTime; // % 10;
                        m_scaleCount += ( runningTime ) / 10;


                        if( m_count > 8 )
                        {
                            m_count = 8;
                        }

                        m_scale = ( 1f - m_count / 8f ) * .5f + .5f;
                        var locate = new Point( (int)( m_count * 190f * 0.5f ), (int)( m_count * 90f * 3f / 4f ) );



                        m_background.SourceRect = new Rectangle( locate.X, locate.Y,
                                                                (int)( m_background.TextureImage.Width * m_scale ),
                                                                (int)( m_background.TextureImage.Height * m_scale ) );
                        m_foreground.SourceRect = new Rectangle( locate.X, locate.Y,
                                                                (int)( m_foreground.TextureImage.Width * m_scale ),
                                                                (int)( m_foreground.TextureImage.Height * m_scale ) );
                        m_backgroundMist.SourceRect = new Rectangle( locate.X, locate.Y,
                                                                    (int)( m_backgroundMist.TextureImage.Width * m_scale ),
                                                                    (int)( m_backgroundMist.TextureImage.Height * m_scale ) );
                        m_ufoLight.SourceRect = new Rectangle( locate.X, locate.Y,
                                                              (int)( m_ufoLight.TextureImage.Width * m_scale ),
                                                              (int)( m_ufoLight.TextureImage.Height * m_scale ) );
                        m_staticsDarkPlayer.SourceRect = new Rectangle( locate.X, locate.Y,
                                                                       (int)
                                                                       ( m_staticsDarkPlayer.TextureImage.Width * m_scale ),
                                                                       (int)
                                                                       ( m_staticsDarkPlayer.TextureImage.Height * m_scale ) );
                        m_staticsGlowingPlayer.SourceRect = new Rectangle( locate.X, locate.Y,
                                                                          (int)
                                                                          ( m_staticsGlowingPlayer.TextureImage.Width *
                                                                           m_scale ),
                                                                          (int)
                                                                          ( m_staticsGlowingPlayer.TextureImage.Height *
                                                                           m_scale ) );
                        if( m_scaleCount >= 300 )
                        {
                            m_curChildState = ChildState.CapsuleOnTargetState;

                            m_scaleCount = 0;
                        }

                    }
                    break;

                #endregion

                #region Case ChildState.CapsuleOnTargetState

                case ChildState.CapsuleOnTargetState:
                    {
                        float a = (float)gameTime.TotalGameTime.TotalMilliseconds/10;

                        if( Input.IsConfirmKeyDown() == false )
                        {
                            if( a % 2 == 0 )
                            {
                                ResetTVPictureState();
                                m_picNum = MathTools.RandomGenerate(m_tvPictureList.Count - 1);
                                m_tvPictureList[m_picNum].Visible = true;
                            }
                        }
                        else
                        {
                            ResetTVPictureState();
                            m_curChildState = ChildState.DistortionState;
                        }

                        break;

                    }

                #endregion

                #region Case ChildState.DistortionState

                case ChildState.DistortionState:
                    {
                        float runningTime = m_count + gameTime.ElapsedGameTime.Milliseconds;
                        m_count = (int)runningTime % 10;
                        m_scaleCount += (int)( runningTime ) / 10;

                        if( m_scaleCount > 800 )
                        {
                            m_scaleCount = 800;
                            AIControler.IsDead = false;
                            GameItemState.Reset();
                            MapManager.GetInstance().Goto("Map0");
                            m_stateManager.GotoState( StateManager.States.RunningGameState, null );
                        }
                        var scale = ( 1f - m_scaleCount / 800f );
                        m_effect.Parameters( "Screw", "Intensity" ).SetValue( (float)Math.Pow( scale, .3 ) );
                    }
                    break;

                #endregion
            }
        }

        public override void Draw( GameTime gameTime )
        {
            #region Draw ChildState.BeginState  &&  ChildState.GameLogoDisappearState

            if( m_drawingChildState == ChildState.BeginState
                || m_drawingChildState == ChildState.GameLogoDisappearState )
            {
                SpriteManager.SpriteBatch.Begin();
                foreach( ISprite sprite in m_beginStateCollection )
                {
                    sprite.Draw( gameTime, SpriteManager.SpriteBatch );
                }
                SpriteManager.SpriteBatch.End();
            }

            #endregion

            #region Draw ChildState.StartMenuShowState

            if( m_drawingChildState == ChildState.StartMenuShowState )
            {
                SpriteManager.SpriteBatch.Begin();
                foreach( ISprite sprite in m_startMenuShowStateCollection )
                {
                    sprite.Draw( gameTime, SpriteManager.SpriteBatch );
                }
                SpriteManager.SpriteBatch.End();
            }

            #endregion

            #region Draw ZoomInBackgroundState

            if( m_drawingChildState == ChildState.ZoomInBackgroundState )
            {
                SpriteManager.SpriteBatch.Begin();
                foreach( ISprite sprite in m_zoomInBackgroundStateCollection )
                {
                    sprite.Draw( gameTime, SpriteManager.SpriteBatch );
                }
                SpriteManager.SpriteBatch.End();
            }

            #endregion

            #region Draw ChildState.CapsuleOnTargetState

            if( m_drawingChildState == ChildState.CapsuleOnTargetState )
            {
                SpriteManager.SpriteBatch.Begin();
                foreach( ISprite sprite in m_capsuleOnTargetStateCollection )
                {
                    sprite.Draw( gameTime, SpriteManager.SpriteBatch );
                }
                SpriteManager.SpriteBatch.End();
            }

            #endregion

            #region Draw ChildState.DistortionState

            if( m_drawingChildState == ChildState.DistortionState )
            {
                m_effect.Draw( delegate( SpriteBatch b )
                                  {
                                      foreach( ISprite sprite in m_distortionStateCollection )
                                      {
                                          sprite.Draw( gameTime, b );
                                      }
                                  }, new GraphicsManager.EffectDrawParameters
                                         {
                                             Effect = "Screw",
                                             Technique = 0,
                                             Pass = 0
                                         }, Color.White );
            }


            #endregion
        }
    }
}
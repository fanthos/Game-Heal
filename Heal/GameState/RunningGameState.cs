using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Heal.Core.AI;
using Heal.Core.Entities;
using Heal.Core.GameData;
using Heal.Sprites;
using Heal.Data;
using Heal.Levels;
using Heal.World;
using Heal.Utilities;
using Microsoft.Xna.Framework;
using Heal.Core.Sence;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using Utility = Heal.Core.Utilities.CoreUtilities;
using Microsoft.Xna.Framework.Graphics;
using Heal.Core.Utilities;

namespace Heal.GameState
{
    internal class RunningGameState : GameState
    {
        private AudioManager m_audioManager;

        private MapManager m_levels;
        private WorldManager m_world;
        private SenceManager m_sence;
        private StateManager m_stateManager;
        private GraphicsManager m_effect;
        private Player player;
        private float m_time;
        private float m_timer;
        private bool m_isEscPressed;
        private bool m_isTabPressed;

        private SpriteBatch m_batch = new SpriteBatch( Heal.HealGame.Game.GraphicsDevice );
        private RenderTarget2D m_renderTarget2D;
        private ResolveTexture2D m_resolveTarget;

        private Texture2D m_renderTexture1;
        private Texture2D m_renderTexture2;

        internal override StateManager.States GetState()
        {
            return StateManager.States.RunningGameState;
        }

        public override void Initialize()
        {
            m_audioManager = AudioManager.GetInstance();
            m_audioManager.LoadSound("SongOfWaterSpray",DataReader.Load<SoundEffect>("MusicAndSoundEffect/SoundEffect/SongOfWaterSpray"));
            m_audioManager.LoadSong("SongOfRunningGame",
                                    DataReader.Load<Song>("MusicAndSoundEffect/Song/MusicOfRunningGameState"));

            m_levels = MapManager.GetInstance();
            m_world = WorldManager.GetInstance();
            m_sence = SenceManager.GetInstance();
            m_effect = GraphicsManager.GetInstance();
            m_stateManager = StateManager.GetInstance();
            //m_levels.Load( "Map6" );
        
            m_levels.Goto( "Map2" );
            player = m_world.Player;
            m_renderTarget2D = new RenderTarget2D( HealGame.Game.GraphicsDevice, 800, 600, 1,
                                                   HealGame.Game.GraphicsDevice.PresentationParameters.BackBufferFormat );
            m_resolveTarget = new ResolveTexture2D( HealGame.Game.GraphicsDevice, 800, 600, 1,
                                                  HealGame.Game.GraphicsDevice.PresentationParameters.BackBufferFormat );
                //DataReader.Load<Effect>("Effect/Blur");
        }

        public override void Update( Microsoft.Xna.Framework.GameTime gameTime )
        {
            //m_audioManager.LoadSong( "SongOfWaterSpray",
            //                        DataReader.Load<Song>( "MusicAndSoundEffect/Song/SongOfWaterSpray" ) );

            
            m_audioManager.PlaySong( "SongOfRunningGame", true );
            m_audioManager.PlaySound( "SongOfWaterSpray", 0.05f );
           

            if( !m_isEscPressed && Input.IsPauseKeyDown() )
            {
                m_stateManager.GotoState( StateManager.States.PauseGameState, m_world.ResolveDraw() );
            }

            else if( !m_isTabPressed && Input.IsStatusKeyDown() )
            {
                m_stateManager.GotoState(StateManager.States.PlayerState, m_world.ResolveDraw());
            }
            else if( AIControler.IsDead )
            {
                m_stateManager.GotoState(StateManager.States.GameOverState, m_world.ResolveDraw());
            }
            else
            {
                m_world.Update( gameTime );
            }
            m_isEscPressed = Input.IsPauseKeyDown();
            m_isTabPressed = Input.IsStatusKeyDown();

        }
        
        public override void Draw( Microsoft.Xna.Framework.GameTime gameTime )
        {
            m_effect.Begin( null );
            m_world.Draw(gameTime, m_batch);
            m_effect.End();
        }
    }
}

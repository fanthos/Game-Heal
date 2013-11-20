using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Heal.GameState
{
    /// <summary>
    /// Static class for running state
    /// </summary>
    internal class StateManager : IGameComponent, IDrawable, IUpdateable
    {
        /// <summary>
        /// The enum of game states for every state or program to switch.
        /// </summary>
        internal enum States
        {
            StartMenuGameState = 0,
            MainMenuState = 1,
            RunningGameState = 2,
            OptionMenuGameState = 3,
            VolumnGameState = 4,
            HelpGameState = 5,
            ControlShowGameState = 6,
            AchievementMenuState = 7,
            CreditShowMenuState = 8,
            PauseGameState = 9,
            PlayerState = 10,
            GameOverState = 11
        }

        private const States DefaultState = States.StartMenuGameState;

        private const int StateMaxCount = 15;

        /// <summary>
        /// Returns the array of state types.
        /// </summary>
        internal static readonly Type[] StateTypes = new[]
                                                       {
                                                           typeof(StartMenuGameState),
                                                           typeof(MainMenuGameState),
                                                           typeof(RunningGameState),
                                                           typeof(OptionMenuGameState),
                                                           typeof(VolumnGameState),
                                                           typeof(HelpGameState),
                                                           typeof(ControlShowGameState),
                                                           typeof(AchievementMenuState),
                                                           typeof(CreditShowMenuState),
                                                           typeof(PauseGameState),
                                                           typeof(PlayerShowState),
                                                           typeof(GameOverState)
                                                       };

        #region Instance
        private static StateManager m_instance;
        internal static StateManager GetInstance()
        {
            if (m_instance == null)
            {
                m_instance = new StateManager();
            }
            return m_instance;
        }

        private StateManager()
        { }

        #endregion

        #region States collection
        private class GameStateCollection
        {
            private GameState[] m_state = new GameState[StateMaxCount];
            private static readonly Type GameStateType = typeof( GameState );

            internal void Add(Type state)
            {
                DateTime time;
                if(state.BaseType!=GameStateType)
                {
                    return;
                }
                GameState newState = (GameState) Activator.CreateInstance( state );
                int i = (int)newState.GetState( );
                if (m_state[i] == null)
                {
                    time = DateTime.Now;
                    newState.Initialize();
                    Console.WriteLine((DateTime.Now - time).ToString() + newState.GetType());
                    m_state[i] = newState;
                }
            }
            internal GameState this[States s]
            {
                get
                {
                    return this[(int) s];
                }
            }
            internal GameState this[int i]
            {
                get
                {
                    return m_state[i];
                }
            }
        }

        private GameStateCollection m_collection;
        private States m_runningState;
        private GameState m_nowState;
        private GameState m_drawingState;

        /// <summary>
        /// Gets or sets the game state of the running.
        /// </summary>
        /// <value>The state of the running.</value>
        internal States RunningState
        {
            get
            {
                return m_runningState;
            }
        }

        #endregion

        internal void GotoState(States value, object param)
        {
            m_runningState = value;
            m_nowState = m_collection[value];
            m_nowState.SetEventParam( param );
        }

        #region Initialize and update & draw code

        private bool m_init = false;
        public void Initialize()
        {
            if(m_init)
            {
                return;
            }
            m_init = true;
            m_collection = new GameStateCollection(  );
            foreach( Type type in StateTypes )
            {
                AddNewGameState( type );
            }
            this.GotoState( DefaultState, null );
        }

        private void AddNewGameState(Type state)
        {
            m_collection.Add( state );
        }

        public void Update(GameTime gameTime)
        {
            m_drawingState = m_nowState;
            m_nowState.Update( gameTime );
        }

        public void Draw(GameTime gameTime)
        {
            if(m_drawingState == null)
            {
                return;
            }
            m_drawingState.Draw( gameTime );
        }

        #endregion

        #region Implementation of IDrawable

        public bool Visible { get { return true; } }
        public int DrawOrder { get { return 1; } }
        event EventHandler<EventArgs> IDrawable.DrawOrderChanged
        { add { } remove { } }
        event EventHandler<EventArgs> IDrawable.VisibleChanged
        { add { } remove { } }
        #endregion

        #region Implementation of IUpdateable

        public bool Enabled { get { return true; } }
        public int UpdateOrder { get { return 1; } }
        event EventHandler<EventArgs> IUpdateable.EnabledChanged
        { add { } remove { } }
        event EventHandler<EventArgs> IUpdateable.UpdateOrderChanged
        { add { } remove { } }

        #endregion
    }
}



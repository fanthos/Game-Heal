using System;
using Microsoft.Xna.Framework;

namespace Heal.GameState
{
    /// <summary>
    /// Game state class for showing information or playing.
    /// </summary>
    internal abstract class GameState : IGameComponent, IDrawable, IUpdateable
    {

        internal class GameStateEventArgs : EventArgs
        {
            internal object Param { get; private set; }
            internal GameStateEventArgs(object param)
            {
                Param = param;
            }
        }

        internal delegate void GameStateEventHandler( object sender, GameStateEventArgs args );

        /// <summary>
        /// Gets the enum of the game state.
        /// </summary>
        /// <returns></returns>
        internal abstract StateManager.States GetState();

        internal void SetEventParam(object param)
        {
            InvokeGameStateChanged( new GameStateEventArgs(param) );
        }

        internal event GameStateEventHandler GameStateChanged;

        private void InvokeGameStateChanged( GameStateEventArgs e )
        {
            GameStateEventHandler handler = GameStateChanged;
            if( handler != null ) handler( this, e );
        }

        public abstract void Update(GameTime gameTime);

        public bool Enabled { get { return true; } }

        public int UpdateOrder { get { return 0; } }

        public event EventHandler EnabledChanged;
        public event EventHandler UpdateOrderChanged;

        public abstract void Initialize();

        public abstract void Draw(GameTime gameTime);

        public bool Visible { get { return true; } }

        public int DrawOrder { get { return 0; } }


        event EventHandler<EventArgs> IDrawable.DrawOrderChanged
        {
            add {}
            remove {}
        }

        event EventHandler<EventArgs> IDrawable.VisibleChanged
        {
            add {}
            remove {}
        }


        event EventHandler<EventArgs> IUpdateable.EnabledChanged
        {
            add {}
            remove {}
        }

        event EventHandler<EventArgs> IUpdateable.UpdateOrderChanged
        {
            add {}
            remove {}
        }
    }
}



using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using System.Diagnostics;

namespace Heal.Core.Utilities
{
    public abstract class GameClassManager : IGameComponent
    {
        private bool m_created = false;

        [DebuggerHidden]
        protected GameClassManager()
        {
            if(QueryInstance()!=null)
            {
                throw new Exception("Instance already created.");
            }
        }

        [DebuggerHidden]
        public GameClassManager QueryInstance()
        {
            return InstanceManager.GetInstance( this.GetType() );
        }

        public abstract void Initialize();

        public virtual void PostInitialize()
        {
        }
    }
}

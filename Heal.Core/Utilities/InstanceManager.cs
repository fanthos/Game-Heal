using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using System.Diagnostics;

namespace Heal.Core.Utilities
{
    public static class InstanceManager
    {
        private static Dictionary<Type, GameClassManager> m_instances = new Dictionary<Type, GameClassManager>();

        public static void Initialize()
        {
            ;
        }

        public static void InitializeManagers()
        {
            DateTime time;
            foreach( var instance in m_instances )
            {
                time = DateTime.Now;
                instance.Value.Initialize();
                Console.WriteLine((DateTime.Now - time).ToString() + instance.ToString());
            }
            foreach (var instance in m_instances)
            {
                instance.Value.PostInitialize();
            }
        }

        private static void InternalRegisterInstance(Type type)
        {
            GameClassManager gameClassManager;
            gameClassManager = (GameClassManager) Activator.CreateInstance( type );
            m_instances.Add(type, gameClassManager );
        }

        [DebuggerHidden]
        public static void RegisterInstanceList(Type[] types)
        {
            foreach( var type in types )
            {
                InternalRegisterInstance(type);
            }
        }

        [DebuggerHidden]
        public static void RegisterInstance(params Type[] types )
        {
            foreach (var type in types)
            {
                InternalRegisterInstance(type);
            }
        }

        [DebuggerHidden]
        public static GameClassManager GetInstance(Type type)
        {
            GameClassManager tmp;
            if (m_instances.TryGetValue(type, out tmp))
                return tmp;
            //RegisterInstance(type);
            return null;
            //return m_instances[type];
        }
    }
}

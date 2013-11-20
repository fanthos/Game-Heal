using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Heal.Core.Utilities
{
    public static class GameCommands
    {
        private static readonly Queue<string> m_data = new Queue<string>();

        public static void Enqueue(string command)
        {
            m_data.Enqueue( command );
        }

        public static string Dequeue()
        {
            if (m_data.Count > 0)
                return m_data.Dequeue();
            else return "";
        }

        public static int Count()
        {
            return m_data.Count;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Heal.Core.Utilities
{
    public class MathTools
    {
        private static Random m_random = new Random();

        public static float RandomGenerate()
        {
            return (float) m_random.NextDouble();
        }

        public static int RandomGenerate(int n)
        {
            return m_random.Next(n);
        }
    }
}

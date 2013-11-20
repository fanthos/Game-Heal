using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Heal.Core.GameData;
using Heal.Core.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Heal.Core.Sence
{
    public class SencePart
    {
        public SencePart(int x, int y)
        {
            X = x;
            Y = y;
        }
        public byte[] CollusionTexture;

        public readonly int X;//{ get { return m_locate.X; } set { m_locate.X = value; } }}
        public readonly int Y;//{ get { return m_locate.Y; } set { m_locate.Y = value; } }}
    }
}

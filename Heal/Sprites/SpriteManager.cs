using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Heal.Core.Utilities;

namespace Heal.Sprites
{
    public class SpriteManager : GameClassManager
    {
        public static SpriteBatch SpriteBatch;

        private SpriteManager m_instance;

        #region Overrides of GameClassManager

        public override void Initialize()
        {
            m_instance = this;
        }

        #endregion
    }
}

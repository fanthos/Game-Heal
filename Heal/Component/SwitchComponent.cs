using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Heal.Core.Entities;
using Heal.Data;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Heal.Component
{
    internal class SwitchComponent : ItemComponent
    {
        private Texture2D m_disabled;
        private Texture2D m_enabled;

        internal SwitchComponent(Texture2D enabled, Texture2D disabled)
        {
            m_disabled = disabled;
            m_enabled = enabled;
        }

        protected override void InternalDraw(GameTime gameTime, SpriteBatch batch)
        {
            Switcher item = (Switcher)m_item;
            if (item.Enabled)
            {
                batch.Draw(m_enabled, base.Locate, null, Color.White, 0, new Vector2(400f),
                            0.1f * m_worldManager.Scale, SpriteEffects.None, 0);

            }
            else
            {
                batch.Draw(m_disabled, base.Locate, null, Color.White, 0, new Vector2(400f),
                            0.1f * m_worldManager.Scale, SpriteEffects.None, 0);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Heal.Data;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Heal.Core.Entities;

namespace Heal.Component
{
    internal class TeleportComponent : ItemComponent
    {
        private Texture2D m_disabled;
        private Texture2D m_enabledArrow;
        private Texture2D m_enabledBg;

        internal TeleportComponent()
        {
            m_disabled = DataReader.Load<Texture2D>("Texture/Entities/Teleport/disabled");
            m_enabledArrow = DataReader.Load<Texture2D>("Texture/Entities/Teleport/enabledbg");
            m_enabledBg = DataReader.Load<Texture2D>("Texture/Entities/Teleport/enabledarrow");
        }

        protected override void InternalDraw( GameTime gameTime, SpriteBatch batch )
        {
            Teleport teleport = (Teleport)m_item;
            if(teleport.Enabled)
            {
                batch.Draw(m_enabledBg, (teleport.Locate1 - m_worldManager.Locate) * m_worldManager.Scale + m_worldManager.Space / 2, null, Color.White, 0, new Vector2(400f),
                            0.1f * m_worldManager.Scale, SpriteEffects.None, 0);
                batch.Draw(m_enabledBg, (teleport.Locate2 - m_worldManager.Locate) * m_worldManager.Scale + m_worldManager.Space / 2, null, Color.White, 0, new Vector2(400f),
                            0.1f * m_worldManager.Scale, SpriteEffects.None, 0);
                batch.Draw(m_enabledArrow, (teleport.Locate1 - m_worldManager.Locate) * m_worldManager.Scale + m_worldManager.Space / 2, null, Color.White, teleport.Rotation, new Vector2(400f),
                            0.1f * m_worldManager.Scale, SpriteEffects.None, 0);
                batch.Draw(m_enabledArrow, (teleport.Locate2 - m_worldManager.Locate) * m_worldManager.Scale + m_worldManager.Space / 2, null, Color.White, teleport.Rotation, new Vector2(400f),
                            0.1f * m_worldManager.Scale, SpriteEffects.None, 0);
            }
            else
            {
                batch.Draw(m_disabled, (teleport.Locate1 - m_worldManager.Locate) * m_worldManager.Scale + m_worldManager.Space / 2, null, Color.White, 0, new Vector2(400f),
                            0.1f * m_worldManager.Scale, SpriteEffects.None, 0);
                batch.Draw(m_disabled, (teleport.Locate2 - m_worldManager.Locate) * m_worldManager.Scale + m_worldManager.Space / 2, null, Color.White, 0, new Vector2(400f),
                            0.1f * m_worldManager.Scale, SpriteEffects.None, 0);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Heal.Core.Entities;

namespace Heal.Component
{
    internal class ElementInfoComponent : ItemComponent
    {
        protected override void InternalDraw( GameTime gameTime, SpriteBatch batch )
        {
            ElementInfo element = (ElementInfo)m_entity;
            if (element.Enabled)
            {
                batch.Draw( element.Texture, base.Locate, null, Color.White, 0,
                            element.Locate, m_worldManager.Scale, SpriteEffects.None, 0 );
            }
        }
    }
}

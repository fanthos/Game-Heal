using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Heal.Core.Entities;

namespace Heal.Component
{
    internal class ElementsComponent : ItemComponent
    {
        protected override void InternalDraw( GameTime gameTime, SpriteBatch batch )
        {
            Element element = (Element) m_entity;
            if (element.Enabled)
            {
                batch.Draw( element.Texture, base.Locate, null, Color.White, 0,
                            new Vector2( element.Texture.Width, element.Texture.Height ) / 2,
                            element.Scale * m_worldManager.Scale, SpriteEffects.None, 0 );
            }
        }
    }
}

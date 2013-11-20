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
    internal class DNAComponent : ItemComponent
    {
        public void AddTexture()
        {
            ((DNA)m_item).AddTexture(DataReader.Load<Texture2D>("Texture/Entities/DNA/Zi"),
                DataReader.Load<Texture2D>("Texture/Entities/DNA/Huang"),
                DataReader.Load<Texture2D>("Texture/Entities/DNA/Lv"));
        }

        protected override void InternalDraw(GameTime gameTime, SpriteBatch batch)
        { 
            foreach (var ptr in ((DNA)m_entity).List)
            {
                batch.Draw(ptr.Target, base.Locate, null, Color.AliceBlue,0,
                            new Vector2(400f),DNA.Size * m_worldManager.Scale /10, SpriteEffects.None, 0);
            }
        }
    }
}

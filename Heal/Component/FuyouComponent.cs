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
    internal class FuyouComponent : ItemComponent
    {
        public void AddTexture()
        {
            ((Fuyou)m_item).AddToList(DataReader.Load<Texture2D>("Texture/Entities/Fuyou/1"), new Vector2());
        }

        protected override void InternalDraw(GameTime gameTime, SpriteBatch batch)
        {
            throw new NotImplementedException();
        }
    }
}

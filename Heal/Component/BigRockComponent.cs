using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Heal.Core.AI;
using Heal.Core.Entities;
using Heal.Core.Entities.Enemies;
using Heal.Data;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Heal.Component
{
    internal class BigRockComponent : UnitComponent
    {
        public void AddTexture()
        {
            ((BigRock)m_unit).AddTarget(DataReader.Load<Texture2D>("Texture/Entities/BigRock"));
        }


        protected override void InternalDraw(Microsoft.Xna.Framework.GameTime gameTime, SpriteBatch batch)
        {
            batch.Draw(((BigRock)m_unit).Targetr, base.Locate, null, Color.AliceBlue, 0, new Vector2(400), ((BigRock)m_unit).EnemySize * m_worldManager.Scale, SpriteEffects.None, 0);

        }
    }
}

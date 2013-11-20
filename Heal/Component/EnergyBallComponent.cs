using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Heal.Core.Entities;
using Heal.Core.Entities.Enemies;
using Heal.Data;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Heal.Component
{
    internal class EnergyBallComponent : ItemComponent
    {
        private Texture2D Target;

        internal void AddTexture(String target)
        {
            Target = DataReader.Load<Texture2D>("Texture/Entities/" + target);
        }

        internal void AddNode(Vector2 Postion)
        {
            ((EnergyBall) m_entity).AddNode(Postion);
        }

        protected override void InternalDraw(GameTime gameTime, SpriteBatch batch)
        {
            foreach (var ptr in ((EnergyBall)m_entity).List)
            {
                batch.Draw(Target, base.Locate + ptr.Postion * m_worldManager.Scale, null, ptr.Color, ptr.Rotation,
                            new Vector2(100f),  EnergyBall.Size* m_worldManager.Scale * new Vector2(1, (float)Math.Sin( ptr.Scale) * .45f + .55f), SpriteEffects.None, 0);
            }
        }
    }
}

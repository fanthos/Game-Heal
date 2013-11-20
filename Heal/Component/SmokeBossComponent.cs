using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Heal.Core.Entities.Enemies;
using Heal.Core.Utilities;
using Heal.Data;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Heal.Component
{
    internal class SmokeBossComponent : EnemyComponent
    {
        internal void AddToList(String target,Vector2 postion,double angle)
        {
            Texture2D texture2D = DataReader.Load<Texture2D>("Texture/Entities/" + target);
            ((SmokeBoss)m_entity).AddToList(texture2D,postion,angle);
        }

        protected override void InternalDraw(GameTime gameTime, SpriteBatch batch)
        {
            if (!CoreUtilities.StateIsTrue("map0_smoke"))
            {
                return;
            }
            foreach (var ptr in ((SmokeBoss)m_entity).SmokeBossList)
            {
                batch.Draw(ptr.Target, base.Locate+ptr.Postion*m_worldManager.Scale, null, new Color(1f,1f,1f,Math.Abs(ptr.Alpha)*0.3f + 0.7f), base.Rotation,
                            new Vector2(500f),((SmokeBoss)m_entity).EnemySize * m_worldManager.Scale, SpriteEffects.None, 0);
            }
        }
    }

}

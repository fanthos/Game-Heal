using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Heal.Core.Entities.Enemies;
using Heal.Data;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Heal.Component
{
    internal class StoneBossComponent : EnemyComponent
    {
        internal void AddToList(String body,String eye,String lh,String rh,String ll,String rl)
        {
            Texture2D t1 = DataReader.Load<Texture2D>("Texture/Entities/" + body);
            Texture2D t2 = DataReader.Load<Texture2D>("Texture/Entities/" + eye );
            Texture2D t3 = DataReader.Load<Texture2D>("Texture/Entities/" + lh);
            Texture2D t4 = DataReader.Load<Texture2D>("Texture/Entities/" + rh);
            Texture2D t5 = DataReader.Load<Texture2D>("Texture/Entities/" + ll);
            Texture2D t6 = DataReader.Load<Texture2D>("Texture/Entities/" + rl);
            ((StoneBoss)m_entity).Install(t1,t2,t3,t4,t5,t6);
        }

        protected override void InternalDraw(GameTime gameTime, SpriteBatch batch)
        {
            foreach (var ptr in ((StoneBoss)m_entity).StoList)
            {
                batch.Draw(ptr.Target, base.Locate + ptr.Offset * m_worldManager.Scale, null, ptr.Color
                    ,ptr.Rotation ,new Vector2(400),  ((StoneBoss)m_entity).EnemySize * m_worldManager.Scale/ 100, SpriteEffects.None, 0);
            }
        }
    }
}

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
    internal class GhostComponent : UnitComponent
    {
        internal void AddToLeft(String a,String b,String c,
                              String d,String e,String f,
                              String g,String h,String i)
        {
            Texture2D t1 = DataReader.Load<Texture2D>("Texture/Entities/" + a);
            Texture2D t2 = DataReader.Load<Texture2D>("Texture/Entities/" + b);
            Texture2D t3 = DataReader.Load<Texture2D>("Texture/Entities/" + c);
            Texture2D t4 = DataReader.Load<Texture2D>("Texture/Entities/" + d);
            Texture2D t5 = DataReader.Load<Texture2D>("Texture/Entities/" + e);
            Texture2D t6 = DataReader.Load<Texture2D>("Texture/Entities/" + f);
            Texture2D t7 = DataReader.Load<Texture2D>("Texture/Entities/" + g);
            Texture2D t8 = DataReader.Load<Texture2D>("Texture/Entities/" + h);
            Texture2D t9 = DataReader.Load<Texture2D>("Texture/Entities/" + i);

            ((Ghost)m_entity).AddLeft(t1,t2,t3,t4,t5,t6,t7,t8,t9);
        }

        internal void AddToRight(String a, String b, String c,
                      String d, String e, String f,
                      String g, String h, String i)
        {
            Texture2D t1 = DataReader.Load<Texture2D>("Texture/Entities/" + a);
            Texture2D t2 = DataReader.Load<Texture2D>("Texture/Entities/" + b);
            Texture2D t3 = DataReader.Load<Texture2D>("Texture/Entities/" + c);
            Texture2D t4 = DataReader.Load<Texture2D>("Texture/Entities/" + d);
            Texture2D t5 = DataReader.Load<Texture2D>("Texture/Entities/" + e);
            Texture2D t6 = DataReader.Load<Texture2D>("Texture/Entities/" + f);
            Texture2D t7 = DataReader.Load<Texture2D>("Texture/Entities/" + g);
            Texture2D t8 = DataReader.Load<Texture2D>("Texture/Entities/" + h);
            Texture2D t9 = DataReader.Load<Texture2D>("Texture/Entities/" + i);

            ((Ghost)m_entity).AddRight(t1, t2, t3, t4, t5, t6, t7, t8, t9);
        }

        protected override void InternalDraw(Microsoft.Xna.Framework.GameTime gameTime, SpriteBatch batch)
        {
            foreach (var ptr in ((Ghost)m_entity).CurrentList)
            {
                batch.Draw(ptr[((Ghost)m_entity).CurrentStatus], base.Locate, null, ((Ghost)m_entity).Color, ((Ghost)m_entity).Rotation, new Vector2(40), ((Ghost)m_entity).EnemySize * m_worldManager.Scale, SpriteEffects.None, 0);
            }
            batch.Draw(((Ghost)m_entity).ListRing[((Ghost)m_entity).CurrentRing].Target, base.Locate, ((Ghost)m_entity).ListRing[((Ghost)m_entity).CurrentRing].Frame, ((Ghost)m_entity).ListRing[((Ghost)m_entity).CurrentRing].Color, base.Rotation,
                           new Vector2((float)((Ghost)m_entity).ListRing[((Ghost)m_entity).CurrentRing].Frame.Height / 2), ((Ghost)m_entity).RingSize / 1000 * m_worldManager.Scale, SpriteEffects.None, 0);

            if (m_unit.CurrentDialog != AIBase.StatusDialog.Blank)
            {
                batch.Draw(StatusDialog.List[(int)m_unit.CurrentDialog], base.Locate + new Vector2(-60, -100f), null, m_unit.StatusColor, 0,
                    new Vector2(0), 0.15f * m_worldManager.Scale, SpriteEffects.None, 0);
            }
        }
    }
}

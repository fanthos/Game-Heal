using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Heal.Core.AI;
using Heal.Core.Entities.Enemies;
using Heal.Data;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Heal.Component
{
    internal class EnemyIIComponent : EnemyComponent
    {

        public void AddSharp(string target, int oSize, float time)
        {
            Texture2D texture2D = DataReader.Load<Texture2D>("Texture/Entities/" + target);
            int size = texture2D.Height;
            int picCount = texture2D.Width / size;
            ((EnemyTypeII)m_unit).AddSharp(texture2D,picCount,size,oSize,(int) time);
        }

        public void AddBody()
        {
            ((EnemyTypeII)m_unit).AddBody(DataReader.Load<Texture2D>("Texture/Entities/Monster2/monster2_face"));
        }

        protected override void InternalDraw(Microsoft.Xna.Framework.GameTime gameTime, Microsoft.Xna.Framework.Graphics.SpriteBatch batch)
        {

            if (((EnemyTypeII)m_unit).CombineList.Count != 0)
            {
                foreach (var ptr in ((EnemyTypeII)m_unit).CombineList)
                {
                    ((EnemyIIComponent)ptr.Sprite).Draw(gameTime, batch);
                    //batch.Draw(ptr.Body, ptr.Locate, null, ptr.Color, 0, new Vector2(40), ptr.EnemySize * m_worldManager.Scale, SpriteEffects.None, 0);
                }
                batch.Draw(m_unit.ListRing[m_unit.CurrentRing].Target, base.Locate + new Vector2(20,0),
                           m_unit.ListRing[m_unit.CurrentRing].Frame, m_unit.ListRing[m_unit.CurrentRing].Color,
                           base.Rotation,
                           new Vector2((float) m_unit.ListRing[m_unit.CurrentRing].Frame.Height/2),
                           m_unit.RingSize/1000*m_worldManager.Scale, SpriteEffects.None, 0);
            }
            if(((EnemyTypeII)m_unit).CombineList.Count != 0 || ((EnemyTypeII)m_unit).BeCatch)
            {
                batch.Draw(((EnemyTypeII)m_unit).Body, base.Locate, null, ((EnemyTypeII)m_unit).Color, ((EnemyTypeII)m_unit).Rotation, new Vector2(40), m_worldManager.Scale * ((EnemyTypeII)m_unit).EnemySize, SpriteEffects.None, 0);
            }
            else
            {
                if (((EnemyTypeII)m_unit).Status == AIBase.Status.Attck)
                {
                    foreach (var ptr in ((EnemyTypeII)m_unit).Sharp)
                    {
                        batch.Draw(ptr.Target, base.Locate, ptr.Frame, Color.AliceBlue, 0, new Vector2(40), ((EnemyTypeII)m_unit).EnemySize * m_worldManager.Scale, SpriteEffects.None, 0);
                    }
                }
                batch.Draw(((EnemyTypeII)m_unit).Body, base.Locate, null, ((EnemyTypeII)m_unit).Color, ((EnemyTypeII)m_unit).Rotation, new Vector2(40), m_worldManager.Scale * ((EnemyTypeII)m_unit).EnemySize, SpriteEffects.None, 0);
                batch.Draw(m_unit.ListRing[m_unit.CurrentRing].Target, base.Locate, m_unit.ListRing[m_unit.CurrentRing].Frame, m_unit.ListRing[m_unit.CurrentRing].Color, base.Rotation,
                    new Vector2((float)m_unit.ListRing[m_unit.CurrentRing].Frame.Height / 2), m_unit.RingSize / 1000 * m_worldManager.Scale, SpriteEffects.None, 0);
            }
        }
    }
}

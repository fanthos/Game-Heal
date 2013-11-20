using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Heal.Core.AI;
using Heal.Core.Entities;
using Heal.Data;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Heal.Component
{
    internal abstract class UnitComponent : EntityComponent
    {
        protected Unit m_unit;
        internal override Entity Unit
        {
            get
            {
                return m_unit;
            }
            set
            {
                m_unit = (Unit)value;
                base.m_entity = value;
            }
        }
        internal void AddNew(string target, int oSize, float time, AIBase.List which)
        {
            Texture2D texture2D = DataReader.Load<Texture2D>("Texture/Entities/" + target);
            int size = texture2D.Height;
            int picCount = texture2D.Width / size;
            m_unit.AddNew(texture2D, picCount, size, oSize, time, which);
        }

        protected override void InternalDraw(GameTime gameTime, SpriteBatch batch)
        {

            if (m_unit.CurrentDialog != AIBase.StatusDialog.Blank)
            {
                batch.Draw(StatusDialog.List[(int)m_unit.CurrentDialog], base.Locate + new Vector2(-60, -100f), null, m_unit.StatusColor, 0,
                    new Vector2(0), 0.15f * m_worldManager.Scale, SpriteEffects.None, 0);
            }
            foreach (var ptr in m_unit.List)
            {
                batch.Draw(ptr.Target, base.Locate, ptr.Frame, ptr.Color, base.Rotation,
                            new Vector2((float)ptr.Frame.Height / 2), m_unit.EnemySize * m_worldManager.Scale, SpriteEffects.None, 0);
            }


            batch.Draw(m_unit.ListRing[m_unit.CurrentRing].Target, base.Locate, m_unit.ListRing[m_unit.CurrentRing].Frame, m_unit.ListRing[m_unit.CurrentRing].Color, base.Rotation,
                    new Vector2((float)m_unit.ListRing[m_unit.CurrentRing].Frame.Height / 2), m_unit.RingSize / 1000 * m_worldManager.Scale, SpriteEffects.None, 0);
        }
    }
}

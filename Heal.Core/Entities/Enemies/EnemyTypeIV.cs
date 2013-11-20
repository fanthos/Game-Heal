using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Heal.Core.AI;
using Microsoft.Xna.Framework;

namespace Heal.Core.Entities.Enemies
{
    public class EnemyTypeIV : Enemy
    {
        private AIBase.StatusIV m_status4;
        public AIBase.StatusIV Status4
        {
            set { m_status4 = value;}
            get { return m_status4; }
        }

        private float m_alarmRange;
        public float AlarmRange
        {
            get { return m_alarmRange; }
        }

        public static float Size = 80;

        public bool CallEnemy;

        public EnemyTypeIV(object sprite, Vector2 speed, Vector2 locate, float ringSize, float enemySize,float enemySize2, AIBase.FaceSide face, AIBase.ID id, float range)
            : base(sprite, speed, locate, ringSize, enemySize,enemySize2, face, id)
        {
            this.Status = AIBase.Status.Alarm;
            m_alarmRange = range;
            this.CallEnemy = false;
        }


        public float GiveSignal()
        {
            return this.AlarmRange;
        }

        public override float Rotation
        {
            get
            {
                return 0;
            }
        }

        internal override void ToTurnStatus()
        {
            
        }

        internal override void ToChaseStatus(GameTime gameTime, Player player)
        {
         
        }

        internal override void ToFleeStatus(GameTime gameTime, Player playerr)
        {
        }

        internal override void ToFreezeStatus(GameTime gameTime, Player player)
        {
            
        }

        internal override void ToAttackStatus(GameTime gameTime, Player ptr)
        {
            
        }

        internal override void ToDeadStatus(GameTime gameTime, Player ptr)
        {
            
        }

        public override Rectangle GetDrawingRectangle()
        {
            throw new NotImplementedException();
        }

        public override void Initialize()
        {
            throw new NotImplementedException();
        }
    }
}

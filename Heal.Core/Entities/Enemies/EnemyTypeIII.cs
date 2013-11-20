using System;
using Heal.Core.AI;
using Microsoft.Xna.Framework;

namespace Heal.Core.Entities.Enemies
{
    public class EnemyTypeIII : Enemy
    {
        private AIBase.StatusIII m_status3;
        public AIBase.StatusIII Status3
        {
            set { m_status3 = value; }
            get { return m_status3; }
        }

        private float m_downSpeed;
        private float m_upSpeed;
        public static float Size = 0.8f;
        public EnemyTypeIII(object sprite, Vector2 speed, Vector2 locate, float ringSize, float enemySize, float enemySize2, AIBase.FaceSide face, AIBase.ID id, float m_up)
            : base(sprite, speed, locate, ringSize, enemySize, enemySize2, face, id)
        {
            m_status3 = AIBase.StatusIII.Sleep;
            m_downSpeed = Speed.X;
            m_upSpeed = -m_up;
            this.CurrentRing = 0;
            this.Delta = 1;
            this.Status = AIBase.Status.Attck;
        }

        public float Delta;
        public static float MaxRing = 400;
        public static float MinRing = 100;

        public override float Rotation
        {
            get
            {
                return 0;
            }
        }
        public void DownFall()
        {
            Speed.X = 0f;
            Speed.Y = m_downSpeed;
        }

        public void UpGoes()
        {
            Speed.X = 0f;
            Speed.Y = m_upSpeed;
        }

        public void Sleep()
        {
            Speed.X = 0f;
            Speed.Y = m_downSpeed;
        }


        internal override void ToTurnStatus()
        {

        }

        internal void ToAlarmStatus()
        {
            if ((this.Locate - AIBase.Player.Locate).Length() <= this.RingSize / 2 + AIBase.Player.RingSize / 2 - 100)
            {
                this.Status3 = AIBase.StatusIII.Alarm;
                this.CurrentRing = 3;
                this.Face = AIBase.FaceSide.Right;

            }
        }

        internal bool ToDownFall()
        {
            if (Math.Abs(AIBase.Player.Locate.X - this.Locate.X) <= 20 && AIBase.Player.Locate.Y - this.Locate.Y > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
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

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (this.CurrentDialog != AIBase.StatusDialog.Blank && this.StatusColor.A >0)
            {
                this.StatusColor.A -= 5;
            }
            else
            {
                this.StatusColor.A= 255;
                this.CurrentDialog = AIBase.StatusDialog.Blank;
            }
        }

        #region Overrides of Entity

        public override Rectangle GetDrawingRectangle()
        {
            throw new NotImplementedException();
        }

        public override void Initialize()
        {
            ;
        }

        #endregion
    }
}



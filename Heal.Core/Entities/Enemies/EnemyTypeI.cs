using System;
using Heal.Core.AI;
using Microsoft.Xna.Framework;

namespace Heal.Core.Entities.Enemies
{
    public class EnemyTypeI : Enemy
    {
        public static float FleeRange = 120;
        public float CurrentFlee;
        public Vector2 DefaultSpeed;
        public static float Size = 0.75f;
        //II类敌人Normal状态时候的直线往返次数
        private readonly int m_swingRange;
        public int SwingRange
        {
            get { return m_swingRange; }
        }

        //当前已经走过的步数
        private int m_stepCount;
        public int StepCount
        {
            get { return m_stepCount; }
            set { m_stepCount = value; }
        }

        public EnemyTypeI(object sprite, Vector2 speed, Vector2 locate, float ringSize, float enemySize, float enemySize2, AIBase.FaceSide face, AIBase.ID id, int range)
            : base(sprite, speed, locate, ringSize, enemySize2, enemySize, face, id)
        {
            DefaultSpeed = speed;
            m_swingRange = range;
            m_stepCount = 0;
            CurrentFlee = 0;
            this.CurrentDialog = AIBase.StatusDialog.Blank;
        }

        internal override void ToTurnStatus()
        {
            if (Face == AIBase.FaceSide.Left && Speed.X > 0)
            {
                if (LastStatus == AIBase.Status.Blank)
                    LastStatus = Status;
                Status = AIBase.Status.Turning;
            }
            else
            {
                if (Face == AIBase.FaceSide.Right && Speed.X < 0)
                {
                    if(LastStatus == AIBase.Status.Blank)
                        LastStatus = Status;
                    Status = AIBase.Status.Turning;
                }
            }
        }

        internal override void ToFreezeStatus(GameTime gameTime, Player player)
        {
            if(this.Status == AIBase.Status.Flee
                && AIBase.Player.Status == AIBase.Status.Strike
                && (this.Locate - AIBase.Player.Locate).Length() < 40)
            {
                this.LastStatus = this.Status;
                this.Status = AIBase.Status.Freeze;
            }
        }

        internal override void ToAttackStatus(GameTime gameTime, Player ptr)
        {
            //在攻击范围之内
            if ((this.Locate - ptr.Locate).Length() <= 40
                && this.RingSize >= AIBase.Player.RingSize
                && this.Status != AIBase.Status.Dead
                &&this.Status != AIBase.Status.Turning)
            {
                this.LastStatus = this.Status;
                this.Status = AIBase.Status.Attck;
                this.CurrentDialog = AIBase.StatusDialog.Attack;
            }
        }

        internal override void ToDeadStatus(GameTime gameTime, Player ptr)
        {
            if ((this.Status == AIBase.Status.Flee || !Enemy.Detected((Enemy)this))
                && (AIBase.Player.Locate - this.Locate).Length() <= 50
          
                && ptr.Status == AIBase.Status.Attck)
            {
                this.Status = AIBase.Status.Dead;
            }
        }

        protected override void RadianGenerate(GameTime gameTime)
        {
            float a = Speed.Radian;
            if (Speed.Length < 10)
                a = 0;
            if (Face == AIBase.FaceSide.Left)
            {
                a -= MathHelper.Pi;
            }
            if (Rotate - a > MathHelper.Pi)
            {
                a += MathHelper.TwoPi;
            }
            else if (a - Rotate > MathHelper.Pi)
            {
                a -= MathHelper.TwoPi;
            }
            m_rotate = MathHelper.WrapAngle(Rotate + (float)Math.Atan(a - Rotate) * 12f * (float)gameTime.ElapsedGameTime.TotalSeconds);
        }


        internal override void ToChaseStatus(GameTime gameTime, Player player)
        {

        }

        internal override void ToFleeStatus(GameTime gameTime, Player playerr)
        {

        }

        public override void Update(GameTime gameTime)
        {
            RadianGenerate( gameTime );
            base.Update(gameTime);
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

        public override float Rotation
        {
            get
            {
                return base.Rotate - MathHelper.PiOver2;
            }
        }

        #endregion
    }
}



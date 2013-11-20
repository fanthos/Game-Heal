using System;
using Heal.Core.AI;
using Heal.Core.Entities.Enemies;
using Heal.Core.Utilities;
using Microsoft.Xna.Framework;

namespace Heal.Core.Entities
{
    /// <summary>
    /// Storage unit information for player controlled.
    /// </summary>
    public class GrayPlayer : Player
    {
        private Vector2 m_lastLocate;

        public GrayPlayer(object sprite, Vector2 speed, Vector2 locate, float enemySize, float enemySize2,
            AIBase.FaceSide face, AIBase.ID id, float maxSpeed, float accerlate, int cooldown)
            : base(sprite, speed, locate, 0, enemySize,enemySize2, face, id, maxSpeed, accerlate, cooldown)
        {
            RingSize = 0;
        }

        internal override void ToTurnStatus()
        {
            if (this.Status != AIBase.Status.Attck && this.Status!= AIBase.Status.Flat)
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
                        if (LastStatus == AIBase.Status.Blank)
                            LastStatus = Status;
                        Status = AIBase.Status.Turning;
                    }
                }
            }
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
            if (this.Scale.Y <= 1)
                this.Scale.Y += 0.01f;
            else
                this.Scale.Y = 1;

            switch (Status)
            {
                case AIBase.Status.Turning:
                case AIBase.Status.Blank:
                case AIBase.Status.Normal:
                    {
                        if( Input.Speed.Radian == Input.Speed.Radian && Input.Speed.Length > 0.2 )
                        {
                            this.Speed.Radian = Input.Speed.Radian;
                            this.Speed.Length = Input.Speed.Length * this.MaxSpeed;
                        }
                        else
                        {
                            Speed.Length = 0;
                        }
                        break;
                    }
            }
            this.Postion = AIBase.SenceManager.MoveTest(this.Locate, Speed.Radian, Speed.Length,
                                   (float)gameTime.ElapsedGameTime.TotalSeconds);
            RadianGenerate( gameTime );
            LocateConfirm();
            Speed speed = Speed;
            Vector2 locate = Locate;

            if (Input.IsLeftKeyDown())
            {
                if (this.Face == AIBase.FaceSide.Right && Status == AIBase.Status.Normal)
                {
                    this.LastStatus = this.Status;
                    this.Status = AIBase.Status.Turning;
                    //Speed.X = -Speed.X;
                }
            }

            if (Input.IsRightKeyDown())
            {
                if (this.Face == AIBase.FaceSide.Left && Status == AIBase.Status.Normal)
                {
                    this.LastStatus = this.Status;
                    this.Status = AIBase.Status.Turning;
                    //Speed.X = -Speed.X;
                }
            }

            RingSize = 0;
            //Console.WriteLine(Speed);
            base.Update(gameTime);
            Speed = speed;
            Locate = locate;
        }
        #region Overrides of Unit

        public override Rectangle GetDrawingRectangle()
        {
            return new Rectangle((int)this.Locate.X - 32, (int)this.Locate.Y - 32, 64, 64);

        }

        public override void Initialize()
        {

        }

        protected override void RadianGenerate(GameTime gameTime)
        {
            float a = Speed.Radian;
            if (Speed.Length < 10)
                a = 0;
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
            throw new NotImplementedException();
        }

        internal override void ToFleeStatus(GameTime gameTime, Player playerr)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}



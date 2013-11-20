using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Heal.Core.AI;
using Heal.Core.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Heal.Core.Entities.Enemies
{
    public class BigDaddy : EnemyTypeBoss
    {
        public List<Texture2D> LeftBody;
        public List<Texture2D> LeftDriver;

        public List<Texture2D> RightBody;
        public List<Texture2D> RightDriver;

        public List<ScenceNode> ScenceList;

        public List<ElecBall> ElecList;


        public BigDaddyStatus CurrentStatus;

        public bool BeAttack;

        public enum AttackStyle
        {
            Style1,
            Style2
        } ;

        public enum AttackStyle1
        {
            Aim,
            Charge,
            Strike,
            Judge,
            Stucked,
            Backward
        } ;

        public AttackStyle CurrentAttackStyle;
        public AttackStyle1 Attack1Status;


        public BigDaddy(object sprite, Vector2 speed, Vector2 locate, float enemySize, float enemySize2, AIBase.FaceSide face, AIBase.ID id)
            : base(sprite, speed, locate, 0, enemySize, enemySize2, face, id)
        {
            LeftBody = new List<Texture2D>();
            RightBody = new List<Texture2D>();
            LeftDriver = new List<Texture2D>();
            RightDriver = new List<Texture2D>();
            ScenceList = new List<ScenceNode>();
            ElecList = new List<ElecBall>();
            this.Name = AIBase.BossID.StrikeBoss;
            this.CurrentStatus = BigDaddyStatus.Best;
            this.Status = AIBase.Status.Alarm;
            AIBase.ScenceList = this.ScenceList;
            this.Current = 0;
            this.BeAttack = false;
        }

        public void AddNewBall(Texture2D target, Vector2 postion,Vector2 speed)
        {
            this.ElecList.Add(new ElecBall(target, postion,speed));
        }

        public void AddLeftBody(Texture2D lbbe, Texture2D lbg, Texture2D lbw, Texture2D lbb)
        {
            LeftBody.Add(lbbe);
            LeftBody.Add(lbg);
            LeftBody.Add(lbw);
            LeftBody.Add(lbb);
        }

        public void AddRightBody(Texture2D rbbe, Texture2D rbg, Texture2D rbw, Texture2D rbb)
        {
            RightBody.Add(rbbe);
            RightBody.Add(rbg);
            RightBody.Add(rbw);
            RightBody.Add(rbb);
        }

        public void AddLeftDriver(Texture2D ldbe, Texture2D ldg, Texture2D ldw, Texture2D ldb)
        {
            LeftDriver.Add(ldbe);
            LeftDriver.Add(ldg);
            LeftDriver.Add(ldw);
            LeftDriver.Add(ldb);
        }

        public void AddRightDriver(Texture2D rdbe, Texture2D rdg, Texture2D rdw, Texture2D rdb)
        {
            RightDriver.Add(rdbe);
            RightDriver.Add(rdg);
            RightDriver.Add(rdw);
            RightDriver.Add(rdb);
        }

        public void AddScence(Texture2D target, Rectangle area)
        {
            this.ScenceList.Add(new ScenceNode(target, area));
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
                    if (LastStatus == AIBase.Status.Blank)
                        LastStatus = Status;
                    Status = AIBase.Status.Turning;
                }
            }
        }

        protected override void RadianGenerate(GameTime gameTime)
        {
            float a = Speed.Radian;
            if (Speed.Length < 10)
                a = 0;
            if (Face == AIBase.FaceSide.Left)
            {
                a += MathHelper.PiOver2;
            }
            else
            {
                a -= MathHelper.PiOver2;
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

        public override void Update(GameTime gameTime)
        {
            if ((this.GetZuanTouPostion() - AIBase.Player.Locate).Length() <= 20)
            {
                AIBase.Player.Status = AIBase.Status.Freeze;
            }//钻头的判定

            if ((this.Locate - AIBase.Player.Locate).Length() < 40)
            {
                AIBase.Player.Status = AIBase.Status.Kick;
            }

            if (this.Status == AIBase.Status.Alarm)
            {
                    this.CurrentAttackStyle = AttackStyle.Style1;
                    this.Attack1Status = AttackStyle1.Charge;
                    this.Status = AIBase.Status.Attck;

            }

            switch (this.CurrentAttackStyle)
            {
                case AttackStyle.Style1:
                    this.AttackStyle1Update(gameTime);
                    break;
                case AttackStyle.Style2:
                    this.AttackStyle2Update();
                    break;
                default:
                    break;
            }

            this.Speed.Radian = ( AIBase.Player.Locate - this.Locate ).Radian();
            RadianGenerate( gameTime );
            this.Rotate = m_rotate;
            base.Update(gameTime);
        }

        public Vector2 GetZuanTouPostion()
        {
            return new Vector2((float)this.Postion.X + (float)(this.EnemySize * 160 * Math.Sin(this.Speed.Radian) / 2),
                                (float)this.Postion.Y - (float)(this.EnemySize * 160 * Math.Cos(this.Speed.Radian) / 2)
                              );
        }

        public Vector2 GetWeakPostion()
        {
            return new Vector2((float)this.Postion.X - (float)(this.EnemySize * 160 * Math.Sin(this.Speed.Radian) / 2),
                    (float)this.Postion.Y + (float)(this.EnemySize * 160 * Math.Cos(this.Speed.Radian) / 2)
                  );
        }

        public static int Duration = 60;
        public int Current;
        public void AttackStyle1Update(GameTime gameTime)
        {
            switch (this.Attack1Status)
            {
                case AttackStyle1.Charge:
                    if (this.Current <= 3 * BigDaddy.Duration)
                    {
                        this.Speed = AIBase.Player.Locate - this.Locate;
                        this.Current++;
                    }
                    else
                    {
                        this.Speed.Length = this.Speed.Length / 2 + 200;
                        this.Attack1Status = AttackStyle1.Strike;
                        this.Current = 0;
                    }
                    break;
                case AttackStyle1.Strike:
                    this.PostionGenerate(gameTime);
                    if (!AIBase.SenceManager.IntersectPixels(this.Postion, (int)(this.EnemySize + 30)))
                    {
                        this.LocateConfirm();
                    }
                    else
                    {
                        this.Attack1Status = AttackStyle1.Judge;
                    }
                    break;
                case AttackStyle1.Judge:
                    foreach (var ptr in ScenceList)
                    {
                        Console.WriteLine(this.GetZuanTouPostion());
                        if (this.GetZuanTouPostion().X > ptr.Area.X && this.GetZuanTouPostion().X < ptr.Area.X + ptr.Area.Width
                            && this.GetZuanTouPostion().Y > ptr.Area.Y && this.GetZuanTouPostion().Y < ptr.Area.Y + ptr.Area.Height
                            )
                        {
                            this.Attack1Status = AttackStyle1.Stucked;
                            ptr.Status = false;
                            break;
                        }
                        else
                        {
                            this.Attack1Status = AttackStyle1.Backward;
                            this.BeAttack = false;
                        }
                    }

                    break;
                case AttackStyle1.Stucked:

                    if (this.Current <= BigDaddy.Duration * 4)
                    {
                        this.Current++;

                        if ((this.GetWeakPostion() - AIBase.Player.Locate).Length() < 80
                            && AIBase.Player.Status == AIBase.Status.Attck
                            )
                        {
                            if (this.CurrentStatus == BigDaddyStatus.Best && !this.BeAttack)
                            {
                                this.CurrentStatus = BigDaddyStatus.Good;
                                this.BeAttack = true;
                            }
                            else if (this.CurrentStatus == BigDaddyStatus.Good && !this.BeAttack)
                            {
                                this.CurrentStatus = BigDaddyStatus.Weak;
                                this.BeAttack = true;
                            }
                            else if (this.CurrentStatus == BigDaddyStatus.Weak && !this.BeAttack)
                            {
                                this.CurrentStatus = BigDaddyStatus.Bad;
                                this.BeAttack = true;
                            }
                        }

                    }
                    else
                    {
                        this.Current = 0;
                        this.Attack1Status = AttackStyle1.Backward;
                    }

                    break;
                case AttackStyle1.Backward:
                    if (this.Current <= BigDaddy.Duration * 2)
                    {
                        this.Speed = new Vector2(512) - this.Postion;
                        this.PostionGenerate(gameTime);
                        this.LocateConfirm();
                        this.Current++;
                    }
                    else
                    {
                        this.Status = AIBase.Status.Alarm;
                        this.Current = 0;
                    }
                    break;
            }
        }

        public void AttackStyle2Update()
        {

        }

        public enum BigDaddyStatus
        {
            Best,
            Good,
            Weak,
            Bad
        } ;


        public class ScenceNode
        {
            public Texture2D Target;
            public Rectangle Area;
            public bool Status;

            public ScenceNode(Texture2D target, Rectangle area)
            {
                this.Target = target;
                this.Area = area;
                this.Status = true;
            }
        }

        public class ElecBall
        {
            public Texture2D Ball;
            public Color Color;
            public Vector2 Postion;
            public Vector2 Speed;

            public ElecBall(Texture2D Ball, Vector2 Postion,Vector2 Speed)
            {
                this.Ball = Ball;
                this.Postion = Postion;
                this.Color = new Color(1f, 1f, 1f, 0f);
                //this.Color = Color.TransparentWhite;
                this.Speed = Speed;
            }

            public void ShowBall(Vector2 postion)
            {
                this.Color = Color.AliceBlue;
            }

            public void Update(GameTime gameTime,Vector2 Speed)
            {
                //140 130 190 180 左上角
                //830 125 910 170 右上角
                //120 820 190 890 左下角
                //840 855 900 900 右下角

                if (this.Postion.X > 140 && this.Postion.X < 190
                    && this.Postion.Y > 130 && this.Postion.Y < 180)
                {
                    if(this.Speed.X == 0)
                    {
                        this.Speed.X = -this.Speed.Y;
                        this.Speed.Y = 0;
                    }
                }

                if (this.Postion.X > 830 && this.Postion.X < 910
                    && this.Postion.Y > 125 && this.Postion.Y < 170)
                {

                }

                if (this.Postion.X > 120 && this.Postion.X < 190
                    && this.Postion.Y > 820 && this.Postion.Y < 890)
                {

                }

                if (this.Postion.X > 840 && this.Postion.X < 900
                    && this.Postion.Y > 855 && this.Postion.Y < 900)
                {

                }
            }
        }

        #region Nouse
        public override Rectangle GetDrawingRectangle()
        {
            throw new NotImplementedException();
        }


        public override void Initialize()
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


        #endregion
    }
}

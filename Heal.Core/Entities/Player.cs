using System;
using System.Collections.Generic;
using Heal.Core.AI;
using Heal.Core.Entities.Enemies;
using Heal.Core.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Heal.Core.Entities
{
    /// <summary>
    /// Storage unit information for player controlled.
    /// </summary>
    public class Player : Unit
    {
        private Vector2 m_lastLocate;

        public float BeAttackedRadian;

        //冲刺时候的最大速度
        //public override float MaxSpeed { get; set; }
        public float CommonSpeed;

        public List<Texture2D> Unable;
        public List<Texture2D> Able;
        public Texture2D Default;
        public Texture2D MaskI, MaskII, MaskV;

        public Color EyeColor;
        public Texture2D EyeTexure;
        public List<PicList> HP_3;
        public List<PicList> HP_2;
        public List<PicList> HP_1;

        public List<PicList> Bubble;

        //HP大小
        public int HP;

        public static float Size = 80;

        public float StrikeRadian;

        public int FreezeTimeNow;

        public Vector2 Scale;

        public AIBase.ID FakeID;
        public bool[] HideObtain = new bool[3];

        public bool Transforming;

        public Player(object sprite, Vector2 speed, Vector2 locate, float ringSize, float enemySize, float enemySize2,
            AIBase.FaceSide face, AIBase.ID id, float maxSpeed, float accerlate, int cooldown)
            : base(sprite, speed, locate, ringSize, enemySize, enemySize2, face, id)
        {
            this.Unable = new List<Texture2D>();
            this.Able = new List<Texture2D>();
            this.HP_1 = new List<PicList>();
            this.HP_2 = new List<PicList>();
            this.HP_3 = new List<PicList>();
            FreezeTime = 30;
            MaxSpeed = maxSpeed;
            CommonSpeed = speed.X;
            StrikeRadian = 0;
            AttackIndex = 0;
            FreezeTimeNow = 0;
            HP = 3;
            this.Transforming = false;
            this.BeAttackedRadian = 0;
            Scale = new Vector2(1f, 1f);
            this.FakeID = AIBase.ID.Zero;

            for (int i = 0; i < 3; i++)
            {
                HideObtain[i] = true;
            }
        }

        public void AddBubble(Texture2D bubble)
        {
            //this.Bubble.Add(new PicList(bubble, 4, 80, (int)this.EnemySize, Microsoft.Xna.Framework.Graphics.Color.AliceBlue, 100));
        }

        public void AddEye(Texture2D eye)
        {
            this.EyeColor = Microsoft.Xna.Framework.Graphics.Color.AliceBlue;
            this.EyeTexure = eye;
        }

        public void AddHp(Texture2D hp_3, Texture2D hp_2, Texture2D hp_1)
        {
            this.HP_1.Add(new PicList(hp_1, 2, 80, (int)this.EnemySize,
                                      Microsoft.Xna.Framework.Graphics.Color.AliceBlue, 100));
            this.HP_2.Add(new PicList(hp_2, 2, 80, (int)this.EnemySize,
                          Microsoft.Xna.Framework.Graphics.Color.AliceBlue, 300));
            this.HP_3.Add(new PicList(hp_3, 2, 80, (int)this.EnemySize,
                          Microsoft.Xna.Framework.Graphics.Color.AliceBlue, 500));
        }

        public void AddToUnable(Texture2D I, Texture2D II, Texture2D V)
        {
            this.Unable.Add(I);
            this.Unable.Add(II);
            this.Unable.Add(V);
        }

        public void AddToAble(Texture2D I, Texture2D II, Texture2D V)
        {
            this.Able.Add(I);
            this.Able.Add(II);
            this.Able.Add(V);
        }

        public void AddDefault(Texture2D d)
        {
            this.Default = d;
        }

        public void AddMask(Texture2D I, Texture2D II, Texture2D V)
        {
            this.MaskI = I;
            this.MaskII = II;
            this.MaskV = V;
        }

        internal override void ToTurnStatus()
        {
            if (this.Status != AIBase.Status.Attck && this.Status != AIBase.Status.Flat)
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
            if (this.HP == 0)
            {
                this.Status = AIBase.Status.Dead;
            }
        }

    //    public bool CollsionDetection()
   //     {
    //        foreach (var ptr in AIBase.ScenceList)
    //        {
   //             if (AIBase.Player.Postion.X > ptr.Area.X && AIBase.Player.Postion.X < ptr.Area.X + ptr.Area.Width
     //               && AIBase.Player.Postion.Y > ptr.Area.Y && AIBase.Player.Postion.Y < ptr.Area.Y + ptr.Area.Height
    //                && ptr.Status)
   //             {
  //                  return true;
   //             }
   //         }
   //         return false;
 //       }

        public override void Update(GameTime gameTime)
        {
            Speed inputSpeed = Input.Speed;
            if (this.Scale.Y <= 1)
                this.Scale.Y += 0.01f;
            else
                this.Scale.Y = 1;

            if (this.Scale.X <= 1)
                this.Scale.X += 0.01f;
            else
                this.Scale.X = 1;

            if (Input.IsTransformKeyDown() || this.Status == AIBase.Status.Freeze)
            {
                inputSpeed = new Speed(0, 0);
            }

            switch (Status)
            {
                case AIBase.Status.Turning:
                case AIBase.Status.Blank:
                case AIBase.Status.Normal:
                    {
                        if (inputSpeed.Radian == inputSpeed.Radian && inputSpeed.Length > 0.2)
                        {
                            this.Speed.Radian = inputSpeed.Radian;
                            this.Speed.Length = inputSpeed.Length * this.MaxSpeed;
                        }
                        else
                        {
                            Speed.Length = 0;
                        }
                        break;
                    }
                case AIBase.Status.Attck:
                    {
                        //Speed.
                        break;
                    }
                case AIBase.Status.Strike:

                    if ((!Input.IsSpeedupKeyDown()) || RingSize <= 105)
                    {
                        Status = AIBase.Status.Normal;
                    }
                    else if (inputSpeed.Radian == inputSpeed.Radian && inputSpeed.Length > 0.2)
                    {
                        if (Math.Abs(inputSpeed.Radian - this.Speed.Radian) > 0.7 * MathHelper.Pi)
                        {
                            Status = AIBase.Status.Normal;
                            Speed.Radian = Input.Speed.Radian;
                        }
                        else
                        {
                            Speed.Radian = Speed.Radian - (float)gameTime.ElapsedGameTime.TotalSeconds * (Speed.Radian - Input.Speed.Radian) * 10;
                            if (Speed.Length < 180)
                                Status = AIBase.Status.Normal;
                            else
                                this.RingSize -= (float)gameTime.ElapsedGameTime.TotalSeconds * 60.0f;
                        }
                    }
                    else
                    {
                        Speed.Length = 0;
                        Status = AIBase.Status.Normal;
                    }
                    //Speed.Length -= 80f * (float) gameTime.ElapsedGameTime.TotalSeconds;
                    break;
                    ;
            }
            this.Postion = AIBase.SenceManager.MoveTest(this.Locate, Speed.Radian, Speed.Length,
                                   (float)gameTime.ElapsedGameTime.TotalSeconds);
            RadianGenerate(gameTime);
    //        if(!this.CollsionDetection())
            LocateConfirm();
            //Console.WriteLine(RingSize);

            //Speed = Utilities.Utilities.GetVector(this.CommonSpeed,Input.Radian);

            if (Input.IsTransformKeyDown())
            {
                this.Transforming = true;

                if (Input.IsUpKeyDown() && this.HideObtain[0])
                {
                    this.FakeID = AIBase.ID.II; //刺头 
                }
                else if (Input.IsLeftKeyDown() && this.HideObtain[1])
                {
                    this.FakeID = AIBase.ID.I; //合体怪
                }
                else if (Input.IsRightKeyDown() && this.HideObtain[2])
                {
                    this.FakeID = AIBase.ID.V; //幽灵
                }
                else if (Input.IsDownKeyDown())
                {
                    this.FakeID = AIBase.ID.Zero;
                }
            }
            else
            {
                this.Transforming = false;
            }


            if (Input.IsSpeedupKeyDown() && !Input.IsTransformKeyDown())
            {
                switch (Status)
                {
                    case AIBase.Status.Normal:
                    case AIBase.Status.Blank:
                    case AIBase.Status.Abandon:
                        if (RingSize > 150)
                        {
                            //RingSize -= 10;
                            Status = AIBase.Status.Strike;
                            this.Speed.Length = 400f;
                        }
                        break;
                }
            }

            if (Input.IsLeftKeyDown() && !Input.IsTransformKeyDown())
            {
                if (this.Face == AIBase.FaceSide.Right && Status == AIBase.Status.Normal)
                {
                    this.LastStatus = this.Status;
                    this.Status = AIBase.Status.Turning;
                    //Speed.X = -Speed.X;
                }
            }

            if (Input.IsRightKeyDown() && !Input.IsTransformKeyDown())
            {
                if (this.Face == AIBase.FaceSide.Left && Status == AIBase.Status.Normal)
                {
                    this.LastStatus = this.Status;
                    this.Status = AIBase.Status.Turning;
                    //Speed.X = -Speed.X;
                }
            }

            if (Input.IsAttackKeyDown() && !Input.IsTransformKeyDown())
            {
                //只有在RingSize大于100的时候才能够冲刺
                if (this.RingSize > 100 && (this.Status == AIBase.Status.Normal || this.Status == AIBase.Status.Blank))
                {
                    this.LastStatus = this.Status;
                    this.Status = AIBase.Status.Attck;
                    Speed.Length = 0;
                }
            }

            //运动中环逐渐减小 每秒减小6
            if (RingSize > 200)
            {
                this.RingSize -= (float)gameTime.ElapsedGameTime.TotalSeconds *6;
            }
            if (RingSize < 200)
            {
                this.RingSize += (float)gameTime.ElapsedGameTime.TotalSeconds * 6;
            }
            if (this.Status == AIBase.Status.Strike)
            {
                this.CurrentDialog = AIBase.StatusDialog.Strike;
            }
            else if (this.Status == AIBase.Status.Attck)
            {
                this.CurrentDialog = AIBase.StatusDialog.Kakaka;
            }
            else if (this.Status == AIBase.Status.Freeze)
            {
                this.CurrentDialog = AIBase.StatusDialog.Ahhh;
            }
            else
            {
                this.CurrentDialog = AIBase.StatusDialog.Blank;
            }

            this.EyeColor.A = (byte)(255f * (0.5f * (float)Math.Sin(gameTime.TotalGameTime.TotalSeconds) + 0.5f));

            //Console.WriteLine(Speed);
            if (this.HP == 3)
                this.HP_3[0].Update(gameTime);
            if (this.HP == 2)
                this.HP_2[0].Update(gameTime);
            if (this.HP == 1)
                this.HP_1[0].Update(gameTime);

            base.Update(gameTime);
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



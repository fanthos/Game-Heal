using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Heal.Core.AI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Heal.Core.Entities.Enemies
{
    public class Ghost : Enemy
    {
        public List<Texture2D> EyeListLeft;
        public List<Texture2D> BodyListLeft;
        public List<Texture2D> LightListLeft;

        public List<Texture2D> EyeListRight;
        public List<Texture2D> BodyListRight;
        public List<Texture2D> LightListRight;

        public List<List<Texture2D>> Left;
        public List<List<Texture2D>> Right;

        public List<List<Texture2D>> CurrentList;

        public int CurrentStatus;
        public AIBase.GhostStatus GhostStatus;//放在Turn Chase Flee
        
        public static float Range = 40;
        public static float AlartRange = 300;
        public static int FlashTime = 60;
        public int CurrentFlashTime;
        public Vector2 DefaultSpeed;

        public static float FleeRange = 120;
        public float CurrentFlee;

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

        public Ghost(object sprite, Vector2 speed, Vector2 locate, float ringSize, float enemySize,float enemySize2, AIBase.FaceSide face, AIBase.ID id)
            : base(sprite, speed, locate, ringSize, enemySize,enemySize2, face, id)
        {
            this.EyeListLeft = new List<Texture2D>();
            this.BodyListLeft = new List<Texture2D>();
            this.LightListLeft = new List<Texture2D>();
            this.Left = new List<List<Texture2D>>();

            this.EyeListRight = new List<Texture2D>();
            this.BodyListRight = new List<Texture2D>();
            this.LightListRight = new List<Texture2D>();
            this.Right = new List<List<Texture2D>>();

            this.CurrentList = new List<List<Texture2D>>();
            this.CurrentStatus = 1;
            this.CurrentFlashTime = 0;
            this.DefaultSpeed = speed;

            this.CurrentFlee = 0;
            this.StepCount = 0;
            this.m_swingRange = 120;
        }

        public void AddLeft(Texture2D a,Texture2D b,Texture2D c,
                            Texture2D d,Texture2D e,Texture2D f,
                            Texture2D g,Texture2D h,Texture2D i)
        {
            this.BodyListLeft.Add(a);
            this.BodyListLeft.Add(b);
            this.BodyListLeft.Add(c);
            this.Left.Add(this.BodyListLeft);
            
            this.EyeListLeft.Add(d);
            this.EyeListLeft.Add(e);
            this.EyeListLeft.Add(f);
            this.Left.Add(this.EyeListLeft);

            this.LightListLeft.Add(g);
            this.LightListLeft.Add(h);
            this.LightListLeft.Add(i);
            this.Left.Add(this.LightListLeft);
        
        }


        public void AddRight(Texture2D a, Texture2D b, Texture2D c,
                                    Texture2D d, Texture2D e, Texture2D f,
                                    Texture2D g, Texture2D h, Texture2D i)
        {
            this.BodyListRight.Add(a);
            this.BodyListRight.Add(b);
            this.BodyListRight.Add(c);
            this.Right.Add(this.BodyListRight);

            this.EyeListRight.Add(d);
            this.EyeListRight.Add(e);
            this.EyeListRight.Add(f);
            this.Right.Add(this.EyeListRight);

            this.LightListRight.Add(g);
            this.LightListRight.Add(h);
            this.LightListRight.Add(i);
            this.Right.Add(this.LightListRight);
        }

        public override void Update(GameTime gameTime)
        {
            if (this.Status == AIBase.Status.Attck)
                this.CurrentStatus = 2;
            else if (this.Status == AIBase.Status.Flee)
                this.CurrentStatus = 0;
            else
                this.CurrentStatus = 1;

            if (this.Face == AIBase.FaceSide.Right)
            {
                this.CurrentList = this.Right;
            }
            else
            {
                this.CurrentList = this.Left;
            }
        }

        public override Rectangle GetDrawingRectangle()
        {
            return new Rectangle(0, 0, 0, 0);
        }

        public override void Initialize()
        {
            throw new NotImplementedException();
        }

        internal override void ToTurnStatus()
        {//实际上在消失的同时缓缓消失 等到转身完毕再缓缓出现
            if (Face == AIBase.FaceSide.Left && Speed.X > 0)
            {
                if (LastStatus == AIBase.Status.Blank)
                    LastStatus = Status;
                Status = AIBase.Status.Turning;
                this.GhostStatus = AIBase.GhostStatus.Disappear;
            }
            else
            {
                if (Face == AIBase.FaceSide.Right && Speed.X < 0)
                {
                    if (LastStatus == AIBase.Status.Blank)
                        LastStatus = Status;
                    Status = AIBase.Status.Turning;
                    this.GhostStatus = AIBase.GhostStatus.Disappear;
                }
            }
        }

        internal override void ToChaseStatus(GameTime gameTime, Player player)
        {
            throw new NotImplementedException();
        }

        internal override void ToFleeStatus(GameTime gameTime, Player playerr)
        {
            throw new NotImplementedException();
        }

        internal override void ToFreezeStatus(GameTime gameTime, Player player)
        {
        }

        internal override void ToAttackStatus(GameTime gameTime, Player ptr)
        {
            //在攻击范围之内
            if ((this.Locate - ptr.Locate).Length() <= 40
                && this.RingSize >= AIBase.Player.RingSize
                && this.Status != AIBase.Status.Dead
                && this.Status != AIBase.Status.Turning)
            {
                this.LastStatus = this.Status;

                this.CurrentDialog = AIBase.StatusDialog.Attack;
                this.Status = AIBase.Status.Attck;
            }
        }

        internal override void ToDeadStatus(GameTime gameTime, Player ptr)
        {
            if ((this.Status == AIBase.Status.Flee || !Enemy.Detected((Enemy)this))
                && (AIBase.Player.Locate - this.Locate).Length() <= 20
                && ptr.Status == AIBase.Status.Attck)
            {
                this.Status = AIBase.Status.Dead;
            }
        }
    }
}

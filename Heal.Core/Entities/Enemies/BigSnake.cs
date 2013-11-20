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
    public class BigSnake : EnemyTypeBoss
    {
        public List<Texture2D> Head;
        public List<Texture2D> HeadHP;

        public HeadStatus CurrentStatus;

        public Texture2D HeadElec;

        public Texture2D Smoke;

        public SnakeStatus CurrentSnakeStatus;

        public List<BodyNode> BodyNodeList;

        public static int ChargeTime = 120;
        public int CurrentCharge;

        public bool AlreadyAttacked;

        public static new int FreezeTime = 120;
        public int CurrentFreeze;

        private int[] SnakeAttackRange = new int[]{1,2,4,16,25,30,18,10};

        public BigSnake(object sprite, Vector2 speed, Vector2 locate, float enemySize, float enemySize2, AIBase.ID id)
            : base(sprite, speed, locate, 0, enemySize, enemySize2, 0, id)
        {
            CurrentStatus = HeadStatus.Good;
            Head = new List<Texture2D>();
            HeadHP = new List<Texture2D>();
            BodyNodeList = new List<BodyNode>();
            this.Name = AIBase.BossID.SnakeBoss;
            this.CurrentSnakeStatus = SnakeStatus.Chase;
            this.CurrentCharge = 0;
            this.CurrentFreeze = 0;
        }

        public void AddHeadTexture(Texture2D good, Texture2D weak, Texture2D bad)
        {
            Head.Add(good);
            Head.Add(weak);
            Head.Add(bad);
        }

        public void AddHpTexture(Texture2D good, Texture2D weak, Texture2D bad)
        {
            HeadHP.Add(good);
            HeadHP.Add(weak);
            HeadHP.Add(bad);
        }

        public void AddOther(Texture2D smoke, Texture2D headElec)
        {
            this.Smoke = smoke;
            this.HeadElec = headElec;
        }

        public void AddNode(BodyNode bodyNode)
        {
            this.BodyNodeList.Add(bodyNode);
        }

        public override void Update(GameTime gameTime)
        {
            switch (CurrentSnakeStatus)
            {
                case SnakeStatus.Dead:
                    if (this.Color.A != 0)
                        this.Color.A -= 15;
                    else
                    {
                        GameCommands.Enqueue( "clear" );
                        AIBase.EnemyList.Remove( this );
                    }
                    break;

                case SnakeStatus.Notail:
                    
                    this.Speed = AIBase.Player.Locate - this.Locate;
                    this.Speed.Length = this.Speed.Length/3 + 320;
                    this.PostionGenerate(gameTime);
                    this.LocateConfirm();

                    if ((this.Locate - AIBase.Player.Locate).Length() < 180)
                    {
                        this.CurrentSnakeStatus = SnakeStatus.NotailCharge;
                    }
                    break;

                case SnakeStatus.NotailCharge:
                    if(this.CurrentCharge <= BigSnake.FreezeTime)
                    {
                        this.Speed = AIBase.Player.Locate - this.Locate;
                        this.Speed.Length = this.Speed.Length/2;
                        this.PostionGenerate(gameTime);
                        this.LocateConfirm();
                        this.CurrentCharge++;
                    }else
                    {
                        this.CurrentFreeze = 0;
                        this.CurrentSnakeStatus = SnakeStatus.NotailAttack;
                    }
                    break;
                case SnakeStatus.NotailAttack:

                    if (this.AttackIndex < SnakeAttackRange.Length)
                    {
                        this.Postion += CoreUtilities.GetVector(this.SnakeAttackRange[this.AttackIndex], this.Speed.Radian);
                        this.AttackIndex++;
                        if (!AIBase.SenceManager.IntersectPixels(this.Postion, 40))
                            this.LocateConfirm();
                    }
                    else
                    {
                        this.AttackIndex = 0;
                        this.CurrentSnakeStatus = SnakeStatus.NotailFreeze;
                        AlreadyAttacked = true ;
                    }

                    if ((this.Locate - AIBase.Player.Locate).Length() <= 80)
                    {
                        if (AIBase.Player.Status != AIBase.Status.Freeze)
                        {
                            AIBase.Player.Status = AIBase.Status.Freeze;
                            AIBase.Player.BeAttackedRadian = this.Rotation;
                        }
                    }

                    break;
                case SnakeStatus.NotailFreeze:

                    if ((this.Postion - AIBase.Player.Postion).Length() < 80
                        && AIBase.Player.Status == AIBase.Status.Attck)
                    {
                        if (AlreadyAttacked)
                        {
                            if (this.CurrentStatus == HeadStatus.Good)
                            {
                                this.CurrentStatus = HeadStatus.Weak;
                                AlreadyAttacked = false;
                            }
                            else if (this.CurrentStatus == HeadStatus.Weak)
                            {
                                this.CurrentStatus = HeadStatus.Bad;
                                AlreadyAttacked = false;
                            }
                            else if (this.CurrentStatus == HeadStatus.Bad)
                            {
                                this.CurrentSnakeStatus = SnakeStatus.Dead;
                                AlreadyAttacked = false;
                            }
                        }
                       
                    }

                    if (this.CurrentFreeze < 2 * BigSnake.FreezeTime)
                    {
                        this.CurrentFreeze++;
                    }
                    else
                    {
                        this.CurrentFreeze = 0;
                        if (this.CurrentSnakeStatus != SnakeStatus.Dead)
                            this.CurrentSnakeStatus = SnakeStatus.Notail;
                    }
                    break;

                case SnakeStatus.AttackII:
                    if (this.AttackIndex < this.SnakeAttackRange.Length - 1)
                    {
                        this.AttackIndex++;
                        this.Postion += CoreUtilities.GetVector(this.SnakeAttackRange[this.AttackIndex], this.Speed.Radian);
                        this.LocateConfirm();

                        if ((this.Locate - AIBase.Player.Locate).Length() <= 50)
                        {
                            AIBase.Player.Status = AIBase.Status.Freeze;
                            AIBase.Player.BeAttackedRadian = this.Rotation;
                        }
                    }
                    else
                    {
                        this.AttackIndex = 0;
                        this.CurrentSnakeStatus = SnakeStatus.Freeze;
                    }
                    break;
                case SnakeStatus.Chase:
                    this.Speed = AIBase.Player.Locate - this.Locate;
                    this.Speed.Length = this.Speed.Length / 3 + 320;

                    this.Postion = AIBase.SenceManager.MoveTest(this.Locate, this.Speed,
                                                    (float)gameTime.ElapsedGameTime.TotalSeconds);
                    this.LocateConfirm();

                    this.m_rotate = Speed.Radian;


                    if ((this.Locate - AIBase.Player.Locate).Length() >= 260)
                    {
                        this.CurrentSnakeStatus = SnakeStatus.Charge;
                    }

                    if ((this.Locate - AIBase.Player.Locate).Length() <= 180)
                    {
                        this.CurrentSnakeStatus = SnakeStatus.AttackII;
                    }

                    break;

                case SnakeStatus.Charge:
                    if (this.CurrentCharge <= BigSnake.ChargeTime)
                    {
                        this.CurrentCharge++;

                        this.Speed = AIBase.Player.Locate - this.Locate;
                        this.m_rotate = Speed.Radian;
                        this.Rotate = m_rotate;
                    }
                    else
                    {
                        this.CurrentCharge = 0;
                        this.CurrentSnakeStatus = SnakeStatus.Attack;
                        this.Speed = AIBase.Player.Locate - this.Locate;
                        this.Speed.Length = this.Speed.Length / 3 + 320;
                    }

                    break;

                case SnakeStatus.Attack:

                    this.PostionGenerate(gameTime);
                    if (!AIBase.SenceManager.IntersectPixels(this.Postion, (int)(this.EnemySize * 100 / 2)))
                    {
                        this.LocateConfirm();
                    }
                    else
                    {
                        this.CurrentSnakeStatus = SnakeStatus.Freeze;
                    }

                    if ((this.Locate - AIBase.Player.Locate).Length() < 80)
                    {
                        AIBase.Player.Status = AIBase.Status.Freeze;
                        AIBase.Player.BeAttackedRadian = this.Rotation;
                    }

                    break;

                case SnakeStatus.Freeze:
                    if (this.CurrentFreeze <= BigSnake.FreezeTime)
                    {
                        this.CurrentFreeze++;
                    }
                    else
                    {
                        this.CurrentFreeze = 0;
                        this.CurrentSnakeStatus = SnakeStatus.Chase;
                    }
                    break;
            }

            foreach (var ptr in BodyNodeList)
            {
                ptr.Update(gameTime, this.Locate, this.BodyNodeList, this);
                ptr.RightGlass.GeneratePostion(ptr.Postion, ptr.Rotation);
                ptr.LeftGlass.GeneratePostion(ptr.Postion, ptr.Rotation);

                if (ptr.NodeStatus != NodeStatus.Bad)
                {
                    ptr.LeftGlass.Update(gameTime);
                    ptr.RightGlass.Update(gameTime);
                }
            }
        }

        protected override void RadianGenerate(GameTime gameTime)
        {
            this.m_rotate = Speed.Radian;
        }

        public override float Rotation
        {
            get
            {
                return this.Rotate + MathHelper.Pi;
            }
        }

        public class BodyNode
        {
            public static float NodeSize = 150f;

            public Texture2D Target;

            public RightGlass RightGlass;
            public LeftGlass LeftGlass;

            public Vector2 Postion;
            public Color Color;

            public NodeStatus NodeStatus;
            public Speed Speed;

            public float Rotation;

            public bool Next;

            public int Index;

            public BodyNode(Vector2 postion)
            {
                this.Postion = postion;
                RightGlass = new RightGlass(postion);
                LeftGlass = new LeftGlass(postion);
                this.NodeStatus = NodeStatus.Good;
                Next = true;
                this.Color = Color.AliceBlue;
                this.Rotation = MathHelper.PiOver2;
                this.Speed = new Vector2(0);
            }

            public void AddTexture(Texture2D target)
            {
                this.Target = target;
            }

            public void Update(GameTime gameTime, Vector2 postion, List<BodyNode> list, BigSnake snake)
            {
                if (RightGlass.CurrentStatus == GlassStatus.Bad
                    && LeftGlass.CurrentStatus == GlassStatus.Bad
                    && this.NodeStatus == NodeStatus.Good)
                {
                    this.NodeStatus = NodeStatus.Judge;
                }

                this.RightGlass.Update(gameTime);
                this.LeftGlass.Update(gameTime);
                switch (this.NodeStatus)
                {
                    case NodeStatus.Good:

                        if (list.IndexOf(this) == 0)
                        {
                            this.Speed = postion - this.Postion;
                        }
                        else
                        {
                            this.Speed = list[list.IndexOf(this) - 1].Postion - this.Postion;
                        }

                        this.Speed.Length = this.Speed.Length / 3 + 320;

                        if (list.IndexOf(this) == 0)
                        {
                            if ((this.Postion - postion).Length() > 120)
                            {
                                this.Postion = AIBase.SenceManager.MoveTest(this.Postion, this.Speed,
                                                                            (float)gameTime.ElapsedGameTime.TotalSeconds);
                            }
                            this.Rotation = Speed.Radian + MathHelper.PiOver2;

                        }
                        else
                        {
                            if ((this.Postion - list[list.IndexOf(this) - 1].Postion).Length() >120)
                            {
                                this.Postion = AIBase.SenceManager.MoveTest(this.Postion, this.Speed,
                                                                            (float)gameTime.ElapsedGameTime.TotalSeconds);
                            }
                            this.Rotation = Speed.Radian + MathHelper.PiOver2;
                        }
                        break;

                    case NodeStatus.Judge:
                        if (!this.Next)
                        {
                            this.NodeStatus = NodeStatus.Bad;
                            if (list.IndexOf(this) != 0)
                            {
                                list[list.IndexOf(this) - 1].Next = false;
                            }
                            else
                            {
                                snake.CurrentSnakeStatus = SnakeStatus.Notail;
                            }
                        }
                        else
                        {
                            this.NodeStatus = NodeStatus.Freeze;
                        }
                        break;

                    case NodeStatus.Bad:
                        if (this.Color.A != 0)
                        {
                            this.Color.A -= 15;
                        }
                        else
                        {
                            this.Color.A = 0;

                        }
                        break;

                    case NodeStatus.Freeze:
                        this.Color = Color.Gray;


                        if (list.IndexOf(this) == 0)
                        {
                            this.Speed = postion - this.Postion;
                        }
                        else
                        {
                            this.Speed = list[list.IndexOf(this) - 1].Postion - this.Postion;
                        }
                        this.Speed.Length = this.Speed.Length/2 + 320;

                        if (list.IndexOf(this) == 0)
                        {
                            if ((this.Postion - postion).Length() > 120)
                            {
                                this.Postion = AIBase.SenceManager.MoveTest(this.Postion, this.Speed,
                                                                            (float)gameTime.ElapsedGameTime.TotalSeconds);
                            }
                            this.Rotation = Speed.Radian + MathHelper.PiOver2;

                        }
                        else
                        {
                            if ((this.Postion - list[list.IndexOf(this) - 1].Postion).Length() > 120)
                            {
                                this.Postion = AIBase.SenceManager.MoveTest(this.Postion, this.Speed,
                                                                            (float)gameTime.ElapsedGameTime.TotalSeconds);
                            }
                            this.Rotation = Speed.Radian + MathHelper.PiOver2;
                        }


                        this.Rotation = Speed.Radian + MathHelper.PiOver2;

                        if (!this.Next)
                        {
                            this.NodeStatus = NodeStatus.Bad;
                            if (list.IndexOf(this) != 0)
                            {
                                list[list.IndexOf(this) - 1].Next = false;
                            }
                            else
                            {
                                snake.CurrentSnakeStatus = SnakeStatus.Notail;
                            }
                        }
                        break;
                }
            }

        }

        public class RightGlass
        {
            public List<Texture2D> Target;
            public GlassStatus CurrentStatus;
            public Vector2 Postion;


            public RightGlass(Vector2 postion)
            {
                this.Postion = postion;
                this.Target = new List<Texture2D>();
                this.CurrentStatus = GlassStatus.Good;
            }

            public void AddTexture(Texture2D good, Texture2D bad)
            {
                this.Target.Add(good);
                this.Target.Add(bad);
            }

            public void BeBroke()
            {
                this.CurrentStatus = GlassStatus.Bad;
            }

            public void BeGood()
            {
                this.CurrentStatus = GlassStatus.Good;
            }

            public void GeneratePostion(Vector2 center, float radian)
            {
                this.Postion.X = (float)(center.X + (BodyNode.NodeSize - 75) * Math.Sin(radian) / 2);
                this.Postion.Y = (float)(center.Y - (BodyNode.NodeSize - 75) * Math.Cos(radian) / 2);
            }

            public void Update(GameTime gameTime)
            {
                if ((this.Postion - AIBase.Player.Locate).Length() <= 40
                    && AIBase.Player.Status == AIBase.Status.Attck)
                {
                    this.CurrentStatus = GlassStatus.Bad;
                }
            }
        }

        public class LeftGlass
        {
            public List<Texture2D> Target;
            public GlassStatus CurrentStatus;
            public Vector2 Postion;


            public LeftGlass(Vector2 postion)
            {
                this.Postion = postion;
                this.Target = new List<Texture2D>();
                this.CurrentStatus = GlassStatus.Good;
            }

            public void AddTexture(Texture2D good, Texture2D bad)
            {
                this.Target.Add(good);
                this.Target.Add(bad);
            }

            public void BeBroke()
            {
                this.CurrentStatus = GlassStatus.Bad;
            }

            public void BeGood()
            {
                this.CurrentStatus = GlassStatus.Good;
            }

            public void GeneratePostion(Vector2 Center, float Radian)
            {
                this.Postion.X = (float)(Center.X - (BodyNode.NodeSize - 75) * Math.Sin(Radian) / 2);
                this.Postion.Y = (float)(Center.Y + (BodyNode.NodeSize - 75) * Math.Cos(Radian) / 2);
            }

            public void Update(GameTime gameTime)
            {
                if ((this.Postion - AIBase.Player.Locate).Length() <= 40
                    && AIBase.Player.Status == AIBase.Status.Attck)
                {
                    this.CurrentStatus = GlassStatus.Bad;
                }
            }
        }

        public enum GlassStatus
        {
            Good,
            Bad
        } ;

        public enum NodeStatus
        {
            Good,
            Judge,
            Bad,
            Freeze
        } ;

        public enum HeadStatus
        {
            Good,
            Weak,
            Bad,
        } ;

        public enum SnakeStatus
        {
            Chase,
            Charge,
            Attack,
            AttackII,
            Freeze,
            Notail,
            NotailFreeze,
            NotailAttack,
            NotailCharge,
            Dead
        } ;
        #region NoUse

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

        internal override void ToTurnStatus()
        {
        }

        #endregion
    }
}

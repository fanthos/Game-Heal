using System;
using System.Linq;
using Heal.Core.AI;
using Heal.Core.Entities.Enemies;
using Heal.Core.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Heal.Core.Entities
{
    /// <summary>
    /// Information for entity can fight or do other action.
    /// </summary>
    public abstract class Unit : Entity
    {
        //敌人的ID
        public readonly AIBase.ID Id;

        //保存敌人图片序列的表
        internal readonly List<PicList> ListLeft;

        internal readonly List<PicList> ListRight;

        internal readonly List<PicList> ListTurnLeft;

        internal readonly List<PicList> ListTurnRight;

        public readonly List<PicList> ListRing;

        public int CurrentRing;

        public AIBase.StatusDialog CurrentDialog;

        //当前正在输出的表
        public List<PicList> List;

        //敌人单位的运行速度
        public Speed Speed;

        //决定HPRing的大小
        public float RingSize;

        //决定敌人单体的大小
        public float EnemySize;


        //敌人单位的当前位置 按照此位置画 左上角位置
        private Vector2 m_locate;
        public override Vector2 Locate
        {
            get { return m_locate; }
            set { m_locate = value; }
        }

        //开始位置
        public readonly Vector2 StartPoint;

        //在Locate和Speed的基础上会到达的位置 左上角位置
        public Vector2 Postion;

        //中心位置
        public readonly Vector2 Center;

        //颜色
        public Color Color = Color.AliceBlue;

        //敌人的朝向
        public AIBase.FaceSide Face;

        //旋转角度
        public float Rotate;
        protected float m_rotate = 0;

        //眩晕持续时间 3秒
        internal static int FreezeTime = 2*60;
        internal float CoolDownTime;

        //击飞距离
        internal static int FlyDistance = 100;
        internal int CurrentFly;

        //攻击序列
        internal float[] AttackRange = { 3f, 5f, 7f, 8f, 9f, 10f, -3f, -8f, -10f, -10f , -2f };
        internal int AttackIndex;

        //敌人当前的状态
        public  AIBase.Status Status;

        //上一阶段的状态
        internal AIBase.Status LastStatus;

        public float EnemySize2;


        public Color StatusColor;
        public Vector2 StatusPostion;

        //初始化方法
        public Unit(object sprite, Vector2 speed, Vector2 locate, float ringSize, float enemySize,float enemySize2, AIBase.FaceSide face, AIBase.ID id)
            : base(sprite)
        {
            //四个输入参数
            Speed = speed;
            m_locate = locate;
            Face = face;
            Id = id;
            RingSize = ringSize;
            EnemySize = enemySize;
            CoolDownTime = 0;
            CurrentFly = 0;
            EnemySize2 = enemySize;
            CurrentDialog = AIBase.StatusDialog.Blank;

            StatusColor = Color.AliceBlue;
            StatusPostion = new Vector2(0);

            //放在开始位置
            StartPoint = locate;
            //默认在Normal状态
            Status = AIBase.Status.Normal;
            LastStatus = AIBase.Status.Blank;
            //默认不旋转
            Rotate = 0f;
            this.CurrentRing = 0;
            //表初始化
            ListLeft = new List<PicList>();
            ListRight = new List<PicList>();
            ListTurnLeft = new List<PicList>();
            ListTurnRight = new List<PicList>();
            List = new List<PicList>();
            ListRing = new List<PicList>();
            //中心位置
            Center = new Vector2(enemySize / 2, enemySize / 2);
        }

        //生成位置 在此帧获知下一帧的位置
        public void PostionGenerate(GameTime gameTime)
        {
            Postion.X = m_locate.X + Speed.X * (float)gameTime.ElapsedGameTime.TotalSeconds;
            Postion.Y = m_locate.Y + Speed.Y * (float)gameTime.ElapsedGameTime.TotalSeconds;
            RadianGenerate(gameTime);
        }

        protected virtual void RadianGenerate(GameTime gameTime)
        {

        }

        //位置确认 将下一帧的位置确认
        public void LocateConfirm()
        {
            m_locate = Postion;
            Rotate = m_rotate;
            if (this is Player)
            {

            }
        }

        internal abstract void ToChaseStatus(GameTime gameTime,Player player);
        internal abstract void ToFleeStatus(GameTime gameTime,Player playerr);
        internal abstract void ToFreezeStatus(GameTime gameTime,Player player);
        internal abstract void ToAttackStatus(GameTime gameTime, Player ptr);
        internal abstract void ToDeadStatus(GameTime gameTime, Player ptr);

        internal void ToNormalStatus(GameTime gameTime, Player ptr)
        {
            if ((this.Locate - AIBase.Player.Locate).Length() > 400)
            {
                this.CurrentDialog = AIBase.StatusDialog.Blank;
                this.Status = AIBase.Status.Normal;
            }
        }

        internal abstract void ToTurnStatus();

        //向图片序列表中添加一个节点 先添加的先画
        public void AddNew(Texture2D target, int picCount, int size, int oSize, float time, AIBase.List which)
        {
            switch (which)
            {
                case AIBase.List.Left:
                    ListLeft.Add(new PicList(target, picCount, size, oSize, this.Color, time));
                    break;
                case AIBase.List.Right:
                    ListRight.Add(new PicList(target, picCount, size, oSize, this.Color, time));
                    break;
                case AIBase.List.TurnLeft:
                    ListTurnLeft.Add(new PicList(target, picCount, size, oSize, this.Color, time));
                    break;
                case AIBase.List.TurnRight:
                    ListTurnRight.Add(new PicList(target, picCount, size, oSize, this.Color, time));
                    break;
                case AIBase.List.Ring:
                    ListRing.Add(new PicList(target, picCount, size, oSize, this.Color, time));
                    break;
                default:
                    break;
            }

        }

        //图片序列表的节点 作为内部类
        public class PicList
        {
            //图片序列本身
            //当前图片大小
            private readonly int m_size;
            //图片序列包含的图片的个数 每张小图片 m_size*m_size
            //输出到了第几张图片
            //输出的框
            private Rectangle m_frame;
            //输出增量
            private readonly int m_deltaX;
            //输出时候的大小

            private float m_timer;
            private float m_flash;

            public readonly Texture2D Target;

            public int Size;

            public int CountNow;

            public readonly int PicCount;
            public float Scale = 1;
            //输出的颜色
            public Color Color = Color.White;

            public PicList(Texture2D target, int picCount, int size, int oSize, Color color, float time)
            {
                Target = target;
                PicCount = picCount;
                m_size = size;

                m_flash = time / 1000;

                //输出图片从0开始计算
                CountNow = 0;
                //根据m_size决定框大小
                m_frame = new Rectangle(0, 0, m_size, m_size);
                //根据m_size决定增量大小
                m_deltaX = m_size;
                //输出时候的大小 除了环以外oSize值一般等于size值
                Size = oSize;
                Color = color;
            }

            public void Update(GameTime gameTime)
            {
                if (m_timer < m_flash)
                {
                    m_timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
                    return;
                }
                m_timer -= m_flash;
                if (CountNow < PicCount)
                {
                    m_frame.X = CountNow * m_deltaX;
                    ++CountNow;
                }
                else
                {
                    CountNow = 0;
                }
            }

            public Rectangle Frame
            {
                get { return m_frame; }
            }
        }

        //Update方法
        public override void Update(GameTime gameTime)
        {
            List.Clear();
            if (Face == AIBase.FaceSide.Left && Status != AIBase.Status.Turning) //如果朝向为左 && 没有在转身状态
            {
                foreach (PicList picList in ListLeft)
                {
                    picList.Update(gameTime);
                    List.Add(picList);
                }
            }

            else if (Face == AIBase.FaceSide.Right && Status != AIBase.Status.Turning) //如果朝向为右 && 没有在转身状态
            {
                foreach (PicList picList in ListRight)
                {
                    picList.Update(gameTime);
                    List.Add(picList);
                }
            }

            else if (Face == AIBase.FaceSide.Left && Status == AIBase.Status.Turning) //如果朝向为左 && 在转身状态
            {
                foreach (PicList picList in ListTurnRight)
                {
                    picList.Update(gameTime);
                    List.Add(picList);
                }
            }

            else if (Face == AIBase.FaceSide.Right && Status == AIBase.Status.Turning) //如果朝向为右 && 在转身状态
            {
                foreach (PicList picList in ListTurnLeft)
                {
                    picList.Update(gameTime);
                    List.Add(picList);
                }
            }

            foreach (PicList picList in ListRing)
            {
                picList.Update(gameTime);
                picList.Size = (int)this.RingSize;
            }

            //0 Normal 1 Attack 2 Flee

        }

        public virtual float Acceleration { get; set; }

        public virtual float MaxSpeed { get; set; }

        public virtual Vector2 Destnation { get; set; }

        public override byte[] CollisionTexture
        {
            get { throw new NotImplementedException(); }
        }

        public override float Rotation
        {
            get
            {
                return this.Rotate;
            }
        }
    }
}



using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Heal.Core.AI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Heal.Core.Entities.Enemies
{
    public class StoneBoss : EnemyTypeBoss
    {
        public static float AttackRange = 900;
        public static float AttackSpeed = 10;
        public static float BackRange = 60;
        public float CurrentBack;
        public static float ShineTime = 120;
        public float CurrentShine;
        public static float DownRange = 150;
        public float CurrentDown;
        public static float RollRange = 200;
        public float CurrentRoll;
        public static Vector2 LeftHandOffst = new Vector2(-210, 0);
        public static Vector2 RightHandOffst = new Vector2(180, 0);
        public static Vector2 LeftLegOffst = new Vector2(-60, 130);
        public static Vector2 RightLegOffst = new Vector2(60, 130);

        public bool[] BodyStatus = { true, true, true, true, true, true };
        //身体 眼睛 左手 右手 左腿 右腿

        public List<BodyNode> StoList;

        public StoneBoss(object sprite, Vector2 speed, Vector2 locate, float enemySize,AIBase.ID id)
            : base(sprite, speed, locate, 0, enemySize,30, 0, id)
        {
            this.Name = AIBase.BossID.StoneBoss;
            this.CurrentBack = 0;
            this.CurrentShine = 0;
            this.CurrentDown = 0;
            this.CurrentRoll = 0;
            StoList = new List<BodyNode>();
        }

        public override void Update(GameTime gameTime)
        {
            if (!this.BodyStatus[2] && !BodyStatus[3])
            {
                this.Status = AIBase.Status.Freeze;
            }

            if (!this.BodyStatus[2] && !this.BodyStatus[3]
                && !this.BodyStatus[4] && !this.BodyStatus[5])
            {
                this.Status = AIBase.Status.Dead;
            }

            if(this.Status!= AIBase.Status.Dead)
            {
                foreach (var ptr in StoList)
            {
                switch (ptr.Name)
                {
                    case AIBase.PartStoBoss.Body:
                        {
                            var ptrB = (StoBody)ptr;

                            switch (ptrB.Status)
                            {
                                case AIBase.StatusStoBoss.Attack:
                                    if (Math.Abs((ptrB.Offset + this.Locate - AIBase.Player.Locate).Length()) <= 150)
                                    {
                                        AIBase.Player.Status = AIBase.Status.Freeze;
                                    }

                                    break;

                            }
                            break;
                        }

                    case AIBase.PartStoBoss.Eye:
                        {
                            var ptrE = (StoEye)ptr;

                            switch (ptrE.Status)
                            {
                                case AIBase.StatusStoBoss.Normal:
                                    ptrE.Color.A -= 5;
                                    break;

                            }
                            break;
                        }
                    case AIBase.PartStoBoss.Lefthand:
                        {
                            var ptrLH = (StoLH)ptr;

                            switch (ptrLH.Status)
                            {
                                case AIBase.StatusStoBoss.Normal:
                                    ptrLH.Color = Color.AliceBlue;
                                    break;
                                case AIBase.StatusStoBoss.Attack:
                                    if (ptrLH.Rotation < MathHelper.PiOver2)
                                    {
                                        ptrLH.Rotation += 0.1f;
                                    }
                                    else
                                    {
                                        ptrLH.Rotation = MathHelper.PiOver2;
                                    }

                                    if (Math.Abs((ptrLH.Offset + this.Locate - AIBase.Player.Locate).Length()) <= 100)
                                    {
                                        AIBase.Player.Status = AIBase.Status.Freeze;
                                    }
                                    ptrLH.Offset.X -= StoneBoss.AttackSpeed / 2;
                                    ptrLH.Color = Color.Red;
                                    break;
                                case AIBase.StatusStoBoss.Flee:
                                    if (ptrLH.Rotation > 0)
                                    {
                                        ptrLH.Rotation -= 0.1f;
                                    }
                                    else
                                    {
                                        ptrLH.Rotation = 0;
                                    }

                                    if (Math.Abs((ptrLH.Offset + this.Locate - AIBase.Player.Locate).Length()) <= 100
                                        && AIBase.Player.Status == AIBase.Status.Attck)
                                    {
                                        ptrLH.Status = AIBase.StatusStoBoss.Disappear;
                                    }

                                    ptrLH.Offset.X += StoneBoss.AttackSpeed / 6;
                                    ptrLH.Color = Color.Green;
                                    break;
                                case AIBase.StatusStoBoss.Disappear:
                                    if (ptrLH.Color.A != 0)
                                    {
                                        ptrLH.Color.A -= 15;
                                    }
                                    else
                                    {
                                        BodyStatus[2] = false;
                                    }
                                    break;
                            }
                            break;
                        }

                    case AIBase.PartStoBoss.Righthand:
                        {
                            var ptrRH = (StoRH)ptr;

                            switch (ptr.Status)
                            {
                                case AIBase.StatusStoBoss.Normal:
                                    ptrRH.Color = Color.AliceBlue;
                                    break;
                                case AIBase.StatusStoBoss.Attack:
                                    if (ptrRH.Rotation < MathHelper.PiOver2)
                                    {
                                        ptrRH.Rotation += 0.1f;
                                    }
                                    else
                                    {
                                        ptrRH.Rotation = MathHelper.PiOver2;
                                    }

                                    if (Math.Abs((ptrRH.Offset + this.Locate - AIBase.Player.Locate).Length()) <= 100)
                                    {
                                        AIBase.Player.Status = AIBase.Status.Freeze;
                                    }
                                    ptrRH.Offset.X -= StoneBoss.AttackSpeed / 2;
                                    ptrRH.Color = Color.Red;
                                    break;
                                case AIBase.StatusStoBoss.Flee:
                                    if (ptrRH.Rotation > 0)
                                    {
                                        ptrRH.Rotation -= 0.1f;
                                    }
                                    else
                                    {
                                        ptrRH.Rotation = 0;
                                    }

                                    if (Math.Abs((ptrRH.Offset + this.Locate - AIBase.Player.Locate).Length()) <= 100
                                        && AIBase.Player.Status == AIBase.Status.Attck)
                                    {
                                        ptrRH.Status = AIBase.StatusStoBoss.Disappear;
                                    }

                                    ptrRH.Offset.X += StoneBoss.AttackSpeed / 6;
                                    ptrRH.Color = Color.Green;
                                    break;
                                case AIBase.StatusStoBoss.Disappear:
                                    if (ptrRH.Color.A != 0)
                                    {
                                        ptrRH.Color.A -= 15;
                                    }
                                    else
                                    {
                                        BodyStatus[3] = false;
                                    }
                                    break;
                            }
                            break;
                        }


                    case AIBase.PartStoBoss.Leftleg:
                        {
                            var ptrLL = (StoLL)ptr;

                            switch (ptrLL.Status)
                            {
                                case AIBase.StatusStoBoss.Normal:
                                    ptrLL.Color = Color.AliceBlue;
                                    break;
                                case AIBase.StatusStoBoss.Attack:

                                    if (Math.Abs((ptrLL.Offset + this.Locate - AIBase.Player.Locate).Length()) <= 100)
                                    {
                                        AIBase.Player.Status = AIBase.Status.Freeze;
                                    }
                                    ptrLL.Offset.Y += StoneBoss.AttackSpeed / 2;
                                    ptrLL.Color = Color.Red;
                                    break;
                                case AIBase.StatusStoBoss.Flee:

                                    if (Math.Abs((ptrLL.Offset + this.Locate - AIBase.Player.Locate).Length()) <= 100
                                        && AIBase.Player.Status == AIBase.Status.Attck)
                                    {
                                        ptrLL.Status = AIBase.StatusStoBoss.Disappear;
                                    }

                                    ptrLL.Offset.Y -= StoneBoss.AttackSpeed / 6;
                                    ptrLL.Color = Color.Green;
                                    break;
                                case AIBase.StatusStoBoss.Disappear:
                                    if (ptrLL.Color.A != 0)
                                    {
                                        ptrLL.Color.A -= 15;
                                    }
                                    else
                                    {
                                        BodyStatus[4] = false;
                                    }
                                    break;
                            }
                            break;
                        }

                    case AIBase.PartStoBoss.Rightleg:
                        {
                            var ptrRL = (StoRL)ptr;

                            switch (ptrRL.Status)
                            {
                                case AIBase.StatusStoBoss.Normal:
                                    ptrRL.Color = Color.AliceBlue;
                                    break;
                                case AIBase.StatusStoBoss.Attack:

                                    if (Math.Abs((ptrRL.Offset + this.Locate - AIBase.Player.Locate).Length()) <= 100)
                                    {
                                        AIBase.Player.Status = AIBase.Status.Freeze;
                                    }
                                    ptrRL.Offset.Y += StoneBoss.AttackSpeed / 2;
                                    ptrRL.Color = Color.Red;
                                    break;
                                case AIBase.StatusStoBoss.Flee:

                                    if (Math.Abs((ptrRL.Offset + this.Locate - AIBase.Player.Locate).Length()) <= 100
                                        && AIBase.Player.Status == AIBase.Status.Attck)
                                    {
                                        ptrRL.Status = AIBase.StatusStoBoss.Disappear;
                                    }

                                    ptrRL.Offset.Y -= StoneBoss.AttackSpeed / 6;
                                    ptrRL.Color = Color.Green;
                                    break;
                                case AIBase.StatusStoBoss.Disappear:
                                    if (ptrRL.Color.A != 0)
                                    {
                                        ptrRL.Color.A -= 15;
                                    }
                                    else
                                    {
                                        BodyStatus[5] = false;
                                    }
                                    break;
                            }
                            break;
                        }
                }
            }

            foreach (var ptr in StoList)
            {
                ptr.CheckAll(BodyStatus, this.Locate);
            }
            }

        }

        public void Install(Texture2D body, Texture2D eye,
            Texture2D lh, Texture2D rh, Texture2D ll, Texture2D rl)
        {

            StoList.Add(new StoBody(body, Color.AliceBlue, new Vector2(0)));
            StoList.Add(new StoEye(eye, Color.AliceBlue, new Vector2(0)));
            StoList.Add(new StoLH(lh, Color.AliceBlue, StoneBoss.LeftHandOffst));
            StoList.Add(new StoRH(rh, Color.AliceBlue, StoneBoss.RightHandOffst));
            StoList.Add(new StoLL(ll, Color.AliceBlue, StoneBoss.LeftLegOffst));
            StoList.Add(new StoRL(rl, Color.AliceBlue, StoneBoss.RightLegOffst));
        }

        public override Rectangle GetDrawingRectangle()
        {
            throw new NotImplementedException();
        }

        public override void Initialize()
        {
            throw new NotImplementedException();
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

        public abstract class BodyNode
        {
            public Texture2D Target;
            public Color Color;
            public Vector2 Offset;
            public float Rotation;
            public AIBase.StatusStoBoss Status;
            public AIBase.PartStoBoss Name;

            public BodyNode(Texture2D target, Color color, Vector2 offset)
            {
                this.Target = target;
                this.Offset = offset;
                this.Color = color;
                this.Rotation = 0;
                this.Status = AIBase.StatusStoBoss.Normal;
            }

            public abstract void CheckAll(bool[] bodyStaus, Vector2 postion);
            public abstract void ToAttack(bool[] bodyStatus, Vector2 postion);
            public abstract void ToFlee(bool[] bodyStatus, Vector2 postion);
            public abstract void ToNormal(bool[] bodyStatus, Vector2 postion);
        }

        public class StoBody : BodyNode
        {
            public StoBody(Texture2D target, Color color, Vector2 offset)
                : base(target, color, offset)
            {
                this.Name = AIBase.PartStoBoss.Body;
                this.Status = AIBase.StatusStoBoss.Attack;
                ;
            }

            public override void CheckAll(bool[] bodyStaus, Vector2 postion)
            {
            }

            public override void ToAttack(bool[] bodyStatus, Vector2 postion)
            {

            }

            public override void ToFlee(bool[] bodyStatus, Vector2 postion)
            {
            }

            public override void ToNormal(bool[] bodyStatus, Vector2 postion)
            {

            }

        }

        public class StoEye : BodyNode
        {
            public StoEye(Texture2D target, Color color, Vector2 offset)
                : base(target, color, offset)
            {
                this.Name = AIBase.PartStoBoss.Eye;
                this.Status = AIBase.StatusStoBoss.Normal;
            }

            public override void CheckAll(bool[] bodyStaus, Vector2 postion)
            {

            }

            public override void ToAttack(bool[] bodyStatus, Vector2 postion)
            {

            }

            public override void ToFlee(bool[] bodyStatus, Vector2 postion)
            {

            }

            public override void ToNormal(bool[] bodyStatus, Vector2 postion)
            {

            }

        }

        public class StoLH : BodyNode
        {
            public StoLH(Texture2D target, Color color, Vector2 offset)
                : base(target, color, offset)
            {
                this.Name = AIBase.PartStoBoss.Lefthand;
            }

            public override void CheckAll(bool[] bodyStaus, Vector2 postion)
            {
                if (this.Status != AIBase.StatusStoBoss.Disappear)
                {
                    this.ToAttack(bodyStaus, postion);
                    this.ToNormal(bodyStaus, postion);
                    this.ToFlee(bodyStaus, postion);
                }
            }

            public override void ToAttack(bool[] bodyStatus, Vector2 postion)
            {
                if (Math.Abs(AIBase.Player.Locate.X - postion.X - this.Offset.X) <= StoneBoss.AttackRange
                    && Math.Abs(AIBase.Player.Postion.Y - postion.Y) <= 100
                    && this.Status == AIBase.StatusStoBoss.Normal)
                {
                    this.Status = AIBase.StatusStoBoss.Attack;
                }
            }

            public override void ToFlee(bool[] bodyStatus, Vector2 postion)
            {
                if (Math.Abs(this.Offset.X) >= StoneBoss.AttackRange / 2)
                {
                    this.Status = AIBase.StatusStoBoss.Flee;
                }
            }

            public override void ToNormal(bool[] bodyStatus, Vector2 postion)
            {
                if (Math.Abs(this.Offset.X) < Math.Abs(StoneBoss.LeftHandOffst.X))
                {
                    this.Status = AIBase.StatusStoBoss.Normal;
                    this.Offset = StoneBoss.LeftHandOffst;
                }
            }

        }

        public class StoRH : BodyNode
        {
            public StoRH(Texture2D target, Color color, Vector2 offset)
                : base(target, color, offset)
            {
                this.Name = AIBase.PartStoBoss.Righthand;
            }

            public override void CheckAll(bool[] bodyStaus, Vector2 postion)
            {
                if (this.Status != AIBase.StatusStoBoss.Disappear)
                {
                    this.ToAttack(bodyStaus, postion);
                    this.ToNormal(bodyStaus, postion);
                    this.ToFlee(bodyStaus, postion);
                }
            }

            public override void ToAttack(bool[] bodyStatus, Vector2 postion)
            {
                if (!bodyStatus[2]
                    && Math.Abs(AIBase.Player.Locate.X - postion.X - this.Offset.X) <= StoneBoss.AttackRange
                    && Math.Abs(AIBase.Player.Postion.Y - postion.Y) <= 100
                    && this.Status == AIBase.StatusStoBoss.Normal)
                {
                    this.Status = AIBase.StatusStoBoss.Attack;
                }
            }

            public override void ToFlee(bool[] bodyStatus, Vector2 postion)
            {
                if (Math.Abs(this.Offset.X) >= StoneBoss.AttackRange / 1.8f)
                {
                    this.Status = AIBase.StatusStoBoss.Flee;
                }
            }

            public override void ToNormal(bool[] bodyStatus, Vector2 postion)
            {
                if (this.Offset.X > Math.Abs(StoneBoss.RightHandOffst.X))
                {
                    this.Status = AIBase.StatusStoBoss.Normal;
                    this.Offset = StoneBoss.RightHandOffst;
                }
            }


        }

        public class StoLL : BodyNode
        {
            public StoLL(Texture2D target, Color color, Vector2 offset)
                : base(target, color, offset)
            {
                this.Name = AIBase.PartStoBoss.Leftleg;
            }

            public override void CheckAll(bool[] bodyStaus, Vector2 postion)
            {
                if (this.Status != AIBase.StatusStoBoss.Disappear)
                {
                    this.ToAttack(bodyStaus, postion);
                    this.ToNormal(bodyStaus, postion);
                    this.ToFlee(bodyStaus, postion);
                }
            }

            public override void ToAttack(bool[] bodyStatus, Vector2 postion)
            {
                if (Math.Abs(AIBase.Player.Locate.Y - postion.Y - this.Offset.Y) <= StoneBoss.AttackRange
                    && AIBase.Player.Locate.Y > this.Offset.Y + postion.Y
                    && Math.Abs(AIBase.Player.Postion.X - postion.X) <= 100
                    && this.Status == AIBase.StatusStoBoss.Normal)
                {
                    this.Status = AIBase.StatusStoBoss.Attack;
                }
            }

            public override void ToFlee(bool[] bodyStatus, Vector2 postion)
            {
                if (this.Offset.Y >= StoneBoss.AttackRange / 3f)
                {
                    this.Status = AIBase.StatusStoBoss.Flee;
                }
            }

            public override void ToNormal(bool[] bodyStatus, Vector2 postion)
            {
                if (this.Offset.Y < StoneBoss.LeftLegOffst.Y)
                {
                    this.Status = AIBase.StatusStoBoss.Normal;
                    this.Offset = StoneBoss.LeftLegOffst;
                }
            }


        }

        public class StoRL : BodyNode
        {
            public StoRL(Texture2D target, Color color, Vector2 offset)
                : base(target, color, offset)
            {
                this.Name = AIBase.PartStoBoss.Rightleg;
            }

            public override void CheckAll(bool[] bodyStaus, Vector2 postion)
            {
                if (this.Status != AIBase.StatusStoBoss.Disappear)
                {
                    this.ToAttack(bodyStaus, postion);
                    this.ToNormal(bodyStaus, postion);
                    this.ToFlee(bodyStaus, postion);
                }
            }

            public override void ToAttack(bool[] bodyStatus, Vector2 postion)
            {
                if (Math.Abs(AIBase.Player.Locate.Y - postion.Y - this.Offset.Y) <= StoneBoss.AttackRange
                    && AIBase.Player.Locate.Y > this.Offset.Y + postion.Y
                    && Math.Abs(AIBase.Player.Postion.X - postion.X - 100) <= 100
                    && this.Status == AIBase.StatusStoBoss.Normal)
                {
                    this.Status = AIBase.StatusStoBoss.Attack;
                }
            }

            public override void ToFlee(bool[] bodyStatus, Vector2 postion)
            {
                if (this.Offset.Y >= StoneBoss.AttackRange / 3.5f)
                {
                    this.Status = AIBase.StatusStoBoss.Flee;
                }
            }

            public override void ToNormal(bool[] bodyStatus, Vector2 postion)
            {
                if (this.Offset.Y < StoneBoss.RightLegOffst.Y)
                {
                    this.Status = AIBase.StatusStoBoss.Normal;
                    this.Offset = StoneBoss.RightLegOffst;
                }
            }


        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Heal.Core.AI;
using Heal.Core.Entities.Enemies;
using Heal.Core.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Heal.Core.Entities
{
    public class EnergyBall : Item
    {
        public List<EnergyBallNode> List;
        public static float Size = 0.1f;

        public EnergyBall(object sprite) : base(sprite)
        {
            List = new List<EnergyBallNode>();
        }

        public override Vector2 Locate
        {
            get
            {
                return base.Locate;
            }
            set
            {
                
            }
        }

        public void AddNode(Vector2 Postion)
        {
            this.List.Add(new EnergyBallNode(Postion));
        }

        public class EnergyBallNode
        {
            public Vector2 Postion;
            private static float RefreshTime = 300;
            private float RefreshTimeNow;
            private EnergyBallStatus Status;

            public Color Color;
            public float Rotation;
            public float Scale = 1f;

            private enum EnergyBallStatus
            {
                Disappear,
                Appear,
                Show,
                CoolDown
            } ;

            public EnergyBallNode(Vector2 Postion)
            {
                this.Postion = Postion;
                this.Status = EnergyBallStatus.Show;
                this.RefreshTimeNow = 0;
                this.Color = Color.AliceBlue;
                this.Rotation = MathTools.RandomGenerate();
            }


            public void ToDisappear()
            {
                this.Status = EnergyBallStatus.Disappear;
            }

            public void ToCoolDown()
            {
                this.Status = EnergyBallStatus.CoolDown;
            }

            public void ToAppear()
            {
                this.Status = EnergyBallStatus.Appear;
            }

            public void ToShow()
            {
                this.Status = EnergyBallStatus.Show;
            }

            public void Update(GameTime gameTime, Player player)
            {
                switch (this.Status)
                {
                    case EnergyBallStatus.Show:
                        if ((this.Postion - player.Locate).Length() <= 30)
                        {
                            this.ToDisappear();
                            player.RingSize += 20;;
                        }else
                        {
                            this.Rotation += 0.01f;
                            this.Rotation %= 6.28f;
                            this.Scale += 0.05f;
                            this.Scale %= 6.28f;
                        }
                        break;
                    case EnergyBallStatus.Disappear:
                        if (this.Color.A != 0)
                        {
                            this.Color.A-=5;
                        }
                        else
                        {
                            this.ToCoolDown();
                        }
                        break;
                    case EnergyBallStatus.CoolDown:
                        if (this.RefreshTimeNow < EnergyBallNode.RefreshTime)
                        {
                            this.RefreshTimeNow++;
                        }
                        else
                        {
                            this.RefreshTimeNow = 0;
                            this.ToAppear();
                        }
                        break;
                    case EnergyBallStatus.Appear:
                        if(this.Color.A!=255)
                        {
                            this.Color.A+=5;
                        }
                        else
                        {
                            this.ToShow();
                        }
                        break;
                }
            }   
        }


        public override Rectangle GetDrawingRectangle()
        {
            return new Rectangle();
        }

        public override void Initialize()
        {
        }

        public override void Update(GameTime gameTime)
        {
            foreach (var node in List)
            {
                node.Update(gameTime,AIBase.Player);
            }
        }

    }  
}

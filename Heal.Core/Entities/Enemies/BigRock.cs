using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Heal.Core.AI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Heal.Core.Entities.Enemies
{
    public class BigRock : Enemy
    {
        public Texture2D Targetr;

        public Vector2 Min, Max;
        public BigRock(object sprite, Vector2 speed, Vector2 locate, float enemySize, AIBase.FaceSide face, Vector2 start, Vector2 end)
            : base(sprite, speed, locate, 0, enemySize, 0, face, AIBase.ID.NPC)
        {
            this.Min = start;
            this.Max = end;
        }

        public void AddTarget(Texture2D ta)
        {
            this.Targetr = ta;
        }

        public override void Update(GameTime gameTime)
        {
            this.PostionGenerate(gameTime);
            if (this.Face == AIBase.FaceSide.Right)
            {
                if (this.Locate.X > this.Max.X)
                {
                    this.Speed.X = -this.Speed.X;
                    this.Face = AIBase.FaceSide.Left;
                }
                else
                {
                    this.LocateConfirm();
                    if (Math.Abs(this.Locate.X - AIBase.Player.Locate.X) < 80
                         && Math.Abs(this.Locate.Y - AIBase.Player.Locate.Y) < 40)
                    {
                        AIBase.Player.Locate = this.Postion + new Vector2(90, 0);
                        AIBase.Player.Scale.X = 0.01f;
                        AIBase.Player.HP--;
                    }
                }
            }
            else
            {
                if (this.Locate.X < this.Min.X)
                {
                    this.Speed.X = -this.Speed.X;
                    this.Face = AIBase.FaceSide.Right;
                }
                else
                {
                    this.LocateConfirm();
                    if (Math.Abs(this.Locate.X - AIBase.Player.Locate.X) < 80
                         && Math.Abs(this.Locate.Y - AIBase.Player.Locate.Y) < 40)
                    {
                        AIBase.Player.Locate = this.Postion - new Vector2(90, 0);
                        AIBase.Player.Scale.X = 0.01f;
                        AIBase.Player.HP--;
                    }
                }
            }
        }

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
    }
}

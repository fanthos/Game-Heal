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
    public class LittleFlower : Enemy
    {
        public bool ToUpdate;
        public LittleFlower(object sprite, Vector2 locate, float enemySize, AIBase.ID id)
            : base(sprite, new Vector2(0), locate, 0, enemySize, 0, AIBase.FaceSide.Right, id)
        {
            ToUpdate = true;
        }

        public override void Update(GameTime gameTime)
        {

                base.Update(gameTime);
                if (ToUpdate)
                {
                this.Speed = AIBase.Player.Locate - this.Locate;

                if ((this.Locate - AIBase.Player.Locate).Length() <= 40)
                {
                    this.Speed.Length = 0;
                }
                else if ((this.Locate - AIBase.Player.Locate).Length() >= 600)
                {
                    this.Locate = AIBase.Player.Locate - new Vector2(40, 40);
                }

                this.Postion = AIBase.SenceManager.MoveTest(this.Locate, this.Speed,
                                                            (float)gameTime.ElapsedGameTime.TotalSeconds);
                this.LocateConfirm();
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

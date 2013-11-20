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
    public class EnemyTypeII : Enemy
    {
        public Texture2D Body;
        public List<PicList> Sharp;
        public Speed DefaultSpeed;
        public int AttackTime;
        public bool Find;
        public Vector2[] Offset = {new Vector2(26, -15), new Vector2(26, 15)}; 
        public List<EnemyTypeII> CombineList;
        public bool BeCatch;
        public bool IsLead;


        public int CurrentFlee;


        public EnemyTypeII(object sprite, Vector2 speed, Vector2 locate, float ringSize, float enemySize, float enemySize2, AIBase.FaceSide face, AIBase.ID id)
            : base(sprite, speed, locate, ringSize, enemySize, enemySize2, face, id)
        {
            this.BeCatch = false;
            this.DefaultSpeed = Speed;
            this.CurrentFlee = 0;
            this.AttackTime = 0;
            Find = false;
            IsLead = false;
            Sharp = new List<PicList>();
            this.CombineList = new List<EnemyTypeII>();
        }

        public override void Update(GameTime gameTime)
        {
            if(this.BeCatch) return;
            base.Update(gameTime);

            if(this.CombineList.Count!=0)
            {
                int i = 0;
                foreach (var ptr in CombineList)
                {
                    ptr.Locate = this.Locate + this.Offset[i];
                    i++;
                }
            }
        }

        public void AddSharp(Texture2D target, int picCount, int size, int oSize, int timer)
        {

            Sharp.Add(new PicList(target, picCount, size, oSize, this.Color, timer));
        }

        public void AddBody(Texture2D body)
        {
            Body = body;
        } 

        public override Rectangle GetDrawingRectangle()
        {
            throw new NotImplementedException();
        }

        public override void Initialize()
        {
            throw new NotImplementedException();
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
            if ((this.Locate - ptr.Locate).Length() <= 40
                 && this.RingSize >= AIBase.Player.RingSize
                 && this.Status != AIBase.Status.Dead
                 && this.Status != AIBase.Status.Turning)
            {
                this.LastStatus = this.Status;
                this.Status = AIBase.Status.Attck;
                this.CurrentDialog = AIBase.StatusDialog.Attack;
            }
        }

        internal override void ToDeadStatus(GameTime gameTime, Player ptr)
        {
            if ((this.Status == AIBase.Status.Flee || !Enemy.Detected((Enemy)this))
              && (AIBase.Player.Locate - this.Locate).Length() <= 40
              && ptr.Status == AIBase.Status.Attck)
            {
                this.Status = AIBase.Status.Dead;
            }
        }

        internal override void ToTurnStatus()
        {
        }
    }
}

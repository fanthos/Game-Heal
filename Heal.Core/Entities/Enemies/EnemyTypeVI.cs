using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Heal.Core.AI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Heal.Core.Entities.Enemies
{
    public class EnemyTypeVI : EnemyTypeI
    {
        public int HP;
        public bool BeAttack;
        public int CoolTime;

        public  readonly List<PicList> TurnLeftIron;
        public  readonly List<PicList> TurnRightIron;

        public Texture2D LeftIron;
        public Texture2D RightIron;

        public EnemyTypeVI(object sprite, Vector2 speed, Vector2 locate, float ringSize, float enemySize, float enemySize2, AIBase.FaceSide face, AIBase.ID id, int range) : base(sprite, speed, locate, ringSize, enemySize, enemySize2, face, id, range)
        {
            HP = 2;
            this.CoolTime = 0;
            this.TurnLeftIron = new List<PicList>();
            this.TurnRightIron = new List<PicList>();
            this.BeAttack = false;
        }

        internal override void ToDeadStatus(GameTime gameTime, Player ptr)
        {
            if ((this.Status == AIBase.Status.Flee || !Enemy.Detected((Enemy)this))
                && (AIBase.Player.Locate - this.Locate).Length() <= 20
                && ptr.Status == AIBase.Status.Attck)
            {
                if(this.HP == 2 && !this.BeAttack)
                {
                    this.HP--;
                    this.BeAttack = true;
                    this.Status = AIBase.Status.Normal;
                }
                else if (this.HP == 1 && !this.BeAttack)
                {
                    this.Status = AIBase.Status.Dead;
                }
            }
        }

        public void AddToIronList(Texture2D left,Texture2D right, int picCount, int size, int oSize, float time)
        {
            this.TurnLeftIron.Add(new PicList(left,picCount,size,oSize,Color,time));
            this.TurnRightIron.Add(new PicList(right,picCount,size,oSize,Color,time));
        }

        public void AddIronFace(Texture2D left,Texture2D right)
        {
            LeftIron = left;
            RightIron = right;
        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if(this.Status == AIBase.Status.Turning && this.Face == AIBase.FaceSide.Right )
            {
                foreach (var ptr in TurnLeftIron)
                {
                    ptr.Update(gameTime);
                }
            }
            
            if(this.Status == AIBase.Status.Turning && this.Face == AIBase.FaceSide.Left)
            {
                foreach (var ptr in TurnRightIron)
                {
                    ptr.Update(gameTime);
                }
            }

            if(this.CoolTime <= 60 && this.BeAttack)
            {
                this.CoolTime++;
            }
            else
            {
                this.BeAttack = false;
                this.CoolTime = 0;
            }
        }
    }
}

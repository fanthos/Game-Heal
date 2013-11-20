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
    public class SmokeBoss : EnemyTypeBoss
    {
        public List<ListPart> SmokeBossList;
        public AIBase.StatusSmoBoss SmoBossStatus;
        public float Acclerate;
        public Vector2 ChaseSpeed;
        public Vector2 MostSpeed;
        public Vector2 MinSpeed;

        public SmokeBoss(object sprite, Vector2 speed, Vector2 locate, AIBase.ID id,float acclerate,Vector2 maxSpeed,Vector2 minSpeed)
            : base(sprite, speed, locate, 0, 1.08f,1.1f , 0, id)
        {
            
            this.MostSpeed = maxSpeed;
            this.Status = AIBase.Status.Chase;
            this.SmoBossStatus = AIBase.StatusSmoBoss.SpeedUp;
            this.Name = AIBase.BossID.SmokeBoss;
            this.Acclerate = acclerate;
            this.ChaseSpeed = speed;
            this.MinSpeed = minSpeed;

            SmokeBossList = new List<ListPart>();
        }


        public void ToChase(Player ptr)
        {
            if(this.Speed.X <this.MinSpeed.X)
            {
                this.SmoBossStatus = AIBase.StatusSmoBoss.Chase;
            }
        }

        public void SpeedUp(Player ptr)
        {
            if (Math.Abs(this.Locate.X - ptr.Locate.X) > 800)
            {
                this.SmoBossStatus = AIBase.StatusSmoBoss.SpeedUp;
            }
        }

        public void SlowDown(Player ptr)
        {
            if (Math.Abs(this.Locate.X - ptr.Locate.X) < 700 && Math.Abs(this.Locate.X - ptr.Locate.X) >600)
            {
                this.SmoBossStatus = AIBase.StatusSmoBoss.SlowDown;
            }
        }

        public void ToEnd()
        {
        }

        public override void Update(GameTime gameTime)
        {
            if( !CoreUtilities.StateIsTrue( "map0_smoke" ) )
            {
                return;
            }
            switch (SmoBossStatus)
            {
                case AIBase.StatusSmoBoss.Chase:
                    SpeedUp(AIBase.Player);
                    break;
                case AIBase.StatusSmoBoss.SpeedUp:
                    Speed.X += Math.Abs(Acclerate) * .3f;
                    SlowDown(AIBase.Player);
                    break;
                case AIBase.StatusSmoBoss.SlowDown:
                    Speed.X += -40f * (float)gameTime.ElapsedGameTime.TotalSeconds * Math.Abs(Acclerate);
                    ToChase(AIBase.Player);
                    break;
                case AIBase.StatusSmoBoss.End:
                    break;
            }

            PostionGenerate(gameTime);
            LocateConfirm();
            if ((this.Postion - AIBase.Player.Locate).Length() <= 350 || AIBase.Player.Status == AIBase.Status.Dead)
            {
                GameCommands.Enqueue( "Load Map1;splashtimer splash0_2_1 1 splashtimer splash0_2_2 1 splashtimer splash0_2_3 1 " +
                                      "splashtimer splash0_2_4 1 splashtimer splash0_2_5 1 splashtimer splash0_2_6 1 Goto Map1" );
                AIBase.EnemyList.Remove(this);
            }
            foreach (var ptr in SmokeBossList)
            {
                ptr.Update();
            }
        }

        public void AddToList(Texture2D target, Vector2 postion, double angle)
        {
            SmokeBossList.Add(new ListPart(target, postion, angle));
        }

        public class ListPart
        {
            public Texture2D Target;
            private Vector2 m_center;
            public Vector2 Postion;
            private double m_angle;

            private float m_locateX;
            private float m_locateY;

            public float Alpha;

            public ListPart(Texture2D target, Vector2 postion, double angle)
            {
                Target = target;
                m_center = postion;
                Postion = postion;
                m_angle = angle;
            }

            public void Update()
            {
                Alpha += MathTools.RandomGenerate()*0.05f;
                if (Alpha > 1) Alpha -= 2;
 
                m_locateX += MathTools.RandomGenerate() * 0.2f;
                m_locateY += MathTools.RandomGenerate() * 0.2f;
                if (m_locateX > MathHelper.TwoPi) m_locateX -= MathHelper.TwoPi;
                if (m_locateY > MathHelper.TwoPi) m_locateY -= MathHelper.TwoPi;
                Postion.X = (float)(2.5f * Math.Sin(m_locateX));
                Postion.Y = (float)(2.5f * Math.Cos(m_locateY));

            }
        }

        public override Rectangle GetDrawingRectangle()
        {
            throw new NotImplementedException();
        }

        public override void Initialize()
        {

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
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Heal.Core.AI;
using Heal.Core.Entities.Enemies;
using Heal.Data;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Heal.Component
{
    internal class BigDaddyComponent : UnitComponent
    {
        public void AddLeftBody(String lbbe, String lbg, String lbw, String lbb)
        {
            ((BigDaddy)m_unit).AddLeftBody(DataReader.Load<Texture2D>("Texture/Entities/" + lbbe),
                                           DataReader.Load<Texture2D>("Texture/Entities/" + lbg),
                                           DataReader.Load<Texture2D>("Texture/Entities/" + lbw),
                                           DataReader.Load<Texture2D>("Texture/Entities/" + lbb));
        }

        public void AddRightBody(String rbbe, String rbg, String rbw, String rbb)
        {
            ((BigDaddy)m_unit).AddRightBody(DataReader.Load<Texture2D>("Texture/Entities/" + rbbe),
                                           DataReader.Load<Texture2D>("Texture/Entities/" + rbg),
                                           DataReader.Load<Texture2D>("Texture/Entities/" + rbw),
                                           DataReader.Load<Texture2D>("Texture/Entities/" + rbb));
        }

        public void AddLeftDriver(String ldbe, String ldg, String ldw, String ldb)
        {
            ((BigDaddy)m_unit).AddLeftDriver(DataReader.Load<Texture2D>("Texture/Entities/" + ldbe),
                                            DataReader.Load<Texture2D>("Texture/Entities/" + ldg),
                                            DataReader.Load<Texture2D>("Texture/Entities/" + ldw),
                                            DataReader.Load<Texture2D>("Texture/Entities/" + ldb));
        }

        public void AddRightDriver(String rdbe, String rdg, String rdw, String rdb)
        {
            ((BigDaddy)m_unit).AddRightDriver(DataReader.Load<Texture2D>("Texture/Entities/" + rdbe),
                                           DataReader.Load<Texture2D>("Texture/Entities/" + rdg),
                                           DataReader.Load<Texture2D>("Texture/Entities/" + rdw),

                                           DataReader.Load<Texture2D>("Texture/Entities/" + rdb));
        }

        public void AddElecBall()
        {
            ((BigDaddy)m_unit).AddNewBall(DataReader.Load<Texture2D>("Texture/Entities/BigDaddy/elec_ball"), m_unit.Postion,new Vector2(0));
            ((BigDaddy)m_unit).AddNewBall(DataReader.Load<Texture2D>("Texture/Entities/BigDaddy/elec_ball"), m_unit.Postion,new Vector2(0));       
        }

        public void AddScence()
        {
            Rectangle[] Area = {   new Rectangle(325,200,55,90),
                                   new Rectangle(420,210,40,80),
                                   new Rectangle(500,210,50,80),
                                   new Rectangle(580,210,50,80),
                                   new Rectangle(670,200,45,80),
                                   new Rectangle(750,330,90,50),
                                   new Rectangle(750,420,80,60),
                                   new Rectangle(750,510,80,40),
                                   new Rectangle(750,600,95,45),
                                   new Rectangle(750,680,100,40),
                                   new Rectangle(690,760,60,80),
                                   new Rectangle(600,760,70,80),
                                   new Rectangle(515,760,60,80),
                                   new Rectangle(430,760,55,80),
                                   new Rectangle(330,770,70,80),
                                   new Rectangle(200,670,80,70),
                                   new Rectangle(200,600,90,60),
                                   new Rectangle(200,510,80,50),
                                   new Rectangle(200,420,80,60),
                                   new Rectangle(200,320,70,50)
                               };

            for(int i=0;i<20;i++)
            {
                ((BigDaddy)m_unit).AddScence(DataReader.Load<Texture2D>("Texture/Entities/Scence/map_6_"+ i.ToString()),Area[i]);
            }
        }

        internal new void AddNew(string target, int oSize, float time, AIBase.List which)
        {
            Texture2D texture2D = DataReader.Load<Texture2D>("Texture/Entities/" + target);
            int size = texture2D.Height;
            int picCount = texture2D.Width / size;
            m_unit.AddNew(texture2D, picCount, size, oSize, time, which);
        }

        protected override void InternalDraw(Microsoft.Xna.Framework.GameTime gameTime, SpriteBatch batch)
        {

            foreach (var ptr in ((BigDaddy)m_unit).List)
            {
                batch.Draw(ptr.Target, base.Locate, ptr.Frame, ptr.Color, base.Rotation,
                         new Vector2((float)ptr.Frame.Height / 2), m_unit.EnemySize * m_worldManager.Scale, SpriteEffects.None, 0);
            }

            foreach (var ptr in ((BigDaddy)m_unit).ScenceList)
            {
                if(ptr.Status)
                {
                    batch.Draw(ptr.Target,-m_worldManager.Locate * m_worldManager.Scale + m_worldManager.Space / 2,Color.AliceBlue );
                }
            }

            foreach (var ptr in ((BigDaddy)m_unit).ElecList)
            {
                batch.Draw(ptr.Ball,ptr.Postion * m_worldManager.Scale,ptr.Color);
            }

            if (((BigDaddy)m_unit).Status != AIBase.Status.Turning)
            {

                if (((BigDaddy)m_unit).Face == AIBase.FaceSide.Left)
                {
                    batch.Draw(((BigDaddy)m_unit).LeftDriver[(int)((BigDaddy)m_unit).CurrentStatus], base.Locate,
                               null, ((BigDaddy)m_unit).Color, base.Rotation, new Vector2(80),
                               m_worldManager.Scale * ((BigDaddy)m_unit).EnemySize, SpriteEffects.None, 0);
                    batch.Draw(((BigDaddy)m_unit).LeftBody[(int)((BigDaddy)m_unit).CurrentStatus], base.Locate, null,
                               ((BigDaddy)m_unit).Color, base.Rotation, new Vector2(80),
                               m_worldManager.Scale * ((BigDaddy)m_unit).EnemySize, SpriteEffects.None, 0);
                }
                else
                {
                    batch.Draw(((BigDaddy)m_unit).RightDriver[(int)((BigDaddy)m_unit).CurrentStatus], base.Locate,
                               null, ((BigDaddy)m_unit).Color, base.Rotation, new Vector2(80),
                               m_worldManager.Scale * ((BigDaddy)m_unit).EnemySize, SpriteEffects.None, 0);
                    batch.Draw(((BigDaddy)m_unit).RightBody[(int)((BigDaddy)m_unit).CurrentStatus], base.Locate, null,
                               ((BigDaddy)m_unit).Color, base.Rotation, new Vector2(80),
                               m_worldManager.Scale * ((BigDaddy)m_unit).EnemySize, SpriteEffects.None, 0);
                }
            }

        }
    }
}

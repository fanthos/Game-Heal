using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Heal.Core.Entities.Enemies;
using Heal.Core.Utilities;
using Heal.Data;
using Heal.World;
using Microsoft.Xna.Framework;
using Heal.Core.AI;
using Microsoft.Xna.Framework.Graphics;

namespace Heal.Component
{
    internal class EnemyIVComponent : EnemyComponent
    {
        public Texture2D BlackHole;
        public float Scale = 0;
        public float Rotation =0;
        public bool Status  = false;
        public int tmp;
        public void AddBlackHole()
        {
            this.BlackHole = DataReader.Load<Texture2D>("Texture/Entities/Monster4/blackhole");
        }

        protected override void InternalDraw(Microsoft.Xna.Framework.GameTime gameTime, Microsoft.Xna.Framework.Graphics.SpriteBatch batch)
        {

            base.InternalDraw(gameTime, batch);

            if (((EnemyTypeIV)m_unit).CallEnemy && !Status)
            {
                if (this.Scale < 1.5)
                {
                    this.Scale += 0.02f;
                    this.Rotation++;
                    batch.Draw(this.BlackHole, base.Locate + new Vector2(60, 0), new Rectangle(0, 0, 80, 80), Color.AliceBlue, this.Rotation, new Vector2(40), this.Scale * m_worldManager.Scale, SpriteEffects.None, 0);
                }
                else
                {
                    tmp = (int)MathTools.RandomGenerate(300)%2;

                    if(tmp == 0)
                    {
                        EntityCreator.Create("EnemyTypeI",
                     new object[2] { m_unit.Locate + new Vector2(60, 0), AIBase.FaceSide.Right });
                    }
                    else if(tmp == 1)
                    {
                        EntityCreator.Create("Ghost",
                     new object[2] { m_unit.Locate + new Vector2(60, 0), AIBase.FaceSide.Right });
                    }else if(tmp == 2)
                    {
                        EntityCreator.Create("EnemyTypeI",
                     new object[2] { m_unit.Locate + new Vector2(60, 0), AIBase.FaceSide.Right });
                    }

                    ((EnemyTypeIV) m_unit).CallEnemy = false;
                    Status = true;
                }
            }

            if (this.Scale >=0 &&Status)
            {
                this.Scale -= 0.02f;
                this.Rotation--;
                batch.Draw(this.BlackHole, base.Locate + new Vector2(60, 0), new Rectangle(0, 0, 80, 80), Color.AliceBlue, this.Rotation, new Vector2(40), this.Scale * m_worldManager.Scale, SpriteEffects.None, 0);
            }
            else
            {
                Status = false;
            }
        }
    }
}

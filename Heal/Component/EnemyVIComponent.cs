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
    internal class EnemyVIComponent : UnitComponent
    {
        internal void AddToIron(string left,String right, int oSize, float time)
        {
            Texture2D leftT = DataReader.Load<Texture2D>("Texture/Entities/" + left);
            Texture2D rightT = DataReader.Load<Texture2D>("Texture/Entities/" + right);
            int size = leftT.Height;
            int picCount = leftT.Width / size;
            ((EnemyTypeVI)m_unit).AddToIronList(leftT,rightT, picCount, size, oSize ,time);
        }

        internal void AddIronFace(String left,String right)
        {
            ((EnemyTypeVI)m_unit).AddIronFace(DataReader.Load<Texture2D>("Texture/Entities/" + left),
                                             DataReader.Load<Texture2D>("Texture/Entities/" + right));
        }

        protected override void InternalDraw(Microsoft.Xna.Framework.GameTime gameTime, Microsoft.Xna.Framework.Graphics.SpriteBatch batch)
        {
            base.InternalDraw(gameTime, batch);

            if(((EnemyTypeVI)m_unit).HP == 2)
            {
                if (((EnemyTypeVI)m_unit).Face == AIBase.FaceSide.Right && ((EnemyTypeVI)m_unit).Status != AIBase.Status.Turning)
                {
                    batch.Draw(((EnemyTypeVI)m_unit).RightIron, base.Locate,new Rectangle(0,0,80,80), Color.AliceBlue, base.Rotation,new Vector2(40), m_unit.EnemySize * m_worldManager.Scale, SpriteEffects.None, 0);
                }

                if(((EnemyTypeVI)m_unit).Face == AIBase.FaceSide.Left && ((EnemyTypeVI)m_unit).Status != AIBase.Status.Turning)
                {
                    batch.Draw(((EnemyTypeVI)m_unit).LeftIron, base.Locate, new Rectangle(0,0, 80, 80), Color.AliceBlue, base.Rotation, new Vector2(40), m_unit.EnemySize * m_worldManager.Scale, SpriteEffects.None, 0);
                }

                if(((EnemyTypeVI)m_unit).Status == AIBase.Status.Turning)
                {
                    if(((EnemyTypeVI)m_unit).Face == AIBase.FaceSide.Right)
                    {
                        foreach (var ptr in ((EnemyTypeVI)m_unit).TurnLeftIron)
                        {
                            batch.Draw(ptr.Target, base.Locate, ptr.Frame, ptr.Color, base.Rotation,
                                        new Vector2((float)ptr.Frame.Height / 2), m_unit.EnemySize * m_worldManager.Scale, SpriteEffects.None, 0);
                        }
                    }
                    else
                    {
                        foreach (var ptr in ((EnemyTypeVI)m_unit).TurnRightIron)
                        {
                            batch.Draw(ptr.Target, base.Locate, ptr.Frame, ptr.Color, base.Rotation,
                                        new Vector2((float)ptr.Frame.Height / 2), m_unit.EnemySize * m_worldManager.Scale, SpriteEffects.None, 0);
                        }
                    }
                }
            }
        }
    }
}

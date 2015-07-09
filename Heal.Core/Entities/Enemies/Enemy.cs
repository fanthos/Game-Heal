using System;
using Heal.Core.AI;
using Microsoft.Xna.Framework;

namespace Heal.Core.Entities.Enemies
{
    /// <summary>
    /// Class for emeny unit information and ai info
    /// </summary>
    public abstract class Enemy : Unit
    {
        public static float AlarmRange = 300;
        /// <summary>
        /// Initializes a new instance of the <see cref="Emeny"/> class.
        /// </summary>
        /// <param name="sprite">The ISprite class</param>
        /// <param name="speed">The speed.</param>
        /// <param name="locate">The locate.</param>
        /// <param name="ringSize">Size of the ring.</param>
        /// <param name="enemySize">Size of the enemy.</param>
        /// <param name="face">The face.</param>
        /// <param name="id">The id.</param>
        /// <param name="timer">The timer.</param>
        protected Enemy(object sprite, Vector2 speed, Vector2 locate, float ringSize, float enemySize, float enemySize2, AIBase.FaceSide face, AIBase.ID id)
            : base(sprite, speed, locate, ringSize, enemySize, enemySize2, face, id)
        {

        }

        public static bool Detected(Enemy enemy)
        {
            if ((AIBase.Player.Locate - enemy.Locate).Length() < Enemy.AlarmRange) // 如果在警戒范围之内
            {
                if (AIBase.Player.Face == AIBase.FaceSide.Left) //人物朝左
                {
                    if (enemy.Face == AIBase.FaceSide.Left) // 敌人朝左
                    {
                        if (AIBase.Player.Locate.X < enemy.Locate.X) //人物在左
                        {
                            return true;
                        }
                        else //人物在右
                        {
                            return false;
                        }
                    }
                    else // 敌人朝右
                    {
                        if (AIBase.Player.Locate.X < enemy.Locate.X) // 人物在左
                        {
                            return false;
                        }
                        else // 人物在右
                        {
                            return true;
                        }
                    }
                }
                else //人物朝右
                {
                    if (enemy.Face == AIBase.FaceSide.Left) // 敌人朝左
                    {
                        if (AIBase.Player.Locate.X < enemy.Locate.X) // 人物在左
                        {
                            return true;
                        }
                        else // 人物在右
                        {
                            return false;
                        }
                    }
                    else // 敌人朝右
                    {
                        if (AIBase.Player.Locate.X < enemy.Locate.X) // 人物在左
                        {
                            return false;
                        }
                        else // 人物在右
                        {
                            return true;
                        }
                    }
                }
            }
            else
            {
                return false;
            }
        }

        
    }
}



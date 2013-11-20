using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Heal.Core.AI;
using Microsoft.Xna.Framework;

namespace Heal.Core.Entities.Enemies
{
    public abstract class EnemyTypeBoss : Enemy
    {
        public AIBase.BossID Name;

        public EnemyTypeBoss(object sprite, Vector2 speed, Vector2 locate, float ringSize, float enemySize2, float enemySize, AIBase.FaceSide face, AIBase.ID id)
            : base(sprite, speed, locate, ringSize, enemySize, enemySize2, face, id)
        {
        }
    }
}

using System.Collections.Generic;
using Heal.Core.Entities.Enemies;
using Microsoft.Xna.Framework;
using Heal.Core.Entities;

namespace Heal.Core.AI
{
    public class AIControler
    {
        public static bool IsDead;

        public static void Update(GameTime gameTime, IList<Unit> list, IList<Item> i_list, Player player)
        {
            AIBase.Player = player;
            AIBase.EnemyList = list;
            AIBase.ItemList = i_list;

            
            
            Unit[] units = new Unit[list.Count];
            list.CopyTo(units,0);
            foreach (var ptr in units)
            {
                switch (ptr.Status)
                {
                    case AIBase.Status.Normal:
                        {
                            AINormal.NormalBehave(ptr, gameTime);
                            break;
                        }
                    case AIBase.Status.Turning:
                        {
                            AITurning.AITurn(ptr);
                            break;
                        }
                    case AIBase.Status.Chase:
                        {
                            AIChase.Chase(ptr, gameTime);
                            break;
                        }
                    case AIBase.Status.Attck:
                        {
                            AIAttack.Attack(ptr, gameTime);
                            break;
                        }
                    case AIBase.Status.Dead:
                        {
                            AIDead.Dead(ptr, gameTime);
                            break;
                        }
                    case AIBase.Status.Freeze:
                        {
                            AIFreeze.Freeze(ptr, gameTime);
                            break;
                        }
                    case AIBase.Status.Flee:
                        {
                            AIFlee.Flee(ptr, gameTime);
                            break;
                        }
                    case AIBase.Status.Alarm:
                        {
                            AIAlarm.Alarm(ptr, gameTime);
                            break;
                        }
                    case AIBase.Status.Judge:
                        {
                            AIJudge.Judge(ptr,gameTime);
                            break;
                        }
                    case AIBase.Status.Flat:
                        {
                            AIFlat.Flat(ptr,gameTime);
                            break;
                        }
                    case AIBase.Status.Kick:
                        {
                            AIKick.Kick(ptr,gameTime);
                            break;
                        }

                }
                ptr.Update(gameTime);
            }

            foreach (var ptr in units)
            {
                if (ptr.Status == AIBase.Status.Dead) continue;
                ptr.ToTurnStatus();
                if (!(ptr is Player))
                {
                    if (Enemy.Detected((Enemy) ptr) && AIBase.Player.FakeID != ptr.Id) //如果被看到
                    {
                        if (ptr.Status == AIBase.Status.Normal)
                        {
                            ptr.Status = AIBase.Status.Alarm;
                            ptr.CurrentDialog = AIBase.StatusDialog.Alarm;
                            ptr.CurrentRing = 3;
                        }
                    }
                    ptr.ToNormalStatus(gameTime, player);
                    ptr.ToFreezeStatus(gameTime, player);
                    ptr.ToAttackStatus(gameTime, player);
                }
                ptr.ToDeadStatus(gameTime, player);
                


            }

            foreach (var ptr in i_list)
            {
                ptr.Update(gameTime);
            }

            IsDead = (player.Status == AIBase.Status.Dead);
        }
    }
}
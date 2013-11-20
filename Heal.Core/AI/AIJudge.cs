using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Heal.Core.Entities;
using Heal.Core.Entities.Enemies;
using Microsoft.Xna.Framework;

namespace Heal.Core.AI
{
    public class AIJudge
    {
        public static void Judge(Unit ptr, GameTime gameTime)
        {
            switch (ptr.Id)
            {
                case AIBase.ID.I:
                    {
                        var ptr1 = (EnemyTypeI) ptr;

                        if(ptr1.RingSize >= AIBase.Player.RingSize)
                        {
                            ptr1.CurrentRing = 1;
                            ptr1.CurrentDialog = AIBase.StatusDialog.StrongerThanPlayer;
                            ptr1.Status = AIBase.Status.Chase;
                        }
                        else
                        {
                            ptr1.Speed.X = -ptr1.Speed.X;
                            ptr1.Speed.Y = 0;
                            ptr1.CurrentRing = 2;
                            ptr1.CurrentDialog = AIBase.StatusDialog.WeakerThanPlayer;
                            ptr1.Status = AIBase.Status.Flee;
                        }

                        break;
                    }
                case AIBase.ID.II:
                    {
                        var ptr2 = (EnemyTypeII)ptr;

                        if (ptr2.RingSize >= AIBase.Player.RingSize)
                        {
                            ptr2.CurrentRing = 1;
                            ptr2.CurrentDialog = AIBase.StatusDialog.StrongerThanPlayer;
                            ptr2.Status = AIBase.Status.Chase;
                        }
                        else
                        {
                            ptr2.Speed.X = -ptr2.Speed.X;
                            ptr2.Speed.Y = 0;
                            ptr2.CurrentRing = 2;
                            ptr2.CurrentDialog = AIBase.StatusDialog.WeakerThanPlayer;
                            ptr2.Status = AIBase.Status.Flee;
                            ptr2.Find = false;
                        }

                        break;
                    }
                case AIBase.ID.VI:
                    {
                        var ptr1 = (EnemyTypeVI)ptr;

                        if (ptr1.RingSize >= AIBase.Player.RingSize)
                        {
                            ptr1.CurrentRing = 1;
                            ptr1.CurrentDialog = AIBase.StatusDialog.StrongerThanPlayer;
                            ptr1.Status = AIBase.Status.Chase;
                        }
                        else
                        {
                            ptr1.Speed.X = -ptr1.Speed.X;
                            ptr1.Speed.Y = 0;
                            ptr1.CurrentRing = 2;
                            ptr1.CurrentDialog = AIBase.StatusDialog.WeakerThanPlayer;
                            ptr1.Status = AIBase.Status.Flee;
                        }

                        break;
                    }
                case AIBase.ID.V:
                    {
                        var ptrG = (Ghost) ptr;


                        if (ptrG.RingSize >= AIBase.Player.RingSize)
                        {
                            ptrG.CurrentRing = 1;
                            ptrG.Status = AIBase.Status.Chase;
                        }
                        else
                        {
                            ptrG.Speed.X = -ptrG.Speed.X;
                            ptrG.Speed.Y = 0;
                            ptrG.CurrentRing = 2;
                            ptrG.Status = AIBase.Status.Flee;
                        }

                        break;
                    }
            }
        }
    }
}

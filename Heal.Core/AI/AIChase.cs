using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Heal.Core.Entities;
using Heal.Core.Entities.Enemies;
using Heal.Core.Utilities;
using Microsoft.Xna.Framework;

namespace Heal.Core.AI
{
    public class AIChase
    {
        public static void Chase(Unit ptr, GameTime gameTime)
        {
            switch (ptr.Id)
            {
                case AIBase.ID.Zero:
                    {
                        break;
                    }
                case AIBase.ID.Boss:
                    {

                        var ptrB = (EnemyTypeBoss)ptr;
                        switch (ptrB.Name)
                        {
                            case AIBase.BossID.SmokeBoss:
                                {
                                    var ptrS = (SmokeBoss)ptrB;

                                    break;
                                }
                        }
                        break;
                    }
                case AIBase.ID.I:
                    {
                        var ptr1 = (EnemyTypeI)ptr;
                        if (ptr1.RingSize >= AIBase.Player.RingSize) 
                        {
                            ptr1.Speed = AIBase.Player.Locate - ptr1.Locate;
                            ptr1.Speed.Length = ptr1.Speed.Length/3+ 200 ;
                            ptr1.Postion = AIBase.SenceManager.MoveTest(ptr1.Locate, ptr1.Speed,
                                                                        (float) gameTime.ElapsedGameTime.TotalSeconds);
                            ptr1.LocateConfirm();
                        }
                        else
                        {
                            ptr1.CurrentRing = 2;
                            ptr1.Speed.X = -ptr1.Speed.X;
                            ptr1.Speed.Y = 0;
                            ptr1.Status = AIBase.Status.Flee;
                        }
                        break;
                    }
                case AIBase.ID.II:
                    {
                        var ptr2 = (EnemyTypeII)ptr;
                        if (ptr2.RingSize >= AIBase.Player.RingSize)
                        {
                            ptr2.Speed = AIBase.Player.Locate - ptr2.Locate;
                            ptr2.Speed.Length = ptr2.Speed.Length / 3 + 200;
                            ptr2.Postion = AIBase.SenceManager.MoveTest(ptr2.Locate, ptr2.Speed,
                                                                        (float)gameTime.ElapsedGameTime.TotalSeconds);
                            ptr2.LocateConfirm();
                        }
                        else
                        {
                            ptr2.CurrentRing = 2;
                            ptr2.Speed.X = -ptr2.Speed.X;
                            ptr2.Speed.Y = 0;
                            ptr2.Status = AIBase.Status.Flee;
                        }
                        break;
                    }

                case AIBase.ID.VI:
                    {
                        var ptr6 = (EnemyTypeVI)ptr;
                        if (ptr6.RingSize >= AIBase.Player.RingSize)
                        {
                            ptr6.Speed = AIBase.Player.Locate - ptr6.Locate;
                            ptr6.Speed.Length = ptr6.Speed.Length/3 + 180f;
                            ptr6.Postion = AIBase.SenceManager.MoveTest(ptr6.Locate, ptr6.Speed,
                                                                        (float)gameTime.ElapsedGameTime.TotalSeconds);
                            ptr6.LocateConfirm();
                        }
                        else
                        {
                            ptr6.CurrentRing = 2;
                            ptr6.Speed.X = -ptr6.Speed.X;
                            ptr6.Speed.Y = 0;
                            ptr6.Status = AIBase.Status.Flee;
                        }
                        break;
                    }

                case AIBase.ID.V:
                    {
                        var ptrG = (Ghost)ptr;
                        if (ptrG.RingSize >= AIBase.Player.RingSize)
                        {
                            ptrG.CurrentDialog = AIBase.StatusDialog.StrongerThanPlayer;
                            ptrG.Speed = AIBase.Player.Locate - ptrG.Locate;
                            ptrG.Postion = AIBase.SenceManager.MoveTest(ptrG.Locate, ptrG.Speed,
                                                                        (float)gameTime.ElapsedGameTime.TotalSeconds);
                            ptrG.LocateConfirm();
                        }
                        else
                        {
                            ptrG.CurrentRing = 2;
                            ptrG.Speed.X = -ptrG.Speed.X;
                            ptrG.Speed.Y = 0;
                            ptrG.Status = AIBase.Status.Flee;
                        }
                        break;
                    }
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Heal.Core.Entities;
using Heal.Core.Entities.Enemies;
using Microsoft.Xna.Framework;

namespace Heal.Core.AI
{
    public class AIAlarm
    {
        public static void Alarm(Unit ptr, GameTime gameTime)
        {
            switch (ptr.Id)
            {
                case AIBase.ID.Boss:
                    {
                        var ptrB = (EnemyTypeBoss) ptr;
                        break;
                    }
                case AIBase.ID.I:
                    {
                        var ptr1 = (EnemyTypeI) ptr;
                        if ((ptr1.Locate - AIBase.Player.Locate).Length() >
                            (ptr1.RingSize + AIBase.Player.RingSize)/2 - 100)
                        {
                            ptr1.Speed = AIBase.Player.Locate - ptr1.Locate;
                            ptr1.Speed.Length = ptr1.Speed.Length/3 + 180;
                            ptr1.Postion = AIBase.SenceManager.MoveTest(ptr1.Locate, ptr1.Speed,
                                                                        (float) gameTime.ElapsedGameTime.TotalSeconds);
                            ptr1.LocateConfirm();
                        }
                        else
                        {
                            ptr1.Status = AIBase.Status.Judge;
                        }

                        break;
                    }
                    
                case AIBase.ID.II:
                    {
                        var ptr2 = (EnemyTypeII) ptr;
                        if ((ptr2.Locate - AIBase.Player.Locate).Length() >
                            (ptr2.RingSize + AIBase.Player.RingSize) / 2 - 80)
                        {
                            ptr2.Speed = AIBase.Player.Locate - ptr2.Locate;
                            ptr2.Speed.Length = ptr2.Speed.Length / 3 + 180;
                            ptr2.Postion = AIBase.SenceManager.MoveTest(ptr2.Locate, ptr2.Speed,
                                                                        (float)gameTime.ElapsedGameTime.TotalSeconds);
                            ptr2.LocateConfirm();
                        }
                        else
                        {
                            ptr2.Status = AIBase.Status.Judge;
                        }
                        break;
                    }
                case AIBase.ID.IV:
                    {
                        var ptr4 = (EnemyTypeIV)ptr;
                        switch (ptr4.Status4)
                        {
                            case AIBase.StatusIV.Sleep:
                                if ((AIBase.Player.Locate - ptr4.Locate).Length() <= ptr4.RingSize / 3 + AIBase.Player.RingSize / 3)
                                {
                                    ptr4.Status4 = AIBase.StatusIV.Alarm;
                                    ptr4.Face = AIBase.FaceSide.Right;
                                    ptr4.CallEnemy = true;
                                    ptr4.CurrentRing = 3;
                                    ptr4.CurrentDialog = AIBase.StatusDialog.Alarm;
                                }
                                else
                                {
                                    ptr4.RingSize++;
                                    ptr4.RingSize %= 300;
                                    ptr4.CurrentDialog = AIBase.StatusDialog.Blank;
                                }
                                break;
                            case AIBase.StatusIV.Alarm:
                                {
                                    if(ptr4.CoolDownTime <= 180)
                                    {
                                        ptr4.CoolDownTime++;
                                        ptr4.CurrentDialog = AIBase.StatusDialog.Alarm;
                                    }
                                    else
                                    {
                                        ptr4.Status4 = AIBase.StatusIV.Sleep;
                                        ptr4.Face = AIBase.FaceSide.Left;
                                        ptr4.CurrentRing = 0;
                                        ptr4.CurrentDialog = AIBase.StatusDialog.Blank;
                                        ptr4.CoolDownTime = 0;
                                    }
                                }
                                break;
                        }
                        break;
                    }

                case AIBase.ID.VI:
                    {
                        var ptr6 = (EnemyTypeVI)ptr;
                        if ((ptr6.Locate - AIBase.Player.Locate).Length() > (ptr6.RingSize + AIBase.Player.RingSize) / 2 - 100)
                        {
                            ptr6.Speed = AIBase.Player.Locate - ptr6.Locate;
                            ptr6.Postion = AIBase.SenceManager.MoveTest(ptr6.Locate, ptr6.Speed,
                                                                        (float)gameTime.ElapsedGameTime.TotalSeconds);
                            ptr6.LocateConfirm();
                        }
                        else
                        {
                            ptr6.Status = AIBase.Status.Judge;
                        }

                        break;
                    }

                case AIBase.ID.III:
                    {
                        ptr.Status = AIBase.Status.Attck;
                        break;
                    }
               
                case AIBase.ID.V:
                    {
                        var ptrG = (Ghost)ptr;
                        if ((ptrG.Locate - AIBase.Player.Locate).Length() > (ptrG.RingSize + AIBase.Player.RingSize) / 2 - 80)
                        {
                            ptrG.Speed = AIBase.Player.Locate - ptrG.Locate;
                            ptrG.Postion = AIBase.SenceManager.MoveTest(ptrG.Locate, ptrG.Speed,
                                                                        (float)gameTime.ElapsedGameTime.TotalSeconds);
                            ptrG.LocateConfirm();
                        }
                        else
                        {
                            ptrG.Status = AIBase.Status.Judge;
                        }
                        break;
                    }
            }
        }
    }
}

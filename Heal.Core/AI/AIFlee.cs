using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Heal.Core.Entities;
using Heal.Core.Entities.Enemies;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Heal.Core.AI
{
    public class AIFlee
    {
        public static void Flee(Unit ptr, GameTime gameTime)
        {
            switch (ptr.Id)
            {
                case AIBase.ID.Boss:
                    {
                        var ptrB = (EnemyTypeBoss)ptr;

                        switch (ptrB.Name)
                        {
                            case AIBase.BossID.StoneBoss:
                                {
                                    var ptrS = (StoneBoss)ptrB;

                                    break;
                                }
                        }
                        break;
                    }
                case AIBase.ID.I:
                    {
                        var ptr1 = (EnemyTypeI)ptr;
                        ptr1.PostionGenerate(gameTime);
                        if (ptr1.CurrentFlee <= EnemyTypeI.FleeRange
                            && !AIBase.SenceManager.IntersectPixels(ptr1.Postion, 30))
                        {
                            ptr1.CurrentFlee++;
                            ptr1.LocateConfirm();
                        }
                        else
                        {
                            ptr1.Status = AIBase.Status.Normal;
                            ptr1.CurrentDialog = AIBase.StatusDialog.Blank;
                            ptr1.CurrentRing = 0;
                            ptr1.Speed = ptr1.DefaultSpeed;
                            ptr1.CurrentFlee = 0;
                        }


                        break;
                    }

                case AIBase.ID.II:
                    {
                        var ptr2 = (EnemyTypeII)ptr;

                        foreach (var ptrE in AIBase.EnemyList)
                        {
                            if (ptrE.Id == AIBase.ID.II &&
                                (ptrE.Locate - ptr2.Locate).Length() <= 300 &&
                                ptrE != ptr2
                                &&!ptr2.BeCatch
                                &&!((EnemyTypeII)ptrE).BeCatch
                                &&!((EnemyTypeII)ptrE).IsLead
                                && ptr2.CombineList.Count<2)
                            {

                                ptr2.Speed = ptrE.Locate - ptr2.Locate;
                                ptr2.Speed.Length = ptr2.Speed.Length / 3 ;
                                ptr2.Postion = AIBase.SenceManager.MoveTest(ptr2.Locate, ptr2.Speed,(float)gameTime.ElapsedGameTime.TotalSeconds);
                                ptr2.LocateConfirm();

                                if ((ptrE.Locate - ptr2.Locate).Length() <= 40)
                                {
                                    ((EnemyTypeII)ptrE).BeCatch = true;
                                    ptr2.IsLead = true;
                                    ptr2.CombineList.Add((EnemyTypeII) ptrE);
                                    AIBase.EnemyList.Remove(ptrE);
                                    ptr2.Find = true;
                                    ptr2.Status = AIBase.Status.Alarm;
                                    ptr2.RingSize += 50;
                                    ptr2.CurrentRing = 3;
                                }
                                break;
                            }
                        }

                        if (!ptr2.Find)
                        {
                            ptr2.PostionGenerate(gameTime);
                            if (ptr2.CurrentFlee <= EnemyTypeI.FleeRange
                                && !AIBase.SenceManager.IntersectPixels(ptr2.Postion, 30))
                            {
                                ptr2.CurrentFlee++;
                                ptr2.LocateConfirm();
                            }
                            else
                            {
                                ptr2.Status = AIBase.Status.Normal;
                                ptr2.CurrentDialog = AIBase.StatusDialog.Blank;
                                ptr2.CurrentRing = 0;
                                ptr2.Speed = ptr2.DefaultSpeed;
                                ptr2.CurrentFlee = 0;
                            }
                        }
                        break;
                    }
                case AIBase.ID.VI:
                    {
                        var ptr6 = (EnemyTypeVI)ptr;
                        ptr6.PostionGenerate(gameTime);
                        if (ptr6.CurrentFlee <= EnemyTypeI.FleeRange
                            && !AIBase.SenceManager.IntersectPixels(ptr6.Postion, 30))
                        {
                            ptr6.CurrentFlee++;
                            ptr6.LocateConfirm();
                        }
                        else
                        {
                            ptr6.Status = AIBase.Status.Normal;
                            ptr6.CurrentDialog = AIBase.StatusDialog.Blank;
                            ptr6.CurrentRing = 0;
                            ptr6.Speed = ptr6.DefaultSpeed;
                            ptr6.CurrentFlee = 0;
                        }

                        break;
                    }
                case AIBase.ID.V:
                    {
                        var ptrG = (Ghost)ptr;
                        ptrG.PostionGenerate(gameTime);
                        if (ptrG.CurrentFlee <= Ghost.FleeRange
                            && !AIBase.SenceManager.IntersectPixels(ptrG.Postion, 30))
                        {
                            ptrG.CurrentFlee++;
                            ptrG.LocateConfirm();
                            ptrG.CurrentDialog = AIBase.StatusDialog.WeakerThanPlayer;
                            if(ptrG.Color.A !=0)
                            {
                                ptrG.Color.A-= 15;
                            }
                            else
                            {
                                ptrG.Color.A = 0;
                            }
                        }
                        else
                        {
                            ptrG.Status = AIBase.Status.Normal;
                            ptrG.CurrentRing = 0;
                            ptrG.Speed = ptrG.DefaultSpeed;

                            ptrG.CurrentDialog = AIBase.StatusDialog.Blank;
                            ptrG.CurrentFlee = 0;
                            ptrG.Color.A = 255;
                        }
                        break;
                    }
            }

        }
    }
}

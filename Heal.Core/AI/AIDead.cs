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
    public class AIDead
    {
        public static void Dead(Unit ptr, GameTime gameTime)
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

                                    if (ptrS.CurrentShine <= StoneBoss.ShineTime)
                                    {
                                        if (ptrS.CurrentShine <= 45)
                                            ptrS.StoList[1].Color.A -= 5;
                                        else if (ptrS.CurrentShine <= 90)
                                            ptrS.StoList[1].Color.A += 5;
                                        else if (ptrS.CurrentShine <= 120)
                                        {
                                            if (ptrS.CurrentShine % 2 == 0)
                                                ptrS.StoList[1].Color.A = 255;
                                            else
                                                ptrS.StoList[1].Color.A = 0;
                                        }
                                        ptrS.CurrentShine++;
                                    }
                                    else
                                    {
                                        ptrS.StoList[1].Color.A = 0;
                                        if (ptrS.CurrentDown <= StoneBoss.DownRange)
                                        {
                                            ptrS.PostionGenerate(gameTime);
                                            ptrS.Postion.Y += 2;
                                            ptrS.LocateConfirm();
                                            ptrS.CurrentDown++;
                                        }

                                        else if (ptrS.CurrentRoll <= StoneBoss.RollRange)
                                        {
                                            ptrS.PostionGenerate(gameTime);
                                            ptrS.Postion.X -= 1.6f;
                                            ptrS.Postion.Y += 0.5f;
                                            ptrS.LocateConfirm();
                                            ptrS.StoList[0].Rotation -= 0.003f;
                                            ptrS.StoList[1].Rotation -= 0.003f;
                                            ptrS.CurrentRoll++;
                                        }
                                    }


                                    break;
                                }
                        }
                        break;
                    }
                case AIBase.ID.Zero:
                    {
                        var ptrP = (Player) ptr;

                        
                        ptrP.Speed = new Speed(0,0);

                        break;
                    }
                case AIBase.ID.I:
                    {
                        var ptr1 = (EnemyTypeI)ptr;
                       
                        ptr1.CurrentDialog = AIBase.StatusDialog.Blank;
                        foreach (var pptr in ptr1.List)
                        {
                            if (pptr.Color.A != 0)
                            {
                                pptr.Color.A -= 15;
                            }
                        }

                        foreach (var pptr in ptr1.ListRing)
                        {
                            if (pptr.Color.A != 0)
                            {
                                pptr.Color.A -= 15;
                            }
                        }

                        if (ptr1.StatusColor.A != 0)
                        {
                            ptr1.StatusColor.A -= 15;
                        }
                        break;
                    }
                case AIBase.ID.II:
                    {
                        var ptr2 = (EnemyTypeII)ptr;
                       
                        ptr2.CurrentDialog = AIBase.StatusDialog.Blank;
                        foreach (var pptr in ptr2.List)
                        {
                            if (pptr.Color.A != 0)
                            {
                                pptr.Color.A -= 15;
                            }
                        }

                        foreach (var pptr in ptr2.ListRing)
                        {
                            if (pptr.Color.A != 0)
                            {
                                pptr.Color.A -= 15;
                            }
                        }

                            if (ptr2.Color.A != 0)
                            {
                                ptr2.Color.A -= 15;
                            }
                        
                        if (ptr2.StatusColor.A != 0)
                        {
                            ptr2.StatusColor.A -= 15;
                        }

                        ptr2.Rotate++;
                        break;
                    }
                case AIBase.ID.VI:
                    {
                        var ptr1 = (EnemyTypeVI)ptr;


                        ptr1.CurrentDialog = AIBase.StatusDialog.Blank;
                        foreach (var pptr in ptr1.List)
                        {
                            if (pptr.Color.A != 0)
                            {
                                pptr.Color.A -= 15;
                            }
                        }

                        foreach (var pptr in ptr1.ListRing)
                        {
                            if (pptr.Color.A != 0)
                            {
                                pptr.Color.A -= 15;
                            }
                        }

                        if (ptr1.StatusColor.A != 0)
                        {
                            ptr1.StatusColor.A -= 15;
                        }
                        break;
                    }
                case AIBase.ID.V:
                    {
                        var ptrG = (Ghost)ptr;

                        ptrG.CurrentDialog = AIBase.StatusDialog.Blank;
                        if (ptrG.CurrentFlashTime != Ghost.FlashTime)
                        {
                            if (ptrG.CurrentFlashTime % 5 == 0)
                                ptrG.Color.A = 100;
                            else
                                ptrG.Color.A = 200;
                            ptrG.CurrentFlashTime++;
                        }
                        else
                        {
                            ptrG.RingSize = 0;
                            ptrG.Color.A = 0;
                        }
                        break;
                    }
            }
        }
    }
}

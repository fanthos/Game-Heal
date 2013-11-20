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
    public class AIFreeze
    {
        public static void Freeze(Unit ptr, GameTime gameTime)
        {
            switch (ptr.Id)
            {

                case AIBase.ID.Zero:
                    {
                        var ptrP = (Player)ptr;

                        if (ptrP.FreezeTimeNow <= Unit.FreezeTime)
                        {
                            ptrP.Speed.Length = 200;
                            ptrP.Speed.Radian = -ptrP.BeAttackedRadian + MathHelper.Pi;
                            ptrP.PostionGenerate(gameTime);
                            ptrP.CurrentDialog = AIBase.StatusDialog.Ahhh;
                            ptrP.FakeID = AIBase.ID.Zero;
                            if(!AIBase.SenceManager.IntersectPixels(ptrP.Postion,(int)(ptrP.EnemySize + 20)))
                            {
                                ptrP.LocateConfirm();
                            }

                            ptrP.FreezeTimeNow++;
                            ptrP.Rotate += 1.2f;
                        }
                        else
                        {
                            ptrP.FreezeTimeNow = 0;
                            ptrP.Status = AIBase.Status.Normal;
                            ptrP.CurrentDialog = AIBase.StatusDialog.Blank;
                            ptrP.HP--;
                        }
/*
                        Console.WriteLine(ptrP.Speed.ToString() + ptrP.Postion + ptrP.Locate);
                        if (ptrP.BeAttackedSide == AIBase.FaceSide.Right)
                        {
                            ptrP.Speed.Radian = -MathHelper.PiOver2;
                        }
                        else
                        {
                            ptrP.Speed.Radian = MathHelper.PiOver2;
                        }
                        ptrP.PostionGenerate(gameTime);
                        Console.WriteLine(ptrP.Speed.ToString() + ptrP.Postion + ptrP.Locate);

                        //没撞墙
                        if (!AIBase.SenceManager.IntersectPixels(ptrP.Postion, 30) &&
                            //没飞的足够远
                            ptrP.CurrentFly < Unit.FlyDistance)
                        {
                            ptrP.LocateConfirm();
                            ptrP.Rotate = MathTools.RandomGene();
                            ptrP.CurrentFly += 5;
                        }
                        else
                        {
                            ptrP.Speed.X = -ptrP.Speed.X;
                            ptrP.Rotate = 0;
                            ptrP.CurrentFly = 0;
                            ptrP.HP--;
                            ptrP.Status = ptrP.LastStatus;
                        }
                        */
                        break;
                    }

                case AIBase.ID.Boss:
                    {
                        var ptrB = (EnemyTypeBoss)ptr;

                        switch (ptrB.Name)
                        {
                            case AIBase.BossID.StoneBoss:
                                {
                                    var ptrS = (StoneBoss) ptrB;

                                    if(ptrS.CurrentBack < StoneBoss.BackRange)
                                    {
                                        ptrS.CurrentBack++;
                                        ptrS.PostionGenerate(gameTime);
                                        ptrS.Postion.X += 2;
                                        ptrS.LocateConfirm();
                                    }
                                    else
                                    {
                                        ptrS.Status = AIBase.Status.Normal;
                                    }

                                    break;
                                }
                        }

                        break;
                    }
                case AIBase.ID.I:
                    {
                        var ptr1 = (EnemyTypeI)ptr;

                        if (ptr1.CoolDownTime < Unit.FreezeTime)
                        {
                            ptr1.CoolDownTime++;
                        }
                        else
                        {
                            ptr1.Rotate = 0;
                            ptr1.CoolDownTime = 0;
                            ptr1.Status = AIBase.Status.Normal;
                            ptr1.LastStatus = AIBase.Status.Blank;
                        }
                        break;
                    }
                case AIBase.ID.VI:
                    {
                        var ptr6 = (EnemyTypeVI)ptr;

                        if (ptr6.CoolDownTime < Unit.FreezeTime)
                        {
                            ptr6.CoolDownTime++;
                        }
                        else
                        {
                            ptr6.Rotate = 0;
                            ptr6.CoolDownTime = 0;
                            ptr6.Status = AIBase.Status.Normal;
                            ptr6.LastStatus = AIBase.Status.Blank;
                        }
                        break;
                    }
            }
        }
    }
}

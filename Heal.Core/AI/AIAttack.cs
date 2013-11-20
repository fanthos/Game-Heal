using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Heal.Core.Entities;
using Heal.Core.Entities.Enemies;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Heal.Core.Utilities;

namespace Heal.Core.AI
{
    public class AIAttack
    {
        public static void Attack(Unit ptr, GameTime gameTime)
        {
            switch (ptr.Id)
            {
                case AIBase.ID.Zero:
                    {
                        var ptrP = (Player)ptr;
                        ptrP.PostionGenerate(gameTime);

                        if (ptrP.AttackIndex < ptrP.AttackRange.Length - 1)
                        {
                            ptrP.AttackIndex++;
                            ptrP.CurrentDialog = AIBase.StatusDialog.Kakaka;
                            ptrP.FakeID = AIBase.ID.Zero;

                            //if (ptrP.Face == AIBase.FaceSide.Right)
                            {
                                ptrP.Postion += CoreUtilities.GetVector( ptrP.AttackRange[ptrP.AttackIndex], -ptrP.Speed.Radian);
                            }
                            //else
                            {
                                //ptrP.Postion.X += -ptrP.AttackRange[ptrP.AttackIndex];
                            }
                            if (!AIBase.SenceManager.IntersectPixels(ptrP.Postion, 30))
                            {
                                ptrP.LocateConfirm();
                            }
                        }
                        else
                        {
                            ptrP.CurrentDialog = AIBase.StatusDialog.Blank;
                            if (ptrP.FreezeTimeNow <= Player.FreezeTime)
                            {
                                ptrP.FreezeTimeNow++;
                            }
                            else
                            {
                                ptrP.AttackIndex = 0;
                                ptrP.Status = ptrP.LastStatus;
                                ptrP.FreezeTimeNow = 0;
                            }
                        }
                        break;
                    }
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
                        if (ptr1.AttackIndex < ptr1.AttackRange.Length - 1)
                        {
                            ptr1.AttackIndex++;
                            ptr1.CurrentRing = 2;

                            //if (ptr1.Face == AIBase.FaceSide.Right)
                            {
                                ptr1.Postion += CoreUtilities.GetVector(ptr1.AttackRange[ptr1.AttackIndex], ptr1.Speed.Radian);
                                //ptr1.Postion.X += ptr1.AttackRange[ptr1.AttackIndex];
                            }
                            //else
                            {
                                //ptr1.Postion.X += -ptr1.AttackRange[ptr1.AttackIndex];
                            }
                            if (!AIBase.SenceManager.IntersectPixels(ptr1.Postion, (int)(ptr1.EnemySize * ptr1.EnemySize2 * 40)))
                            {
                                ptr1.LocateConfirm();
                            }

                            if ((AIBase.Player.Locate - ptr1.Locate).Length() <= 20)
                            {
                                AIBase.Player.Status = AIBase.Status.Freeze;
                                AIBase.Player.BeAttackedRadian = ptr1.Speed.Radian;
                            }
                        }
                        else
                        {
                            if (ptr.CoolDownTime <= Unit.FreezeTime)
                            {
                                ptr1.CoolDownTime++;
                            }
                            else
                            {
                                ptr1.AttackIndex = 0;
                                ptr1.CurrentRing = 1;
                                ptr1.Status = AIBase.Status.Chase;
                                ptr1.CoolDownTime = 0;
                            }
                        }
                        break;
                    }

                case AIBase.ID.II:
                    {
                        var ptr2 = (EnemyTypeII)ptr;

                        if (ptr2.AttackTime < 120)
                        {
                            foreach (var ptrr in ptr2.Sharp)
                            {
                                ptrr.Update(gameTime);
                            }
                            ptr2.AttackTime++;

                            if((ptr2.Locate - AIBase.Player.Locate).Length()< 30)
                            {
                                AIBase.Player.BeAttackedRadian = ptr2.Speed.Radian;
                                AIBase.Player.Status = AIBase.Status.Freeze;
                            }
                        }
                        else
                        {
                            ptr2.CurrentRing = 3;
                            ptr2.AttackTime = 0;
                            ptr2.CurrentDialog = AIBase.StatusDialog.Alarm;
                            ptr2.Status = AIBase.Status.Alarm;
                        }

                        break;
                    }

                case AIBase.ID.VI:
                    {
                        var ptr6 = (EnemyTypeVI)ptr;
                        ptr6.PostionGenerate(gameTime);
                        if (ptr6.AttackIndex < ptr6.AttackRange.Length - 1)
                        {
                            ptr6.AttackIndex++;

                            //if (ptr1.Face == AIBase.FaceSide.Right)
                            {
                                ptr6.Postion += CoreUtilities.GetVector(ptr6.AttackRange[ptr6.AttackIndex], ptr6.Speed.Radian);
                                //ptr1.Postion.X += ptr1.AttackRange[ptr1.AttackIndex];
                            }
                            //else
                            {
                                //ptr1.Postion.X += -ptr1.AttackRange[ptr1.AttackIndex];
                            }
                            if (!AIBase.SenceManager.IntersectPixels(ptr6.Postion, (int)(ptr6.EnemySize * ptr6.EnemySize2 * 40)))
                            {
                                ptr6.LocateConfirm();
                            }

                            if ((AIBase.Player.Locate - ptr6.Locate).Length() <= 20)
                            {
                                AIBase.Player.Status = AIBase.Status.Freeze;
                                AIBase.Player.BeAttackedRadian = ptr6.Speed.Radian;
                            }
                        }
                        else
                        {
                            if (ptr.CoolDownTime <= Unit.FreezeTime)
                            {
                                ptr6.CoolDownTime++;
                            }
                            else
                            {
                                ptr6.AttackIndex = 0;
                                ptr6.Status = AIBase.Status.Chase;
                                ptr6.CoolDownTime = 0;
                            }
                        }
                        break;
                    }
                case AIBase.ID.III:
                    {
                        var ptr3 = (EnemyTypeIII)ptr;
                        ptr3.PostionGenerate(gameTime);
                        switch (ptr3.Status3)
                        {
                            case AIBase.StatusIII.Sleep:
                                if (ptr3.Delta > 0)
                                {
                                    if (ptr3.RingSize >= EnemyTypeIII.MaxRing)
                                    {
                                        ptr3.Delta = -ptr3.Delta;
                                    }
                                    else
                                    {
                                        ptr3.RingSize += ptr3.Delta;
                                    }
                                }
                                else
                                {
                                    if (ptr3.RingSize <= EnemyTypeIII.MinRing)
                                    {
                                        ptr3.Delta = -ptr3.Delta;
                                    }
                                    else
                                    {
                                        ptr3.RingSize += ptr3.Delta;
                                    }
                                }
                                ptr3.ToAlarmStatus();
                                break;
                            case AIBase.StatusIII.Alarm:
                                ptr3.CurrentRing = 2;
                                if (ptr3.ToDownFall())
                                {
                                    ptr3.Status3 = AIBase.StatusIII.Downfall;
                                    ptr3.CurrentRing = 1;
                                    ptr3.Face = AIBase.FaceSide.Right;
                                    ptr3.DownFall();
                                }
                                break;

                            case AIBase.StatusIII.Downfall:
                                ptr3.PostionGenerate(gameTime);
                                if (!AIBase.SenceManager.IntersectPixels(ptr3.Postion, (int)(ptr3.EnemySize * ptr3.EnemySize2 * 40)))
                                {
                                    ptr3.Speed.Y += (float)gameTime.ElapsedGameTime.TotalSeconds * 800f;
                                    ptr3.LocateConfirm();

                                    if ((ptr3.Locate - AIBase.Player.Locate).Length() <= 40)
                                    {
                                        AIBase.Player.Postion = ptr3.Postion - new Vector2(0,10);
                                        AIBase.Player.LocateConfirm();
                                        AIBase.Player.Scale.Y = 0.01f;
                                    }
                                }
                                else
                                {
                                    ptr3.CurrentDialog = AIBase.StatusDialog.FallToGround;
                                    ptr3.Status3 = AIBase.StatusIII.UpGoes;
                                    ptr3.UpGoes();
                                }
                                break;
                            case AIBase.StatusIII.UpGoes:
                                ptr3.PostionGenerate(gameTime);
                                if (!AIBase.SenceManager.IntersectPixels(ptr3.Postion, (int)(ptr3.EnemySize * ptr3.EnemySize2 * 40)))
                                {
                                    ptr3.CurrentRing = 3;
                                    ptr3.LocateConfirm();
                                }
                                else
                                {
                                    ptr3.Sleep();
                                    ptr3.CurrentRing = 0;
                                    ptr3.Status = AIBase.Status.Attck;
                                    ptr3.Status3 = AIBase.StatusIII.Sleep;
                                    ptr3.Face = AIBase.FaceSide.Left;
                                }
                                break;
                        }
                        break;
                    }
                case AIBase.ID.V:
                    {
                        var ptrG = (Ghost)ptr;
                        ptrG.PostionGenerate(gameTime);
                        if (ptrG.AttackIndex < ptrG.AttackRange.Length - 1)
                        {
                            ptrG.AttackIndex++;

                            ptrG.CurrentDialog = AIBase.StatusDialog.Attack;
                            if (ptrG.Face == AIBase.FaceSide.Right)
                            {
                                ptrG.Postion.X += ptrG.AttackRange[ptrG.AttackIndex];
                            }
                            else
                            {
                                ptrG.Postion.X += -ptrG.AttackRange[ptrG.AttackIndex];
                            }
                            if (!AIBase.SenceManager.IntersectPixels(ptrG.Postion, (int)(ptrG.EnemySize * ptrG.EnemySize2 * 40)))
                            {
                                ptrG.LocateConfirm();
                            }

                            if ((AIBase.Player.Locate - ptrG.Locate).Length() <= 20)
                            {
                                AIBase.Player.Status = AIBase.Status.Freeze;
                                AIBase.Player.BeAttackedRadian = ptrG.Speed.Radian;
                            }
                        }
                        else
                        {
                            if (ptr.CoolDownTime <= Unit.FreezeTime)
                            {
                                ptrG.CoolDownTime++;
                            }
                            else
                            {
                                ptrG.AttackIndex = 0;
                                ptrG.CurrentDialog = AIBase.StatusDialog.StrongerThanPlayer;
                                ptrG.Status = AIBase.Status.Chase;
                                ptrG.CoolDownTime = 0;
                            }
                        }
                        break;
                    }
            }
        }
    }
}

using System;
using Heal.Core.Entities.Enemies;
using Microsoft.Xna.Framework;
using Heal.Core.Entities;

namespace Heal.Core.AI
{
    public class AINormal
    {
        public static void NormalBehave(Unit ptr, GameTime gameTime)
        {
            switch (ptr.Id)
            {


                case AIBase.ID.V:
                    {
                        var ptr5 = (Ghost)ptr;
                        ptr5.PostionGenerate(gameTime);
                        if (!AIBase.SenceManager.IntersectPixels(ptr.Postion, (int)(ptr5.EnemySize * ptr5.EnemySize2 * 40)))
                        //在此处添加 与场景的碰撞检测 和 与其他敌人的碰撞检测
                        {

                            if (ptr5.StepCount == ptr5.SwingRange)
                            {
                                ptr5.Speed *= -1;
                                ptr5.StepCount = 0;
                            }
                            else
                            {
                                ptr5.LocateConfirm();
                                ptr5.StepCount += 1;
                            }
                        }
                        else
                        {
                            ptr5.Speed.X = -ptr5.Speed.X;
                            ptr5.StepCount = 0;
                        }
                        break;
                    }
                    ;
                case AIBase.ID.I:   //直线往返行走
                    {
                        var ptr1 = (EnemyTypeI)ptr;
                        ptr1.PostionGenerate(gameTime);
                        if (!AIBase.SenceManager.IntersectPixels(ptr.Postion, (int)(ptr1.EnemySize * ptr1.EnemySize2 * 40)))
                        //在此处添加 与场景的碰撞检测 和 与其他敌人的碰撞检测
                        {

                            if (ptr1.StepCount == ptr1.SwingRange)
                            {
                                ptr1.Speed *= -1;
                                ptr1.StepCount = 0;
                            }
                            else
                            {
                                ptr1.LocateConfirm();
                                ptr1.StepCount += 1;
                            }
                        }
                        else
                        {
                            ptr1.Speed.X = -ptr1.Speed.X;
                            ptr1.StepCount = 0;
                        }
                        break;
                    }
                case AIBase.ID.VI:   //直线往返行走
                    {
                        var ptr6 = (EnemyTypeVI)ptr;
                        ptr6.PostionGenerate(gameTime);
                        if (!AIBase.SenceManager.IntersectPixels(ptr.Postion, (int)(ptr6.EnemySize * ptr6.EnemySize2 * 40)))
                        //在此处添加 与场景的碰撞检测 和 与其他敌人的碰撞检测
                        {

                            if (ptr6.StepCount == ptr6.SwingRange)
                            {
                                ptr6.Speed *= -1;
                                ptr6.StepCount = 0;
                            }
                            else
                            {
                                ptr6.LocateConfirm();
                                ptr6.StepCount += 1;
                            }
                        }
                        else
                        {
                            ptr6.Speed.X = -ptr6.Speed.X;
                            ptr6.StepCount = 0;
                        }
                        break;
                    }
                case AIBase.ID.III:
                    {
                        var ptr3 = (EnemyTypeIII)ptr;

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
                    }
            }
        }
    }
}



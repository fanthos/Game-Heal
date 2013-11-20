using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Heal.Core.Entities;
using Microsoft.Xna.Framework;

namespace Heal.Core.AI
{
    public class AIKick //弹开 不减 HP
    {
        public static void Kick(Unit ptr,GameTime gameTime)
        {
            switch (ptr.Id)
            {
                case AIBase.ID.Zero:
                    {
                        var ptrP = (Player) ptr;

                        if (ptrP.FreezeTimeNow <= Unit.FreezeTime)
                        {
                            ptrP.Speed.Length = 200;
                            ptrP.Speed.Radian = ptrP.BeAttackedRadian + MathHelper.Pi;
                            ptrP.PostionGenerate(gameTime);

                            if (!AIBase.SenceManager.IntersectPixels(ptrP.Postion, (int)(ptrP.EnemySize + 20)))
                            {
                                ptrP.LocateConfirm();
                            }

                            ptrP.FreezeTimeNow++;
                        }
                        else
                        {
                            ptrP.FreezeTimeNow = 0;
                            ptrP.Status = AIBase.Status.Normal;
                        }

                        break;
                    }
            }
        }
    }
}

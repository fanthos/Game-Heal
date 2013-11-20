using System.Linq;
using Heal.Core.Entities;
using Heal.Core.Entities.Enemies;

namespace Heal.Core.AI
{
    public class AITurning
    {
        public static void AITurn(Unit ptr)
        {
            switch (ptr.Id)
            {
                case AIBase.ID.V:
                    {
                        var ptrG = (Ghost)ptr;

                        if (ptrG.GhostStatus == AIBase.GhostStatus.Disappear)
                        {
                            if (ptrG.Color.A != 0)
                                ptrG.Color.A -= 15;
                            else
                            {
                                ptrG.GhostStatus = AIBase.GhostStatus.Appear;
                                if (ptrG.Face == AIBase.FaceSide.Left)
                                    ptrG.Face = AIBase.FaceSide.Right;
                                else
                                    ptrG.Face = AIBase.FaceSide.Left;

                            }
                        }
                        else
                        {
                            if (ptrG.Color.A != 255)
                                ptrG.Color.A += 15;
                            else
                            {
                                ptrG.Status = ptrG.LastStatus;
                                ptrG.LastStatus = AIBase.Status.Blank;
                            }
                        }

                        break;
                    }
                default:
                    {
                        if (ptr.Face == AIBase.FaceSide.Left)
                        {
                            if (ptr.ListTurnRight.First().CountNow == ptr.ListTurnRight.First().PicCount)
                            {
                                if (ptr.LastStatus != AIBase.Status.Blank)
                                    ptr.Status = ptr.LastStatus;
                                ptr.Face = AIBase.FaceSide.Right;
                                ptr.LastStatus = AIBase.Status.Blank;
                                ptr.ListTurnRight.First().CountNow = 0;
                            }
                        }
                        else
                        {
                            if (ptr.ListTurnLeft.First().CountNow == ptr.ListTurnLeft.First().PicCount)
                            {
                                if (ptr.LastStatus != AIBase.Status.Blank)
                                    ptr.Status = ptr.LastStatus;
                                ptr.LastStatus = AIBase.Status.Blank;
                                ptr.Face = AIBase.FaceSide.Left;
                                ptr.ListTurnLeft.First().CountNow = 0;
                            }
                        }
                    }
                    break;
            }

        }
    }
}

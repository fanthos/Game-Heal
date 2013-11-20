using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Heal.Core.Entities;
using Microsoft.Xna.Framework;

namespace Heal.Core.AI
{
    public class AIFlat
    {
        public static void Flat(Unit ptr,GameTime gameTime)
        {
            switch (ptr.Id)
            {
                    case AIBase.ID.Zero:
                    {
                        var ptr0 = (Player) ptr;
                           
                        if(ptr0.Scale.Y <=1)
                        {
                            ptr0.Scale.Y += 0.01f;
                        }
                        else
                        {
                            ptr0.Scale.Y = 1;
                            ptr0.Status = ptr0.LastStatus;
                        }

                        break;
                    }
            }
        }
    }
}

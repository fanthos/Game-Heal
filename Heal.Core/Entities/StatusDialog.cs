using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Heal.Core.AI;
using Microsoft.Xna.Framework.Graphics;

namespace Heal.Core.Entities
{
    public static class StatusDialog
    {
        public static List<Texture2D> List = new List<Texture2D>();

        static StatusDialog()
        {
            
        }

        public static void InstallTexture(Texture2D target)
        {
            List.Add(target);
        }


    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Heal.Component;
using Heal.Core.Entities;

namespace Heal.Utilities
{
    public static class HealUtilities
    {
        internal static EntityComponent Component(this Entity item)
        {
            return (EntityComponent) item.Sprite;
        }
    }
}

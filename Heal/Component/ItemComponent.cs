using System;
using Heal.Core.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Heal.Component
{
    internal abstract class ItemComponent : EntityComponent
    {        
        protected Item m_item;
        internal override Entity Unit
        {
            get
            {
                return m_item;
            }
            set
            {
                m_item = (Item)value;
                base.m_entity = value;
            }
        }
    }
}

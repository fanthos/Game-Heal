using System;
using Heal.Core.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Heal.Component
{
    internal class NPCComponent: UnitComponent
    {

        public override void Initialize( )
        {
            //m_unit = new NPC( this );
            //m_unit.Initialize(  );
        }

        protected override void InternalDraw(GameTime gameTime, Microsoft.Xna.Framework.Graphics.SpriteBatch batch)
        {
            foreach (var ptr in ((LittleFlower)m_unit).List)
            {
                batch.Draw(ptr.Target, base.Locate, ptr.Frame, ptr.Color, base.Rotation,
                            new Vector2((float)ptr.Frame.Height / 2), m_unit.EnemySize * m_worldManager.Scale * 0.4f, SpriteEffects.None, 0);
            }
        }
    }
}



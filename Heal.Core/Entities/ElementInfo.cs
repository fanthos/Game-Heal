using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Heal.Core.Entities
{
    public class ElementInfo : Item
    {
        private string m_config;
        public bool Enabled;
        public Microsoft.Xna.Framework.Graphics.Texture2D Texture;

        public ElementInfo( Microsoft.Xna.Framework.Graphics.Texture2D texture2D, string config, object sprite ) : base( sprite )
        {
            Texture = texture2D;
            m_config = config;
            Locate = new Vector2( texture2D.Width / 2, texture2D.Height / 2 );

        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            Enabled = Core.Utilities.CoreUtilities.StateIsTrue( m_config );
            base.Update(gameTime);
        }
    }
}

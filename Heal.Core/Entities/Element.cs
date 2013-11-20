using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Heal.Core.AI;
using Heal.Core.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Heal.Core.Entities
{
    public class Element : Item
    {
        private string m_config;
        private string m_command;
        public Texture2D Texture;
        private bool m_lastKey;
        public float Scale;
        public bool Enabled;
        public Vector2 Origin;

        public Element(Vector2 locate, string config, string command, Texture2D texture2D, float scale, object sprite)
            : base(sprite)
        {
            base.Locate = locate;
            m_config = config;
            m_command = command;
            Texture = texture2D;
            Scale = scale;
            Origin = new Vector2((float)Texture.Width / 2, (float)Texture.Height / 2);
        }

        public override void Update(GameTime gameTime)
        {
            Enabled = !CoreUtilities.StateIsTrue( m_config );
            if (Input.IsActionKeyDown() && !m_lastKey && Enabled)
            {
                if ((AIBase.Player.Locate - Locate).Length() < 40f)
                {
                    Enabled = true;
                    GameItemState.Set( m_config, true );
                    GameCommands.Enqueue( m_command );
                }
            }
            m_lastKey = Input.IsActionKeyDown();
            base.Update(gameTime);
        }
    }
}

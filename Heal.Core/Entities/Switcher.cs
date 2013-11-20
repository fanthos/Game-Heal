using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Heal.Core.AI;
using Heal.Core.Utilities;
using Microsoft.Xna.Framework;

namespace Heal.Core.Entities
{
    public class Switcher : Item
    {
        public float Rotate;
        private string m_config;
        public bool Enabled;
        private bool m_lastKey = false;
        private float m_timer;
        private float m_count;
        public Switcher( Vector2 locate, string config, float timer, object sprite ) : base( sprite )
        {
            base.Locate = locate;
            m_config = config;
            m_timer = timer;
            m_count = 0;
        }

        public override void Update(GameTime gameTime)
        {
            if (GameItemState.Get(m_config) == null)
            {
                Enabled = false;
            }
            else if ((Enabled = (bool)GameItemState.Get(m_config)) == false)
            {
                Enabled = false;
            }
            if (Input.IsActionKeyDown() && !m_lastKey)
            {
                if ((AIBase.Player.Locate - Locate).Length() < 20f)
                {
                    Enabled = !Enabled;
                    m_count = 0;
                }
            }
            if (m_timer > 0)
            {
                if (m_count >= m_timer)
                {
                    m_count = 0;
                    Enabled = false;
                }   
                else
                {
                    m_count += (float)gameTime.ElapsedGameTime.TotalSeconds;
                }
            }
            GameItemState.Set(m_config, Enabled);
            m_lastKey = Input.IsActionKeyDown();

            Rotate = MathHelper.WrapAngle(Rotate);
        }
    }
}

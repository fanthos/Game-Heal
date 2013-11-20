using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Heal.Core.AI;
using Microsoft.Xna.Framework;
using Heal.Core.Utilities;

namespace Heal.Core.Entities
{
    public class Teleport : Item
    {
        private string m_config;
        public Vector2 Locate1;
        public Vector2 Locate2;
        private bool m_lastKey = false;
        public bool Enabled = false;
        public float Rotate;

        public Teleport(Vector2 locate1, Vector2 locate2, string config, object sprite)
            : base(sprite)
        {
            m_config = config;
            Locate1 = locate1;
            Locate2 = locate2;
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
            if (Enabled && Input.IsActionKeyDown() && !m_lastKey)
            {
                if ((AIBase.Player.Locate - Locate1).Length() < 20f)
                {
                    AIBase.Player.Locate = Locate2;
                }
                else if ((AIBase.Player.Locate - Locate2).Length() < 20f)
                {
                    AIBase.Player.Locate = Locate1;
                }
            }
            m_lastKey = Input.IsActionKeyDown();
            
            if(Enabled)
            {
                Rotate += (float) gameTime.ElapsedGameTime.TotalSeconds * 3f;
            }
            else
            {
                Rotate = 0;
            }
            Rotate = MathHelper.WrapAngle( Rotate );
        }

        public override float Rotation
        {
            get
            {
                return Rotate;
            }
        }
    }
}

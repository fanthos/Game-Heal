using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Heal.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Heal.World
{
    internal class WorldLayer : IUpdateableSprite
    {
        private EffectType m_effect = EffectType.None;

        public float Alpha = 1f;
        public MapDrawer[,] Pieces;
        private float m_alphaParam1;
        private float m_alphaParam2;
        private enum EffectType
        {
            None,
            Alpha
        }
        internal WorldLayer(int x, int y)
        {
            Pieces = new MapDrawer[x,y];
        }

        internal void SetEffect(string effect)
        {
            effect = effect.Replace( "  ", " " ).Trim();
            if (effect.Length == 0)
            {
                m_effect = EffectType.None;
            }
            else
            {
                string[] strs = effect.Split( ' ' );
                switch( strs[0].ToLower() )
                {
                    case "alpha":
                        m_effect = EffectType.Alpha;
                        m_alphaParam1 = Convert.ToSingle(strs[1]);
                        m_alphaParam2 = Convert.ToSingle(strs[2]);
                        break;
                }
            }
        }
        public void Initialize()
        {
            
        }

        public void Draw( GameTime gameTime, SpriteBatch batch )
        {
            
        }

        public void UpdateDraw( GameTime gameTime )
        {
            switch( m_effect )
            {
                case EffectType.Alpha:
                    Alpha = m_alphaParam2 / 2 * (float)Math.Sin(MathHelper.WrapAngle((float)gameTime.TotalGameTime.TotalSeconds * m_alphaParam1)) + (1-m_alphaParam2/2);
                    break;
            }
        }

        public bool Palse
        {
            get { return false; }
        }
    }
}

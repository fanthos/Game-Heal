using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Heal.Data;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Heal.Sprites.Packagings
{
    public class ControlShowTexPackaging : IGameComponent, ISprite
    {
        private ScenceSprite m_controlShowTex;

        public void Initialize()
        {
            m_controlShowTex = new ScenceSprite();
            m_controlShowTex.TextureImage = DataReader.Load<Texture2D>( "Texture/Menu/Control/key" );
            m_controlShowTex.Position = new Vector2( 250, 45 );
            m_controlShowTex.TColor = Color.White;
            m_controlShowTex.DestRect = new Rectangle( (int)m_controlShowTex.Position.X,
                                                            (int)m_controlShowTex.Position.Y,
                                                                510, 510 );
        }

        public void Draw(GameTime gameTime, SpriteBatch batch)
        {
            m_controlShowTex.DrawWithDestRectangle(gameTime,SpriteManager.SpriteBatch);
        }
    }
}

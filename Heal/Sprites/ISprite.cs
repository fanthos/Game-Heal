using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Heal.Sprites
{
    internal interface ISprite : IGameComponent//, IDrawable
    {
        void Draw( GameTime gameTime, SpriteBatch batch );
    }
}

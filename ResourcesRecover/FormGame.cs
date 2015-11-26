using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ResourcesRecover
{
    class FormGame : Game
    {
        protected override void LoadContent()
        {
            base.LoadContent();
            Content.RootDirectory = "Content";
            ContentManager cm = new ContentManager(Content.ServiceProvider, Content.RootDirectory);
            cm.Load<Texture2D>
        }
    }
}

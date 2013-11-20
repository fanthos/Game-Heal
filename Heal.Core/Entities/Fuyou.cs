using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Heal.Core.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Heal.Core.Entities
{
    public class Fuyou : Item
    {
        public List<FuyouNode> FuyouList;

        public Fuyou(object sprite)
            : base(sprite)
        {
            FuyouList = new List<FuyouNode>();
        }

        public void AddToList(Texture2D target,Vector2 postion)
        {
            this.FuyouList.Add(new FuyouNode(target,postion));
        }

        public class FuyouNode
        {
            public Texture2D Target;
            public Vector2 Postion;

            public FuyouNode(Texture2D target, Vector2 postion)
            {
                this.Target = target;
                this.Postion = postion;
            }

            public void Update(GameTime gameTime)
            {
                this.Postion += new Vector2((float) ((float) (80 * Math.Cos(MathTools.RandomGenerate() % 10)) * gameTime.ElapsedGameTime.TotalMilliseconds), (float) ((float) (80 * Math.Sin(MathTools.RandomGenerate() % 10))* gameTime.ElapsedGameTime.TotalMilliseconds));
            }
        }
    }
}

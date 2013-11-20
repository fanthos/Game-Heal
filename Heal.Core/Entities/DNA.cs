using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Heal.Core.AI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Heal.Core.Entities
{
    public class DNA : Item
    {
        public List<DNANode> List;
        public static float Size = 1f;

        public Texture2D Zi, Huang, Lv;

        public void AddTexture(Texture2D z,Texture2D h, Texture2D l)
        {
            this.Zi = z;
            this.Huang = h;
            this.Lv = l;
        }

        public DNA() : base(0)
        {
            List = new List<DNANode>();
        }

        public override Vector2 Locate
        {
            get
            {
                return base.Locate;
            }
            set
            {
                
            }
        }

        public void AddNode(int x,Vector2 Postion)
        {
            if (x == 0)
            {
                this.List.Add(new DNANode(Zi, Postion));
            }
            else if (x == 1)
            {
                this.List.Add(new DNANode(Huang,Postion));
            }
            else if (x == 2)
            {
                this.List.Add(new DNANode(Lv,Postion));
            }
        }


        public class DNANode
        {
            public Texture2D Target;
            public Vector2 Postion;
            public Color Color;
            public float Scale = 1f;

            public DNANode(Texture2D target,Vector2 Postion)
            {
                this.Target = target;
                this.Postion = Postion;
                this.Color = Color.AliceBlue;
            }

            public void Update(GameTime gameTime, Player player,List<DNANode> list)
            {
                if((this.Postion - AIBase.Player.Locate).Length() <= 20)
                {
                    list.Remove(this);
                }
            }   
        }


        public override Rectangle GetDrawingRectangle()
        {
            return new Rectangle();
        }

        public override void Initialize()
        {
        }

        public override void Update(GameTime gameTime)
        {
            foreach (var node in List)
            {
                node.Update(gameTime,AIBase.Player,List);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Heal.Core.Entities.Enemies;
using Heal.Data;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Heal.Component
{
    internal class BigSnakeComponent : UnitComponent
    {
        public void AddHead(String good, String weak, String bad)
        {
            Texture2D g, w, b;
            g = DataReader.Load<Texture2D>("Texture/Entities/" + good);
            w = DataReader.Load<Texture2D>("Texture/Entities/" + weak);
            b = DataReader.Load<Texture2D>("Texture/Entities/" + bad);

            ((BigSnake)m_unit).AddHeadTexture(g, w, b);
        }

        public void AddHP(String good, String weak, String bad)
        {
            Texture2D g, w, b;
            g = DataReader.Load<Texture2D>("Texture/Entities/" + good);
            w = DataReader.Load<Texture2D>("Texture/Entities/" + weak);
            b = DataReader.Load<Texture2D>("Texture/Entities/" + bad);

            ((BigSnake)m_unit).AddHpTexture(g, w, b);
        }

        public void AddOther(String smoke, String headElec)
        {
            Texture2D s, h;
            s = DataReader.Load<Texture2D>("Texture/Entities/" + smoke);
            h = DataReader.Load<Texture2D>("Texture/Entities/" + headElec);

            ((BigSnake)m_unit).AddOther(s, h);
        }

        public void AddNewBodyNode(String body, String rightGlassGood, String rightGlassBad,
                    String leftGlassGood, String leftGlassBad,Vector2 locate)
        {
            Texture2D b, lg, lb, rg, rb;
            BigSnake.BodyNode Tmp;

            b = DataReader.Load<Texture2D>("Texture/Entities/" + body);
            lg = DataReader.Load<Texture2D>("Texture/Entities/" + leftGlassGood);
            lb = DataReader.Load<Texture2D>("Texture/Entities/" + leftGlassBad);
            rg = DataReader.Load<Texture2D>("Texture/Entities/" + rightGlassGood);
            rb = DataReader.Load<Texture2D>("Texture/Entities/" + rightGlassBad);

            Tmp = new BigSnake.BodyNode(locate);
            Tmp.AddTexture(b);
            Tmp.RightGlass.AddTexture(rg, rb);
            Tmp.LeftGlass.AddTexture(lg, lb);

            ((BigSnake)m_unit).AddNode(Tmp);

        }

        protected override void InternalDraw(Microsoft.Xna.Framework.GameTime gameTime, SpriteBatch batch)
        {
            //画其他
            //batch.Draw(((BigSnake)m_unit).Smoke, Locate, null, ((BigSnake)m_unit).Color, ((BigSnake)m_unit).Rotation, new Vector2(160f), 1, SpriteEffects.None, 0);
            batch.Draw(((BigSnake)m_unit).HeadElec, Locate, null, ((BigSnake)m_unit).Color, ((BigSnake)m_unit).Rotation, new Vector2(160f), m_worldManager.Scale * m_unit.EnemySize * 0.8f, SpriteEffects.None, 0);

            //画骨节
            foreach (var ptr in ((BigSnake)m_unit).BodyNodeList)
            {
                batch.Draw(ptr.RightGlass.Target[(int)ptr.RightGlass.CurrentStatus],
                          (ptr.RightGlass.Postion - m_worldManager.Locate) * m_worldManager.Scale + m_worldManager.Space / 2, null, ptr.Color, ptr.Rotation,
                           new Vector2(75, 40), m_worldManager.Scale * m_unit.EnemySize * 0.8f, SpriteEffects.None, 0);
                batch.Draw(ptr.LeftGlass.Target[(int)ptr.LeftGlass.CurrentStatus],
                          (ptr.LeftGlass.Postion - m_worldManager.Locate) * m_worldManager.Scale + m_worldManager.Space / 2, null, ptr.Color, ptr.Rotation,
                           new Vector2(75, 40), m_worldManager.Scale * m_unit.EnemySize * 0.8f, SpriteEffects.None, 0);
                batch.Draw(ptr.Target, (ptr.Postion - m_worldManager.Locate) * m_worldManager.Scale + m_worldManager.Space / 2, null, ptr.Color, ptr.Rotation,
                          new Vector2(160f), m_worldManager.Scale*m_unit.EnemySize * 0.8f, SpriteEffects.None, 0);
            }

            //画头
            batch.Draw(((BigSnake)m_unit).Head[(int)((BigSnake)m_unit).CurrentStatus], Locate, null, ((BigSnake)m_unit).Color, ((BigSnake)m_unit).Rotation, new Vector2(160f), m_worldManager.Scale * m_unit.EnemySize *1.4f, SpriteEffects.None, 0);

            //画血
            batch.Draw(((BigSnake)m_unit).HeadHP[(int)((BigSnake)m_unit).CurrentStatus], Locate, null, ((BigSnake)m_unit).Color, ((BigSnake)m_unit).Rotation, new Vector2(160f), m_worldManager.Scale * m_unit.EnemySize*1.4f, SpriteEffects.None, 0);
        }

    }
}

using System;
using Heal.Core.AI;
using Heal.Data;
using Heal.Sprites;
using Microsoft.Xna.Framework;
using Heal.Core.Entities;
using Microsoft.Xna.Framework.Graphics;

namespace Heal.Component
{
    internal class PlayerComponent : UnitComponent
    {
        private ScenceSprite m_body;
        private SheetSprite m_eyes;
        private SheetSprite m_headband;
        private SheetSprite m_powerSlot;

        private Vector2[] Offset = {
                                       new Vector2(-40, 0),
                                       new Vector2(0, -40),
                                       new Vector2(40, 0),
                                       new Vector2(0, 40),
                                   };
        protected override void InternalDraw(GameTime gameTime, Microsoft.Xna.Framework.Graphics.SpriteBatch batch)
        {
            foreach (var ptr in m_unit.List)
            {
                batch.Draw(ptr.Target, base.Locate, ptr.Frame, ptr.Color, base.Rotation,
                            new Vector2((float)ptr.Frame.Height / 2), new Vector2(AIBase.Player.Scale.X * m_worldManager.Scale * AIBase.Player.EnemySize, AIBase.Player.Scale.Y * m_worldManager.Scale * AIBase.Player.EnemySize), SpriteEffects.None, 0);
            }

            batch.Draw(m_unit.ListRing[m_unit.CurrentRing].Target, base.Locate, m_unit.ListRing[m_unit.CurrentRing].Frame, m_unit.ListRing[m_unit.CurrentRing].Color, base.Rotation,
                    new Vector2((float)m_unit.ListRing[m_unit.CurrentRing].Frame.Height / 2), m_unit.RingSize / 1000 * m_worldManager.Scale, SpriteEffects.None, 0);

            if (m_unit.CurrentDialog == AIBase.StatusDialog.Strike && m_unit.Speed.Length != 0)
            {
                batch.Draw(StatusDialog.List[(int)m_unit.CurrentDialog], base.Locate, null, new Color(1f, 1f, 1f, 0.4f), m_unit.Rotation,
                    new Vector2(400f, 260f), 0.13f * m_worldManager.Scale, SpriteEffects.None, 0);
            }
            else if (m_unit.CurrentDialog != AIBase.StatusDialog.Blank)
            {
                batch.Draw(StatusDialog.List[(int)m_unit.CurrentDialog], base.Locate + new Vector2(40, -40), null,
                           Color.AliceBlue, 0,
                           new Vector2(400f), 0.13f * m_worldManager.Scale, SpriteEffects.None, 0);
            }

            if (((Player)m_unit).Transforming)
            {
                for (int i = 0; i < 3; i++)
                {
                    if (((Player)m_unit).HideObtain[i])
                    {
                        batch.Draw(((Player)m_unit).Able[i], base.Locate + this.Offset[i], null, new Color(255, 255, 255, 128), 0, new Vector2(40), ((Player)m_unit).EnemySize * m_worldManager.Scale, SpriteEffects.None, 0);
                    }
                    else
                    {
                        batch.Draw(((Player)m_unit).Unable[i], base.Locate + this.Offset[i], null, new Color(255, 255, 255, 128), 0, new Vector2(40), ((Player)m_unit).EnemySize * m_worldManager.Scale, SpriteEffects.None, 0);
                    }
                }
                batch.Draw(((Player)m_unit).Default, base.Locate + this.Offset[3], null, new Color(255, 255, 255, 128), 0, new Vector2(40), ((Player)m_unit).EnemySize * m_worldManager.Scale, SpriteEffects.None, 0);
            }

            if (((Player)m_unit).Status != AIBase.Status.Turning)
            {
                if (((Player)m_unit).HP == 3)
                {
                    batch.Draw(((Player)m_unit).HP_3[0].Target, base.Locate, ((Player)m_unit).HP_3[0].Frame, Color.AliceBlue, ((Player)m_unit).Rotation, new Vector2(40), ((Player)m_unit).EnemySize * m_worldManager.Scale, SpriteEffects.None, 0);
                }
                else if (((Player)m_unit).HP == 2)
                {
                    batch.Draw(((Player)m_unit).HP_2[0].Target, base.Locate, ((Player)m_unit).HP_2[0].Frame, Color.AliceBlue, ((Player)m_unit).Rotation, new Vector2(40), ((Player)m_unit).EnemySize * m_worldManager.Scale, SpriteEffects.None, 0);
                }
                else if (((Player)m_unit).HP == 1)
                {
                    batch.Draw(((Player)m_unit).HP_1[0].Target, base.Locate, ((Player)m_unit).HP_1[0].Frame, Color.AliceBlue, ((Player)m_unit).Rotation, new Vector2(40), ((Player)m_unit).EnemySize * m_worldManager.Scale, SpriteEffects.None, 0);
                }
                
                batch.Draw(((Player)m_unit).EyeTexure,base.Locate,null,((Player)m_unit).EyeColor,base.Rotation,new Vector2(40),((Player)m_unit).EnemySize * m_worldManager.Scale,SpriteEffects.None,0 );

            }

            if (((Player)m_unit).FakeID != AIBase.ID.Zero)
            {
                switch (((Player)m_unit).FakeID)
                {
                    case AIBase.ID.I:
                        batch.Draw(((Player)m_unit).MaskI, base.Locate, null, new Color(255, 255, 255, 200), 0, new Vector2(40), ((Player)m_unit).EnemySize * m_worldManager.Scale * 1.3f, SpriteEffects.None, 0);
                        break;
                    case AIBase.ID.II:
                        batch.Draw(((Player)m_unit).MaskII, base.Locate, null, new Color(255, 255, 255, 200), 0, new Vector2(40), ((Player)m_unit).EnemySize * m_worldManager.Scale * 1.7f, SpriteEffects.None, 0);
                        break;
                    case AIBase.ID.V:
                        batch.Draw(((Player)m_unit).MaskV, base.Locate, null, new Color(255, 255, 255, 200), 0, new Vector2(40), ((Player)m_unit).EnemySize * m_worldManager.Scale * 1.3f, SpriteEffects.None, 0);
                        break;
                }
            }
          //  batch.Draw(((Player)m_unit).Bubble[0].Target, base.Locate - new Vector2(  (float) (40*Math.Sin(((Player)m_unit).Rotation)), (float) (40* Math.Cos(((Player)m_unit).Rotation))  ),((Player)m_unit).Bubble[0].Frame,Color.AliceBlue,base.Rotation,new Vector2(40,0),((Player)m_unit).EnemySize * m_worldManager.Scale,SpriteEffects.None,0);
        }

        public void AddBubble()
        {
            ((Player)m_unit).AddBubble(DataReader.Load<Texture2D>("Texture/Entities/Player/Bubble"));
        }

        public void AddToUnable()
        {
            ((Player)m_unit).AddToUnable(DataReader.Load<Texture2D>("Texture/Entities/Player/unable_I"),
                                         DataReader.Load<Texture2D>("Texture/Entities/Player/unable_II"),
                                         DataReader.Load<Texture2D>("Texture/Entities/Player/unable_V")
             );
        }

        public void AddToAble()
        {
            ((Player)m_unit).AddToAble(DataReader.Load<Texture2D>("Texture/Entities/Player/able_I"),
                             DataReader.Load<Texture2D>("Texture/Entities/Player/able_II"),
                             DataReader.Load<Texture2D>("Texture/Entities/Player/able_V")
             );
        }

        public void AddDefault()
        {
            ((Player)m_unit).AddDefault(DataReader.Load<Texture2D>("Texture/Entities/Player/default"));
        }

        public void AddMask()
        {
            ((Player)m_unit).AddMask(DataReader.Load<Texture2D>("Texture/Entities/Player/mask_I"),
                                     DataReader.Load<Texture2D>("Texture/Entities/Player/mask_II"),
                                     DataReader.Load<Texture2D>("Texture/Entities/Player/mask_V"));
        }

        public void AddEye()
        {
            ((Player)m_unit).AddEye(DataReader.Load<Texture2D>("Texture/Entities/Player/eye"));
        }

        public void AddHP()
        {
            ((Player)m_unit).AddHp(DataReader.Load<Texture2D>("Texture/Entities/Player/hp_3"),
                                   DataReader.Load<Texture2D>("Texture/Entities/Player/hp_2"),
                                   DataReader.Load<Texture2D>("Texture/Entities/Player/hp_1"));
        }

        public PlayerComponent()
        {
            //Unit = new Player(this);
        }
    }
}



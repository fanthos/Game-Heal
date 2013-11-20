using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Heal.Core.GameData;
using Heal.Core.Utilities;
using Heal.Data;
using Heal.Utilities;
using Microsoft.Xna.Framework;
using Heal.Sprites;
using Microsoft.Xna.Framework.Graphics;

namespace Heal.World
{
    public class DialogManager : GameClassManager, IUpdateable, ISprite
    {
        #region Instance Manager
        private static DialogManager m_instance;

        public static DialogManager GetInstance()
        {
            return m_instance;
        }

        public DialogManager()
        {
            m_instance = this;
        }

        #endregion

        private DialogInfo m_dialog;
        private int m_talkState;
        private Dictionary<string, Texture2D> m_cache;
        private bool m_lastState;
        private Texture2D m_backGround;
        private Texture2D m_textBox;
        private Texture2D m_arrow;
        private GraphicsManager m_effect;

        private float m_alpha;

        public override void Initialize()
        {
            m_cache = new Dictionary<string, Texture2D>(StringComparer.OrdinalIgnoreCase);
            m_effect = GraphicsManager.GetInstance();
            m_textBox = DataReader.Load<Texture2D>("Texture/Dialogs/Box");
            m_arrow = DataReader.Load<Texture2D>("Texture/Dialogs/Arrow");
        }

        public void Draw( GameTime gameTime, SpriteBatch batch )
        {
            if (m_talkState == m_dialog.List.Count)
            {
                return;
            }
            m_effect.Draw( b=>InternalDraw( gameTime, b ) );
        }

        private void InternalDraw(GameTime gameTime, SpriteBatch batch)
        {
            DialogText text = m_dialog.List[m_talkState];
            Color img1 = Color.White, img2 = Color.White;
            batch.Draw( m_backGround, Vector2.Zero, null, Color.DarkGray );
            switch(text.Talking)
            {
                case 0:
                    img1 = Color.Gray;
                    img2 = Color.Gray;
                    break;
                case 1:
                    img1 = Color.White;
                    img2 = Color.Gray;
                    break;
                case 2:
                    img1 = Color.Gray;
                    img2 = Color.White;
                    break;
                case 3:
                    img1 = Color.White;
                    img2 = Color.White;
                    break;
            }
            if (!string.IsNullOrEmpty(text.Image1))
            {
                batch.Draw( GetTexture( text.Image1 ), new Vector2( 150f, 300f ), null, img1, 0, new Vector2( 400f ),
                            0.45f, SpriteEffects.None, 0 );
            }
            if (!string.IsNullOrEmpty(text.Image2))
            {
                batch.Draw( GetTexture( text.Image2 ), new Vector2( 650f, 300f ), null, img2, 0, new Vector2( 400f ),
                            0.45f, SpriteEffects.None, 0 );
            }
            batch.Draw(m_textBox, Vector2.Zero, null, Color.White);
            batch.Draw(m_arrow, new Vector2(640, 480), null, new Color(1f,1f,1f,(float)Math.Sin( m_alpha ) * .6f + .4f ));
            batch.DrawString(m_effect.Fonts("DefaultFont"), text.Line1, new Vector2(70f, 410f), Color.Black);
            batch.DrawString(m_effect.Fonts("DefaultFont"), text.Line2, new Vector2(70f, 455f), Color.Black);
            batch.DrawString(m_effect.Fonts("DefaultFont"), text.Line3, new Vector2(70f, 500f), Color.Black);
        }

        private Texture2D GetTexture(string texture)
        {
            Texture2D texture2D;
            if(m_cache.TryGetValue( texture, out texture2D ))
            {
                return texture2D;
            }
            texture2D = DataReader.Load<Texture2D>( "Texture/Dialogs/" + texture );
            m_cache.Add( texture, texture2D );
            return texture2D;
        }

        public void Load(string dialog, Texture2D backGround)
        {
            m_dialog = DataReader.Load<DialogInfo>("Data/Dialogs/" + dialog);
            m_talkState = 0;
            m_lastState = true;
            m_backGround = backGround;

            m_alpha = 0;
        }

        public void Update( GameTime gameTime )
        {
            m_alpha += (float) gameTime.ElapsedGameTime.TotalSeconds * 3;
            m_alpha = MathHelper.WrapAngle( m_alpha );
            bool spaceState = Input.IsActionKeyDown();
            if(!m_lastState && spaceState )
            {
                m_talkState++;
            }
            if(m_talkState == m_dialog.List.Count)
            {
                GameCommands.Enqueue( m_dialog.PostTalk );
            }
            m_lastState = spaceState;
        }

        #region Unused items

        public bool Enabled
        {
            get { throw new NotImplementedException(); }
        }

        public int UpdateOrder
        {
            get { throw new NotImplementedException(); }
        }
        event EventHandler<EventArgs> IUpdateable.EnabledChanged
        { add { } remove { } }
        event EventHandler<EventArgs> IUpdateable.UpdateOrderChanged
        { add { } remove { } }


        #endregion
    }
}

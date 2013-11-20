using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Heal.Data;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Heal.Sprites.Packagings
{
    public class PauseMenuTexPackaging : IGameComponent, ISprite
    {
        private ScenceSprite m_background;
        private ScenceSprite m_figure;

        private List<ScenceSprite> m_pauseTexList;

        public void Initialize(Texture2D texture)
        {
            #region Initialize the background

            m_background = new ScenceSprite()
                               {
                                   Origin = Vector2.Zero,
                                   Position = Vector2.Zero,
                                   Visible = true,
                                   TColor = new Color(0.5f, 0.5f, 0.5f, 1f)
                               };

            if( texture !=null)
                m_background.TextureImage = texture;
            else
            {
                m_background.TextureImage = DataReader.Load<Texture2D>( "Texture/Menu/Pausing/DefaultTex" );
            }
            m_background.DestRect = new Rectangle((int) m_background.Position.X, (int) m_background.Position.Y,
                                                  (int) m_background.TextureImage.Width,
                                                  (int) m_background.TextureImage.Height);
            #endregion

            #region Initialize the figure

            m_figure = new ScenceSprite()
            {
                TextureImage = DataReader.Load<Texture2D>( "Texture/Menu/Pausing/pausingTex" ),
                
                Origin = Vector2.Zero,
                Position = new Vector2(250,45),
                Visible = true,
                TColor = Color.White
            };
            m_figure.DestRect = new Rectangle( (int)m_figure.Position.X, (int)m_figure.Position.Y,
                                     (int) (m_figure.TextureImage.Width*0.6), (int) (m_figure.TextureImage.Height*0.6));
            #endregion

            m_pauseTexList = new List<ScenceSprite>();
            m_pauseTexList.Add(m_background);
            m_pauseTexList.Add(m_figure);
        }

        public void Draw(GameTime gameTime, SpriteBatch batch)
        {
            for (int i = 0; i < m_pauseTexList.Count; i++)
            {
                m_pauseTexList[i].DrawWithDestRectangle(gameTime,batch);
            }
        }

        public void Initialize()
        {
            
        }
    }
}

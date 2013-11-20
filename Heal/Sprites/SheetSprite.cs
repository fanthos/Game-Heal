using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Heal.Data;

namespace Heal.Sprites
{
    public class SheetSprite : IGameComponent, ISprite
    {
        protected Point FrameSize;
        private Point m_currentFrame;
        private Point m_sheetSize;
        private int m_timeSinceLastFrame = 0;
        private int m_millisecondsPerFrame;
        private string m_texPath;
        protected Vector2 speed;
        public Texture2D TextureImage { get; set; }
        public Vector2 Position { get; set; }
        private const int m_defaultMillisecondsPerFrame = 16;

        public float Scale
        {
            get; set;
        }
        public Rectangle SourceRect
        {
            get; set;
        }

        public Rectangle DestRect
        {
            get; set;
        }

        public SheetSprite(string texPath,
                        Vector2 position,
                        Point frameSize,
                        Point currentFrame,
                        Point sheetSize,
                        Vector2 speed)
            : this( texPath,
                    position,
                    frameSize,
                    currentFrame,
                    sheetSize,
                    speed,
                    m_defaultMillisecondsPerFrame)
        {

        }

        public SheetSprite( string texPath,
                        Vector2 position,
                        Point frameSize,
                        Point currentFrame,
                        Point sheetSize,
                        Vector2 speed,
                        int millisecondsPerFrame)
        {
            this.m_texPath = texPath;
            this.Position = position;
            this.FrameSize = frameSize;
            this.m_currentFrame = currentFrame;
            this.m_sheetSize = sheetSize;
            this.speed = speed;
            this.m_millisecondsPerFrame = millisecondsPerFrame;
            this.TextureImage = DataReader.Load<Texture2D>(m_texPath);
            this.SourceRect = new Rectangle(m_currentFrame.X*FrameSize.X, m_currentFrame.Y*FrameSize.Y, FrameSize.X,
                                            FrameSize.Y);
            this.DestRect = new Rectangle(m_currentFrame.X*FrameSize.X, m_currentFrame.Y*FrameSize.Y, FrameSize.X,FrameSize.Y);

            //this.DestRect = new Rectangle(950,750, FrameSize.X,FrameSize.Y );
        }

        public void Draw(GameTime gameTime, SpriteBatch batch)
        {
            batch.Draw(TextureImage,
                new Rectangle( m_currentFrame.X * FrameSize.X, m_currentFrame.Y * FrameSize.Y, FrameSize.X, FrameSize.Y ), 
                new Rectangle(m_currentFrame.X * FrameSize.X, m_currentFrame.Y * FrameSize.Y, FrameSize.X, FrameSize.Y), 
                Color.White, 
                0, 
                Vector2.Zero, 
                SpriteEffects.None,
                1);
        }

        public void UpdateDraw( GameTime gameTime )
        {
            m_timeSinceLastFrame += gameTime.ElapsedGameTime.Milliseconds;
            if( m_timeSinceLastFrame > m_millisecondsPerFrame )
            {
                m_timeSinceLastFrame = 0;
                ++m_currentFrame.X;
                if( m_currentFrame.X >= m_sheetSize.X )
                {
                    m_currentFrame.X = 0;
                    ++m_currentFrame.Y;
                    if( m_currentFrame.Y >= m_sheetSize.Y )
                    {
                        m_currentFrame.Y = 0;
                    }
                }
            }
        }

        public void Initialize()
        {
        }
    }
}

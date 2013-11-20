using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Heal.Data;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Heal.Sprites.Packagings
{
    public class ShowStoryTexPackaging : IGameComponent, ISprite
    {
        #region Instructions variable

        private ScenceSprite m_story_0;
        private ScenceSprite m_story_1;
        private ScenceSprite m_story_2;
        private ScenceSprite m_story_3;
        private ScenceSprite m_story_4;
        private ScenceSprite m_story_5;
        private ScenceSprite m_story_6;
        private ScenceSprite m_story_7;
        private ScenceSprite m_story_8;
        private ScenceSprite m_story_9;
        private ScenceSprite m_story_10;
        private ScenceSprite m_story_11;
        private ScenceSprite m_story_12;
        private ScenceSprite m_story_13;
        private ScenceSprite m_story_14;
        private ScenceSprite m_story_15;
        private ScenceSprite m_story_16;
        private ScenceSprite m_story_17;
        private ScenceSprite m_story_18;
        private ScenceSprite m_story_19;
        private ScenceSprite m_story_20;
        private ScenceSprite m_story_21;
        private ScenceSprite m_story_22;
        private ScenceSprite m_story_23;

        #endregion

        public int CurStoryCount{ get; set;}
        private int m_storyMaxCount;
        private int m_tempCount;

        private float m_timer;
        private float m_count = 0.016f;
        public bool IsFinished{ get; set;}

        private List<ScenceSprite> m_storyList;

        public void Initialize()
        {
            #region Initialize the story pictures

            m_story_0 = new ScenceSprite();
            m_story_1 = new ScenceSprite();
            m_story_2 = new ScenceSprite();
            m_story_3 = new ScenceSprite();
            m_story_4 = new ScenceSprite();
            m_story_5 = new ScenceSprite();
            m_story_6 = new ScenceSprite();
            m_story_7 = new ScenceSprite();
            m_story_8 = new ScenceSprite();
            m_story_9 = new ScenceSprite();
            m_story_10 = new ScenceSprite();
            m_story_11 = new ScenceSprite();
            m_story_12 = new ScenceSprite();
            m_story_13 = new ScenceSprite();
            m_story_14 = new ScenceSprite();
            m_story_15 = new ScenceSprite();
            m_story_16 = new ScenceSprite();
            m_story_17 = new ScenceSprite();
            m_story_18 = new ScenceSprite();
            m_story_19 = new ScenceSprite();
            m_story_20 = new ScenceSprite();
            m_story_21 = new ScenceSprite();
            m_story_22 = new ScenceSprite();
            m_story_23 = new ScenceSprite();

            m_story_0.TextureImage = DataReader.Load<Texture2D>( "Texture/Menu/ShowStory/end_a" );
            m_story_1.TextureImage = DataReader.Load<Texture2D>( "Texture/Menu/ShowStory/end_b" );
            m_story_2.TextureImage = DataReader.Load<Texture2D>( "Texture/Menu/ShowStory/end_0" );
            m_story_3.TextureImage = DataReader.Load<Texture2D>( "Texture/Menu/ShowStory/end_1" );
            m_story_4.TextureImage = DataReader.Load<Texture2D>( "Texture/Menu/ShowStory/end_2" );
            m_story_5.TextureImage = DataReader.Load<Texture2D>( "Texture/Menu/ShowStory/end_3" );
            m_story_6.TextureImage = DataReader.Load<Texture2D>( "Texture/Menu/ShowStory/end_4" );
            m_story_7.TextureImage = DataReader.Load<Texture2D>( "Texture/Menu/ShowStory/end_5" );
            m_story_8.TextureImage = DataReader.Load<Texture2D>( "Texture/Menu/ShowStory/end_6" );
            m_story_9.TextureImage = DataReader.Load<Texture2D>( "Texture/Menu/ShowStory/end_7" );
            m_story_10.TextureImage = DataReader.Load<Texture2D>( "Texture/Menu/ShowStory/end_8" );
            m_story_11.TextureImage = DataReader.Load<Texture2D>( "Texture/Menu/ShowStory/end_9" );
            m_story_12.TextureImage = DataReader.Load<Texture2D>( "Texture/Menu/ShowStory/end_10" );
            m_story_13.TextureImage = DataReader.Load<Texture2D>( "Texture/Menu/ShowStory/end_11" );
            m_story_14.TextureImage = DataReader.Load<Texture2D>( "Texture/Menu/ShowStory/end_12" );
            m_story_15.TextureImage = DataReader.Load<Texture2D>( "Texture/Menu/ShowStory/end_13" );
            m_story_16.TextureImage = DataReader.Load<Texture2D>( "Texture/Menu/ShowStory/end_14" );
            m_story_17.TextureImage = DataReader.Load<Texture2D>( "Texture/Menu/ShowStory/end_15" );
            m_story_18.TextureImage = DataReader.Load<Texture2D>( "Texture/Menu/ShowStory/end_16" );
            m_story_19.TextureImage = DataReader.Load<Texture2D>( "Texture/Menu/ShowStory/end_17" );
            m_story_20.TextureImage = DataReader.Load<Texture2D>( "Texture/Menu/ShowStory/end_18" );
            m_story_21.TextureImage = DataReader.Load<Texture2D>( "Texture/Menu/ShowStory/end_19" );
            m_story_22.TextureImage = DataReader.Load<Texture2D>( "Texture/Menu/ShowStory/end_20" );
            m_story_23.TextureImage = DataReader.Load<Texture2D>( "Texture/Menu/ShowStory/end_21" );

            #endregion

            #region Initialize the story list

            m_storyList = new List<ScenceSprite>();

            m_storyList.Add( m_story_0 );
            m_storyList.Add( m_story_1 );
            m_storyList.Add( m_story_2 );
            m_storyList.Add( m_story_3 );
            m_storyList.Add( m_story_4 );
            m_storyList.Add( m_story_5 );
            m_storyList.Add( m_story_6 );
            m_storyList.Add( m_story_7 );
            m_storyList.Add( m_story_8 );
            m_storyList.Add( m_story_9 );
            m_storyList.Add( m_story_10 );
            m_storyList.Add( m_story_11 );
            m_storyList.Add( m_story_12 );
            m_storyList.Add( m_story_13 );
            m_storyList.Add( m_story_14 );
            m_storyList.Add( m_story_15 );
            m_storyList.Add( m_story_16 );
            m_storyList.Add( m_story_17 );
            m_storyList.Add( m_story_18 );
            m_storyList.Add( m_story_19 );
            m_storyList.Add( m_story_20 );
            m_storyList.Add( m_story_21 );
            m_storyList.Add( m_story_22 );
            m_storyList.Add( m_story_23 );


            m_storyList[0].Name = "Story_a";
            m_storyList[1].Name = "Story_b";

            for(int i = 2;i<=23;i++)
            {
                m_storyList[i].Name = "Story_" + i;
            }

            CurStoryCount = 0;
            m_storyMaxCount = m_storyList.Count - 1;

            for( int i = 0; i <= m_storyMaxCount; i++ )
            {
                m_storyList[i].Position = Vector2.Zero;
                m_storyList[i].DestRect = new Rectangle( (int)m_storyList[i].Position.X,
                                                              (int)m_storyList[i].Position.Y,
                                                              800, 600 );
                m_storyList[i].Visible = false;
                m_storyList[i].FlashInterval = 0.005f;
                m_storyList[i].CurAlpha = 0.0f;
                m_storyList[i].TColor = new Color( new Vector4( 255, 255, 255, m_storyList[i].CurAlpha ) );
            }
            m_storyList[0].Visible = true;

            #endregion
        }


        internal void Twinkle( ScenceSprite scenceSprite )
        {
            if( scenceSprite.CurAlpha <= ScenceSprite.MaxAlpha )
            {
                scenceSprite.TColor = new Color( 1f, 1f, 1f,(float)Math.Sin( scenceSprite.CurAlpha * Math.PI ) );
                scenceSprite.CurAlpha += scenceSprite.FlashInterval;
            }
        }

        public void Update()
        {
             m_timer += m_count;
            if( m_timer >= 3 )
            {
                m_storyList[CurStoryCount].Visible = false;
                CurStoryCount++;
                if( CurStoryCount >= 23 )
                {
                    CurStoryCount = 23;
                }
                else
                {
                    m_storyList[CurStoryCount].Visible = true;
                }
                m_timer = 0;
            }
            else
            {
                Twinkle( m_storyList[CurStoryCount] );
            }
            if( CurStoryCount == 23 )
            {
                IsFinished = true;
            }


        }

        public void Draw(GameTime gameTime, SpriteBatch batch)
        {
            foreach( ScenceSprite t in m_storyList )
            {
                if( t.Visible )
                    t.DrawWithDestRectangle( gameTime, batch );
            }
        }
    }
}

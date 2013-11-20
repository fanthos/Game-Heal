using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Heal.Data;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Heal.Sprites.Packagings
{
    public class AchievementTexPackaging : IGameComponent, ISprite
    {
        #region AchievementTex variable

        private ScenceSprite m_achievementTex_0;
        private ScenceSprite m_achievementTex_1;
        private ScenceSprite m_achievementTex_2;
        private ScenceSprite m_achievementTex_3;
        private ScenceSprite m_achievementTex_4;
        private ScenceSprite m_achievementTex_5;
        private ScenceSprite m_achievementTex_6;
        private ScenceSprite m_achievementTex_7;
        #endregion

        private int m_curAchievementCount;
        private int m_achievementMaxCount;
        private int m_tempCount;

        private List<ScenceSprite> m_achievementList;


        public void Initialize()
        {
            #region Initialize the instructions

            m_achievementTex_0 = new ScenceSprite();
            m_achievementTex_1 = new ScenceSprite();
            m_achievementTex_2 = new ScenceSprite();
            m_achievementTex_3 = new ScenceSprite();
            m_achievementTex_4 = new ScenceSprite();
            m_achievementTex_5 = new ScenceSprite();
            m_achievementTex_6 = new ScenceSprite();
            m_achievementTex_7 = new ScenceSprite();

            m_achievementTex_0.TextureImage = DataReader.Load<Texture2D>( "Texture/Menu/Achievements/first_0" );
            m_achievementTex_1.TextureImage = DataReader.Load<Texture2D>( "Texture/Menu/Achievements/trans_0" );
            m_achievementTex_2.TextureImage = DataReader.Load<Texture2D>( "Texture/Menu/Achievements/save_0" );
            m_achievementTex_3.TextureImage = DataReader.Load<Texture2D>( "Texture/Menu/Achievements/earth_0" );
            m_achievementTex_4.TextureImage = DataReader.Load<Texture2D>( "Texture/Menu/Achievements/first_1" );
            m_achievementTex_5.TextureImage = DataReader.Load<Texture2D>( "Texture/Menu/Achievements/trans_1" );
            m_achievementTex_6.TextureImage = DataReader.Load<Texture2D>( "Texture/Menu/Achievements/save_1" );
            m_achievementTex_7.TextureImage = DataReader.Load<Texture2D>( "Texture/Menu/Achievements/earth_1" );

            #endregion

            #region Initialize the instruction list

            m_achievementList = new List<ScenceSprite>();

            m_achievementList.Add( m_achievementTex_0 );
            m_achievementList.Add( m_achievementTex_1 );
            m_achievementList.Add( m_achievementTex_2 );
            m_achievementList.Add( m_achievementTex_3 );
            m_achievementList.Add( m_achievementTex_4 );
            m_achievementList.Add( m_achievementTex_5 );
            m_achievementList.Add( m_achievementTex_6 );
            m_achievementList.Add( m_achievementTex_7 );

            m_curAchievementCount = 0;
            m_achievementMaxCount = m_achievementList.Count - 1;



            for( int i = 0; i <= 3; i++ )
            {
                m_achievementList[i].Position = new Vector2( 300, 20 + i * 105 );
                m_achievementList[i].TColor = Color.White;
                m_achievementList[i].DestRect = new Rectangle( (int)m_achievementList[i].Position.X,
                                                                (int)m_achievementList[i].Position.Y,
                                                                    320, 80 );

                m_achievementList[i + 4].Position = new Vector2( 300, 20 + i * 105 );
                m_achievementList[i + 4].TColor = Color.White;
                m_achievementList[i + 4].DestRect = new Rectangle( (int)m_achievementList[i+4].Position.X,
                                                                (int)m_achievementList[i+4].Position.Y,
                                                                    320, 80 );
                m_achievementList[i].Visible = true;
                m_achievementList[i + 4].Visible = false;
            }

            
            m_tempCount = m_curAchievementCount;
            #endregion
        }

        internal void IsAttainedPressed()
        {
            for(int i = 0;i<=3;i++)
            {
                m_achievementList[i].Visible = false;
                m_achievementList[i + 4].Visible = true;
            }
        }

        internal void IsNotAttainedPressed()
        {
            for( int i = 0; i <=3; i++ )
            {
                m_achievementList[i].Visible = true;
                m_achievementList[i + 4].Visible = false;
            }
        }

        public void Draw( GameTime gameTime, SpriteBatch batch )
        {
            for( int i = 0; i <= m_achievementMaxCount; i++ )
            {
                if( m_achievementList[i].Visible )
                    m_achievementList[i].DrawWithDestRectangle(gameTime, batch);
            }

        }
    }
}

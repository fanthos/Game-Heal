using System.Collections.Generic;
using Heal.Data;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Heal.Sprites.Packagings
{
    public class VolumnTexPackaging : IGameComponent, ISprite
    {
        private ScenceSprite m_volumn_0;
        private ScenceSprite m_volumn_1;
        private ScenceSprite m_volumn_2;
        private ScenceSprite m_volumn_3;
        private ScenceSprite m_volumn_4;
        private ScenceSprite m_volumn_5;
        private ScenceSprite m_volumn_6;

        private int m_curInstructionCount;
        private int m_instructionMaxCount;
        private int m_tempCount;

        private List<ScenceSprite> m_volumnTexList;

        public void Initialize()
        {
            #region Initialize the instructions

            m_volumn_0 = new ScenceSprite();
            m_volumn_1 = new ScenceSprite();
            m_volumn_2 = new ScenceSprite();
            m_volumn_3 = new ScenceSprite();
            m_volumn_4 = new ScenceSprite();
            m_volumn_5 = new ScenceSprite();
            m_volumn_6 = new ScenceSprite();


            m_volumn_0.TextureImage = DataReader.Load<Texture2D>( "Texture/Menu/VolumnTex/xin_0" );
            m_volumn_1.TextureImage = DataReader.Load<Texture2D>( "Texture/Menu/VolumnTex/xin_1" );
            m_volumn_2.TextureImage = DataReader.Load<Texture2D>( "Texture/Menu/VolumnTex/xin_2" );
            m_volumn_3.TextureImage = DataReader.Load<Texture2D>( "Texture/Menu/VolumnTex/xin_3" );
            m_volumn_4.TextureImage = DataReader.Load<Texture2D>( "Texture/Menu/VolumnTex/xin_4" );
            m_volumn_5.TextureImage = DataReader.Load<Texture2D>( "Texture/Menu/VolumnTex/xin_5" );
            m_volumn_6.TextureImage = DataReader.Load<Texture2D>( "Texture/Menu/VolumnTex/xin_6" );

            #endregion

            #region Initialize the instruction list

            m_volumnTexList = new List<ScenceSprite>();

            m_volumnTexList.Add( m_volumn_0 );
            m_volumnTexList.Add( m_volumn_1 );
            m_volumnTexList.Add( m_volumn_2 );
            m_volumnTexList.Add( m_volumn_3 );
            m_volumnTexList.Add( m_volumn_4 );
            m_volumnTexList.Add( m_volumn_5 );
            m_volumnTexList.Add( m_volumn_6 );

            m_curInstructionCount = 0;
            m_instructionMaxCount = m_volumnTexList.Count - 1;

            for( int i = 0; i <= m_instructionMaxCount; i++ )
            {
                m_volumnTexList[i].Position = new Vector2( 150, 80 );
                m_volumnTexList[i].TColor = Color.White;
                m_volumnTexList[i].DestRect = new Rectangle( (int)m_volumnTexList[i].Position.X,
                                                             (int)m_volumnTexList[i].Position.Y,
                                                             510, 510 );
            }
            m_volumnTexList[0].Visible = true;
            m_tempCount = m_curInstructionCount;
            #endregion

        }

        internal void IsAddPressed()
        {
            if( m_curInstructionCount == m_instructionMaxCount )
                m_curInstructionCount = m_instructionMaxCount;
            else
                m_curInstructionCount++;
        }

        internal void IsMinusPressed()
        {
            if( m_curInstructionCount == 0 )
                m_curInstructionCount = 0;
            else
                m_curInstructionCount--;
        }

        public void ChangeInstruction( bool IsAdd )
        {
            m_tempCount = m_curInstructionCount;
            if( IsAdd )
                this.IsAddPressed();
            else
                this.IsMinusPressed();
        }

        public void Draw(GameTime gameTime, SpriteBatch batch)
        {
            m_volumnTexList[m_curInstructionCount].DrawWithDestRectangle( gameTime, batch );

        }
    }
}



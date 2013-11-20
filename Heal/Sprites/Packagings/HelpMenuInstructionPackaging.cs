using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Heal.Data;

namespace Heal.Sprites.Packagings
{
    public class HelpMenuInstructionPackaging : IGameComponent, ISprite
    {

        #region Instructions variable

        private ScenceSprite m_instruction_0;
        private ScenceSprite m_instruction_1;
        private ScenceSprite m_instruction_2;
        private ScenceSprite m_instruction_3;
        private ScenceSprite m_instruction_4;
        private ScenceSprite m_instruction_5;
        private ScenceSprite m_instruction_6;
        private ScenceSprite m_instruction_7;
        private ScenceSprite m_instruction_8;
        private ScenceSprite m_instruction_9;
        private ScenceSprite m_instruction_10;
        #endregion

        #region Tips variable

        private ScenceSprite m_tip_0;
        private ScenceSprite m_tip_1;
        private ScenceSprite m_tip_2;
        private ScenceSprite m_tip_3;
        private ScenceSprite m_tip_4;
        private ScenceSprite m_tip_5;
        private ScenceSprite m_tip_6;
        private ScenceSprite m_tip_7;
        private ScenceSprite m_tip_8;
        private ScenceSprite m_tip_9;
        private ScenceSprite m_tip_10;
        #endregion

        private int m_curInstructionCount;
        private int m_instructionMaxCount;
        private int m_tempCount;

        private List<ScenceSprite> m_instructionList;
        private List<ScenceSprite> m_tipsList;

        public void Initialize()
        {
            #region Initialize the instructions

            m_instruction_0 = new ScenceSprite();
            m_instruction_1 = new ScenceSprite();
            m_instruction_2 = new ScenceSprite();
            m_instruction_3 = new ScenceSprite();
            m_instruction_4 = new ScenceSprite();
            m_instruction_5 = new ScenceSprite();
            m_instruction_6 = new ScenceSprite();
            m_instruction_7 = new ScenceSprite();
            m_instruction_8 = new ScenceSprite();
            m_instruction_9 = new ScenceSprite();
            m_instruction_10 = new ScenceSprite();

            m_instruction_0.TextureImage = DataReader.Load<Texture2D>( "Texture/Menu/HelpInstructions/help_0" );
            m_instruction_1.TextureImage = DataReader.Load<Texture2D>( "Texture/Menu/HelpInstructions/help_1" );
            m_instruction_2.TextureImage = DataReader.Load<Texture2D>( "Texture/Menu/HelpInstructions/help_2" );
            m_instruction_3.TextureImage = DataReader.Load<Texture2D>( "Texture/Menu/HelpInstructions/help_3" );
            m_instruction_4.TextureImage = DataReader.Load<Texture2D>( "Texture/Menu/HelpInstructions/help_4" );
            m_instruction_5.TextureImage = DataReader.Load<Texture2D>( "Texture/Menu/HelpInstructions/help_5" );
            m_instruction_6.TextureImage = DataReader.Load<Texture2D>( "Texture/Menu/HelpInstructions/help_6" );
            m_instruction_7.TextureImage = DataReader.Load<Texture2D>( "Texture/Menu/HelpInstructions/help_7" );
            m_instruction_8.TextureImage = DataReader.Load<Texture2D>( "Texture/Menu/HelpInstructions/help_8" );
            m_instruction_9.TextureImage = DataReader.Load<Texture2D>( "Texture/Menu/HelpInstructions/help_9" );
            m_instruction_10.TextureImage = DataReader.Load<Texture2D>( "Texture/Menu/HelpInstructions/help_10" );

            #endregion

            #region Initialize the tips

            m_tip_0 = new ScenceSprite();
            m_tip_1 = new ScenceSprite();
            m_tip_2 = new ScenceSprite();
            m_tip_3 = new ScenceSprite();
            m_tip_4 = new ScenceSprite();
            m_tip_5 = new ScenceSprite();
            m_tip_6 = new ScenceSprite();
            m_tip_7 = new ScenceSprite();
            m_tip_8 = new ScenceSprite();
            m_tip_9 = new ScenceSprite();
            m_tip_10 = new ScenceSprite();

            m_tip_0.TextureImage = DataReader.Load<Texture2D>( "Texture/Menu/HelpInstructions/p_0" );
            m_tip_1.TextureImage = DataReader.Load<Texture2D>( "Texture/Menu/HelpInstructions/p_1" );
            m_tip_2.TextureImage = DataReader.Load<Texture2D>( "Texture/Menu/HelpInstructions/p_2" );
            m_tip_3.TextureImage = DataReader.Load<Texture2D>( "Texture/Menu/HelpInstructions/p_3" );
            m_tip_4.TextureImage = DataReader.Load<Texture2D>( "Texture/Menu/HelpInstructions/p_4" );
            m_tip_5.TextureImage = DataReader.Load<Texture2D>( "Texture/Menu/HelpInstructions/p_5" );
            m_tip_6.TextureImage = DataReader.Load<Texture2D>( "Texture/Menu/HelpInstructions/p_6" );
            m_tip_7.TextureImage = DataReader.Load<Texture2D>( "Texture/Menu/HelpInstructions/p_7" );
            m_tip_8.TextureImage = DataReader.Load<Texture2D>( "Texture/Menu/HelpInstructions/p_8" );
            m_tip_9.TextureImage = DataReader.Load<Texture2D>( "Texture/Menu/HelpInstructions/p_9" );
            m_tip_10.TextureImage = DataReader.Load<Texture2D>( "Texture/Menu/HelpInstructions/p_10" );
            #endregion

            #region Initialize the instruction list

            m_instructionList = new List<ScenceSprite>();

            m_instructionList.Add( m_instruction_0 );
            m_instructionList.Add( m_instruction_1 );
            m_instructionList.Add( m_instruction_2 );
            m_instructionList.Add( m_instruction_3 );
            m_instructionList.Add( m_instruction_4 );
            m_instructionList.Add( m_instruction_5 );
            m_instructionList.Add( m_instruction_6 );
            m_instructionList.Add( m_instruction_7 );
            m_instructionList.Add( m_instruction_8 );
            m_instructionList.Add( m_instruction_9 );
            m_instructionList.Add( m_instruction_10 );

            m_curInstructionCount = 0;
            m_instructionMaxCount = m_instructionList.Count-1;

            for( int i = 0; i <= m_instructionMaxCount; i++ )
            {
                m_instructionList[i].Position = new Vector2(250,45);
                m_instructionList[i].TColor = Color.White;
                m_instructionList[i].DestRect = new Rectangle((int)m_instructionList[i].Position.X,
                                                              (int)m_instructionList[i].Position.Y ,
                                                              510,510);
                m_instructionList[i].Visible = false;
            }
            m_instructionList[0].Visible = true;
            m_tempCount = m_curInstructionCount;
            #endregion

            #region Initialize the tip list

            m_tipsList = new List<ScenceSprite>();

            m_tipsList.Add( m_tip_0 );
            m_tipsList.Add( m_tip_1 );
            m_tipsList.Add( m_tip_2 );
            m_tipsList.Add( m_tip_3 );
            m_tipsList.Add( m_tip_4 );
            m_tipsList.Add( m_tip_5 );
            m_tipsList.Add( m_tip_6 );
            m_tipsList.Add( m_tip_7 );
            m_tipsList.Add( m_tip_8 );
            m_tipsList.Add( m_tip_9 );
            m_tipsList.Add( m_tip_10 );

            for( int i = 0; i <= m_instructionMaxCount; i++ )
            {
                m_tipsList[i].Position = new Vector2( 200, 313 );
                m_tipsList[i].TColor = Color.White;
                m_tipsList[i].DestRect = new Rectangle( (int)m_tipsList[i].Position.X,
                                                        (int)m_tipsList[i].Position.Y,
                                                        50, 242 );
            }
            m_tipsList[0].Visible = true;
            #endregion
        }

        internal void IsNextPressed()
        {
            if( m_curInstructionCount == m_instructionMaxCount )
                m_curInstructionCount = 0;
            else
                m_curInstructionCount++;
        }

        internal void IsPrePressed()
        {
            if( m_curInstructionCount == 0 )
                m_curInstructionCount = m_instructionMaxCount;
            else
                m_curInstructionCount--;
        }

        public void ChangeInstruction(bool IsNext)
        {
            m_tempCount = m_curInstructionCount;
            if(IsNext)
                this.IsNextPressed();
            else
                this.IsPrePressed();
        }

        public void Draw( GameTime gameTime, SpriteBatch batch )
        {
            m_instructionList[m_curInstructionCount].DrawWithDestRectangle(gameTime, batch);
            m_tipsList[m_curInstructionCount].DrawWithDestRectangle( gameTime, batch );
        }
    }
}



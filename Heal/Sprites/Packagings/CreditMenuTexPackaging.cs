using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Heal.Data;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Heal.Sprites.Packagings
{
    public class CreditMenuTexPackaging : IGameComponent, ISprite
    {
        #region Credit variable

        private ScenceSprite m_credit_0;
        private ScenceSprite m_credit_1;
        private ScenceSprite m_credit_2;
        private ScenceSprite m_credit_3;
        private ScenceSprite m_credit_4;
        private ScenceSprite m_credit_5;
        private ScenceSprite m_credit_6;
        private ScenceSprite m_credit_7;
        #endregion

        private static int m_curCreditCount;
        public static int CurCreditCount
        {
            get
            {
                return m_curCreditCount;
            }
            set
            {
                m_curCreditCount = value;
            }
        }
        private int m_creditMaxCount;
        private int m_tempCount;
        private float m_timer;
        private float m_count;
        public bool IsFinished{ get; set;}
        public bool IsReset
        {
            get; set;
        }

        private List<ScenceSprite> m_creditList;

        public void Initialize()
        {
            #region Initialize the credits

            m_credit_0 = new ScenceSprite();
            m_credit_1 = new ScenceSprite();
            m_credit_2 = new ScenceSprite();
            m_credit_3 = new ScenceSprite();
            m_credit_4 = new ScenceSprite();
            m_credit_5 = new ScenceSprite();
            m_credit_6 = new ScenceSprite();
            m_credit_7 = new ScenceSprite();

            m_credit_0.TextureImage = DataReader.Load<Texture2D>( "Texture/Menu/Credits/credit_0");
            m_credit_1.TextureImage = DataReader.Load<Texture2D>( "Texture/Menu/Credits/credit_1" );
            m_credit_2.TextureImage = DataReader.Load<Texture2D>( "Texture/Menu/Credits/credit_2" );
            m_credit_3.TextureImage = DataReader.Load<Texture2D>( "Texture/Menu/Credits/credit_3" );
            m_credit_4.TextureImage = DataReader.Load<Texture2D>( "Texture/Menu/Credits/credit_4" );
            m_credit_5.TextureImage = DataReader.Load<Texture2D>( "Texture/Menu/Credits/credit_5" );
            m_credit_6.TextureImage = DataReader.Load<Texture2D>( "Texture/Menu/Credits/credit_6" );
            m_credit_7.TextureImage = DataReader.Load<Texture2D>( "Texture/Menu/Credits/credit_7" );
            #endregion

            InitCreditList();
            IsFinished = false;
            IsReset = false;
            m_count = 0.016f;
        }

        public void InitCreditList()
        {
            #region Initialize the credit list

            m_creditList = new List<ScenceSprite>();

            m_creditList.Add( m_credit_0 );
            m_creditList.Add( m_credit_1 );
            m_creditList.Add( m_credit_2 );
            m_creditList.Add( m_credit_3 );
            m_creditList.Add( m_credit_4 );
            m_creditList.Add( m_credit_5 );
            m_creditList.Add( m_credit_6 );
            m_creditList.Add( m_credit_7 );

            m_curCreditCount = 0;
            m_creditMaxCount = m_creditList.Count - 1;

            for( int i = 0; i <= m_creditMaxCount; i++ )
            {
                m_creditList[i].Position = Vector2.Zero;
                m_creditList[i].CurAlpha = 0.0f;
                m_creditList[i].FlashInterval = 0.002f;
                m_creditList[i].TColor = new Color( new Vector4( 255, 255, 255, m_creditList[i].CurAlpha ) );
                m_creditList[i].DestRect = new Rectangle( (int)m_creditList[i].Position.X,
                                                              (int)m_creditList[i].Position.Y,
                                                              800, 600 );
                m_creditList[i].SourceRect = new Rectangle( 0, 0, m_creditList[i].TextureImage.Width, m_creditList[i].TextureImage.Height );

                m_creditList[i].Visible = false;
            }
            m_creditList[0].Visible = true;

            #endregion
        }

        public void Reset()
        {
            InitCreditList();
            m_timer = 0;
        }

        public void Twinkle( ScenceSprite scenceSprite )
        {
            if( scenceSprite.CurAlpha <= ScenceSprite.MaxAlpha )
            {
                scenceSprite.TColor = new Color( 1f, 1f, 1f, (float)Math.Sin(scenceSprite.CurAlpha * Math.PI ) );
                scenceSprite.CurAlpha += scenceSprite.FlashInterval;
            }
        }



        public void Update(GameTime gametime)
        {
            m_timer += m_count;
            if (m_timer >= 10)
            {
                m_creditList[m_curCreditCount].Visible = false;
                m_curCreditCount++;
                if( m_curCreditCount >= 8 )
                {
                    m_curCreditCount = 8;
                }
                else
                {
                    m_creditList[m_curCreditCount].Visible = true;
                }
                m_timer = 0;
            }
            else
            {
                Twinkle(m_creditList[m_curCreditCount]);
            }
            if (m_curCreditCount >= m_creditMaxCount+1)
            {
                IsFinished = true;
            }

        }

        public void Draw(GameTime gameTime, SpriteBatch batch)
        {
            foreach (ScenceSprite t in m_creditList)
            {
                if( t.Visible )
                    t.Draw(gameTime, batch);
            }
        }
    }
}

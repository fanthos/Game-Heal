using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Heal.Data;
using Heal.Core.Utilities;

namespace Heal.Sprites.Packagings
{
    public class PlayerStateTexPackaging : IGameComponent, ISprite
    {
        #region PlayerState Variable

        private ScenceSprite m_playerStateBase;
        private ScenceSprite m_twig_1;
        private ScenceSprite m_twig_2;
        private ScenceSprite m_twig_3;
        private ScenceSprite m_shiningTwig_1;
        private ScenceSprite m_shiningTwig_2;
        private ScenceSprite m_shiningTwig_3;

        private ScenceSprite m_disguiseBase_1;
        private ScenceSprite m_disguiseBase_2;
        private ScenceSprite m_disguiseBase_3;

        private ScenceSprite m_playerStateBaseShining;
        private ScenceSprite m_playerStateBaseShiningEye;
        private ScenceSprite m_playerStateBaseShiningBody;

        private ScenceSprite m_sunKey;
        private ScenceSprite m_moonKey;
        private ScenceSprite m_triangleKey;

        private ScenceSprite m_tag_1;
        private ScenceSprite m_tag_2;
        private ScenceSprite m_tag_3;
        private ScenceSprite m_tag_4;
        private ScenceSprite m_tag_5;
        private ScenceSprite m_tag_6;
        private ScenceSprite m_tag_7;
        private ScenceSprite m_tag_8;


        private ScenceSprite m_disguise_1;
        private ScenceSprite m_disguise_2;
        private ScenceSprite m_disguise_3;

        private ScenceSprite m_disguise_1_Counter_Shining;
        private ScenceSprite m_disguise_2_Counter_Shining;
        private ScenceSprite m_disguise_3_Counter_Shining;

        private ScenceSprite m_background;
        private ScenceSprite m_playerHp_3;
        private ScenceSprite m_playerHp_2;
        private ScenceSprite m_playerHp_1;


        private ScenceSprite m_zeroFifthFirstEnemyDNA;
        private ScenceSprite m_oneFifthFirstEnemyDNA;
        private ScenceSprite m_twoFifthFirstEnemyDNA;
        private ScenceSprite m_threeFifthFirstEnemyDNA;
        private ScenceSprite m_fourFifthFirstEnemyDNA;
        private ScenceSprite m_fiveFifthFirstEnemyDNA;

        private ScenceSprite m_zeroFifthSecondEnemyDNA;
        private ScenceSprite m_oneFifthSecondEnemyDNA;
        private ScenceSprite m_twoFifthSecondEnemyDNA;
        private ScenceSprite m_threeFifthSecondEnemyDNA;
        private ScenceSprite m_fourFifthSecondEnemyDNA;
        private ScenceSprite m_fiveFifthSecondEnemyDNA;

        private ScenceSprite m_zeroFifthThirdEnemyDNA;
        private ScenceSprite m_oneFifthThirdEnemyDNA;
        private ScenceSprite m_twoFifthThirdEnemyDNA;
        private ScenceSprite m_threeFifthThirdEnemyDNA;
        private ScenceSprite m_fourFifthThirdEnemyDNA;
        private ScenceSprite m_fiveFifthThirdEnemyDNA;




        private List<ScenceSprite> m_playerStateTexList;
        #endregion

        public void Initialize( Texture2D texture )
        {
            #region Initialize the background

            m_background = new ScenceSprite()
            {
                TextureImage = DataReader.Load<Texture2D>( "Texture/Menu/Pausing/DefaultTex" ),
                Origin = Vector2.Zero,
                Position = Vector2.Zero,
                Visible = true,
                TColor = new Color( 0.5f, 0.5f, 0.5f, 1f )
            };
            //m_background.TextureImage = DataReader.Load<Texture2D>( "Texture/Menu/Pausing/DefaultTex" );

            //if( texture != null )
            //    m_background.TextureImage = texture;
            //else
            //{
            //    m_background.TextureImage = DataReader.Load<Texture2D>( "Texture/Menu/Pausing/DefaultTex" );
            //}
            m_background.DestRect = new Rectangle( 0, 0,
                                                  (int)m_background.TextureImage.Width,
                                                  (int)m_background.TextureImage.Height );
            #endregion

            #region Initialize the playerHp 3

            m_playerHp_3 = new ScenceSprite()
                               {
                                   Name = "Hp_Three",
                                   TextureImage = DataReader.Load<Texture2D>("Texture/PlayerState/Hp_Three"),
                                   Origin = Vector2.Zero,
                                   Position = Vector2.Zero,
                                   BeginingFlashInterval = 0.008f,
                                   CurAlpha = ScenceSprite.MinAlpha
                               };
           
            m_playerHp_3.DestRect = new Rectangle( 0, 0,
                                                  (int)m_background.TextureImage.Width,
                                                  (int)m_background.TextureImage.Height );
            m_playerHp_3.TColor = new Color( new Vector4( 255, 255, 255, m_playerHp_3.CurAlpha ) );
            #endregion

            #region Initialize the playerHp 2

            m_playerHp_2 = new ScenceSprite()
            {
                Name = "Hp_Two",
                TextureImage = DataReader.Load<Texture2D>( "Texture/PlayerState/Hp_Two" ),
                Origin = Vector2.Zero,
                Position = Vector2.Zero,
                BeginingFlashInterval = 0.016f,
                CurAlpha = ScenceSprite.MinAlpha
            };
            m_playerHp_2.DestRect = new Rectangle( 0, 0,
                                                  (int)m_background.TextureImage.Width,
                                                  (int)m_background.TextureImage.Height );
            m_playerHp_2.TColor = new Color( new Vector4( 255, 255, 255, m_playerHp_2.CurAlpha ) );
            #endregion

            #region Initialize the playerHp 1

            m_playerHp_1 = new ScenceSprite()
                               {
                                   Name = "Hp_One",
                                   TextureImage = DataReader.Load<Texture2D>("Texture/PlayerState/Hp_One"),
                                   Origin = Vector2.Zero,
                                   Position = Vector2.Zero,
                                   BeginingFlashInterval = 0.024f,
                                   CurAlpha = ScenceSprite.MinAlpha
                               };
            m_playerHp_1.DestRect = new Rectangle( 0, 0,
                                                  (int)m_background.TextureImage.Width,
                                                  (int)m_background.TextureImage.Height );
            m_playerHp_1.TColor = new Color( new Vector4( 255, 255, 255, m_playerHp_1.CurAlpha ) );
            #endregion

            #region Initialize the ?/? 

            #region FirstEnemy ?/?

            #region 0/5 of the First

            m_zeroFifthFirstEnemyDNA = new ScenceSprite()
            {
                Name = "ZeroFifthOfTheFirst",
                TextureImage = DataReader.Load<Texture2D>( "Texture/PlayerState/ZeroFifthofTheFirst" ),
                Origin = Vector2.Zero,
                DestRect = new Rectangle( 520, 25, 115, 115 ),
                BeginingFlashInterval = 0.008f,
                CurAlpha = ScenceSprite.MinAlpha
            };
            m_zeroFifthFirstEnemyDNA.TColor = new Color( new Vector4( 255, 255, 255, m_zeroFifthFirstEnemyDNA.CurAlpha ) );
            #endregion

            #region 1/5 of the First

            m_oneFifthFirstEnemyDNA = new ScenceSprite()
            {
                Name = "OneFifthOfTheFirst",
                TextureImage = DataReader.Load<Texture2D>( "Texture/PlayerState/OneFifthofTheFirst" ),
                Origin = Vector2.Zero,
                DestRect = new Rectangle( 520, 25, 115, 115 ),
                BeginingFlashInterval = 0.008f,
                CurAlpha = ScenceSprite.MinAlpha
            };
            m_zeroFifthFirstEnemyDNA.TColor = new Color( new Vector4( 255, 255, 255, m_zeroFifthFirstEnemyDNA.CurAlpha ) );
            #endregion

            #region 2/5 of the First

            m_twoFifthFirstEnemyDNA = new ScenceSprite()
            {
                Name = "TwoFifthOfTheFirst",
                TextureImage = DataReader.Load<Texture2D>( "Texture/PlayerState/TwoFifthofTheFirst" ),
                Origin = Vector2.Zero,
                DestRect = new Rectangle( 520, 25, 115, 115 ),
                BeginingFlashInterval = 0.008f,
                CurAlpha = ScenceSprite.MinAlpha
            };
            m_twoFifthFirstEnemyDNA.TColor = new Color( new Vector4( 255, 255, 255, m_twoFifthFirstEnemyDNA.CurAlpha ) );
            #endregion

            #region 3/5 of the First

            m_threeFifthFirstEnemyDNA = new ScenceSprite()
            {
                Name = "ThreeFifthOfTheFirst",
                TextureImage = DataReader.Load<Texture2D>( "Texture/PlayerState/ThreeFifthofTheFirst" ),
                Origin = Vector2.Zero,
                DestRect = new Rectangle( 520, 25, 115, 115 ),
                BeginingFlashInterval = 0.008f,
                CurAlpha = ScenceSprite.MinAlpha
            };
            m_threeFifthFirstEnemyDNA.TColor = new Color( new Vector4( 255, 255, 255, m_threeFifthFirstEnemyDNA.CurAlpha ) );
            #endregion

            #region 4/5 of the First

            m_fourFifthFirstEnemyDNA = new ScenceSprite()
            {
                Name = "FourFifthOfTheFirst",
                TextureImage = DataReader.Load<Texture2D>( "Texture/PlayerState/FourFifthofTheFirst" ),
                Origin = Vector2.Zero,
                DestRect = new Rectangle( 520, 25, 115, 115 ),
                BeginingFlashInterval = 0.008f,
                CurAlpha = ScenceSprite.MinAlpha
            };
            m_fourFifthFirstEnemyDNA.TColor = new Color( new Vector4( 255, 255, 255, m_fourFifthFirstEnemyDNA.CurAlpha ) );
            #endregion

            #region 5/5 of the First

            m_fiveFifthFirstEnemyDNA = new ScenceSprite()
            {
                Name = "FiveFifthOfTheFirst",
                TextureImage = DataReader.Load<Texture2D>( "Texture/PlayerState/FiveFifthofTheFirst" ),
                Origin = Vector2.Zero,
                DestRect = new Rectangle( 520, 25, 115, 115 ),
                BeginingFlashInterval = 0.008f,
                CurAlpha = ScenceSprite.MinAlpha
            };
            m_fiveFifthFirstEnemyDNA.TColor = new Color( new Vector4( 255, 255, 255, m_fiveFifthFirstEnemyDNA.CurAlpha ) );
            #endregion
            #endregion

            #region Second Enemy ?/?

            #region 0/5 of the Second

            m_zeroFifthSecondEnemyDNA = new ScenceSprite()
            {
                Name = "ZeroFifthOfTheSecond",
                TextureImage = DataReader.Load<Texture2D>( "Texture/PlayerState/ZeroFifthofTheSecond" ),
                Origin = Vector2.Zero,
                DestRect = new Rectangle( 650, 25, 115, 115 ),
                BeginingFlashInterval = 0.008f,
                CurAlpha = ScenceSprite.MinAlpha
            };
            m_zeroFifthSecondEnemyDNA.TColor = new Color( new Vector4( 255, 255, 255, m_zeroFifthSecondEnemyDNA.CurAlpha ) );
            #endregion

            #region 1/5 of the Second

            m_oneFifthSecondEnemyDNA = new ScenceSprite()
            {
                Name = "OneFifthOfTheSecond",
                TextureImage = DataReader.Load<Texture2D>( "Texture/PlayerState/OneFifthofTheSecond" ),
                Origin = Vector2.Zero,
                DestRect = new Rectangle( 650, 25, 115, 115 ),
                BeginingFlashInterval = 0.008f,
                CurAlpha = ScenceSprite.MinAlpha
            };
            m_zeroFifthSecondEnemyDNA.TColor = new Color( new Vector4( 255, 255, 255, m_zeroFifthSecondEnemyDNA.CurAlpha ) );
            #endregion

            #region 2/5 of the Second

            m_twoFifthSecondEnemyDNA = new ScenceSprite()
            {
                Name = "TwoFifthOfTheSecond",
                TextureImage = DataReader.Load<Texture2D>( "Texture/PlayerState/TwoFifthofTheSecond" ),
                Origin = Vector2.Zero,
                DestRect = new Rectangle( 650, 25, 115, 115 ),
                BeginingFlashInterval = 0.008f,
                CurAlpha = ScenceSprite.MinAlpha
            };
            m_twoFifthSecondEnemyDNA.TColor = new Color( new Vector4( 255, 255, 255, m_twoFifthSecondEnemyDNA.CurAlpha ) );
            #endregion

            #region 3/5 of the Second

            m_threeFifthSecondEnemyDNA = new ScenceSprite()
            {
                Name = "ThreeFifthOfTheSecond",
                TextureImage = DataReader.Load<Texture2D>( "Texture/PlayerState/ThreeFifthofTheSecond" ),
                Origin = Vector2.Zero,
                DestRect = new Rectangle( 650, 25, 115, 115 ),
                BeginingFlashInterval = 0.008f,
                CurAlpha = ScenceSprite.MinAlpha
            };
            m_threeFifthSecondEnemyDNA.TColor = new Color( new Vector4( 255, 255, 255, m_threeFifthSecondEnemyDNA.CurAlpha ) );
            #endregion

            #region 4/5 of the Second

            m_fourFifthSecondEnemyDNA = new ScenceSprite()
            {
                Name = "FourFifthOfTheSecond",
                TextureImage = DataReader.Load<Texture2D>( "Texture/PlayerState/FourFifthofTheSecond" ),
                Origin = Vector2.Zero,
                DestRect = new Rectangle( 650, 25, 115, 115 ),
                BeginingFlashInterval = 0.008f,
                CurAlpha = ScenceSprite.MinAlpha
            };
            m_fourFifthSecondEnemyDNA.TColor = new Color( new Vector4( 255, 255, 255, m_fourFifthSecondEnemyDNA.CurAlpha ) );
            #endregion

            #region 5/5 of the Second

            m_fiveFifthSecondEnemyDNA = new ScenceSprite()
            {
                Name = "FiveFifthOfTheSecond",
                TextureImage = DataReader.Load<Texture2D>( "Texture/PlayerState/FiveFifthofTheSecond" ),
                Origin = Vector2.Zero,
                DestRect = new Rectangle( 650, 25, 115, 115 ),
                BeginingFlashInterval = 0.008f,
                CurAlpha = ScenceSprite.MinAlpha
            };
            m_fiveFifthSecondEnemyDNA.TColor = new Color( new Vector4( 255, 255, 255, m_fiveFifthSecondEnemyDNA.CurAlpha ) );
            #endregion
            #endregion

            #region Third Enemy ?/?

            #region 0/5 of the Third

            m_zeroFifthThirdEnemyDNA = new ScenceSprite()
            {
                Name = "ZeroFifthOfTheThird",
                TextureImage = DataReader.Load<Texture2D>( "Texture/PlayerState/ZeroFifthofTheThird" ),
                Origin = Vector2.Zero,
                DestRect = new Rectangle( 650, 150, 115, 115 ),
                BeginingFlashInterval = 0.008f,
                CurAlpha = ScenceSprite.MinAlpha
            };
            m_zeroFifthThirdEnemyDNA.TColor = new Color( new Vector4( 255, 255, 255, m_zeroFifthThirdEnemyDNA.CurAlpha ) );
            #endregion

            #region 1/5 of the Third

            m_oneFifthThirdEnemyDNA = new ScenceSprite()
            {
                Name = "OneFifthOfTheThird",
                TextureImage = DataReader.Load<Texture2D>( "Texture/PlayerState/OneFifthofTheThird" ),
                Origin = Vector2.Zero,
                DestRect = new Rectangle( 650, 150, 115, 115 ),
                BeginingFlashInterval = 0.008f,
                CurAlpha = ScenceSprite.MinAlpha
            };
            m_zeroFifthThirdEnemyDNA.TColor = new Color( new Vector4( 255, 255, 255, m_zeroFifthThirdEnemyDNA.CurAlpha ) );
            #endregion

            #region 2/5 of the Third

            m_twoFifthThirdEnemyDNA = new ScenceSprite()
            {
                Name = "TwoFifthOfTheThird",
                TextureImage = DataReader.Load<Texture2D>( "Texture/PlayerState/TwoFifthofTheThird" ),
                Origin = Vector2.Zero,
                DestRect = new Rectangle( 650, 150, 115, 115 ),
                BeginingFlashInterval = 0.008f,
                CurAlpha = ScenceSprite.MinAlpha
            };
            m_twoFifthThirdEnemyDNA.TColor = new Color( new Vector4( 255, 255, 255, m_twoFifthThirdEnemyDNA.CurAlpha ) );
            #endregion

            #region 3/5 of the Third

            m_threeFifthThirdEnemyDNA = new ScenceSprite()
            {
                Name = "ThreeFifthOfTheThird",
                TextureImage = DataReader.Load<Texture2D>( "Texture/PlayerState/ThreeFifthofTheThird" ),
                Origin = Vector2.Zero,
                DestRect = new Rectangle( 650, 150, 115, 115 ),
                BeginingFlashInterval = 0.008f,
                CurAlpha = ScenceSprite.MinAlpha
            };
            m_threeFifthThirdEnemyDNA.TColor = new Color( new Vector4( 255, 255, 255, m_threeFifthThirdEnemyDNA.CurAlpha ) );
            #endregion

            #region 4/5 of the Third

            m_fourFifthThirdEnemyDNA = new ScenceSprite()
            {
                Name = "FourFifthOfTheThird",
                TextureImage = DataReader.Load<Texture2D>( "Texture/PlayerState/FourFifthofTheThird" ),
                Origin = Vector2.Zero,
                DestRect = new Rectangle( 650, 150, 115, 115 ),
                BeginingFlashInterval = 0.008f,
                CurAlpha = ScenceSprite.MinAlpha
            };
            m_fourFifthThirdEnemyDNA.TColor = new Color( new Vector4( 255, 255, 255, m_fourFifthThirdEnemyDNA.CurAlpha ) );
            #endregion

            #region 5/5 of the Third

            m_fiveFifthThirdEnemyDNA = new ScenceSprite()
            {
                Name = "FiveFifthOfTheThird",
                TextureImage = DataReader.Load<Texture2D>( "Texture/PlayerState/FiveFifthofTheThird" ),
                Origin = Vector2.Zero,
                DestRect = new Rectangle( 650, 150, 115, 115 ),
                BeginingFlashInterval = 0.008f,
                CurAlpha = ScenceSprite.MinAlpha
            };
            m_fiveFifthThirdEnemyDNA.TColor = new Color( new Vector4( 255, 255, 255, m_fiveFifthThirdEnemyDNA.CurAlpha ) );
            #endregion
            #endregion

            #endregion


            #region Initialize the elements of player state

            #region Initialize the playerStateBase

            m_playerStateBase = new ScenceSprite()
                                    {
                                        TextureImage =
                                            DataReader.Load<Texture2D>( "Texture/PlayerState/PlayerState_Base" ),
                                        DestRect = new Rectangle( 0, 0, HealGame.Game.GraphicsDevice.Viewport.Width,
                                                                 HealGame.Game.GraphicsDevice.Viewport.Height ),
                                        TColor = Color.White,
                                        FlashInterval = 0.000f,
                                        Visible = true,
                                    };
            #endregion

            #region Initialize the shining playerStateBase

            m_playerStateBaseShining = new ScenceSprite()
            {
                TextureImage =
                    DataReader.Load<Texture2D>( "Texture/PlayerState/PlayerState_Base_Shining" ),
                DestRect = new Rectangle( 0, 0, HealGame.Game.GraphicsDevice.Viewport.Width,
                                         HealGame.Game.GraphicsDevice.Viewport.Height ),
                BeginingFlashInterval = 0.008f,
                CurAlpha = ScenceSprite.MinAlpha,
                Visible = true,
            };
            m_playerStateBaseShining.TColor = new Color( new Vector4( 255, 255, 255, m_playerStateBaseShining.CurAlpha ) );
            #endregion

            #region Initialize the shining eye of playerStateBase

            m_playerStateBaseShiningEye = new ScenceSprite()
            {
                TextureImage =
                    DataReader.Load<Texture2D>( "Texture/PlayerState/PlayerState_Base_ShiningBody_2" ),
                DestRect = new Rectangle( 0, 0, HealGame.Game.GraphicsDevice.Viewport.Width,
                                         HealGame.Game.GraphicsDevice.Viewport.Height ),
                BeginingFlashInterval = 0.007f,
                CurAlpha = ScenceSprite.MinAlpha,
                Visible = true,
            };
            m_playerStateBaseShiningEye.TColor = new Color( new Vector4( 255, 255, 255, m_playerStateBaseShiningEye.CurAlpha ) );
            #endregion

            #region Initialize the twig 1

            m_twig_1 = new ScenceSprite()
                           {
                               TextureImage = DataReader.Load<Texture2D>( "Texture/PlayerState/Twig_1_Base" ),
                               DestRect = new Rectangle( 0, 0, HealGame.Game.GraphicsDevice.Viewport.Width,
                                                        HealGame.Game.GraphicsDevice.Viewport.Height ),
                               TColor = Color.White,
                               FlashInterval = 0.000f,
                               Visible = true,
                           };
            #endregion

            #region Initialize the twig 2

            m_twig_2 = new ScenceSprite()
                           {
                               TextureImage = DataReader.Load<Texture2D>( "Texture/PlayerState/Twig_2_Base" ),
                               DestRect = new Rectangle( 0, 0, HealGame.Game.GraphicsDevice.Viewport.Width,
                                                        HealGame.Game.GraphicsDevice.Viewport.Height ),
                               TColor = Color.White,
                               FlashInterval = 0.000f,
                               Visible = true,
                           };
            #endregion

            #region Initialize the twig 3

            m_twig_3 = new ScenceSprite()
                           {
                               TextureImage = DataReader.Load<Texture2D>( "Texture/PlayerState/Twig_3_Base" ),
                               DestRect = new Rectangle( 0, 0, HealGame.Game.GraphicsDevice.Viewport.Width,
                                                        HealGame.Game.GraphicsDevice.Viewport.Height ),
                               TColor = Color.White,
                               FlashInterval = 0.000f,
                               Visible = true,
                           };
            #endregion

            #region Initialize the shining twig 1

            m_shiningTwig_1 = new ScenceSprite()
            {
                TextureImage = DataReader.Load<Texture2D>( "Texture/PlayerState/Twig_1_Base_Shining" ),
                DestRect = new Rectangle( 0, 0, HealGame.Game.GraphicsDevice.Viewport.Width,
                                         HealGame.Game.GraphicsDevice.Viewport.Height ),
                BeginingFlashInterval = 0.008f,
                CurAlpha = ScenceSprite.MinAlpha,
                Visible = true,
            };
            m_shiningTwig_1.TColor = new Color( new Vector4( 255, 255, 255, m_shiningTwig_1.CurAlpha ) );
            #endregion

            #region Initialize the shining twig 2

            m_shiningTwig_2 = new ScenceSprite()
            {
                TextureImage = DataReader.Load<Texture2D>( "Texture/PlayerState/Twig_2_Base_Shining" ),
                DestRect = new Rectangle( 0, 0, HealGame.Game.GraphicsDevice.Viewport.Width,
                                         HealGame.Game.GraphicsDevice.Viewport.Height ),
                BeginingFlashInterval = 0.008f,
                CurAlpha = ScenceSprite.MinAlpha,
                Visible = true,
            };
            m_shiningTwig_2.TColor = new Color( new Vector4( 255, 255, 255, m_shiningTwig_2.CurAlpha ) );
            #endregion

            #region Initialize the shining twig 3

            m_shiningTwig_3 = new ScenceSprite()
            {
                TextureImage = DataReader.Load<Texture2D>( "Texture/PlayerState/Twig_3_Base_Shining" ),
                DestRect = new Rectangle( 0, 0, HealGame.Game.GraphicsDevice.Viewport.Width,
                                         HealGame.Game.GraphicsDevice.Viewport.Height ),
                BeginingFlashInterval = 0.008f,
                CurAlpha = ScenceSprite.MinAlpha,
                Visible = true,
            };
            m_shiningTwig_3.TColor = new Color( new Vector4( 255, 255, 255, m_shiningTwig_3.CurAlpha ) );
            #endregion

            #region Initialize the disguise base 1

            m_disguiseBase_1 = new ScenceSprite()
            {
                TextureImage = DataReader.Load<Texture2D>( "Texture/PlayerState/Disguise_1_Base" ),
                DestRect = new Rectangle( 520, 25, 115, 115 ),
                TColor = Color.White,
                FlashInterval = 0.000f,
                Visible = true,
            };
            #endregion

            #region Initialize the disguise base 2

            m_disguiseBase_2 = new ScenceSprite()
            {
                TextureImage = DataReader.Load<Texture2D>( "Texture/PlayerState/Disguise_2_Base" ),
                DestRect = new Rectangle( 650, 25, 115, 115 ),
                TColor = Color.White,
                FlashInterval = 0.000f,
                Visible = true,
            };
            #endregion

            #region Initialize the disguise base 3

            m_disguiseBase_3 = new ScenceSprite()
            {
                TextureImage = DataReader.Load<Texture2D>( "Texture/PlayerState/Disguise_3_Base" ),
                DestRect = new Rectangle( 650, 150, 115, 115 ),
                TColor = Color.White,
                FlashInterval = 0.000f,
                Visible = true,
            };
            #endregion

            #region Initialize the disguise 1

            m_disguise_1 = new ScenceSprite()
            {
                TextureImage = DataReader.Load<Texture2D>( "Texture/PlayerState/Disguise_1" ),
                DestRect = new Rectangle( 520, 25, 115, 115 ),
                TColor = Color.White,
                FlashInterval = 0.000f,
                Visible = false,
            };
            #endregion

            #region Initialize the disguise 2

            m_disguise_2 = new ScenceSprite()
            {
                TextureImage = DataReader.Load<Texture2D>( "Texture/PlayerState/Disguise_2" ),
                DestRect = new Rectangle( 650, 25, 115, 115 ),
                TColor = Color.White,
                FlashInterval = 0.000f,
                Visible = false,
            };
            #endregion

            #region Initialize the disguise 3

            m_disguise_3 = new ScenceSprite()
            {
                TextureImage = DataReader.Load<Texture2D>( "Texture/PlayerState/Disguise_3" ),
                DestRect = new Rectangle( 650, 150, 115, 115 ),
                TColor = Color.White,
                FlashInterval = 0.000f,
                Visible = false,
            };
            #endregion



            #region Initialize the shining disguise1

            m_disguise_1_Counter_Shining = new ScenceSprite()
            {
                TextureImage = DataReader.Load<Texture2D>( "Texture/PlayerState/Disguise_1_Counter_Shining" ),
                DestRect = new Rectangle( 520, 25, 115, 115 ),
                BeginingFlashInterval = 0.008f,
                CurAlpha = ScenceSprite.MinAlpha,
                Visible = true,
            };
            m_disguise_1_Counter_Shining.TColor = new Color( new Vector4( 255, 255, 255, m_disguise_1_Counter_Shining.CurAlpha ) );
            #endregion

            #region Initialize the shining disguise2

            m_disguise_2_Counter_Shining = new ScenceSprite()
            {
                TextureImage = DataReader.Load<Texture2D>( "Texture/PlayerState/Disguise_2_Counter_Shining" ),
                DestRect = new Rectangle( 650, 25, 115, 115 ),
                BeginingFlashInterval = 0.008f,
                CurAlpha = ScenceSprite.MinAlpha,
                Visible = true,
            };
            m_disguise_2_Counter_Shining.TColor = new Color( new Vector4( 255, 255, 255, m_disguise_2_Counter_Shining.CurAlpha ) );
            #endregion

            #region Initialize the shining disguise3

            m_disguise_3_Counter_Shining = new ScenceSprite()
            {
                TextureImage = DataReader.Load<Texture2D>( "Texture/PlayerState/Disguise_3_Counter_Shining" ),
                DestRect = new Rectangle( 650, 150, 115, 115 ),
                BeginingFlashInterval = 0.008f,
                CurAlpha = ScenceSprite.MinAlpha,
                Visible = true,
            };
            m_disguise_3_Counter_Shining.TColor = new Color( new Vector4( 255, 255, 255, m_disguise_3_Counter_Shining.CurAlpha ) );
            #endregion



            #region Initialize the keys

            #region Initialize the sun key

            m_sunKey = new ScenceSprite()
            {
                Name = "SunKey",
                TextureImage = DataReader.Load<Texture2D>( "Texture/PlayerState/key_sun" ),
                DestRect = new Rectangle( 437, 260, 118, 108 ),
                TColor = Color.White,
                FlashInterval = 0.000f
            };
            #endregion

            #region Initialize the moon key

            m_moonKey = new ScenceSprite()
            {
                Name = "MoonKey",
                TextureImage = DataReader.Load<Texture2D>( "Texture/PlayerState/key_moon" ),
                DestRect = new Rectangle( 517, 296, 100, 102 ),
                TColor = Color.White,
                FlashInterval = 0.000f
            };
            #endregion

            #region Initialize the triangle key

            m_triangleKey = new ScenceSprite()
            {
                Name = "TriangleKey",
                TextureImage = DataReader.Load<Texture2D>( "Texture/PlayerState/key_triangle" ),
                DestRect = new Rectangle( 601, 310, 115, 115 ),
                TColor = Color.White,
                FlashInterval = 0.000f
            };
            #endregion

            #endregion

            #region Initialize the tags

            #region Initialize the tag 1

            m_tag_1 = new ScenceSprite()
            {
                Name = "Tag_1",
                TextureImage = DataReader.Load<Texture2D>( "Texture/PlayerState/tag_1" ),
                DestRect = new Rectangle( 411, 479, 40, 40 ),
                TColor = Color.White,
                FlashInterval = 0.000f
            };
            #endregion

            #region Initialize the tag 2

            m_tag_2 = new ScenceSprite()
            {
                Name = "Tag_2",
                TextureImage = DataReader.Load<Texture2D>( "Texture/PlayerState/tag_2" ),
                DestRect = new Rectangle( 457, 505, 40, 45 ),
                TColor = Color.White,
                FlashInterval = 0.000f
            };
            #endregion

            #region Initialize the tag 3

            m_tag_3 = new ScenceSprite()
            {
                Name = "Tag_3",
                TextureImage = DataReader.Load<Texture2D>( "Texture/PlayerState/tag_3" ),
                DestRect = new Rectangle( 492, 476, 40, 40 ),
                TColor = Color.White,
                FlashInterval = 0.000f
            };
            #endregion

            #region Initialize the tag 4

            m_tag_4 = new ScenceSprite()
            {
                Name = "Tag_4",
                TextureImage = DataReader.Load<Texture2D>( "Texture/PlayerState/tag_4" ),
                DestRect = new Rectangle( 538, 506, 40,40 ),
                TColor = Color.White,
                FlashInterval = 0.000f
            };
            #endregion

            #region Initialize the tag 5

            m_tag_5 = new ScenceSprite()
            {
                Name = "Tag_5",
                TextureImage = DataReader.Load<Texture2D>( "Texture/PlayerState/tag_5" ),
                DestRect = new Rectangle( 585, 478, 40, 40 ),
                TColor = Color.White,
                FlashInterval = 0.000f
            };
            #endregion

            #region Initialize the tag 6

            m_tag_6 = new ScenceSprite()
            {
                Name = "Tag_6",
                TextureImage = DataReader.Load<Texture2D>( "Texture/PlayerState/tag_6" ),
                DestRect = new Rectangle( 630, 510, 40, 40 ),
                TColor = Color.White,
                FlashInterval = 0.000f
            };
            #endregion

            #region Initialize the tag 7

            m_tag_7 = new ScenceSprite()
            {
                Name = "Tag_7",
                TextureImage = DataReader.Load<Texture2D>( "Texture/PlayerState/tag_7" ),
                DestRect = new Rectangle( 667, 477, 40, 40 ),
                TColor = Color.White,
                FlashInterval = 0.000f
            };
            #endregion

            #region Initialize the tag 8

            m_tag_8 = new ScenceSprite()
            {
                Name = "Tag_8",
                TextureImage = DataReader.Load<Texture2D>( "Texture/PlayerState/tag_8" ),
                DestRect = new Rectangle( 706, 505, 45, 45 ),
                TColor = Color.White,
                FlashInterval = 0.000f
            };
            #endregion

            #endregion


            #endregion

            #region Select whose visible is ture

            if( CoreUtilities.StateIsTrue( "items_un1" ) )
            {
                m_tag_1.Visible = true;
            }

            if (CoreUtilities.StateIsTrue("items_un2"))
            {
                m_tag_2.Visible = true;
            }
            if (CoreUtilities.StateIsTrue("items_un3"))
            {
                m_tag_3.Visible = true;
            }
            if (CoreUtilities.StateIsTrue("items_un4"))
            {
                m_tag_4.Visible = true;
            }
            if (CoreUtilities.StateIsTrue("items_un5"))
            {
                m_tag_5.Visible = true;
            }
            if (CoreUtilities.StateIsTrue("items_un6"))
            {
                m_tag_6.Visible = true;
            }
            if (CoreUtilities.StateIsTrue("items_un7"))
            {
                m_tag_7.Visible = true;
            }
            if (CoreUtilities.StateIsTrue("items_un8"))
            {
                m_tag_8.Visible = true;
            }



            if( CoreUtilities.StateIsTrue( "key_sun" ) )
            {
                m_sunKey.Visible = true;
            }
            if( CoreUtilities.StateIsTrue( "key_moon" ) )
            {
                m_moonKey.Visible = true;
            }
            if( CoreUtilities.StateIsTrue( "key_tri" ) )
            {
                m_triangleKey.Visible = true;
            }

            GameItemState.Set("hp", 1);
            int hp = (int)GameItemState.Get( "hp" );
            switch( hp )
            {
                case 1:
                    m_playerHp_1.Visible = true;
                    break;
                case 2:
                    m_playerHp_2.Visible = true;
                    break;
                case 3:
                    m_playerHp_3.Visible = true;
                    break;
            }


            GameItemState.Set( "enemyKind", 1 );
            GameItemState.Set( "collectingFirstDNANum", 3 );
            GameItemState.Set( "collectingSecondDNANum", 2 );
            GameItemState.Set( "collectingThirdDNANum", 1 );

            int enemyKind = (int)GameItemState.Get( "enemyKind" );
            int collectingFirstDNANum = (int)GameItemState.Get( "collectingFirstDNANum" );
            int collectingSecondDNANum = (int)GameItemState.Get( "collectingSecondDNANum" );
            int collectingThirdDNANum = (int)GameItemState.Get( "collectingThirdDNANum" );
                    switch(collectingFirstDNANum)
                    {
                        case 0:
                            m_zeroFifthFirstEnemyDNA.Visible = true;
                            m_disguiseBase_1.Visible = true;
                            break;
                        case 1:
                            m_oneFifthFirstEnemyDNA.Visible = true;
                            m_disguiseBase_1.Visible = true;
                            break;
                        case 2:
                            m_twoFifthFirstEnemyDNA.Visible = true;
                            m_disguiseBase_1.Visible = true;
                            break;
                        case 3:
                            m_threeFifthFirstEnemyDNA.Visible = true;
                            m_disguiseBase_1.Visible = true;
                            break;
                        case 4:
                            m_fourFifthFirstEnemyDNA.Visible = true;
                            m_disguiseBase_1.Visible = true;
                            break;
                        case 5:
                            m_fiveFifthFirstEnemyDNA.Visible = true;
                            m_disguise_1.Visible = true;
                            break;
                    }
                    switch( collectingSecondDNANum )
                    {
                        case 0:
                            m_zeroFifthSecondEnemyDNA.Visible = true;
                            m_disguiseBase_2.Visible = true;
                            break;
                        case 1:
                            m_oneFifthSecondEnemyDNA.Visible = true;
                            m_disguiseBase_2.Visible = true;
                            break;
                        case 2:
                            m_twoFifthSecondEnemyDNA.Visible = true;
                            m_disguiseBase_2.Visible = true;
                            break;
                        case 3:
                            m_threeFifthSecondEnemyDNA.Visible = true;
                            m_disguiseBase_2.Visible = true;
                            break;
                        case 4:
                            m_fourFifthSecondEnemyDNA.Visible = true;
                            m_disguiseBase_2.Visible = true;
                            break;
                        case 5:
                            m_fiveFifthSecondEnemyDNA.Visible = true;
                            m_disguise_2.Visible = true;
                            break;
                    }
                    switch( collectingThirdDNANum )
                    {
                        case 0:
                            m_zeroFifthThirdEnemyDNA.Visible = true;
                            m_disguiseBase_3.Visible = true;
                            break;
                        case 1:
                            m_oneFifthThirdEnemyDNA.Visible = true;
                            m_disguiseBase_3.Visible = true;
                            break;
                        case 2:
                            m_twoFifthThirdEnemyDNA.Visible = true;
                            m_disguiseBase_3.Visible = true;
                            break;
                        case 3:
                            m_threeFifthThirdEnemyDNA.Visible = true;
                            m_disguiseBase_3.Visible = true;
                            break;
                        case 4:
                            m_fourFifthThirdEnemyDNA.Visible = true;
                            m_disguiseBase_3.Visible = true;
                            break;
                        case 5:
                            m_fiveFifthThirdEnemyDNA.Visible = true;
                            m_disguise_3.Visible = true;
                            break;
                    }

            #endregion

            #region Add the elements to the Collection

            m_playerStateTexList = new List<ScenceSprite>();
            m_playerStateTexList.Add( m_background );
            m_playerStateTexList.Add( m_playerStateBase );

            m_playerStateTexList.Add( m_playerStateBaseShining );
            m_playerStateTexList.Add( m_playerStateBaseShiningEye );

            m_playerStateTexList.Add( m_playerHp_3 );
            m_playerStateTexList.Add( m_playerHp_2 );
            m_playerStateTexList.Add( m_playerHp_1 );

            m_playerStateTexList.Add( m_twig_1 );
            m_playerStateTexList.Add( m_twig_2 );
            m_playerStateTexList.Add( m_twig_3 );

            m_playerStateTexList.Add( m_shiningTwig_1 );
            m_playerStateTexList.Add( m_shiningTwig_2 );
            m_playerStateTexList.Add( m_shiningTwig_3 );

            m_playerStateTexList.Add( m_disguise_1_Counter_Shining );
            m_playerStateTexList.Add( m_disguise_2_Counter_Shining );
            m_playerStateTexList.Add( m_disguise_3_Counter_Shining );


            m_playerStateTexList.Add( m_disguiseBase_1 );
            m_playerStateTexList.Add( m_disguiseBase_2 );
            m_playerStateTexList.Add( m_disguiseBase_3 );

            m_playerStateTexList.Add( m_disguise_1 );
            m_playerStateTexList.Add( m_disguise_2 );
            m_playerStateTexList.Add( m_disguise_3 );


            m_playerStateTexList.Add( m_sunKey );
            m_playerStateTexList.Add( m_moonKey );
            m_playerStateTexList.Add( m_triangleKey );

            m_playerStateTexList.Add( m_tag_1 );
            m_playerStateTexList.Add( m_tag_2 );
            m_playerStateTexList.Add( m_tag_3 );
            m_playerStateTexList.Add( m_tag_4 );
            m_playerStateTexList.Add( m_tag_5 );
            m_playerStateTexList.Add( m_tag_6 );
            m_playerStateTexList.Add( m_tag_7 );
            m_playerStateTexList.Add( m_tag_8 );



            m_playerStateTexList.Add( m_zeroFifthFirstEnemyDNA );
            m_playerStateTexList.Add( m_oneFifthFirstEnemyDNA );
            m_playerStateTexList.Add( m_twoFifthFirstEnemyDNA );
            m_playerStateTexList.Add( m_threeFifthFirstEnemyDNA );
            m_playerStateTexList.Add( m_fourFifthFirstEnemyDNA );
            m_playerStateTexList.Add( m_fiveFifthFirstEnemyDNA );

            m_playerStateTexList.Add( m_zeroFifthSecondEnemyDNA );
            m_playerStateTexList.Add( m_oneFifthSecondEnemyDNA );
            m_playerStateTexList.Add( m_twoFifthSecondEnemyDNA );
            m_playerStateTexList.Add( m_threeFifthSecondEnemyDNA );
            m_playerStateTexList.Add( m_fourFifthSecondEnemyDNA );
            m_playerStateTexList.Add( m_fiveFifthSecondEnemyDNA );

            m_playerStateTexList.Add( m_zeroFifthThirdEnemyDNA );
            m_playerStateTexList.Add( m_oneFifthThirdEnemyDNA );
            m_playerStateTexList.Add( m_twoFifthThirdEnemyDNA );
            m_playerStateTexList.Add( m_threeFifthThirdEnemyDNA );
            m_playerStateTexList.Add( m_fourFifthThirdEnemyDNA );
            m_playerStateTexList.Add( m_fiveFifthThirdEnemyDNA );

            #endregion

            if (texture != null)
            {
                m_background.TextureImage = texture;
            }
        }

        internal void Twinkle( ScenceSprite scenceSprite )
        {
            if( scenceSprite.CurAlpha <= ScenceSprite.MaxAlpha )
            {
                scenceSprite.TColor = new Color( 1f, 1f, 1f, (float)Math.Sin( scenceSprite.CurAlpha * MathHelper.TwoPi ) * 0.5f + 0.5f );
                scenceSprite.CurAlpha += scenceSprite.FlashInterval;
                if( scenceSprite.CurAlpha >= ScenceSprite.MaxAlpha )
                    scenceSprite.CurAlpha = 0.0f;
            }
        }

        internal void ConverseTwinkle( ScenceSprite scenceSprite )
        {
            if( scenceSprite.CurAlpha <= ScenceSprite.MaxAlpha )
            {
                scenceSprite.TColor = new Color( 1f, 1f, 1f, -(float)Math.Sin( scenceSprite.CurAlpha * MathHelper.TwoPi ) * 0.5f + 0.5f );
                scenceSprite.CurAlpha += scenceSprite.FlashInterval;
                if( scenceSprite.CurAlpha >= ScenceSprite.MaxAlpha )
                    scenceSprite.CurAlpha = 0.0f;
            }
        }

        public void Update()
        {
            Twinkle(m_playerStateBaseShining);
            Twinkle( m_playerStateBaseShiningEye );

            Twinkle( m_disguise_1_Counter_Shining );
            Twinkle( m_disguise_2_Counter_Shining );
            Twinkle( m_disguise_3_Counter_Shining );

            Twinkle( m_disguise_1 );
            Twinkle( m_disguise_2 );
            Twinkle( m_disguise_3 );

            Twinkle( m_playerHp_1 );
            Twinkle( m_playerHp_2 );
            Twinkle( m_playerHp_3 );
            Twinkle( m_shiningTwig_1 );
            Twinkle( m_shiningTwig_2 );
            Twinkle( m_shiningTwig_3 );


            ConverseTwinkle( m_zeroFifthFirstEnemyDNA );
            ConverseTwinkle( m_oneFifthFirstEnemyDNA );
            ConverseTwinkle( m_twoFifthFirstEnemyDNA );
            ConverseTwinkle( m_threeFifthFirstEnemyDNA );
            ConverseTwinkle( m_fourFifthFirstEnemyDNA );
            ConverseTwinkle( m_fourFifthFirstEnemyDNA );


            ConverseTwinkle( m_zeroFifthSecondEnemyDNA );
            ConverseTwinkle( m_oneFifthSecondEnemyDNA );
            ConverseTwinkle( m_twoFifthSecondEnemyDNA );
            ConverseTwinkle( m_threeFifthSecondEnemyDNA );
            ConverseTwinkle( m_fourFifthSecondEnemyDNA );
            ConverseTwinkle( m_fourFifthSecondEnemyDNA );

            ConverseTwinkle( m_zeroFifthThirdEnemyDNA );
            ConverseTwinkle( m_oneFifthThirdEnemyDNA );
            ConverseTwinkle( m_twoFifthThirdEnemyDNA );
            ConverseTwinkle( m_threeFifthThirdEnemyDNA );
            ConverseTwinkle( m_fourFifthThirdEnemyDNA );
            ConverseTwinkle( m_fourFifthThirdEnemyDNA );


        }

        public void Draw( GameTime gameTime, SpriteBatch batch )
        {
            for( int i = 0; i < m_playerStateTexList.Count; i++ )
            {
                if( m_playerStateTexList[i].Visible )
                    m_playerStateTexList[i].DrawWithDestRectangle( gameTime, batch );
            }
            //batch.DrawString();
        }

        public void Initialize()
        {

        }
    }
}


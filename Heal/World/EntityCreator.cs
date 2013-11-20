using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Heal.Component;
using Heal.Core.AI;
using Heal.Core.Entities;
using Heal.Core.Entities.Enemies;
using Microsoft.Xna.Framework;
using Heal.Core.Utilities;
using System.Reflection;
using Heal.Data;
using Microsoft.Xna.Framework.Graphics;

namespace Heal.World
{
    public class EntityCreator : GameClassManager
    {
        public static EntityCreator GetInstance()
        {
            return m_instance;
        }

        public List<Unit> Units;
        public List<Item> Items;
        public List<Region> Regions;
        public Player Player;

        private static EntityCreator m_instance;

        public EntityCreator()
        {
            m_instance = this;
            Units = new List<Unit>();
            Items = new List<Item>();
            Regions = new List<Region>();
        }

        public delegate void EntityLoader();

        internal static void Create(string item, object[] param)
        {
            Type type = typeof (EntityCreator);
            type.InvokeMember("Create" + item, BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase | BindingFlags.InvokeMethod, null, m_instance,
                               param);
        }

        internal static void Load(string map)
        {
            m_instance.Units.Clear();
            m_instance.Items.Clear();
            m_instance.Regions.Clear();
            Type type = typeof(EntityCreator);
            type.InvokeMember("Load" + map, BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase | BindingFlags.InvokeMethod, null, m_instance,
                               new object[0] );
        }

        #region xutong

        public void CreateEnemyTypeI(Vector2 locate, AIBase.FaceSide faceSide)//new Vector2(650, 500), R
        {
            EnemyComponent enemyCompoment = new EnemyComponent();
            Enemy Monster1 = new EnemyTypeI( enemyCompoment, new Vector2( 60f, 0f ), locate, 215, 0.6f,
                                             EnemyTypeI.Size, faceSide, AIBase.ID.I, 200 );
            enemyCompoment.Initialize();
            enemyCompoment.Unit = Monster1;
            Monster1.Initialize();
            enemyCompoment.AddNew( "Monster1/m1_face_left", (int) Monster1.EnemySize, 100, AIBase.List.Left );
            enemyCompoment.AddNew( "Monster1/m1_eyes_left", (int) Monster1.EnemySize, 100, AIBase.List.Left );
            enemyCompoment.AddNew( "Monster1/m1_sharp_left", (int) Monster1.EnemySize, 100, AIBase.List.Left );

            enemyCompoment.AddNew( "Monster1/m1_face_right", (int) Monster1.EnemySize, 100, AIBase.List.Right );
            enemyCompoment.AddNew( "Monster1/m1_eyes_right", (int) Monster1.EnemySize, 100, AIBase.List.Right );
            enemyCompoment.AddNew( "Monster1/m1_sharp_right", (int) Monster1.EnemySize, 100, AIBase.List.Right );

            enemyCompoment.AddNew( "Monster1/m1_turn_right", (int) Monster1.EnemySize, 20, AIBase.List.TurnRight );
            enemyCompoment.AddNew( "Monster1/m1_turn_left", (int) Monster1.EnemySize, 20, AIBase.List.TurnLeft );

            enemyCompoment.AddNew( "Rings/E_cycle_b", (int) ( Monster1.RingSize ), 100, AIBase.List.Ring );
            enemyCompoment.AddNew( "Rings/E_cycle_r", (int) ( Monster1.RingSize ), 100, AIBase.List.Ring );
            enemyCompoment.AddNew( "Rings/E_cycle_g", (int) ( Monster1.RingSize ), 100, AIBase.List.Ring );
            enemyCompoment.AddNew( "Rings/E_cycle_y", (int) ( Monster1.RingSize ), 100, AIBase.List.Ring );
            Units.Add( Monster1 );
        }

        public void CreateEnemyTypeIII(Vector2 locate, AIBase.FaceSide faceSide)// new Vector2(700, 400), L
        {
            EnemyComponent enemyCompoment;
            enemyCompoment = new EnemyComponent();
            Enemy Monster3 = new EnemyTypeIII(enemyCompoment, new Vector2(80f, 0f), locate, 300, 0.6f, EnemyTypeIII.Size,
                                             faceSide, AIBase.ID.III, 140f);
            enemyCompoment.Initialize();
            enemyCompoment.Unit = Monster3;
            Monster3.Initialize();

            enemyCompoment.AddNew("Monster3/monster3_sleep", (int)Monster3.EnemySize, 500, AIBase.List.Left);
            enemyCompoment.AddNew("Monster3/monster3_downfall", (int)Monster3.EnemySize, 100, AIBase.List.Right);
            enemyCompoment.AddNew("Rings/E_cycle_b", (int)(Monster3.RingSize), 100, AIBase.List.Ring);
            enemyCompoment.AddNew("Rings/E_cycle_r", (int)(Monster3.RingSize), 100, AIBase.List.Ring);
            enemyCompoment.AddNew("Rings/E_cycle_g", (int)(Monster3.RingSize), 100, AIBase.List.Ring);
            enemyCompoment.AddNew("Rings/E_cycle_y", (int)(Monster3.RingSize), 100, AIBase.List.Ring);
            Units.Add(Monster3);
        }

        public void CreateEnemyTypeIV(Vector2 locate, AIBase.FaceSide faceSide)//new Vector2(700, 800), L
        {
            EnemyComponent enemyCompoment;

            enemyCompoment = new EnemyIVComponent();
            Enemy Monster4 = new EnemyTypeIV(enemyCompoment, new Vector2(0), locate, 300, 0.6f, EnemyTypeIV.Size,
                                            faceSide, AIBase.ID.IV, 200);
            enemyCompoment.Initialize();
            enemyCompoment.Unit = Monster4;
            enemyCompoment.AddNew("Monster4/monster4_silence", (int)Monster4.EnemySize, 100, AIBase.List.Left);
            enemyCompoment.AddNew("Monster4/monster4_sleep", (int)Monster4.EnemySize, 100, AIBase.List.Left);
            enemyCompoment.AddNew("Monster4/monster4_alarm", (int)Monster4.EnemySize, 100, AIBase.List.Right);
            ((EnemyIVComponent)enemyCompoment).AddBlackHole();
            enemyCompoment.AddNew("Rings/E_cycle_b", (int)Monster4.RingSize, 100, AIBase.List.Ring);
            enemyCompoment.AddNew("Rings/E_cycle_r", (int)Monster4.RingSize, 100, AIBase.List.Ring);
            enemyCompoment.AddNew("Rings/E_cycle_g", (int)Monster4.RingSize, 100, AIBase.List.Ring);
            enemyCompoment.AddNew("Rings/E_cycle_y", (int)Monster4.RingSize, 100, AIBase.List.Ring);
            Units.Add(Monster4);
        }

        public void CreatEnemyTypeII(Vector2 locate, AIBase.FaceSide faceSide)
        {
            EnemyIIComponent enemyComponent;
            enemyComponent = new EnemyIIComponent();
            enemyComponent.Initialize();
            EnemyTypeII Monster2 = new EnemyTypeII(enemyComponent, new Vector2(80, 0), locate,300, 1, 1,
                                             AIBase.FaceSide.Right, AIBase.ID.II);
            
            enemyComponent.Unit = Monster2;
            enemyComponent.AddBody();
            enemyComponent.AddSharp("Monster2/monster2_body",(int)Monster2.EnemySize,200);
            enemyComponent.AddNew("Rings/E_cycle_b", (int)Monster2.RingSize, 100, AIBase.List.Ring);
            enemyComponent.AddNew("Rings/E_cycle_r", (int)Monster2.RingSize, 100, AIBase.List.Ring);
            enemyComponent.AddNew("Rings/E_cycle_g", (int)Monster2.RingSize, 100, AIBase.List.Ring);
            enemyComponent.AddNew("Rings/E_cycle_y", (int)Monster2.RingSize, 100, AIBase.List.Ring);
            Units.Add(Monster2);
        }

        public void CreateGhost(Vector2 locate, AIBase.FaceSide faceSide)//new Vector2(800, 500), R
        {
            GhostComponent ghostComponent = new GhostComponent();
            ghostComponent.Initialize();
            Enemy ghost = new Ghost(ghostComponent, new Vector2(80, 0), locate, 350, 1f, 80,
                                   faceSide, AIBase.ID.V);
            ghostComponent.Unit = ghost;
            ghostComponent.AddToLeft("Ghost/ghost_bodyleft_weak", "Ghost/ghost_bodyleft_normal",
                                     "Ghost/ghost_bodyleft_strong",
                                     "Ghost/ghost_eyeleft_weak", "Ghost/ghost_eyeleft_normal",
                                     "Ghost/ghost_eyeleft_strong",
                                     "Ghost/ghost_lightleft_weak", "Ghost/ghost_lightleft_normal",
                                     "Ghost/ghost_lightleft_strong");
            ghostComponent.AddToRight("Ghost/ghost_bodyRight_weak", "Ghost/ghost_bodyRight_normal",
                                      "Ghost/ghost_bodyRight_strong",
                                      "Ghost/ghost_eyeRight_weak", "Ghost/ghost_eyeRight_normal",
                                      "Ghost/ghost_eyeRight_strong",
                                      "Ghost/ghost_lightRight_weak", "Ghost/ghost_lightRight_normal",
                                      "Ghost/ghost_lightRight_strong");
            ghostComponent.AddNew("Rings/E_cycle_b", (int)ghost.RingSize, 100, AIBase.List.Ring);
            ghostComponent.AddNew("Rings/E_cycle_r", (int)ghost.RingSize, 100, AIBase.List.Ring);
            ghostComponent.AddNew("Rings/E_cycle_g", (int)ghost.RingSize, 100, AIBase.List.Ring);
            ghostComponent.AddNew("Rings/E_cycle_y", (int)ghost.RingSize, 100, AIBase.List.Ring);

            Units.Add( ghost );
        }

        public void CreateSmokeBoss()
        {
            SmokeBossComponent bossComponent = new SmokeBossComponent();
            SmokeBoss smokeBoss = new SmokeBoss(bossComponent, new Vector2(80, 0), new Vector2(0, 512), AIBase.ID.Boss, 2f,
                                           new Vector2(300f, 0f), new Vector2(100, 0));
            bossComponent.Initialize();
            bossComponent.Unit = smokeBoss;
            bossComponent.AddToList("SmokeBoss/sb_firstFloor", smokeBoss.Postion, 20);
            bossComponent.AddToList("SmokeBoss/sb_middleFloor", smokeBoss.Postion, 60);
            bossComponent.AddToList("SmokeBoss/sb_lastFloor", smokeBoss.Postion, 40);
            Units.Add(smokeBoss);
        }

        public void CreateStoneBoss(Vector2 locate, AIBase.FaceSide faceSide)//new Vector2(5450, 400)
        {            StoneBossComponent stoneBossComponent = new StoneBossComponent();
            stoneBossComponent.Initialize();
            Enemy StoBoss = new StoneBoss(stoneBossComponent, new Vector2(StoneBoss.AttackSpeed, 0), locate, 0.8f, AIBase.ID.Boss);
            stoneBossComponent.Unit = StoBoss;
            stoneBossComponent.AddToList("StoneBoss/m6_body",
                                         "StoneBoss/m6_eye",
                                         "StoneBoss/m6_lefthand",
                                         "StoneBoss/m6_righthand",
                                         "StoneBoss/m6_leftleg",
                                         "StoneBoss/m6_rightleg");
            Units.Add(StoBoss);
        } 

        public void CreateBall(Vector2[] position)
        {
            EnergyBallComponent energyBallComponent = new EnergyBallComponent();
            energyBallComponent.Initialize();
            EnergyBall energyBall = new EnergyBall(energyBallComponent);
            energyBallComponent.Unit = energyBall;
            energyBallComponent.AddTexture("energy_ball");
            Items.Add( energyBall );
            foreach( Vector2 vector2 in position )
            {
                energyBall.AddNode( vector2 );
            }
        }

        public void CreateRegion(Point[] points, string cmd)
        {

        }

        public void CreateEnemyTypeVI(Vector2 locate, AIBase.FaceSide faceSide)//new Vector2(650, 500), R
        {
            EnemyVIComponent enemyCompoment = new EnemyVIComponent();
            Enemy Monster6 = new EnemyTypeVI(enemyCompoment, new Vector2(80f, 0f), locate, 200, 0.6f,
                                             EnemyTypeI.Size, faceSide, AIBase.ID.VI, 200);
            enemyCompoment.Initialize();
            enemyCompoment.Unit = Monster6;
            Monster6.Initialize();
            enemyCompoment.AddNew("Monster1/m1_face_left", (int)Monster6.EnemySize, 100, AIBase.List.Left);
            enemyCompoment.AddNew("Monster1/m1_eyes_left", (int)Monster6.EnemySize, 100, AIBase.List.Left);
            enemyCompoment.AddNew("Monster1/m1_sharp_left", (int)Monster6.EnemySize, 100, AIBase.List.Left);

            enemyCompoment.AddNew("Monster1/m1_face_right", (int)Monster6.EnemySize, 100, AIBase.List.Right);
            enemyCompoment.AddNew("Monster1/m1_eyes_right", (int)Monster6.EnemySize, 100, AIBase.List.Right);
            enemyCompoment.AddNew("Monster1/m1_sharp_right", (int)Monster6.EnemySize, 100, AIBase.List.Right);

            enemyCompoment.AddNew("Monster1/m1_turn_right", (int)Monster6.EnemySize, 20, AIBase.List.TurnRight);
            enemyCompoment.AddNew("Monster1/m1_turn_left", (int)Monster6.EnemySize, 20, AIBase.List.TurnLeft);

            enemyCompoment.AddNew("Rings/E_cycle_b", (int)(Monster6.RingSize), 100, AIBase.List.Ring);
            enemyCompoment.AddNew("Rings/E_cycle_r", (int)(Monster6.RingSize), 100, AIBase.List.Ring);
            enemyCompoment.AddNew("Rings/E_cycle_g", (int)(Monster6.RingSize), 100, AIBase.List.Ring);
            enemyCompoment.AddNew("Rings/E_cycle_y", (int)(Monster6.RingSize), 100, AIBase.List.Ring);

            enemyCompoment.AddToIron("Monster5/m5_turn_left","Monster5/m5_turn_right",(int)Monster6.EnemySize,100);
            enemyCompoment.AddIronFace("Monster5/iron_left","Monster5/iron_right");

            Units.Add(Monster6);
        }

        public void CreatBigDaddy()
        {
            BigDaddyComponent bigDaddyComponent = new BigDaddyComponent();
            bigDaddyComponent.Initialize();
            Enemy BigDaddy = new BigDaddy(bigDaddyComponent, new Vector2(120, 0), new Vector2(512), 120, 1.2f,
                                          AIBase.FaceSide.Right, AIBase.ID.Boss);
            bigDaddyComponent.Unit = BigDaddy;

            bigDaddyComponent.AddLeftBody("BigDaddy/boss1_body_left_best",
                                          "BigDaddy/boss1_body_left_good",
                                          "BigDaddy/boss1_body_left_weak",
                                          "BigDaddy/boss1_body_left_bad");
            bigDaddyComponent.AddRightBody("BigDaddy/boss1_body_right_best",
                                          "BigDaddy/boss1_body_right_good",
                                          "BigDaddy/boss1_body_right_weak",
                                          "BigDaddy/boss1_body_right_bad");
            bigDaddyComponent.AddLeftDriver("BigDaddy/boss1_driver_left_best",
                                            "BigDaddy/boss1_driver_left_good",
                                            "BigDaddy/boss1_driver_left_weak",
                                            "BigDaddy/boss1_driver_left_bad");
            bigDaddyComponent.AddRightDriver("BigDaddy/boss1_driver_right_best",
                                            "BigDaddy/boss1_driver_right_good",
                                            "BigDaddy/boss1_driver_right_weak",
                                            "BigDaddy/boss1_driver_right_bad");

            bigDaddyComponent.AddNew("BigDaddy/boss1_aiguille_left",(int)BigDaddy.EnemySize,50,AIBase.List.Left);
            bigDaddyComponent.AddNew("BigDaddy/boss1_smoke_left",(int)BigDaddy.EnemySize,50,AIBase.List.Left);

            bigDaddyComponent.AddNew("BigDaddy/boss1_aiguille_right",(int)BigDaddy.EnemySize,50,AIBase.List.Right);
            bigDaddyComponent.AddNew("BigDaddy/boss1_smoke_right",(int)BigDaddy.EnemySize,50,AIBase.List.Right);

            bigDaddyComponent.AddNew("BigDaddy/boss1_turn_left",(int)BigDaddy.EnemySize,50,AIBase.List.TurnLeft);
            bigDaddyComponent.AddNew("BigDaddy/boss1_turn_right",(int)BigDaddy.EnemySize,50,AIBase.List.TurnRight);
            bigDaddyComponent.AddElecBall();
            bigDaddyComponent.AddScence();

            Units.Add(BigDaddy);
        }

        public void CreatBigSnake()
        {
            BigSnakeComponent bigSnakeComponet = new BigSnakeComponent();
            bigSnakeComponet.Initialize();
            Enemy BigSnake = new BigSnake(bigSnakeComponet, new Vector2(80, 0), new Vector2(768,512), 320, 0.5f,
                                          AIBase.ID.Boss);
            bigSnakeComponet.Unit = BigSnake;
            
            bigSnakeComponet.AddHead("BigSnake/boss2_head_good",
                                     "BigSnake/boss2_head_weak",
                                     "BigSnake/boss2_head_bad");

            bigSnakeComponet.AddHP("BigSnake/boss2_hp_1",
                                   "BigSnake/boss2_hp_2",
                                   "BigSnake/boss2_hp_3");

            bigSnakeComponet.AddOther("BigSnake/boss2_smoke","BigSnake/boss2_headelec");

            bigSnakeComponet.AddNewBodyNode("BigSnake/boss2_body",
                                            "BigSnake/boss2_rightglass_good",
                                            "BigSnake/boss2_rightglass_broke",
                                            "BigSnake/boss2_leftglass_good",
                                            "BigSnake/boss2_leftglass_broke",
                                            BigSnake.Locate);

            bigSnakeComponet.AddNewBodyNode("BigSnake/boss2_body",
                                            "BigSnake/boss2_rightglass_good",
                                            "BigSnake/boss2_rightglass_broke",
                                            "BigSnake/boss2_leftglass_good",
                                            "BigSnake/boss2_leftglass_broke",
                                            BigSnake.Locate);

            bigSnakeComponet.AddNewBodyNode("BigSnake/boss2_body",
                                "BigSnake/boss2_rightglass_good",
                                "BigSnake/boss2_rightglass_broke",
                                "BigSnake/boss2_leftglass_good",
                                "BigSnake/boss2_leftglass_broke",
                                BigSnake.Locate);

            bigSnakeComponet.AddNewBodyNode("BigSnake/boss2_body",
                                "BigSnake/boss2_rightglass_good",
                                "BigSnake/boss2_rightglass_broke",
                                "BigSnake/boss2_leftglass_good",
                                "BigSnake/boss2_leftglass_broke",
                                BigSnake.Locate);

            bigSnakeComponet.AddNewBodyNode("BigSnake/boss2_body",
                                "BigSnake/boss2_rightglass_good",
                                "BigSnake/boss2_rightglass_broke",
                                "BigSnake/boss2_leftglass_good",
                                "BigSnake/boss2_leftglass_broke",
                                BigSnake.Locate);

            ((BigSnake) BigSnake).BodyNodeList[4].Next = false;
            Units.Add(BigSnake);
        }

        #endregion

        #region Test Initialize Data
        public void CreatePlayer()
        {
            PlayerComponent playerComponent = new PlayerComponent();
            Player player = new Player(playerComponent, new Vector2(180f, 0f), new Vector2(150, 150),200, 0.6f, Player.Size,
                                      AIBase.FaceSide.Right, AIBase.ID.Zero, 180, 400, 20);
            playerComponent.Initialize();
            playerComponent.Unit = player;
            playerComponent.Unit.Initialize();

            playerComponent.AddNew("player/player_face", (int)player.EnemySize, 100, AIBase.List.Left);
            playerComponent.AddNew("player/player_eyes", (int)player.EnemySize, 100, AIBase.List.Left);
            playerComponent.AddNew("player/player_light", (int)player.EnemySize, 100, AIBase.List.Left);
            playerComponent.AddNew("player/player_headband_left", (int)player.EnemySize, 100, AIBase.List.Left);

            playerComponent.AddNew("player/player_face", (int)player.EnemySize, 100, AIBase.List.Right);
            playerComponent.AddNew("player/player_eyes", (int)player.EnemySize, 100, AIBase.List.Right);
            playerComponent.AddNew("player/player_light", (int)player.EnemySize, 100, AIBase.List.Right);
            playerComponent.AddNew("player/player_headband_right", (int)player.EnemySize, 100, AIBase.List.Right);

            playerComponent.AddNew("player/player_turn_left", (int)player.EnemySize, 17, AIBase.List.TurnLeft);

            playerComponent.AddNew("player/player_turn_right", (int)player.EnemySize, 17, AIBase.List.TurnRight);

            playerComponent.AddNew("Rings/E_cycle_b", (int)player.RingSize, 100, AIBase.List.Ring);

            playerComponent.AddToUnable();
            playerComponent.AddToAble();
            playerComponent.AddDefault();
            playerComponent.AddMask();
            playerComponent.AddEye();
            playerComponent.AddHP();
            //playerComponent.AddBubble();

            Player = player;

            Units.Add(player);

        }

        public void CreatDNA()
        {
            DNAComponent dnaComponent = new DNAComponent();
            DNA dna = new DNA();
            dnaComponent.Initialize();
            dnaComponent.Unit = dna;
                
            (dnaComponent).AddTexture();

            Items.Add(dna);
        }

        public void CreatLittlFlower(Vector2 postion,bool up)
        {
            NPCComponent npcComponent = new NPCComponent();
            LittleFlower Flower = new LittleFlower(npcComponent,postion,1f,AIBase.ID.NPC);

            Flower.ToUpdate = up;
            npcComponent.Initialize();
            npcComponent.Unit = Flower;

            npcComponent.AddNew("Flower/friend_face",(int)Flower.EnemySize,100,AIBase.List.Right);

            npcComponent.AddNew("Flower/friend_leaf", (int)Flower.EnemySize, 100, AIBase.List.Right);
            
            npcComponent.AddNew("Flower/friend_face", (int)Flower.EnemySize, 100, AIBase.List.Left);

            npcComponent.AddNew("Flower/friend_leaf", (int)Flower.EnemySize, 100, AIBase.List.Left);
           
            npcComponent.AddNew("Flower/friend_face", (int)Flower.EnemySize, 100, AIBase.List.TurnRight);

            npcComponent.AddNew("Flower/friend_leaf", (int) Flower.EnemySize, 100, AIBase.List.TurnLeft);
            npcComponent.AddNew("Flower/friend_face", (int)Flower.EnemySize, 100, AIBase.List.Ring);

            Units.Add(Flower);

        }

        public void CreateGrayPlayer()
        {
            PlayerComponent playerComponent = new PlayerComponent();
            Player player = new GrayPlayer(playerComponent, new Vector2(180f, 0f), new Vector2(150, 150), 0.6f, Player.Size,
                                      AIBase.FaceSide.Right, AIBase.ID.Zero, 180, 400, 20);
            playerComponent.Initialize();
            playerComponent.Unit = player;
            playerComponent.Unit.Initialize();

            playerComponent.AddNew("grayplayer/player_face", (int)player.EnemySize, 100, AIBase.List.Left);
            playerComponent.AddNew("grayplayer/player_eyes", (int)player.EnemySize, 100, AIBase.List.Left);
            playerComponent.AddNew("grayplayer/player_light", (int)player.EnemySize, 100, AIBase.List.Left);
            playerComponent.AddNew("grayplayer/player_headband_left", (int)player.EnemySize, 100, AIBase.List.Left);

            playerComponent.AddNew("grayplayer/player_face", (int)player.EnemySize, 100, AIBase.List.Right);
            playerComponent.AddNew("grayplayer/player_eyes", (int)player.EnemySize, 100, AIBase.List.Right);
            playerComponent.AddNew("grayplayer/player_light", (int)player.EnemySize, 100, AIBase.List.Right);
            playerComponent.AddNew("grayplayer/player_headband_right", (int)player.EnemySize, 100, AIBase.List.Right);

            playerComponent.AddNew("grayplayer/player_turn_left", (int)player.EnemySize, 17, AIBase.List.TurnLeft);

            playerComponent.AddNew("grayplayer/player_turn_right", (int)player.EnemySize, 17, AIBase.List.TurnRight);

            playerComponent.AddNew("Rings/E_cycle_b", (int)player.RingSize, 100, AIBase.List.Ring);

            playerComponent.AddToUnable();
            playerComponent.AddToAble();
            playerComponent.AddDefault();
            playerComponent.AddMask();
            playerComponent.AddEye();
            playerComponent.AddHP();
            Player = player;

            Units.Add(player);

        }

        public void CreateTeleport(Vector2 locate1, Vector2 locate2, string config)
        {
            TeleportComponent component = new TeleportComponent();
            Teleport teleport = new Teleport( locate1, locate2, config, component );
            component.Unit = teleport;
            Items.Add( teleport );
        }

        public void CreateSwitcher(Vector2 locate, string config, float timer)
        {
            SwitchComponent component =
                new SwitchComponent( DataReader.Load<Texture2D>( "Texture/Entities/Switcher/a_en" ),
                                     DataReader.Load<Texture2D>( "Texture/Entities/Switcher/a_dis" ) );
            Switcher item = new Switcher( locate, config, timer, component );
            component.Unit = item;
            Items.Add( item );
        }

        public void CreateElement(Vector2 locate, string config, string command, string texture, float scale)
        {
            ItemComponent component = new ElementsComponent();
            Item item = new Element( locate, config, command, DataReader.Load<Texture2D>( "Texture/Elements/" + texture ), scale, component );
            component.Unit = item;
            Items.Add( item );
        }

        public void CreateElementInfo(string texture, string config)
        {
            ItemComponent component = new ElementInfoComponent();
            Item item = new ElementInfo( DataReader.Load<Texture2D>( "Texture/ElementInfo/" + texture ), config,
                                         component );
            component.Unit = item;
            Items.Add( item );
        }
            #endregion

        public void LoadMap0()
        {
            CreateGrayPlayer();
            CreateSmokeBoss();

            Regions.Add(new Region(
                               new[] { new Vector2(1400, 330), new Vector2(1320, 620), new Vector2(1380, 690), new Vector2(1530, 460) },
                               "Camera Play0_1 ; State map0_play1 bool false ; State map0_smoke bool true", "map0_play1"));

            GameItemState.Set( "map0_smoke", false );
            GameItemState.Set("map0_play1", true);
            GameItemState.Set("map1_first", true);
            GameItemState.Set("map2_first", true);
            GameItemState.Set("map3_normal", true);
        }

        public void LoadMap1()
        {
            CreatePlayer();

            Regions.Add(new Region(
                             new[] { new Vector2(3380, 350), new Vector2(3610, 650), new Vector2(3820, 490) },
                             "Read Map2", null));

            Regions.Add(new Region(
                             new[]
                                 {
                                     new Vector2( 5050, 300 ), new Vector2( 5050, 800 ), new Vector2( 5115, 800 ),
                                     new Vector2( 5115, 300 )
                                 },
                             "GotoXY Map2 200 500 ; State map1_first bool false", null));

            if( !CoreUtilities.StateIsFalse( "map1_first" ) )
            {
                Regions.Add(new Region(
                                 new[]
                                 {
                                     new Vector2( 1600, 580 ), new Vector2( 1420, 820 ), new Vector2( 1460, 870 ),
                                     new Vector2( 1680, 650 )
                                 },
                                 "Camera Play1_1 ; State map1_first_play1 bool false", "map1_first_play1"));

                GameItemState.Set("map1_first_play1", true);
                GameItemState.Set( "map2_first", true );

                this.CreateEnemyTypeI(new Vector2(2300, 480), AIBase.FaceSide.Right);
                this.CreateEnemyTypeI(new Vector2(3170,718),AIBase.FaceSide.Right );
                this.CreateEnemyTypeI(new Vector2(3578,482), AIBase.FaceSide.Left);

                this.CreatLittlFlower(new Vector2(2500, 480),false);
                
                this.CreateBall(new Vector2[] {new Vector2(2082,347),new Vector2(2252,723),new Vector2(2375,571),new Vector2(1935,574),new Vector2(2464,279),new Vector2(2827,727),new Vector2(2996,522),new Vector2(3314,783),
                new Vector2(3754,748),new Vector2(2130,567)});
            }
            else
            {
                CreateEnemyTypeI( new Vector2(644,480),AIBase.FaceSide.Right  );
                CreateEnemyTypeI( new Vector2(870,450),AIBase.FaceSide.Right  );
                CreateEnemyTypeI( new Vector2(780,788),AIBase.FaceSide.Right  );
                CreateEnemyTypeI( new Vector2(110,570),AIBase.FaceSide.Right  );
                CreateEnemyTypeI( new Vector2(1950,440),AIBase.FaceSide.Right  );
                CreateEnemyTypeI( new Vector2(2010,595),AIBase.FaceSide.Right  );
                CreateEnemyTypeI( new Vector2(2340,585),AIBase.FaceSide.Right  );
                CreateEnemyTypeI( new Vector2(3190,730),AIBase.FaceSide.Right  );
                CreateEnemyTypeI( new Vector2(3473,480),AIBase.FaceSide.Right  );
                
                CreateElement( new Vector2(370,690), "items_un1", "splash un1 resume", "un1", 1 );
//370,690 UN1
//644,480 E1
//870,450 E1
//780,788 E1
//110,570 E1
//1950,440 E1
//2010,595 E1
//2340,585 E1
//3190,730 E1
//3473,480 E1
            }

        }

        public void LoadMap2()
        {
            CreatePlayer();
            CreatLittlFlower(new Vector2(200), true);

            GameItemState.Set( "map3_normal", true );

            Regions.Add(new Region(
                             new[] { new Vector2(150, 150), new Vector2(150, 400), new Vector2(400, 400), new Vector2(400, 150) },
                             "Read Map1", null));
            Regions.Add(new Region(
                             new[]
                                 {
                                     new Vector2( 1, 210 ), new Vector2( 1, 850 ), new Vector2( 60, 850 ),
                                     new Vector2( 60, 210 )
                                 },
                             "GotoXY Map1 5000 550 ; State map2_first bool false", null));



            Regions.Add(new Region(
                             new[] { new Vector2(640, 190), new Vector2(1350, 260), new Vector2(1400, 92) },
                             "map3_normal=true:Read Map3;map3_normal=false:Read Map17", null));
            Regions.Add(new Region(
                             new[]
                                 {
                                     new Vector2( 800, 1 ), new Vector2( 1200, 1 ), new Vector2( 1200, 60 ),
                                     new Vector2( 800, 60 )
                                 },
                             "map3_normal=true:GotoXY Map3 1870 1450 ; map3_normal=false:GotoXY Map17 1870 1450 ; State map2_first bool false", null));



            Regions.Add(new Region(
                             new[] { new Vector2(640, 870), new Vector2(1350, 780), new Vector2(1400, 910) },
                             "Read Map4", null));
            Regions.Add(new Region(
                             new[]
                                 {
                                     new Vector2( 800, 960 ), new Vector2( 1200, 960 ), new Vector2( 1200, 1023 ),
                                     new Vector2( 800, 1023 )
                                 },
                             "GotoXY Map4 340 70 ; State map2_first bool false", null));


            
        }

        public void LoadMap3()
        {
            GameItemState.Set("map3_normal", true);
            CreatePlayer();
            CreatLittlFlower(new Vector2(100), true);

            this.CreateEnemyTypeI(new Vector2(587, 1211), AIBase.FaceSide.Right);
            this.CreatEnemyTypeII(new Vector2(1018, 436), AIBase.FaceSide.Right);
            this.CreatEnemyTypeII(new Vector2(659, 561), AIBase.FaceSide.Right);
            this.CreatEnemyTypeII(new Vector2(433, 353), AIBase.FaceSide.Right);

            this.CreateEnemyTypeIII(new Vector2(2722, 471), AIBase.FaceSide.Left);

            this.CreateStoneBoss(new Vector2(5400, 414), AIBase.FaceSide.Left);

            this.CreateBall(new Vector2[] {new Vector2(1296,1204),new Vector2(1591,1171),new Vector2(920,682),new Vector2(1037,335),
            new Vector2(974,279),new Vector2(894,288),new Vector2(880,361),new Vector2(943,384),new Vector2(577,1205) ,new Vector2(2099,1180),
            new Vector2(2061,738),new Vector2(4393,188),new Vector2(4448,162),new Vector2(4551,123),new Vector2(4513,96),new Vector2(2722,564),
            new Vector2(2779,555),new Vector2(4389,1115)  
        });
            /*880,435 Enemy2
745,555 Enemy2
433,1230 Enemy1
3105,525 Enemy1
3850,315 Enemy1
5609,1210 Enemy2
5700,1260 Enemy2
5630,1110 Enemy2*/


            CreateTeleport(new Vector2(1900, 600), new Vector2(1975, 180), "map3_tp1");
            CreateSwitcher(new Vector2(410, 373), "map3_tp1", -1f);

            Regions.Add(new Region(
                             new[] { new Vector2(4200, 40), new Vector2(4420, 365), new Vector2(4700, 200) },
                             "Read Map16", null));
            Regions.Add(new Region(
                             new[]
                                 {
                                     new Vector2( 4150, 1 ), new Vector2( 4150, 60 ), new Vector2( 4650, 60 ),
                                     new Vector2( 4650, 1 )
                                 },
                             "GotoXY Map16 530 940", null));

            Regions.Add(new Region(
                             new[] { new Vector2(1510, 1400), new Vector2(2100, 1500), new Vector2(2100, 1200) },
                             "Read Map2", null));
            Regions.Add(new Region(
                             new[]
                                 {
                                     new Vector2( 1600, 1476 ), new Vector2( 1600, 1536 ), new Vector2( 2200, 1536 ),
                                     new Vector2( 2200, 1476 )
                                 },
                             "GotoXY Map2 1000 75", null));

            CreateElement( new Vector2(290,1265), "key_tri", null, "key_tri", 1 );

            CreateElement(new Vector2(5900, 258), "key_sun", null, "key_sun", 1);
        }

        public void LoadMap4()
        {
            CreatePlayer();
            /*
             * 1420,515 落石
    2177,513 落石
    570,580 E1
    1193,605 E1
    2709,377 E1*/
            this.CreatLittlFlower(new Vector2(500),true);
            this.CreateEnemyTypeIII(new Vector2(1420,515), AIBase.FaceSide.Left );
            this.CreateEnemyTypeIII(new Vector2(2177,513),AIBase.FaceSide.Right);

            this.CreateEnemyTypeI(new Vector2(1193,605),AIBase.FaceSide.Left );
            this.CreateEnemyTypeI(new Vector2(2709,377),AIBase.FaceSide.Right );

            this.CreateGhost(new Vector2(3768,372),AIBase.FaceSide.Right);
            this.CreateEnemyTypeIV(new Vector2(1869,308),AIBase.FaceSide.Left);
           

            this.CreateBall(new[] { new Vector2(3356,311),new Vector2(3356,353),new Vector2(3356,390),new Vector2(3407,3310),new Vector2(3467,333),
            new Vector2(3435,360),new Vector2(3383,397) , new Vector2(382,675),new Vector2(845,598),new Vector2(1393,721) ,new Vector2(1957,444)});

            Regions.Add(new Region(
                             new[] { new Vector2(150, 240), new Vector2(570, 240), new Vector2(180, 430) },
                             "Read Map2", null));
            Regions.Add(new Region(
                             new[]
                                 {
                                     new Vector2( 220, 1 ), new Vector2( 220, 60 ), new Vector2( 460, 60 ),
                                     new Vector2( 460, 1 )
                                 },
                             "GotoXY Map2 1000 940", null));

            Regions.Add(new Region(
                             new[] { new Vector2(3500, 750), new Vector2(3500, 940), new Vector2(4100, 950) },
                             "Read Map5", null));
            Regions.Add(new Region(
                             new[]
                                 {
                                     new Vector2( 3550, 970 ), new Vector2( 3550, 1023 ), new Vector2( 5000, 1023 ),
                                     new Vector2( 5000, 970 )
                                 },
                             "GotoXY Map5 420 80", null));

            CreateElement( new Vector2( 4860, 337 ), "key_moon", null, "key_moon", 1 );
        }

        public void LoadMap5()
        {
            CreatePlayer();
            CreatLittlFlower(new Vector2(100),true);

            Regions.Add( new Region(
                             new[]
                                 {
                                     new Vector2( 200, 100 ), new Vector2( 100, 240 ), new Vector2( 700, 240 ),
                                     new Vector2( 650, 100 )
                                 },
                             "Read Map4", null ) );
            Regions.Add(new Region(
                             new[]
                                 {
                                     new Vector2( 260, 1 ), new Vector2( 260, 60 ), new Vector2( 600, 60 ),
                                     new Vector2( 600, 1 )
                                 },
                             "GotoXY Map4 3784 940", null));

            Regions.Add( new Region(
                             new[] {new Vector2( 725, 325 ), new Vector2( 680, 825 ), new Vector2( 915, 810 )},
                             "Read Map7", null ) );
            Regions.Add(new Region(
                             new[]
                                 {
                                     new Vector2( 970, 420 ), new Vector2( 970, 650 ), new Vector2( 1023, 650 ),
                                     new Vector2( 1023, 420 )
                                 },
                             "GotoXY Map8 70 745", null));

            CreateElement( new Vector2( 400, 750 ), "items_un4", "splash un4 resume", "un4", 1 );
        }

        public void LoadMap8()
        {
            /*
             417,649 GHOST
741,621 GHOST
1357,480 GHOST*/
            CreatePlayer();
            this.CreatLittlFlower(new Vector2(100),true);

            this.CreateGhost(new Vector2(417,649),AIBase.FaceSide.Left );
            this.CreateGhost(new Vector2(741,621),AIBase.FaceSide.Right);
            this.CreateGhost(new Vector2(735,480),AIBase.FaceSide.Left );
            

            this.CreateBall(new[] { new Vector2(600) , new Vector2(620) ,new Vector2(640),new Vector2(660) ,
                                    new Vector2(580,620),new Vector2(560,640), new Vector2(540,660)   });

            Regions.Add(new Region(
                             new[]
                                 {
                                     new Vector2( 100, 640 ), new Vector2( 200, 850 ), new Vector2( 185, 565 )
                                 },
                             "Read Map5", null));
            Regions.Add(new Region(
                             new[]
                                 {
                                     new Vector2( 1, 650 ), new Vector2( 60, 650 ), new Vector2( 60, 850 ),
                                     new Vector2( 1, 850 )
                                 },
                             "GotoXY Map5 951 536", null));

            Regions.Add(new Region(
                             new[]
                                 {
                                     new Vector2( 990, 910 ), new Vector2( 1090, 990 ), new Vector2( 1420, 935 )
                                 },
                             "Read Map15", null));
            Regions.Add(new Region(
                             new[]
                                 {
                                     new Vector2( 1100, 975 ), new Vector2( 1100, 1023 ), new Vector2( 1325, 1023 ),
                                     new Vector2( 1325, 975 )
                                 },
                             "GotoXY Map15 530 75", null));

            CreateElement(new Vector2(1830, 198), "items_un7", "splash un7 resume", "un7", 1);
        }

        public void LoadMap12()
        {
            CreatePlayer();

            Regions.Add(new Region(
                             new[]
                                 {
                                     new Vector2( 190, 410 ), new Vector2( 75, 660 ), new Vector2( 245, 660 ),
                                 },
                             "Read Map18", null));
            Regions.Add(new Region(
                             new[]
                                 {
                                     new Vector2( 1, 400 ), new Vector2( 1, 700 ), new Vector2( 60, 700 ),
                                     new Vector2( 60, 400 )
                                 },
                             "GotoXY Map18 940 505", null));

            Regions.Add(new Region(
                             new[]
                                 {
                                     new Vector2( 3285, 355 ), new Vector2( 3285, 800 ), new Vector2( 3475, 800 ),
                                     new Vector2( 3475, 355 )
                                 },
                             "Read Map15", null));
            Regions.Add(new Region(
                             new[]
                                 {
                                     new Vector2( 3530, 450 ), new Vector2( 3530, 730 ), new Vector2( 3583, 730 ),
                                     new Vector2( 3583, 450 )
                                 },
                             "GotoXY Map15 75 565", null));

            CreateElement(new Vector2(780, 311), "items_un3", "splash un3 resume", "un3", 1);
            CreateEnemyTypeVI( new Vector2(2818,586), AIBase.FaceSide.Left  );
            CreateEnemyTypeIII( new Vector2( 2047,526),AIBase.FaceSide.Left );
            CreateEnemyTypeI( new Vector2(2337,723), AIBase.FaceSide.Right );
            CreatEnemyTypeII(new Vector2(819, 489), AIBase.FaceSide.Left);
            CreatEnemyTypeII(new Vector2(1366, 589), AIBase.FaceSide.Left);
            CreatEnemyTypeII(new Vector2(1166, 388), AIBase.FaceSide.Left);
            CreateBall( new[]
                            {
                                new Vector2( 2927, 511 ), new Vector2( 3034,758 ), new Vector2( 2533,718 ),
                                new Vector2( 1693,791 ), new Vector2( 1857,793 ), new Vector2( 1148,746 ),
                                new Vector2( 1194,507 ), 
                            } );
        }

        public void LoadMap13()
        {
            this.CreatePlayer();
            this.CreatBigSnake();
        }

        public void LoadMap15()
        {
            CreatePlayer();

            Regions.Add(new Region(
                             new[]
                                 {
                                     new Vector2( 285, 100 ), new Vector2( 655, 275 ), new Vector2( 615, 100 ),
                                 },
                             "Read Map8", null));
            Regions.Add(new Region(
                             new[]
                                 {
                                     new Vector2( 375, 1 ), new Vector2( 375, 60 ), new Vector2( 700, 60 ),
                                     new Vector2( 700, 1 )
                                 },
                             "GotoXY Map8 1210 960", null));

            Regions.Add(new Region(
                             new[]
                                 {
                                     new Vector2( 190, 785 ), new Vector2( 90, 445 ), new Vector2( 300, 380 ),
                                 },
                             "Read Map12", null));
            Regions.Add( new Region(
                             new[]
                                 {
                                     new Vector2( 1, 640 ), new Vector2( 60, 641 ), new Vector2( 60, 400 ),
                                     new Vector2( 1, 401 ),
                                 },
                             "GotoXY Map12 3500 585", null));

            CreateElement(new Vector2(500, 630), "items_un2", "splash un2 resume", "un2", 1);
        }

        public void LoadMap16()
        {
            CreatePlayer();

            Regions.Add(new Region(
                             new[]
                                 {
                                     new Vector2( 600, 120 ), new Vector2( 990, 125 ), new Vector2( 950, 60 ),
                                     new Vector2( 690, 60 ),
                                 },
                             "map3_normal=true:Read Map3;map3_normal=false:Read Map17", null));
            Regions.Add(new Region(
                             new[]
                                 {
                                     new Vector2( 350, 1023 ), new Vector2( 750, 1023 ), new Vector2( 750, 970 ),
                                     new Vector2( 350, 970 )
                                 },
                             "map3_normal=true:GotoXY Map3 4440 75;map3_normal=false:gotoxy Map17 4440 75", null));

            CreateElementInfo("key_moon", "key_moon");
            CreateElementInfo("key_sun", "key_sun");
            CreateElementInfo("key_tri", "key_tri");
            CreateElementInfo("light_1", "light_1");

            if (CoreUtilities.StateIsTrue("key_moon") && CoreUtilities.StateIsTrue("key_sun") && CoreUtilities.StateIsTrue("key_tri"))
            {
                GameItemState.Set("light_1", true);
                GameItemState.Set("map3_normal", false);
            }
        }

        public void LoadMap17()
        {
            CreatePlayer();

            CreateTeleport(new Vector2(1900, 600), new Vector2(1975, 180), "map3_tp1");
            CreateSwitcher(new Vector2(410, 373), "map3_tp1", -1f);


            Regions.Add(new Region(
                             new[] { new Vector2(4200, 40), new Vector2(4420, 365), new Vector2(4700, 200) },
                             "Read Map16", null));
            Regions.Add(new Region(
                             new[]
                                 {
                                     new Vector2( 4150, 1 ), new Vector2( 4150, 60 ), new Vector2( 4650, 60 ),
                                     new Vector2( 4650, 1 )
                                 },
                             "GotoXY Map16 530 940", null));

            Regions.Add(new Region(
                             new[] { new Vector2(1510, 1400), new Vector2(2100, 1500), new Vector2(2100, 1200) },
                             "Read Map2", null));
            Regions.Add(new Region(
                             new[]
                                 {
                                     new Vector2( 1600, 1476 ), new Vector2( 1600, 1536 ), new Vector2( 2200, 1536 ),
                                     new Vector2( 2200, 1476 )
                                 },
                             "GotoXY Map2 1000 75", null));

            CreateElement(new Vector2(5722, 1210), "items_un5", "splash un5 resume", "un5", 1);

            CreateElement(new Vector2(300, 1270), "items_un6", "splash un6 resume", "un6", 1);
        }

        public void LoadMap18()
        {
            CreatePlayer();

            Regions.Add(new Region(
                             new[]
                                 {
                                     new Vector2( 600, 120 ), new Vector2( 990, 125 ), new Vector2( 950, 60 ),
                                     new Vector2( 690, 60 ),
                                 },
                             "Read Map12", null));
            Regions.Add(new Region(
                             new[]
                                 {
                                     new Vector2( 970,420 ), new Vector2( 1023,420 ), new Vector2( 1023,590 ),
                                     new Vector2( 970,590 )
                                 },
                             "GotoXY Map12 75 550", null));

            CreateElement(new Vector2(464, 538), "items_un8", "splash un8 resume", "un8", 1);

            CreateElementInfo("un1", "items_un1");
            CreateElementInfo("un2", "items_un2");
            CreateElementInfo("un3", "items_un3");
            CreateElementInfo("un4", "items_un4");
            CreateElementInfo("un5", "items_un5");
            CreateElementInfo("un6", "items_un6");
            CreateElementInfo("un7", "items_un7");
            CreateElementInfo("un8", "items_un8");

            CreateElement(new Vector2(512, 512), "tp_boss", "gotoxy map13 500 500", "teleport_13", 1);
        }

        public override void Initialize()
        {
        }

        public override void PostInitialize()
        {
            base.PostInitialize();
        }

        public void LoadMap6()
        {
            this.CreatePlayer();
            this.CreatLittlFlower(new Vector2(100), true);
            this.CreatBigDaddy();
            // this.CreateEnemyTypeI(new Vector2(150), AIBase.FaceSide.Right);
            // this.CreateEnemyTypeVI(new Vector2(512), AIBase.FaceSide.Right);
        }

        public void LoadMap7()
        {
        }

        public void LoadMap9()
        {
            CreatePlayer();

            Regions.Add(new Region(
                             new[]
                                 {
                                     new Vector2( 600, 120 ), new Vector2( 990, 125 ), new Vector2( 950, 60 ),
                                     new Vector2( 690, 60 ),
                                 },
                             "Read Map8", null));
            Regions.Add(new Region(
                             new[]
                                 {
                                     new Vector2( 690, 1 ), new Vector2( 690, 60 ), new Vector2( 950, 60 ),
                                     new Vector2( 950, 1 )
                                 },
                             "GotoXY Map8 1210 960", null));

            Regions.Add(new Region(
                             new[]
                                 {
                                     new Vector2( 60, 640 ), new Vector2( 75, 965 ), new Vector2( 330, 960 ),
                                     new Vector2( 340, 700 )
                                 },
                             "Read Map18", null));
            Regions.Add(new Region(
                             new[]
                                 {
                                     new Vector2( 1, 690 ), new Vector2( 60, 690 ), new Vector2( 60, 890 ),
                                     new Vector2( 1, 890 )
                                 },
                             "GotoXY Map18 940 505", null));

            Regions.Add(new Region(
                             new[]
                                 {
                                     new Vector2( 650, 1260 ), new Vector2( 650, 1480 ), new Vector2( 945, 1480 ),
                                     new Vector2(945, 1260), 
                                 },
                             "Read Map10", null));
            Regions.Add(new Region(
                             new[]
                                 {
                                     new Vector2( 705, 1480 ), new Vector2( 705, 1535 ), new Vector2( 940, 1535 ),
                                     new Vector2( 940, 1480 )
                                 },
                             "GotoXY Map10 505 80", null));
        }

        public void LoadMap10()
        {
            CreatePlayer();

            Regions.Add(new Region(
                             new[]
                                 {
                                     new Vector2( 400, 360 ), new Vector2( 620, 360 ), new Vector2( 620, 60 ),
                                     new Vector2( 400, 60 ),
                                 },
                             "Read Map9", null));
            Regions.Add(new Region(
                             new[]
                                 {
                                     new Vector2( 400, 1 ), new Vector2( 400, 60 ), new Vector2( 620, 60 ),
                                     new Vector2( 620, 1 )
                                 },
                             "GotoXY Map9 820 1460", null));

            Regions.Add(new Region(
                             new[]
                                 {
                                     new Vector2( 400, 1840 ), new Vector2( 640, 1840 ), new Vector2( 640, 1970 ),
                                     new Vector2( 400, 1970 ),
                                 },
                             "Read Map15", null));
            Regions.Add(new Region(
                             new[]
                                 {
                                     new Vector2( 400, 1970 ), new Vector2( 400, 2047 ), new Vector2( 640, 2047 ),
                                     new Vector2( 640, 1970 )
                                 },
                             "GotoXY Map15 495 75", null));
        }

        public void LoadMap11()
        {
            CreatePlayer();

            Regions.Add(new Region(
                             new[]
                                 {
                                     new Vector2( 125, 875 ), new Vector2( 200, 1300 ), new Vector2( 395, 1195 ),
                                 },
                             "Read Map15", null));
            Regions.Add(new Region(
                             new[]
                                 {
                                     new Vector2( 1, 1000 ), new Vector2( 1, 1280 ), new Vector2( 60, 1280 ),
                                     new Vector2( 60, 1000 )
                                 },
                             "GotoXY Map15 955 570", null));

            Regions.Add(new Region(
                             new[]
                                 {
                                     new Vector2( 4675, 180 ), new Vector2( 4675, 545 ), new Vector2( 4810, 545 ),
                                     new Vector2( 4810, 180 )
                                 },
                             "Read Map12", null));
            Regions.Add(new Region(
                             new[]
                                 {
                                     new Vector2( 5060, 180 ), new Vector2( 5060, 480 ), new Vector2( 5119, 480 ),
                                     new Vector2( 5119, 180 )
                                 },
                             "GotoXY Map12 75 550", null));
        }

        public void LoadMap19()
        {
            CreatePlayer();

            Regions.Add(new Region(
                             new[]
                                 {
                                     new Vector2( 80, 740 ), new Vector2( 200, 740 ), new Vector2( 200, 270 ),
                                     new Vector2( 80, 270 ),
                                 },
                             "Read Map12", null));
            Regions.Add(new Region(
                             new[]
                                 {
                                     new Vector2( 1, 350 ), new Vector2( 1, 680 ), new Vector2( 60, 680 ),
                                     new Vector2( 60, 350 )
                                 },
                             "GotoXY Map12 1210 960", null));
        }
    }
}

    /*
    CreateTeleport( new Vector2(500,450), new Vector2(800,450), "enabled"  );

    CreateSwitcher( new Vector2(600, 600),"enabled", 5f );

    GameItemState.Set( "enabled", true );
    GameItemState.Save();

    Regions.Add(new Region(
                       new[] { new Vector2(4600, 1000), new Vector2(4600, 1400), new Vector2(4800, 1400), new Vector2(4800, 1000) },
                       "Dialog Dialog1", null));
    Regions.Add(new Region(
                       new[] { new Vector2(400, 400), new Vector2(400, 600), new Vector2(450, 600), new Vector2(450, 400) },
                       "Camera Play1", null));
    Regions.Add(new Region(
                       new[] { new Vector2(4800, 1000), new Vector2(4800, 1400), new Vector2(4900, 1400), new Vector2(4900, 1000) },
                       "GotoXY Map1 1000 500", null));

    CreateEnemyTypeI(new Vector2(650, 500), AIBase.FaceSide.Right);

    CreateEnemyTypeIII(new Vector2(700, 400), AIBase.FaceSide.Left);

    //CreateEnemyTypeIV(new Vector2(700, 800), AIBase.FaceSide.Left);

    CreateGhost(new Vector2(800, 500), AIBase.FaceSide.Right);

    CreateBall( new [] {new Vector2( 500f, 500f ), new Vector2( 600f, 600f )} );
    //for (int i = 0; i < 10; i++)
    //    energyBallComponent.AddNode(new Vector2(4600, 1200));

    CreatePlayer();
    */

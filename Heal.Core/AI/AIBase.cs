using System.Collections.Generic;
using Heal.Core.Entities;
using Heal.Core.Entities.Enemies;
using Heal.Core.Sence;
using Heal.Core.Utilities;
using Microsoft.Xna.Framework;

namespace Heal.Core.AI
{
    public sealed class AIBase
    {
        public static AudioManager AudioManager;
        public static Player Player;
        public static IList<Unit> EnemyList;
        public static IList<Item> ItemList;
        public static readonly SenceManager SenceManager;
        public static System.Collections.Generic.List<BigDaddy.ScenceNode> ScenceList;

        static AIBase()
        {
            SenceManager = SenceManager.GetInstance();
            AudioManager = AudioManager.GetInstance();
        }

        public static bool RectangleCollusion(Rectangle Postion)
        {
           if(AIBase.Player.Locate.X > Postion.X && AIBase.Player.Locate.X < Postion.X + Postion.Width
               && AIBase.Player.Locate.Y > Postion.Y && AIBase.Player.Locate.Y < Postion.Y + Postion.Height)
           {
               return true;
           }
           else
           {
               return false;
           }
        }

        public enum Status
        {
            Normal,   //巡逻
            Chase,    //追逐敌人
            Flee,     //逃跑
            Turning,  //转身
            Attck,    //攻击
            Freeze,   //眩晕 敌人特有
            Blank,    //空 辅助记录上一帧状态
            Dead,      //GameOver
            Abandon,
            Alarm,    //警戒
            Judge,    //抉择
            Flat,     //被压扁
            Strike,
            Kick
        };

        public enum FaceSide
        {
            Left,
            Right
        };

        public enum List
        {
            Left,
            Right,
            TurnLeft,
            TurnRight,
            Ring
        };

        public enum ID
        {
            Zero,
            Boss,
            I,
            II,
            III,
            IV,
            V,
            VI,
            NPC
        };

        public enum StatusIII
        {
            Alarm,
            Sleep,
            UpGoes,
            Downfall
        } ;

        public enum StatusSmoBoss
        {
            SpeedUp,
            SlowDown,
            Chase,
            End
        } ;

        public enum StatusIV
        {
            Sleep,
            Alarm
        } ;

        public enum PartStoBoss
        {
            Body,
            Eye,
            Lefthand,
            Righthand,
            Leftleg,
            Rightleg
        } ;

        public enum StatusStoBoss
        {
            Normal,
            Attack,
            Flee,
            Disappear
        } ;

        public enum BossID
        {
            SmokeBoss,
            StrikeBoss,
            SnakeBoss,
            CapsuleBoss,
            StoneBoss
        } ;

        public enum GhostStatus
        {
            Disappear,
            Appear,
        } ;

        public enum StatusDialog
        {
            FallToGround,
            Alarm,
            Attack,
            StrongerThanPlayer,
            WeakerThanPlayer,
            Strike,
            Ahhh,
            Kakaka,
            Soughhh,
            Blank,
        } ;
    }
}



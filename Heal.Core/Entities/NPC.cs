using System;
using Heal.Core.AI;
using Microsoft.Xna.Framework;

namespace Heal.Core.Entities
{
    /// <summary>
    /// Some NPC could tiger action or other things.
    /// </summary>
    public class NPC : Unit
    {
        private byte[] m_collisionTexture;

        private float m_rotation;

        private Vector2 m_locate;

        public NPC(object sprite, Vector2 speed, Vector2 locate, float ringSize, float enemySize,float enemySize2 ,AIBase.FaceSide face, AIBase.ID id,int range)
            : base(sprite, speed, locate, ringSize, enemySize,enemySize2, face, id)
        {
        }

        internal override void ToTurnStatus()
        {
            throw new NotImplementedException();
        }

        internal override void ToChaseStatus(GameTime gameTime, Player player)
        {
            throw new NotImplementedException();
        }

        internal override void ToFleeStatus(GameTime gameTime, Player playerr)
        {
            throw new NotImplementedException();
        }

        internal override void ToFreezeStatus(GameTime gameTime, Player player)
        {
            throw new NotImplementedException();
        }

        internal override void ToAttackStatus(GameTime gameTime, Player ptr)
        {
            
        }


        internal override void ToDeadStatus(GameTime gameTime, Player ptr)
        {
            throw new NotImplementedException();
        }

        public override byte[] CollisionTexture
        {
            get { return m_collisionTexture; }
        }

        public override float Rotation
        {
            get { return m_rotation; }
        }

        public override Vector2 Locate
        {
            get { return m_locate; }
        }

        public Vector2 Size
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public override Rectangle GetDrawingRectangle( )
        {
            throw new NotImplementedException( );
        }

        public override void Initialize( )
        {
            throw new NotImplementedException( );
        }
    }
}



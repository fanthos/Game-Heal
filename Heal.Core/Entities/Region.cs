using System;
using Heal.Core.AI;
using Microsoft.Xna.Framework;
using Heal.Core.Utilities;

namespace Heal.Core.Entities
{
    public class Region : Entity
    {
        private byte[] m_collisionTexture;

        private float m_rotation;

        private readonly string m_cmd;
        private bool m_send;
        private string m_config;
        public bool Enabled;

        private Vector2[] m_fencePnts;

        private float isLeft(Vector2 P0, Vector2 P1, Vector2 P2)
        {
            float abc = ((P1.X - P0.X) * (P2.Y - P0.Y) - (P2.X - P0.X) * (P1.Y - P0.Y));
            return abc;

        }

        public bool PointInRegion(Vector2 pnt1)
        {

            int wn = 0, j = 0; //wn 计数器 j第二个点指针
            for (int i = 0; i < m_fencePnts.Length; i++)
            {//开始循环
                if (i == m_fencePnts.Length - 1)
                    j = 0;//如果 循环到最后一点 第二个指针指向第一点
                else
                    j = j + 1; //如果不是 ，则找下一点


                if (m_fencePnts[i].Y <= pnt1.Y) // 如果多边形的点 小于等于 选定点的 Y 坐标
                {
                    if (m_fencePnts[j].Y > pnt1.Y) // 如果多边形的下一点 大于于 选定点的 Y 坐标
                    {
                        if (isLeft(m_fencePnts[i], m_fencePnts[j], pnt1) > 0)
                        {
                            wn++;
                        }
                    }
                }
                else
                {
                    if (m_fencePnts[j].Y <= pnt1.Y)
                    {
                        if (isLeft(m_fencePnts[i], m_fencePnts[j], pnt1) < 0)
                        {
                            wn--;
                        }
                    }
                }
            }
            if (wn == 0)
                return false;
            else
                return true;
        }
        /*
        public bool PointInRegion(Vector2 pnt1)
        {
            int j = 0, cnt = 0;
            for (int i = 0; i < m_fencePnts.Length; i++)
            {
                j = ( i == m_fencePnts.Length - 1 ) ? 0 : j + 1;
                if( ( ( ( pnt1.Y >= m_fencePnts[i].Y ) && ( pnt1.Y < m_fencePnts[j].Y ) ) ||
                      ( ( pnt1.Y >= m_fencePnts[j].Y ) && ( pnt1.Y < m_fencePnts[i].Y ) ) ) &&
                    ( pnt1.X - m_fencePnts[i].X ) * ( m_fencePnts[j].Y - m_fencePnts[i].Y ) <
                    ( m_fencePnts[j].X - m_fencePnts[i].X ) * ( pnt1.Y - m_fencePnts[i].Y ) )
                    cnt++;
            }
            return (cnt % 2 != 0) ? true : false;
        }
        */

        public Region( Vector2[] fencePnts, string cmd, string config ) : base( null )
        {
            m_cmd = cmd;
            m_fencePnts = fencePnts;
            m_config = config;
        }

        #region Overrides of Entity

        public override byte[] CollisionTexture
        {
            get { return m_collisionTexture; }
        }

        public override float Rotation
        {
            get { return m_rotation; }
        }

        public override Vector2 Locate { get; set; }
        public override void Update( GameTime gameTime )
        {
            if(m_config == null)
            {
                Enabled = true;
            }
            else if(GameItemState.Get( m_config )!= null)
            {
                Enabled = (bool)GameItemState.Get(m_config);
            }
            else
            {
                Enabled = true;
            }
            if (!Enabled) return;
            if(PointInRegion( AIBase.Player.Locate ))
            {
                if (!m_send)
                {
                    AIBase.SenceManager.ItemCommand( m_cmd );
                }
                m_send = true;
            }
            else
            {
                m_send = false;
            }
        }

        public override Rectangle GetDrawingRectangle()
        {
            throw new NotImplementedException();
        }

        public override void Initialize()
        {
            
        }

        #endregion
    }
}

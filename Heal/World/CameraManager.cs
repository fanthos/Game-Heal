using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Heal.Core.Utilities;
using Heal.Core.Sence;
using Microsoft.Xna.Framework;
using Heal.Data;
using Heal.Core.GameData;

namespace Heal.World
{
    public class CameraManager : GameClassManager, IUpdateable
    {
        #region Instance Manager

        private static CameraManager m_instance;

        public CameraManager()
        {
            m_instance = this;
        }

        public static CameraManager GetInstance()
        {
            return m_instance;
        }

        #endregion

        private SenceManager m_sence;
        private WorldManager m_world;

        private CameraInfo m_data;

        private float m_timer;
        private int m_viewState;

        private Vector2 m_cameraLocate;
        
        public override void Initialize()
        {
            m_sence = SenceManager.GetInstance();
            m_world = WorldManager.GetInstance();
        }

        internal void Load(string name)
        {
            m_data = DataReader.Load<CameraInfo>("Data/Cameras/" + name);
            m_viewState = 0;
            m_timer = m_data[0].Time;
        }

        public void Update( GameTime gameTime )
        {
            CameraData data = m_data[m_viewState];
            m_timer -= (float) gameTime.ElapsedGameTime.TotalSeconds;
            m_sence.CameraFollow += ( new Vector2( data.X, data.Y ) - m_sence.CameraFollow ) *
                                    (float)gameTime.ElapsedGameTime.TotalSeconds / m_timer;

            m_world.Scale += ( data.Scale - m_world.Scale ) * (float) gameTime.ElapsedGameTime.TotalSeconds / m_timer;

            if(m_timer <= 0)
            {
                m_viewState++;

                if (m_viewState == m_data.List.Count)
                {
                    GameCommands.Enqueue(m_data.PostPlay);
                    return;
                }
                m_timer = m_data[m_viewState].Time;
            }
        }

        #region Unused Items

        public bool Enabled
        {
            get { return true; }
        }

        public int UpdateOrder
        {
            get { return 0; }
        }

        public event EventHandler EnabledChanged;
        public event EventHandler UpdateOrderChanged;

        #endregion
    }
}

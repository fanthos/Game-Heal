using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Heal.Core.GameData;
using Heal.Data;
using Heal.Sprites;
using Heal.Core.Sence;
using Heal.Levels;

namespace Heal.World
{
    public class WorldPart : IDisposable, IUpdateableSprite
    {
        private string[] m_filepath;
        private int m_collusionLayer;
        private SencePart m_mapData;
        private WorldManager m_manager;
        private MapDrawer[] m_layers;
        private Vector2 m_locate;
        public Vector2 Locate;
        private MapManager.MapLoadingData m_data;

        internal const int ImageSize = 512;
        internal WorldPart(MapItem data, MapInfo mapDataList, SencePart sencePart, int collusionLayer, MapManager.MapLoadingData loadingData)
        {
            m_data = loadingData;
            int count = data.List.Count;
            m_manager = WorldManager.GetInstance();
            m_mapData = sencePart;
            m_collusionLayer = collusionLayer;

            //m_filepath = new string[count];
            m_layers = new MapDrawer[count +1];
            string path = "Maps/" + mapDataList.Name + "/";
            m_locate = new Vector2(data.X * ImageSize, data.Y * ImageSize);
            foreach( var textureInfo in data.List)
            {
                DataReader.LoadAsync<Texture2D>( path + textureInfo.Texture, LoadingItemCallback, textureInfo.Layer );
                m_data.LoadingLeft++;
            }
        }

        ~WorldPart()
        {
            this.Dispose();
        }

        private void LoadingItemCallback(object item, string path, object param)
        {
            int type = (int) param;
            var texture2D = (Texture2D)item;
            //m_filepath[type] = path;
            m_layers[type] = new MapDrawer(this, m_data.Layers[type], texture2D, type, 1f);//, (m_data.CollusionLayer > type)?0.95f:1f );
            m_data.Pieces[type, m_mapData.X, m_mapData.Y] = m_layers[type];

            m_data.Layers[type].Pieces[m_mapData.X, m_mapData.Y] =
                m_data.Pieces[type, m_mapData.X, m_mapData.Y];
            if(m_data.CollusionLayer == type)
            {
                Texture2D shadow = new Texture2D(Heal.HealGame.Game.GraphicsDevice, ImageSize, ImageSize, false, SurfaceFormat.Color);
                Color[] data = new Color[ImageSize * ImageSize];
                texture2D.GetData( data );
                byte[] infos = m_manager.MakeColorArrayToByte( data );
                m_mapData.CollusionTexture = infos;
                shadow.SetData( data );
                m_data.Pieces[m_layers.Length - 1, m_mapData.X, m_mapData.Y] = new MapDrawer(this, m_data.Layers[type], shadow, type, .4f, 0.85f);
                m_data.Layers[m_layers.Length - 1].Pieces[m_mapData.X, m_mapData.Y] =
                    m_data.Pieces[m_layers.Length - 1, m_mapData.X, m_mapData.Y];
            }
            m_data.LoadingLeft--;
        }

        public void Dispose()
        {
        }

        public void Initialize()
        {
            
        }

        public void Draw( GameTime gameTime, SpriteBatch batch )
        {
            foreach( var mapDrawer in m_layers )
            {
                mapDrawer.Draw( gameTime, batch );
            }
        }

        public void UpdateDraw( GameTime gameTime )
        {
            Locate = (m_locate - m_manager.Locate) * m_manager.Scale + m_manager.Space / 2;
        }

        public bool Palse
        {
            get { return true; }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Heal.Core.GameData;
using Heal.Data;
using Heal.World;
using Heal.Core.Utilities;
using Microsoft.Xna.Framework;
using Heal.Core.Sence;

namespace Heal.Levels
{
    public class MapManager : GameClassManager
    {
        #region Instance Manager
        private static MapManager m_instance;
        public MapManager()
        {
            m_instance = this;
        }

        public override void Initialize()
        {
            m_worldManager = WorldManager.GetInstance();
            m_senceManager = SenceManager.GetInstance();
        }

        public static MapManager GetInstance()
        {
            return m_instance;
        }
        #endregion

        internal class MapLoadingData 
        {
            public string Name;
            public WorldLayer[] Layers;
            public WorldPart[,] Parts;
            public SencePart[,] Sence;
            public int LoadingLeft;
            public Point Size;
            public Point WorldSize;
            public int LayerCount;
            public MapDrawer[,,] Pieces;
            public int CollusionLayer;
            public Vector2 Player;
            public string PostLoading;
        }

        private Dictionary<string, WeakReference> m_mapLoadingDict = new Dictionary<string, WeakReference>(StringComparer.OrdinalIgnoreCase);
        private List<MapLoadingData> m_mapLoadingList = new List<MapLoadingData>();
        private WorldManager m_worldManager;
        private SenceManager m_senceManager;
        private MapInfo m_levelData;

        internal void Load(string mapName)
        {
            WeakReference reference;
            MapLoadingData data;
            if(m_mapLoadingDict.TryGetValue( mapName, out reference ))
            {
                data = (MapLoadingData)reference.Target;
                if(data != null)
                {
                    if (!m_mapLoadingList.Contains(data))
                    {
                        m_mapLoadingList.Add( data );
                        return;
                    }
                    else
                    {
                        return;
                    }
                }
            }
            data = new MapLoadingData();
            if (reference == null)
            {
                m_mapLoadingDict.Add(mapName, new WeakReference(data));
            }
            else
            {
                reference.Target = data;
            }
            MapInfo level = DataReader.Load<MapInfo>("Data/Maps/" + mapName);
            data = InternalLoad(level, data);
            m_mapLoadingList.Add( data );
        }

        internal void Goto(string mapName)
        {
            Load( mapName );
            MapLoadingData data = (MapLoadingData) m_mapLoadingDict[mapName].Target;
            m_worldManager.Load( data, (int)data.Player.X, (int)data.Player.Y );
            m_mapLoadingList.Clear();
        }

        internal void GotoXY(string mapName, int x, int y)
        {
            Load(mapName);
            MapLoadingData data = (MapLoadingData)m_mapLoadingDict[mapName].Target;
            m_worldManager.Load(data, x, y);
            m_mapLoadingList.Clear();
        }

        private MapLoadingData InternalLoad(MapInfo level, MapLoadingData data)
        {
            data.Name = level.Name;
            data.Parts = new WorldPart[level.X, level.Y];
            data.Sence = new SencePart[level.X, level.Y];
            data.Size = new Point(level.X, level.Y);
            //m_senceManager.Initialize(size.X, size.Y, WorldPart.ImageSize);
            data.WorldSize = new Point(data.Size.X * WorldPart.ImageSize, data.Size.Y * WorldPart.ImageSize);
            data.LoadingLeft = 0;
            data.LayerCount = level.LayerCount;
            data.Pieces = new MapDrawer[data.LayerCount + 1, data.Size.X, data.Size.Y];
            data.CollusionLayer = level.CollusionLayer;
            data.Player = new Vector2(level.PX, level.PY);
            data.Layers = new WorldLayer[data.LayerCount + 1];
            data.PostLoading = level.PostLoading;
            for( int i = 0; i < data.LayerCount + 1; i++ )
            {
                data.Layers[i] = new WorldLayer( data.Size.X, data.Size.Y );
            }
            foreach( var item in level.Effects.List )
            {
                data.Layers[item.Layer].SetEffect( item.Effect );
            }
            foreach (MapItem mapData in level.List)
            {
                data.Sence[mapData.X,mapData.Y] = new SencePart(mapData.X, mapData.Y);
                data.Parts[mapData.X, mapData.Y] = new WorldPart(mapData, level, data.Sence[mapData.X, mapData.Y], data.CollusionLayer, data);
            }
            return data;
        }
    }
}

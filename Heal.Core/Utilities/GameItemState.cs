using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Text;
using System.Xml.Serialization;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Storage;

namespace Heal.Core.Utilities
{
    public class GameItemState : GameClassManager
    {
        private static GameItemState m_instance;
        public GameItemState()
        {
            m_instance = this;
        }
        public static GameItemState GetInstance()
        {
            return m_instance;
        }

        private Dictionary<string, object> m_configs;

        public override void Initialize()
        {
            m_configs = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);
            Load();
        }

        public static void Load()
        {
            if (!Guide.IsVisible)
            {
                StorageDevice.BeginShowSelector(m_instance.FindStorageDevice, false);
            }
        }

        public static void Save()
        {
            if (!Guide.IsVisible)
            {
                StorageDevice.BeginShowSelector(m_instance.FindStorageDevice, true);
            }
        }

        public static void Set( string key, object value )
        {
            m_instance[key] = value;
        }

        public static object Get(string key)
        {
            return m_instance[key];
        }

        public static void Reset()
        {
            m_instance.m_configs.Clear();
        }

        private void FindStorageDevice(IAsyncResult result)
        {
            StorageDevice sd = StorageDevice.EndShowSelector(result);
            if (sd != null)
            {
                StorageContainer container = XnaFixes.OpenContainer(sd, "Heal");
                //string filePath = Path.Combine(container.Path, "Config.dat");
                string filePath = "Config.dat";
                DataContractSerializer dataContractSerializer = new DataContractSerializer(typeof(Dictionary<string, object>));
                if ((bool)result.AsyncState)
                {
                    try
                    {
                        //m_configs.
                        //FileStream stream = new FileStream(filePath, FileMode.OpenOrCreate);
                        Stream stream = container.OpenFile(filePath, FileMode.OpenOrCreate);
                        dataContractSerializer.WriteObject(stream, m_configs);
                    }
                    catch{}
                }
                else
                {
                    try
                    {
                        //FileStream stream = new FileStream(filePath, FileMode.OpenOrCreate);
                        Stream stream = container.OpenFile(filePath, FileMode.OpenOrCreate);
                        m_configs = (Dictionary<string, object>)dataContractSerializer.ReadObject(stream);
                    }
                    catch { }
                    if (m_configs == null)
                    {
                        m_configs = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);
                    }
                }
            }
        }

        public object this[string key]
        {
            get
            {
                object value;
                m_configs.TryGetValue( key, out value );
                return value;
            }
            set
            {
                object obj;
                if(m_configs.TryGetValue( key, out obj ))
                {
                    m_configs[key] = value;
                }
                else
                {
                    m_configs.Add( key, value );
                }
            }
        }
    }
}

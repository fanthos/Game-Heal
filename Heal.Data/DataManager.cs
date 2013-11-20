using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using Heal.Data.MpqReader;
using Microsoft.Xna.Framework.Content;
using System.Diagnostics;
using System.Security.AccessControl;

namespace Heal.Data
{
    public class DataManager : ContentManager
    {
        CompressedFileManager m_fileManager = new MpqFileManager();

        private object m_streamLock1 = new object();
        private object m_streamLock2 = new object();
        private Dictionary<string, WeakReference> m_loadedAssets;

        public DataManager(ContentManager contentManager): this(contentManager.ServiceProvider, contentManager.RootDirectory)
        {
            contentManager.Dispose();
        }
        public DataManager(IServiceProvider serviceProvider)
            : this(serviceProvider, string.Empty)
        {
        }

        public DataManager(IServiceProvider serviceProvider, string rootDirectory)
            : base(serviceProvider, rootDirectory)
        {
            m_loadedAssets = new Dictionary<string, WeakReference>(StringComparer.OrdinalIgnoreCase);
            //OpenFile(rootDirectory);
        }

        [DebuggerHidden]
        public override void Unload()
        {
            m_loadedAssets.Clear();
            base.Unload();
        }


        private static string GetCleanPath(string path)
        {
            int num2;
            path = path.Replace(Path.AltDirectorySeparatorChar, Path.DirectorySeparatorChar);
            for (int i = 1; i < path.Length; i = Math.Max(num2 - 1, 1))
            {
                i = path.IndexOf(@"\..\", i);
                if (i < 0)
                {
                    return path;
                }
                num2 = path.LastIndexOf(Path.DirectorySeparatorChar, i - 1) + 1;
                path = path.Remove(num2, (i - num2) + @"\..\".Length);
            }
            return path;
        }

        /// <summary>
        /// Loads an asset that has been processed by the Content Pipeline.  Reference page contains code sample.
        /// </summary>
        /// <param name="assetName">Asset name, relative to the loader root directory, and not including the .xnb file extension.</param>
        [DebuggerHidden]
        public override T Load<T>(string assetName)
        {
            object obj2;
            T data;
            WeakReference item;
            if( m_loadedAssets == null )
            {
                throw new ObjectDisposedException( this.ToString() );
            }
            if( string.IsNullOrEmpty( assetName ) )
            {
                throw new ArgumentNullException( "assetName" );
            }
            assetName = GetCleanPath( assetName );
            
            lock (m_streamLock1)
            {
                if (this.m_loadedAssets.TryGetValue(assetName, out item))
                {
                    obj2 = item.Target;
                    if (obj2 != null)
                    {
                        if (!(obj2 is T))
                        {
                            throw new ContentLoadException(string.Format(CultureInfo.CurrentCulture,
                                                                         "Wrong XNB file type.",
                                                                         new object[]
                                                                             {assetName, obj2.GetType(), typeof (T)}));
                        }
                        return (T) obj2;
                    }
                    else
                    {
                        data = base.ReadAsset<T>(assetName, null);
                        item.Target = data;
                        return data;
                    }
                }
                data = base.ReadAsset<T>(assetName, null);
                item = new WeakReference(data);
                m_loadedAssets.Add(assetName, item);
            }
            return data;
        }

        [DebuggerHidden]
        protected override Stream OpenStream( string assetName )
        {
            Stream stream = null;
            string path = GetCleanPath(Path.Combine(this.RootDirectory, assetName + ".xnb"));
            try
            {
                stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);
                return stream;
            }
            catch
            {
            }
            stream = m_fileManager.GetFile(assetName + ".xnb");
            if (stream == null)
            {
                throw new ContentLoadException(
                    string.Format("OpenStreamNotFound: {0}", new object[] {assetName}));
            }
            return stream;
        }

        [DebuggerHidden]
        public void OpenFile(string filename)
        {
            m_fileManager.OpenFile(filename + ".heal");
        }

        [DebuggerHidden]
        public void OpenPatch(string filename)
        {
            m_fileManager.OpenPatch(filename + ".heal");
        }

        /*
        /// <summary>
        /// Unloads an asset that has been processed by the Content Pipeline.  Reference page contains code sample.
        /// </summary>
        /// <param name="assetName">Asset name, relative to the loader root directory, and not including the .xnb file extension.</param>
        public void Unload(string assetName)
        {
            IDisposable disposable;
            WeakReference item;
            object obj2;
            if (this.m_loadedAssets == null)
            {
                throw new ObjectDisposedException(this.ToString());
            }
            if (string.IsNullOrEmpty(assetName))
            {
                throw new ArgumentNullException("assetName");
            }
            assetName = GetCleanPath(assetName);
            if (this.m_loadedAssets.TryGetValue(assetName, out item))
            {
                obj2 = item.Target;
                if (obj2 is IDisposable)
                {
                    ((IDisposable)obj2).Dispose();
                }
                m_loadedAssets.Remove(assetName);
            }
        }
        */
    }
}

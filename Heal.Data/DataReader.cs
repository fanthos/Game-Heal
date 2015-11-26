using System;
using System.Collections.Generic;
using System.Threading;
using Heal.Core.Utilities;

namespace Heal.Data
{
    public class DataReader : IDisposable
    {
        #region Thread and Instance management

        private static DataReader m_instance;
        private bool m_running;

        static DataReader()
        {
        }

        private DataReader()
        {
            m_running = true;
            m_loadingItems = new Queue<LoadAsyncInfo>();
            m_callback = new Queue<PostLoadItemProc>();
            m_loadingThread = new Thread(LoadingProcess) {Priority = ThreadPriority.Lowest, Name = "Loading Thread" };
            m_loadingThread.Start();
            //m_callbackThread = new Thread(PostLoadingProcess) {Priority = ThreadPriority.Lowest, Name = "Loading Callback Thread"};
            //m_callbackThread.Start();
        }

        public static void Initialize( Thread mainThread )
        {
            CoreUtilities.MainThread = mainThread;
            m_instance = new DataReader();
        }

        public void Dispose()
        {
            m_running = false;
        }

        #endregion

        private Queue<LoadAsyncInfo> m_loadingItems;
        private Queue<PostLoadItemProc> m_callback;

        private Thread m_loadingThread;
        private Thread m_callbackThread;

        //private static EventWaitHandle m_event = new EventWaitHandle(false);

        private void LoadingProcess()
        {
            while (CoreUtilities.Running)
            {
                Thread.Sleep( 50 );
                while( m_loadingItems.Count > 0 )
                {
                    if( !m_running ) return;
                    var info = m_loadingItems.Dequeue();
                    try
                    {
                        object obj = info.LoadFunction( info.AssetPath );
                        m_callback.Enqueue( () => info.Callback( obj, info.AssetPath, info.Param ) );
                    }
                    finally
                    {
                    }
                }
            }
        }

        public static void SyncCallback()
        {
            m_instance.InternalSyncCallback();
        }
        private void InternalSyncCallback()
        {
            while( m_callback.Count > 0 )
            {
                if( !m_running ) return;
                var info = m_callback.Dequeue();
                try
                {
                    info();
                }
                finally
                {
                }
            }
        }

        public static DataManager Manager;

        /// <summary>
        /// Loads the specified path.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="path">The path.</param>
        /// <returns></returns>
        public static T Load<T>(string path)
        {
            return Manager.Load<T>( path );
        }

        /*
        /// <summary>
        /// Unloads the specified path.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="path">The path.</param>
        public static void Unload(string path)
        {
            Manager.Unload( path );
        }
        */
        public delegate void ItemLoadedDelegate(object obj, string path, object param);
         
        private delegate void PostLoadItemProc();

        /// <summary>
        /// Class storage data about async loading.
        /// </summary>
        private abstract class LoadAsyncInfo
        {
            public delegate object LoadFunctionDelegate(string assetName);
            public ItemLoadedDelegate Callback;
            public string AssetPath;
            public object Param;
            public LoadFunctionDelegate LoadFunction;
        }
        /// <summary>
        /// Specify the loading data class for item type.
        /// </summary>
        /// <typeparam name="T">Type of loading item.</typeparam>
        private class LoadAsyncInfo<T>:LoadAsyncInfo
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="LoadAsyncInfo&lt;T&gt;"/> class.
            /// </summary>
            /// <param name="assetPath">The asset path.</param>
            /// <param name="callback">The callback function.</param>
            /// <param name="param">The param when calling callback function.</param>
            public LoadAsyncInfo(string assetPath, ItemLoadedDelegate callback, object param )
            {
                Callback = callback;
                AssetPath = assetPath;
                Param = param;
                LoadFunction = str  => Manager.Load<T>( str );
            }
        }
        /// <summary>
        /// Loads the asset async.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="path">The path.</param>
        /// <param name="callback">The callback.</param>
        /// <param name="param">The param.</param>
        public static void LoadAsync<T>(string path, ItemLoadedDelegate callback, object param)
        {
            m_instance.m_loadingItems.Enqueue( new LoadAsyncInfo<T>( path, callback, param ) );
        }
    }
}

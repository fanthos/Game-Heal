using System;
using System.Collections.Generic;
using System.IO;
using Heal.Data.MpqReader.filter;

namespace Heal.Data.MpqReader
{
    public class MpqDirectory : IDisposable
    {
        //protected Dictionary<MpqArchive.FileInfo, Stream> m_Cache = new Dictionary<MpqArchive.FileInfo, Stream>();
        protected MpqArchive.FileInfo[] m_File;
        protected Dictionary<MpqArchive.FileInfo, MpqArchive> m_Mpq = new Dictionary<MpqArchive.FileInfo, MpqArchive>();

        public MpqDirectory()
        {
        }

        public MpqDirectory(IEnumerable<string> MpqArchives)
        {
            foreach (string str in MpqArchives)
            {
                if (File.Exists(str))
                {
                    MpqArchive archive = new MpqArchive(str);
                    foreach (MpqArchive.FileInfo info in archive.Files)
                    {
                        if (!this.m_Mpq.ContainsKey(info))
                        {
                            this.m_Mpq.Add(info, archive);
                        }
                    }
                }
            }
        }

        public void OpenExtraFile(string mpqArchive)
        {
            MpqArchive archive = new MpqArchive(mpqArchive);
            foreach (MpqArchive.FileInfo info in archive.Files)
            {
                if (!this.m_Mpq.ContainsKey(info))
                {
                    this.m_Mpq.Add(info, archive);
                }
            }
        }

        public void OpenPatchFile(string mpqArchive)
        {
            MpqArchive archive = new MpqArchive(mpqArchive);
            foreach (MpqArchive.FileInfo info in archive.Files)
            {
                if (!this.m_Mpq.ContainsKey(info))
                {
                    this.m_Mpq.Add(info, archive);
                }
                else
                {
                    this.m_Mpq[info] = archive;
                }
            }
        }

        public void Dispose()
        {
            //this.m_Cache.Clear();
            foreach (MpqArchive archive in this.m_Mpq.Values)
            {
                archive.Dispose();
            }
        }

        public bool FileExists(string file)
        {
            return this.FileExists(new MpqArchive.FileInfo(file));
        }

        public bool FileExists(MpqArchive.FileInfo file)
        {
            return (this.m_Mpq.ContainsKey(file) && this.m_Mpq[file].FileExists(file.Name));
        }

        public MpqArchive.FileInfo[] FilterFileList(string FilterPattern)
        {
            return this.FilterFileList(FilterPattern, new DefaultListFilter());
        }

        public MpqArchive.FileInfo[] FilterFileList(string FilterPattern, IListFilter Filter)
        {
            return Filter.FilterList(this.Files, FilterPattern);
        }

        public Stream OpenFile(string file)
        {
            return this.OpenFile(new MpqArchive.FileInfo(file));
        }

        public Stream OpenFile(MpqArchive.FileInfo file)
        {
            /*
            if (this.m_Cache.ContainsKey(file))
            {
                if (this.m_Cache[file].CanRead)
                {
                    return this.m_Cache[file];
                }
                this.m_Cache.Remove(file);
            }
            */
            if (this.FileExists(file))
            {
                Stream stream = this.m_Mpq[file].OpenFile(file.Name);
                //this.m_Cache.Add(file, stream);
                return stream;
            }
            return null;
        }

        public MpqArchive.FileInfo[] Files
        {
            get
            {
                if (this.m_File == null)
                {
                    this.m_File = new MpqArchive.FileInfo[this.m_Mpq.Keys.Count];
                    this.m_Mpq.Keys.CopyTo(this.m_File, 0);
                }
                return this.m_File;
            }
        }

        public Stream this[string file]
        {
            get
            {
                return this.OpenFile(file);
            }
        }
    }
}



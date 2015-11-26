using System.IO;
using Heal.Data.MpqReader;
using System.IO.Packaging;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO.Compression;

namespace Heal.Data
{
    public class ZipFileManager : CompressedFileManager
    {
        private ZipArchive m_zipPack;
        private Dictionary<string, ZipArchive> m_zipPackPatch;

        private IDictionary<string, ZipArchiveEntry> m_zipEntryList;

        public ZipFileManager()
        {
            throw new NotImplementedException();
        }

        private void InternalReadZip(ZipArchive zip, string fileName)
        {
            string basePath = fileName == "" ? "" : fileName + "/";
            foreach(var entry in zip.Entries)
            {
                m_zipEntryList[basePath + entry.FullName] = entry;
            }
        }

        public ZipFileManager(string filename)
            : base(filename)
        {
            m_zipPack = new ZipArchive(File.OpenRead("Content/" + filename + ".zip"), ZipArchiveMode.Read);
            m_zipPackPatch = new Dictionary<string, ZipArchive>();
            m_zipEntryList = new Dictionary<string, ZipArchiveEntry>(StringComparer.OrdinalIgnoreCase);
            InternalReadZip(m_zipPack, "");
        }

        public override void CloseFile()
        {
            foreach(var i in m_zipPackPatch)
            {
                i.Value.Dispose();
            }
            m_zipPack.Dispose();
        }

        public override void OpenFile(string filename)
        {
            throw new NotImplementedException();
        }

        public override void OpenPatch(string filename)
        {
            var newfile = new ZipArchive(File.OpenRead("Content/" + filename + ".zip"), ZipArchiveMode.Read);
            m_zipPackPatch[filename] = newfile;
            InternalReadZip(newfile, filename);
            base.OpenPatch(filename);
        }
        
        public override Stream GetFile(string filepath)
        {
            var newpath = filepath.Replace('\\', '/');
            return m_zipEntryList[newpath].Open();
            var sstr = newpath.LastIndexOf('/');

            ZipArchiveEntry entry;
            if (sstr >= 0) {
                string pstr;
                ZipArchive tzip;
                while (sstr >= 0)
                {
                    pstr = filepath.Substring(0, sstr);
                    if (m_zipPackPatch.TryGetValue(pstr, out tzip))
                    {
                        entry = tzip.GetEntry(newpath.Substring(sstr + 1));
                        if(entry != null)
                        {
                            return entry.Open();
                        }
                    }
                    sstr = pstr.LastIndexOf('/');
                }
            }
            entry = m_zipPack.GetEntry(newpath);
            if(entry != null)
            {
                return entry.Open();
            }
            else
            {
                return null;
            }
        }
    }
}



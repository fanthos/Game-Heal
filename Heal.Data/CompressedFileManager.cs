using System.Collections.Generic;
using System.IO;

namespace Heal.Data
{
    public abstract class CompressedFileManager
    {
        private List<string> m_strFile = new List<string>();

        public CompressedFileManager(string filename)
        {
            //m_strFile.Add(filename);
            //OpenFile(filename);
        }

        public CompressedFileManager()
        {
        }

        public virtual void CloseFile()
        {
        }

        public virtual void OpenFile(string filename)
        {
            m_strFile.Add(filename);
        }

        public virtual Stream GetFile(string Filepath)
        {
            return null;
        }

        public virtual void OpenPatch(string filename)
        {
        }
    }
}



using System.IO;
using Heal.Data.MpqReader;

namespace Heal.Data
{
    public class MpqFileManager : CompressedFileManager
    {
        MpqDirectory m_mpqFile = new MpqDirectory();

        public MpqFileManager()
        {
        }

        public MpqFileManager(string filename)
            : base(filename)
        {
        }

        public override void CloseFile()
        {
            m_mpqFile.Dispose();
            base.CloseFile();
        }

        public override void OpenFile(string filename)
        {
            m_mpqFile.OpenExtraFile(filename);
            base.OpenFile(filename);
        }

        public override void OpenPatch(string filename)
        {
            m_mpqFile.OpenPatchFile(filename);
            base.OpenPatch(filename);
        }

        public override Stream GetFile(string filepath)
        {
            return m_mpqFile.OpenFile( filepath );
        }
    }
}



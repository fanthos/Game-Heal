using System.IO;

namespace Heal.Data.ICSharpCode.SharpZipLib.BZip2
{
    public sealed class BZip2
    {
        public static void Decompress(Stream instream, Stream outstream)
        {
            Stream stream = outstream;
            Stream stream2 = instream;
            BZip2InputStream stream3 = new BZip2InputStream(stream2);
            for (int i = stream3.ReadByte(); i != -1; i = stream3.ReadByte())
            {
                stream.WriteByte((byte) i);
            }
            stream.Flush();
        }
    }
}



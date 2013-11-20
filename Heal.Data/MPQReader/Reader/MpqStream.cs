using Heal.Data.ICSharpCode.SharpZipLib.BZip2;
using Heal.Data.ICSharpCode.SharpZipLib.Zip.Compression.Streams;
using System;
using System.IO;

namespace Heal.Data.MpqReader.Reader
{
    public class MpqStream : Stream
    {
        private MpqArchive.Block mBlock;
        private uint[] mBlockPositions;
        private int mBlockSize;
        private int mCurrentBlockIndex;
        private byte[] mCurrentData;
        private long mPosition;
        private uint mSeed1;
        private Stream mStream;

        private MpqStream()
        {
            this.mCurrentBlockIndex = -1;
        }

        internal MpqStream(MpqArchive File, MpqArchive.Block Block)
        {
            this.mCurrentBlockIndex = -1;
            this.mBlock = Block;
            this.mStream = File.BaseStream;
            this.mBlockSize = File.BlockSize;
            if (this.mBlock.IsCompressed)
            {
                this.LoadBlockPositions();
            }
        }

        private void BufferData()
        {
            int blockIndex = (int) (this.mPosition / ((long) this.mBlockSize));
            if (blockIndex != this.mCurrentBlockIndex)
            {
                int expectedLength = (int) Math.Min(this.Length - (blockIndex * this.mBlockSize), (long) this.mBlockSize);
                this.mCurrentData = this.LoadBlock(blockIndex, expectedLength);
                this.mCurrentBlockIndex = blockIndex;
            }
        }

        private static byte[] BZip2Decompress(Stream Data, int ExpectedLength)
        {
            MemoryStream outstream = new MemoryStream();
            BZip2.Decompress(Data, outstream);
            return outstream.ToArray();
        }

        public override void Close()
        {
        }

        private static byte[] DecompressMulti(byte[] Input, int OutputLength)
        {
            Stream data = new MemoryStream(Input);
            byte num = (byte) data.ReadByte();
            if ((num & 0x10) != 0)
            {
                byte[] buffer = BZip2Decompress(data, OutputLength);
                num = (byte) (num & 0xef);
                if (num == 0)
                {
                    return buffer;
                }
                data = new MemoryStream(buffer);
            }
            if ((num & 8) != 0)
            {
                byte[] buffer2 = PKDecompress(data, OutputLength);
                num = (byte) (num & 0xf7);
                if (num == 0)
                {
                    return buffer2;
                }
                data = new MemoryStream(buffer2);
            }
            if ((num & 2) != 0)
            {
                byte[] buffer3 = ZlibDecompress(data, OutputLength);
                num = (byte) (num & 0xfd);
                if (num == 0)
                {
                    return buffer3;
                }
                data = new MemoryStream(buffer3);
            }
            if ((num & 1) != 0)
            {
                byte[] buffer4 = MpqHuffman.Decompress(data);
                num = (byte) (num & 0xfe);
                if (num == 0)
                {
                    return buffer4;
                }
                data = new MemoryStream(buffer4);
            }
            if ((num & 0x80) != 0)
            {
                byte[] buffer5 = MpqWavCompression.Decompress(data, 2);
                num = (byte) (num & 0x7f);
                if (num == 0)
                {
                    return buffer5;
                }
                data = new MemoryStream(buffer5);
            }
            if ((num & 0x40) != 0)
            {
                byte[] buffer6 = MpqWavCompression.Decompress(data, 1);
                num = (byte) (num & 0xbf);
                if (num == 0)
                {
                    return buffer6;
                }
                data = new MemoryStream(buffer6);
            }
            throw new Exception(string.Format("Unhandled compression flags: 0x{0:X}", num));
        }

        public override void Flush()
        {
        }

        private byte[] LoadBlock(int BlockIndex, int ExpectedLength)
        {
            uint num;
            int num2;
            if (this.mBlock.IsCompressed)
            {
                num = this.mBlockPositions[BlockIndex];
                num2 = (int) (this.mBlockPositions[BlockIndex + 1] - num);
            }
            else
            {
                num = (uint) (BlockIndex * this.mBlockSize);
                num2 = ExpectedLength;
            }
            num += this.mBlock.FilePos;
            byte[] buffer = new byte[num2];
            lock (this.mStream)
            {
                this.mStream.Seek((long) num, SeekOrigin.Begin);
                this.mStream.Read(buffer, 0, num2);
            }
            if (this.mBlock.IsEncrypted && (this.mBlock.FileSize > 3))
            {
                if (this.mSeed1 == 0)
                {
                    throw new Exception("Unable to determine encryption key");
                }
                MpqArchive.DecryptBlock(buffer, this.mSeed1 + ((uint) BlockIndex));
            }
            if (!this.mBlock.IsCompressed || (buffer.Length == ExpectedLength))
            {
                return buffer;
            }
            if ((this.mBlock.Flags & MpqArchive.FileFlag.CompressedMulti) != ((MpqArchive.FileFlag) 0))
            {
                return DecompressMulti(buffer, ExpectedLength);
            }
            return PKDecompress(new MemoryStream(buffer), ExpectedLength);
        }

        private void LoadBlockPositions()
        {
            int num = ((int) (((this.mBlock.FileSize + this.mBlockSize) - ((long) 1L)) / ((long) this.mBlockSize))) + 1;
            this.mBlockPositions = new uint[num];
            lock (this.mStream)
            {
                this.mStream.Seek((long) this.mBlock.FilePos, SeekOrigin.Begin);
                BinaryReader reader = new BinaryReader(this.mStream);
                for (int i = 0; i < num; i++)
                {
                    this.mBlockPositions[i] = reader.ReadUInt32();
                }
            }
            uint decrypted = (uint) (num * 4);
            if (((this.mBlock.Flags & MpqArchive.FileFlag.FileHasMetadata) == ((MpqArchive.FileFlag) 0)) && (this.mBlockPositions[0] != decrypted))
            {
                this.mBlock.Flags |= MpqArchive.FileFlag.Encrypted;
            }
            if (this.mBlock.IsEncrypted)
            {
                if (this.mSeed1 == 0)
                {
                    this.mSeed1 = MpqArchive.DetectFileSeed(this.mBlockPositions, decrypted);
                    if (this.mSeed1 == 0)
                    {
                        throw new Exception("Unable to determine encyption seed");
                    }
                }
                MpqArchive.DecryptBlock(this.mBlockPositions, this.mSeed1);
                this.mSeed1++;
            }
        }

        private static byte[] PKDecompress(Stream Data, int ExpectedLength)
        {
            PKLibDecompress decompress = new PKLibDecompress(Data);
            return decompress.Explode(ExpectedLength);
        }

        public override int Read(byte[] Buffer, int Offset, int Count)
        {
            int count = Count;
            int num2 = 0;
            while (count > 0)
            {
                int num3 = this.ReadInternal(Buffer, Offset, count);
                if (num3 == 0)
                {
                    return num2;
                }
                num2 += num3;
                Offset += num3;
                count -= num3;
            }
            return num2;
        }

        public override int ReadByte()
        {
            if (this.mPosition >= this.Length)
            {
                return -1;
            }
            this.BufferData();
            int index = (int) (this.mPosition % ((long) this.mBlockSize));
            this.mPosition += 1L;
            return this.mCurrentData[index];
        }

        private int ReadInternal(byte[] Buffer, int Offset, int Count)
        {
            if (this.mPosition >= this.Length)
            {
                return 0;
            }
            this.BufferData();
            int sourceIndex = (int) (this.mPosition % ((long) this.mBlockSize));
            int length = Math.Min(this.mCurrentData.Length - sourceIndex, Count);
            if (length <= 0)
            {
                return 0;
            }
            Array.Copy(this.mCurrentData, sourceIndex, Buffer, Offset, length);
            this.mPosition += length;
            return length;
        }

        public override long Seek(long Offset, SeekOrigin Origin)
        {
            long num;
            switch (Origin)
            {
                case SeekOrigin.Begin:
                    num = Offset;
                    break;

                case SeekOrigin.Current:
                    num = this.Position + Offset;
                    break;

                case SeekOrigin.End:
                    num = this.Length + Offset;
                    break;

                default:
                    throw new ArgumentException("Origin", "Invalid SeekOrigin");
            }
            if (num < 0L)
            {
                throw new ArgumentOutOfRangeException("Attmpted to Seek before the beginning of the stream");
            }
            if (num >= this.Length)
            {
                throw new ArgumentOutOfRangeException("Attmpted to Seek beyond the end of the stream");
            }
            this.mPosition = num;
            return this.mPosition;
        }

        public override void SetLength(long Value)
        {
            throw new NotSupportedException("SetLength is not supported");
        }

        public override void Write(byte[] Buffer, int Offset, int Count)
        {
            throw new NotSupportedException("Writing is not supported");
        }

        private static byte[] ZlibDecompress(Stream Data, int ExpectedLength)
        {
            byte[] buffer = new byte[ExpectedLength];
            Stream stream = new InflaterInputStream(Data);
            int offset = 0;
            while (true)
            {
                int num2 = stream.Read(buffer, offset, ExpectedLength);
                if (num2 == 0)
                {
                    return buffer;
                }
                offset += num2;
            }
        }

        public override bool CanRead
        {
            get
            {
                return true;
            }
        }

        public override bool CanSeek
        {
            get
            {
                return true;
            }
        }

        public override bool CanWrite
        {
            get
            {
                return false;
            }
        }

        public override long Length
        {
            get
            {
                return (long) this.mBlock.FileSize;
            }
        }

        public override long Position
        {
            get
            {
                return this.mPosition;
            }
            set
            {
                this.Seek(value, SeekOrigin.Begin);
            }
        }
    }
}



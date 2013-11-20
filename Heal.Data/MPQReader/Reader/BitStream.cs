using System;
using System.IO;

namespace Heal.Data.MpqReader.Reader
{
    internal class BitStream
    {
        private int mBitCount;
        private int mCurrent;
        private Stream mStream;

        public BitStream(Stream SourceStream)
        {
            this.mStream = SourceStream;
        }

        public bool EnsureBits(int BitCount)
        {
            if (BitCount > this.mBitCount)
            {
                if (this.mStream.Position >= this.mStream.Length)
                {
                    return false;
                }
                int num = this.mStream.ReadByte();
                this.mCurrent |= num << this.mBitCount;
                this.mBitCount += 8;
            }
            return true;
        }

        public int PeekByte()
        {
            if (!this.EnsureBits(8))
            {
                return -1;
            }
            return (this.mCurrent & 0xff);
        }

        public int ReadBits(int BitCount)
        {
            if (BitCount > 0x10)
            {
                throw new ArgumentOutOfRangeException("BitCount", "Maximum BitCount is 16");
            }
            if (!this.EnsureBits(BitCount))
            {
                return -1;
            }
            int num = this.mCurrent & (((int) 0xffff) >> (0x10 - BitCount));
            this.WasteBits(BitCount);
            return num;
        }

        private bool WasteBits(int BitCount)
        {
            this.mCurrent = this.mCurrent >> BitCount;
            this.mBitCount -= BitCount;
            return true;
        }

        public Stream BaseStream
        {
            get
            {
                return this.mStream;
            }
        }
    }
}



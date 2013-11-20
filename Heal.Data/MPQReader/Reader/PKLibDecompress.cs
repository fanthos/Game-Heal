using System;
using System.IO;

namespace Heal.Data.MpqReader.Reader
{
    public class PKLibDecompress
    {
        private CompressionType mCType;
        private int mDSizeBits;
        private BitStream mStream;
        private static readonly byte[] sDistBits = new byte[] { 
                                                                  2, 4, 4, 5, 5, 5, 5, 6, 6, 6, 6, 6, 6, 6, 6, 6, 
                                                                  6, 6, 6, 6, 6, 6, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 
                                                                  7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 
                                                                  8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8
                                                              };
        private static readonly byte[] sDistCode = new byte[] { 
                                                                  3, 13, 5, 0x19, 9, 0x11, 1, 0x3e, 30, 0x2e, 14, 0x36, 0x16, 0x26, 6, 0x3a, 
                                                                  0x1a, 0x2a, 10, 50, 0x12, 0x22, 0x42, 2, 0x7c, 60, 0x5c, 0x1c, 0x6c, 0x2c, 0x4c, 12, 
                                                                  0x74, 0x34, 0x54, 20, 100, 0x24, 0x44, 4, 120, 0x38, 0x58, 0x18, 0x68, 40, 0x48, 8, 
                                                                  240, 0x70, 0xb0, 0x30, 0xd0, 80, 0x90, 0x10, 0xe0, 0x60, 160, 0x20, 0xc0, 0x40, 0x80, 0
                                                              };
        private static readonly byte[] sExLenBits = new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 1, 2, 3, 4, 5, 6, 7, 8 };
        private static readonly ushort[] sLenBase = new ushort[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 10, 14, 0x16, 0x26, 70, 0x86, 0x106 };
        private static readonly byte[] sLenBits = new byte[] { 3, 2, 3, 3, 4, 4, 4, 5, 5, 5, 5, 6, 6, 6, 7, 7 };
        private static readonly byte[] sLenCode = new byte[] { 5, 3, 1, 6, 10, 2, 12, 20, 4, 0x18, 8, 0x30, 0x10, 0x20, 0x40, 0 };
        private static byte[] sPosition1 = GenerateDecodeTable(sDistBits, sDistCode);
        private static byte[] sPosition2 = GenerateDecodeTable(sLenBits, sLenCode);

        public PKLibDecompress(Stream Input)
        {
            this.mStream = new BitStream(Input);
            this.mCType = (CompressionType) Input.ReadByte();
            if ((this.mCType != CompressionType.Binary) && (this.mCType != CompressionType.Ascii))
            {
                throw new Exception("Invalid compression type: " + this.mCType);
            }
            this.mDSizeBits = Input.ReadByte();
            if ((4 > this.mDSizeBits) || (this.mDSizeBits > 6))
            {
                throw new Exception("Invalid dictionary size: " + this.mDSizeBits);
            }
        }

        private int DecodeDist(int Length)
        {
            if (!this.mStream.EnsureBits(8))
            {
                return 0;
            }
            int index = sPosition1[this.mStream.PeekByte()];
            byte bitCount = sDistBits[index];
            if (this.mStream.ReadBits(bitCount) == -1)
            {
                return 0;
            }
            if (Length == 2)
            {
                if (!this.mStream.EnsureBits(2))
                {
                    return 0;
                }
                index = (index << 2) | this.mStream.ReadBits(2);
            }
            else
            {
                if (!this.mStream.EnsureBits(this.mDSizeBits))
                {
                    return 0;
                }
                index = (index << this.mDSizeBits) | this.mStream.ReadBits(this.mDSizeBits);
            }
            return (index + 1);
        }

        private int DecodeLit()
        {
            switch (this.mStream.ReadBits(1))
            {
                case -1:
                    return -1;

                case 0:
                    if (this.mCType != CompressionType.Binary)
                    {
                        throw new NotImplementedException("Text mode is not yet implemented");
                    }
                    return this.mStream.ReadBits(8);

                case 1:
                    {
                        int index = sPosition2[this.mStream.PeekByte()];
                        if (this.mStream.ReadBits(sLenBits[index]) == -1)
                        {
                            return -1;
                        }
                        int bitCount = sExLenBits[index];
                        if (bitCount != 0)
                        {
                            int num3 = this.mStream.ReadBits(bitCount);
                            if ((num3 == -1) && ((index + num3) != 270))
                            {
                                return -1;
                            }
                            index = sLenBase[index] + num3;
                        }
                        return (index + 0x100);
                    }
            }
            return 0;
        }

        public byte[] Explode(int ExpectedSize)
        {
            int num;
            byte[] buffer = new byte[ExpectedSize];
            Stream stream = new MemoryStream(buffer);
            while ((num = this.DecodeLit()) != -1)
            {
                if (num < 0x100)
                {
                    stream.WriteByte((byte) num);
                }
                else
                {
                    int length = num - 0xfe;
                    int num3 = this.DecodeDist(length);
                    if (num3 == 0)
                    {
                        goto Label_0067;
                    }
                    int num4 = ((int) stream.Position) - num3;
                    while (length-- > 0)
                    {
                        stream.WriteByte(buffer[num4++]);
                    }
                }
            }
            Label_0067:
            if (stream.Position == ExpectedSize)
            {
                return buffer;
            }
            byte[] destinationArray = new byte[stream.Position];
            Array.Copy(buffer, 0, destinationArray, 0, destinationArray.Length);
            return destinationArray;
        }

        private static byte[] GenerateDecodeTable(byte[] Bits, byte[] Codes)
        {
            byte[] buffer = new byte[0x100];
            for (int i = Bits.Length - 1; i >= 0; i--)
            {
                uint index = Codes[i];
                uint num3 = ((uint) 1) << Bits[i];
                do
                {
                    buffer[index] = (byte) i;
                    index += num3;
                }
                while (index < 0x100);
            }
            return buffer;
        }
    }
}



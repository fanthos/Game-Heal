using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using Heal.Data.MpqReader.Reader;

namespace Heal.Data.MpqReader
{
    public class MpqArchive : IDisposable
    {
        private FileInfo[] m_Files;
        private Block[] mBlocks;
        private int mBlockSize;
        private Hash[] mHashes;
        private Header mHeader;
        private long mHeaderOffset;
        private Stream mStream;
        private static uint[] sStormBuffer = BuildStormBuffer();

        public MpqArchive(Stream SourceStream)
        {
            this.mStream = SourceStream;
            this.Init();
        }

        public MpqArchive(string Filename)
        {
            this.mStream = File.Open(Filename, FileMode.Open, FileAccess.Read);
            this.Init();
        }

        private static uint[] BuildStormBuffer()
        {
            uint num = 0x100001;
            uint[] numArray = new uint[0x500];
            for (uint i = 0; i < 0x100; i++)
            {
                uint index = i;
                int num4 = 0;
                while (num4 < 5)
                {
                    num = ((num * 0x7d) + 3) % 0x2aaaab;
                    uint num5 = (uint) ((num & 0xffff) << 0x10);
                    num = ((num * 0x7d) + 3) % 0x2aaaab;
                    numArray[index] = num5 | (num & 0xffff);
                    num4++;
                    index += 0x100;
                }
            }
            return numArray;
        }

        internal static void DecryptBlock(byte[] Data, uint Seed1)
        {
            uint num = 0xeeeeeeee;
            for (int i = 0; i < (Data.Length - 3); i += 4)
            {
                num += sStormBuffer[(int) ((IntPtr) (0x400 + (Seed1 & 0xff)))];
                uint num3 = BitConverter.ToUInt32(Data, i) ^ (Seed1 + num);
                Seed1 = ((~Seed1 << 0x15) + 0x11111111) | (Seed1 >> 11);
                num = ((num3 + num) + (num << 5)) + 3;
                if (BitConverter.IsLittleEndian)
                {
                    Data[i] = (byte) (num3 & 0xff);
                    Data[i + 1] = (byte) ((num3 >> 8) & 0xff);
                    Data[i + 2] = (byte) ((num3 >> 0x10) & 0xff);
                    Data[i + 3] = (byte) ((num3 >> 0x18) & 0xff);
                }
                else
                {
                    Data[i + 3] = (byte) (num3 & 0xff);
                    Data[i + 2] = (byte) ((num3 >> 8) & 0xff);
                    Data[i + 1] = (byte) ((num3 >> 0x10) & 0xff);
                    Data[i] = (byte) ((num3 >> 0x18) & 0xff);
                }
            }
        }

        internal static void DecryptBlock(uint[] Data, uint Seed1)
        {
            uint num = 0xeeeeeeee;
            for (int i = 0; i < Data.Length; i++)
            {
                num += sStormBuffer[(int) ((IntPtr) (0x400 + (Seed1 & 0xff)))];
                uint num3 = Data[i];
                num3 ^= Seed1 + num;
                Seed1 = ((~Seed1 << 0x15) + 0x11111111) | (Seed1 >> 11);
                num = ((num3 + num) + (num << 5)) + 3;
                Data[i] = num3;
            }
        }

        internal static void DecryptTable(byte[] Data, string Key)
        {
            DecryptBlock(Data, HashString(Key, 0x300));
        }

        internal static uint DetectFileSeed(uint[] Data, uint Decrypted)
        {
            uint num = Data[0];
            uint num2 = Data[1];
            uint num3 = (num ^ Decrypted) - 0xeeeeeeee;
            for (int i = 0; i < 0x100; i++)
            {
                uint num5 = num3 - sStormBuffer[0x400 + i];
                uint num6 = 0xeeeeeeee + sStormBuffer[(int) ((IntPtr) (0x400 + (num5 & 0xff)))];
                uint num7 = num ^ (num5 + num6);
                if (num7 == Decrypted)
                {
                    uint num8 = num5;
                    num5 = ((~num5 << 0x15) + 0x11111111) | (num5 >> 11);
                    num6 = ((num7 + num6) + (num6 << 5)) + 3;
                    num6 += sStormBuffer[(int) ((IntPtr) (0x400 + (num5 & 0xff)))];
                    num7 = num2 ^ (num5 + num6);
                    if ((num7 & 0xffff0000) == 0)
                    {
                        return num8;
                    }
                }
            }
            return 0;
        }

        public void Dispose()
        {
            if (this.mStream != null)
            {
                this.mStream.Close();
            }
        }

        public bool FileExists(string Filename)
        {
            return (this.GetHashEntry(Filename).BlockIndex != uint.MaxValue);
        }

        private Hash GetHashEntry(string Filename)
        {
            uint num = HashString(Filename, 0) & (this.mHeader.HashTableSize - 1);
            uint num2 = HashString(Filename, 0x100);
            uint num3 = HashString(Filename, 0x200);
            for (uint i = num; i < this.mHashes.Length; i++)
            {
                Hash hash = this.mHashes[i];
                if ((hash.Name1 == num2) && (hash.Name2 == num3))
                {
                    return hash;
                }
            }
            Hash hash2 = new Hash();
            hash2.BlockIndex = uint.MaxValue;
            return hash2;
        }

        internal static uint HashString(string Input, int Offset)
        {
            uint num = 0x7fed7fed;
            uint num2 = 0xeeeeeeee;
            foreach (char ch in Input)
            {
                int num3 = char.ToUpper(ch);
                num = sStormBuffer[Offset + num3] ^ (num + num2);
                num2 = (uint) ((((num3 + num) + num2) + (num2 << 5)) + 3);
            }
            return num;
        }

        private void Init()
        {
            if (!this.LocateMpqHeader())
            {
                throw new Exception("Unable to find MPQ header");
            }
            BinaryReader reader = new BinaryReader(this.mStream);
            this.mBlockSize = ((int) 0x200) << this.mHeader.BlockSize;
            this.mStream.Seek((long) this.mHeader.HashTablePos, SeekOrigin.Begin);
            byte[] data = reader.ReadBytes((int) (this.mHeader.HashTableSize * Hash.Size));
            DecryptTable(data, "(hash table)");
            BinaryReader br = new BinaryReader(new MemoryStream(data));
            this.mHashes = new Hash[this.mHeader.HashTableSize];
            for (int i = 0; i < this.mHeader.HashTableSize; i++)
            {
                this.mHashes[i] = new Hash(br);
            }
            this.mStream.Seek((long) this.mHeader.BlockTablePos, SeekOrigin.Begin);
            byte[] buffer2 = reader.ReadBytes((int) (this.mHeader.BlockTableSize * 0x10));
            DecryptTable(buffer2, "(block table)");
            br = new BinaryReader(new MemoryStream(buffer2));
            this.mBlocks = new Block[this.mHeader.BlockTableSize];
            for (int j = 0; j < this.mHeader.BlockTableSize; j++)
            {
                this.mBlocks[j] = new Block(br, (uint) this.mHeaderOffset);
            }
        }

        private bool LocateMpqHeader()
        {
            BinaryReader br = new BinaryReader(this.mStream);
            for (long i = 0L; i < (this.mStream.Length - Header.Size); i += 0x200L)
            {
                this.mStream.Seek(i, SeekOrigin.Begin);
                this.mHeader = new Header(br);
                if (this.mHeader.ID == Header.MpqId)
                {
                    this.mHeaderOffset = i;
                    this.mHeader.HashTablePos += (uint) this.mHeaderOffset;
                    this.mHeader.BlockTablePos += (uint) this.mHeaderOffset;
                    if (this.mHeader.DataOffset == 0x6d9e4b86)
                    {
                        this.mHeader.DataOffset = Header.Size + ((uint) i);
                    }
                    return true;
                }
            }
            return false;
        }

        public MpqStream OpenFile(string Filename)
        {
            uint blockIndex = this.GetHashEntry(Filename).BlockIndex;
            if (blockIndex == uint.MaxValue)
            {
                throw new FileNotFoundException("File not found: " + Filename);
            }
            return new MpqStream(this, this.mBlocks[blockIndex]);
        }

        internal Stream BaseStream
        {
            get
            {
                return this.mStream;
            }
        }

        internal int BlockSize
        {
            get
            {
                return this.mBlockSize;
            }
        }

        public FileInfo[] Files
        {
            get
            {
                if (this.m_Files == null)
                {
                    try
                    {
                        using (Stream stream = this.OpenFile("(listfile)"))
                        {
                            using (StreamReader reader = new StreamReader(stream))
                            {
                                string str;
                                List<FileInfo> list = new List<FileInfo>();
                                while ((str = reader.ReadLine()) != null)
                                {
                                    Hash hashEntry = this.GetHashEntry(str);
                                    if (this.mBlocks.Length > hashEntry.BlockIndex)
                                    {
                                        Block block = this.mBlocks[hashEntry.BlockIndex];
                                        FileInfo item = new FileInfo(str);
                                        item.Flags = block.Flags;
                                        item.UncompressedSize = block.FileSize;
                                        item.CompressedSize = block.CompressedSize;
                                        list.Add(item);
                                    }
                                }
                                this.m_Files = list.ToArray();
                            }
                        }
                    }
                    catch (FileNotFoundException)
                    {
                        throw new NotSupportedException("Error: the archive contains no listfile");
                    }
                }
                return this.m_Files;
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct Block
        {
            public const uint SIZE = 0x10;
            public uint FilePos;
            public uint CompressedSize;
            public uint FileSize;
            public MpqArchive.FileFlag Flags;
            public Block(BinaryReader br, uint HeaderOffset)
            {
                this.FilePos = br.ReadUInt32() + HeaderOffset;
                this.CompressedSize = br.ReadUInt32();
                this.FileSize = br.ReadUInt32();
                this.Flags = (MpqArchive.FileFlag) br.ReadUInt32();
            }

            public bool IsEncrypted
            {
                get
                {
                    return ((this.Flags & MpqArchive.FileFlag.Encrypted) != ((MpqArchive.FileFlag) 0));
                }
            }
            public bool IsCompressed
            {
                get
                {
                    return ((this.Flags & MpqArchive.FileFlag.Compressed) != ((MpqArchive.FileFlag) 0));
                }
            }
        }

        public enum FileFlag : uint
        {
            Changed = 1,
            Compressed = 0xff00,
            CompressedMulti = 0x200,
            CompressedPK = 0x100,
            Encrypted = 0x10000,
            Exists = 0x80000000,
            FileHasMetadata = 0x4000000,
            FixSeed = 0x20000,
            Protected = 2,
            SingleUnit = 0x1000000,
            Unknown_02000000 = 0x2000000
        }

        public class FileInfo
        {
            private long m_CompressedSize;
            private MpqArchive.FileFlag m_FileFlag;
            private int m_Hash;
            private string m_Name;
            private long m_UncompressedSize;

            public FileInfo(string name)
            {
                this.m_Name = name;
                this.m_Hash = name.ToLower().GetHashCode();
            }

            public override bool Equals(object obj)
            {
                return this.Equals(obj as MpqArchive.FileInfo);
            }

            public bool Equals(MpqArchive.FileInfo obj)
            {
                return ((obj != null) && this.Name.ToLower().Equals(obj.Name.ToLower()));
            }

            public override int GetHashCode()
            {
                return this.m_Hash;
            }

            public long CompressedSize
            {
                get
                {
                    return this.m_CompressedSize;
                }
                internal set
                {
                    this.m_CompressedSize = value;
                }
            }

            public MpqArchive.FileFlag Flags
            {
                get
                {
                    return this.m_FileFlag;
                }
                internal set
                {
                    this.m_FileFlag = value;
                }
            }

            public string Name
            {
                get
                {
                    return this.m_Name;
                }
                internal set
                {
                    this.m_Name = value;
                }
            }

            public long UncompressedSize
            {
                get
                {
                    return this.m_UncompressedSize;
                }
                internal set
                {
                    this.m_UncompressedSize = value;
                }
            }

            public class Comparer : IComparer<MpqArchive.FileInfo>
            {
                public int Compare(MpqArchive.FileInfo x, MpqArchive.FileInfo y)
                {
                    return string.Compare(x.Name, y.Name);
                }
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct Hash
        {
            public uint Name1;
            public uint Name2;
            public uint Locale;
            public uint BlockIndex;
            public static readonly uint Size;
            public Hash(BinaryReader br)
            {
                this.Name1 = br.ReadUInt32();
                this.Name2 = br.ReadUInt32();
                this.Locale = br.ReadUInt32();
                this.BlockIndex = br.ReadUInt32();
            }

            public bool IsValid
            {
                get
                {
                    return ((this.Name1 != uint.MaxValue) && (this.Name2 != uint.MaxValue));
                }
            }
            static Hash()
            {
                Size = 0x10;
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct Header
        {
            public uint ID;
            public uint DataOffset;
            public uint ArchiveSize;
            public ushort Offs0C;
            public ushort BlockSize;
            public uint HashTablePos;
            public uint BlockTablePos;
            public uint HashTableSize;
            public uint BlockTableSize;
            public static readonly uint MpqId;
            public static readonly uint Size;
            public Header(BinaryReader br)
            {
                this.ID = br.ReadUInt32();
                this.DataOffset = br.ReadUInt32();
                this.ArchiveSize = br.ReadUInt32();
                this.Offs0C = br.ReadUInt16();
                this.BlockSize = br.ReadUInt16();
                this.HashTablePos = br.ReadUInt32();
                this.BlockTablePos = br.ReadUInt32();
                this.HashTableSize = br.ReadUInt32();
                this.BlockTableSize = br.ReadUInt32();
            }

            static Header()
            {
                MpqId = 0x1a51504d;
                Size = 0x20;
            }
        }
    }
}



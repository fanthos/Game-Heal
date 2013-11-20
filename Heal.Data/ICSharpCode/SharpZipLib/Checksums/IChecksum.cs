﻿namespace Heal.Data.ICSharpCode.SharpZipLib.Checksums
{
    public interface IChecksum
    {
        void Reset();
        void Update(int bval);
        void Update(byte[] buffer);
        void Update(byte[] buf, int off, int len);

        long Value { get; }
    }
}



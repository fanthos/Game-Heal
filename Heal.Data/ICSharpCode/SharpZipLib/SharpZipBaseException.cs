using System;

namespace Heal.Data.ICSharpCode.SharpZipLib
{
    public class SharpZipBaseException : ApplicationException
    {
        public SharpZipBaseException()
        {
        }

        public SharpZipBaseException(string msg) : base(msg)
        {
        }

        public SharpZipBaseException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}



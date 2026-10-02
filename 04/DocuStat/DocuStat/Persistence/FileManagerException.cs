using System;

namespace ELTE.DocuStat.Persistence
{
    [Serializable]
    public class FileManagerException : Exception
    {
        public FileManagerException()
        {
        }

        public FileManagerException(string? message) : base(message)
        {
        }

        public FileManagerException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
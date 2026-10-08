namespace NonogramModel.Persistence
{
    [Serializable]
    internal class NonogramFileException : Exception
    {
        public NonogramFileException()
        {
        }

        public NonogramFileException(string? message) : base(message)
        {
        }

        public NonogramFileException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
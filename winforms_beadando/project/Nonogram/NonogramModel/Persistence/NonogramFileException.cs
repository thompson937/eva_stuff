namespace NonogramModel.Persistence
{
    [Serializable]
    public class NonogramFileException : Exception
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
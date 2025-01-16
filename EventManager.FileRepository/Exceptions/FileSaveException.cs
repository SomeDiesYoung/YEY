namespace EventManager.FileRepository.Exceptions
{
    public class FileSaveException : Exception
    {
        public FileSaveException(string message) : base(message)
        {
        }

        public FileSaveException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}

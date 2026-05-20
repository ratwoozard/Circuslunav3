namespace CirkusLuna.Core.Exceptions
{
    /// <summary>
    /// Exception thrown when attempting to reserve tickets for a past performance
    /// </summary>
    public class PastPerformanceException : Exception
    {
        public PastPerformanceException(string message) : base(message)
        {
        }
    }
}

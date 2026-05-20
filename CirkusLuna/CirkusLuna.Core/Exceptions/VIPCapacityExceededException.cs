namespace CirkusLuna.Core.Exceptions
{
    /// <summary>
    /// Exception thrown when VIP capacity is exceeded
    /// </summary>
    public class VIPCapacityExceededException : Exception
    {
        public VIPCapacityExceededException(string message) : base(message)
        {
        }
    }
}

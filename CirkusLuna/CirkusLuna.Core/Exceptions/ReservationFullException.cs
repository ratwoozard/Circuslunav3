namespace CirkusLuna.Core.Exceptions
{
    /// <summary>
    /// Exception thrown when a performance has no available seats
    /// </summary>
    public class ReservationFullException : Exception
    {
        public ReservationFullException(string message) : base(message)
        {
        }
    }
}

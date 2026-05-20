using CirkusLuna.Core.Models;

namespace CirkusLuna.Core.Interfaces
{
    /// <summary>
    /// Repository interface for Reservation (Booking) entities
    /// </summary>
    public interface IReservationRepository : IRepository<Reservation>
    {
        List<Reservation> GetByForestillingId(int forestillingId);
        List<Reservation> GetByKundeId(int kundeId);
    }
}

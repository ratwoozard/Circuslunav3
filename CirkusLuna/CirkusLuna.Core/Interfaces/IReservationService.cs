using CirkusLuna.Core.Models;

namespace CirkusLuna.Core.Interfaces
{
    /// <summary>
    /// Service interface for reservation-related business logic and validation
    /// </summary>
    public interface IReservationService
    {
        Reservation CreateReservation(int kundeId, int forestillingId, int antalBilletter, Billettype billettype);
        bool CanReserve(int forestillingId, int antalBilletter, Billettype billettype);
        List<Reservation> GetReservationsByKunde(int kundeId);
        decimal CalculateTotalPrice(int antalBilletter, Billettype billettype);
    }
}

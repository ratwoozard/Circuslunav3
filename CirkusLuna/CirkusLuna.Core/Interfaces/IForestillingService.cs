using CirkusLuna.Core.Models;

namespace CirkusLuna.Core.Interfaces
{
    /// <summary>
    /// Service interface for performance-related business logic
    /// </summary>
    public interface IForestillingService
    {
        List<Forestilling> GetAllForestillinger();
        Forestilling? GetForestillingById(int id);
        List<Forestilling> SearchByCity(string byNavn);
        List<Forestilling> GetUpcomingForestillinger();
        List<By> GetCitiesSortedAlphabetically();
    }
}

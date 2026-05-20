using CirkusLuna.Core.Models;

namespace CirkusLuna.Core.Interfaces
{
    /// <summary>
    /// Repository interface for Forestilling (Performance) entities
    /// Includes method for self-written search algorithm
    /// </summary>
    public interface IForestillingRepository : IRepository<Forestilling>
    {
        /// <summary>
        /// Searches for performances in a specific city
        /// CRITICAL: Must use self-written search algorithm (manual loop)
        /// Do NOT rely solely on LINQ Where()
        /// </summary>
        List<Forestilling> SearchByCity(string byNavn);
        
        List<Forestilling> GetByDate(DateTime dato);
        List<Forestilling> GetUpcomingPerformances();
        List<Forestilling> GetPerformancesInCity(int byId);
    }
}

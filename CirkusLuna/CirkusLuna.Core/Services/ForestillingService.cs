using CirkusLuna.Core.Interfaces;
using CirkusLuna.Core.Models;

namespace CirkusLuna.Core.Services
{
    /// <summary>
    /// Service for performance-related business logic
    /// Orchestrates repository calls and exposes high-level operations
    /// </summary>
    public class ForestillingService : IForestillingService
    {
        private readonly IForestillingRepository _forestillingRepo;
        private readonly IByRepository _byRepo;
        
        public ForestillingService(
            IForestillingRepository forestillingRepo,
            IByRepository byRepo)
        {
            _forestillingRepo = forestillingRepo;
            _byRepo = byRepo;
        }
        
        public List<Forestilling> GetAllForestillinger()
        {
            return _forestillingRepo.GetAll();
        }
        
        public Forestilling? GetForestillingById(int id)
        {
            return _forestillingRepo.GetById(id);
        }
        
        /// <summary>
        /// Search performances by city name
        /// Uses self-written search algorithm in repository
        /// </summary>
        public List<Forestilling> SearchByCity(string byNavn)
        {
            return _forestillingRepo.SearchByCity(byNavn);
        }
        
        public List<Forestilling> GetUpcomingForestillinger()
        {
            return _forestillingRepo.GetUpcomingPerformances();
        }
        
        /// <summary>
        /// Get all cities sorted alphabetically
        /// Uses self-written bubble sort algorithm in repository
        /// </summary>
        public List<By> GetCitiesSortedAlphabetically()
        {
            return _byRepo.GetCitiesSortedAlphabetically();
        }
    }
}

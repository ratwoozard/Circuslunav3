using CirkusLuna.Core.Models;

namespace CirkusLuna.Core.Interfaces
{
    /// <summary>
    /// Repository interface for By (City) entities
    /// Includes method for self-written alphabetical sorting algorithm
    /// </summary>
    public interface IByRepository : IRepository<By>
    {
        /// <summary>
        /// Returns all cities sorted alphabetically by name
        /// CRITICAL: Must use self-written sorting algorithm (bubble sort, selection sort, or insertion sort)
        /// Do NOT use LINQ OrderBy() as the only implementation
        /// </summary>
        List<By> GetCitiesSortedAlphabetically();
        
        By? GetByName(string navn);
    }
}

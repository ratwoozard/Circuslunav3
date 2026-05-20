using CirkusLuna.Core.Interfaces;
using CirkusLuna.Core.Models;

namespace CirkusLuna.Core.Repositories
{
    /// <summary>
    /// Simpelt in-memory repository til byer.
    /// Her gemmer vi byer i en liste mens programmet kører.
    /// </summary>
    public class InMemoryByRepository : IByRepository
    {
        private List<By> _byer = new();
        private int _nextId = 1;
        
        public List<By> GetAll()
        {
            return new List<By>(_byer);
        }
        
        public By? GetById(int id)
        {
            foreach (var by in _byer)
            {
                if (by.Id == id)
                {
                    return by;
                }
            }
            return null;
        }
        
        public void Add(By entity)
        {
            // Giver byen et nyt id, så hvert objekt bliver unikt.
            entity.Id = _nextId++;
            _byer.Add(entity);
        }
        
        public void Update(By entity)
        {
            var existing = GetById(entity.Id);
            if (existing != null)
            {
                existing.Navn = entity.Navn;
                existing.Region = entity.Region;
            }
        }
        
        public void Delete(int id)
        {
            var entity = GetById(id);
            if (entity != null)
            {
                _byer.Remove(entity);
            }
        }
        
        public By? GetByName(string navn)
        {
            foreach (var by in _byer)
            {
                // Vi sammenligner uden at skelne mellem store/små bogstaver.
                if (by.Navn.Equals(navn, StringComparison.OrdinalIgnoreCase))
                {
                    return by;
                }
            }
            return null;
        }
        
        /// <summary>
        /// Sorterer byer alfabetisk med bubble sort.
        /// Vi bruger en manuel algoritme i stedet for OrderBy.
        /// </summary>
        public List<By> GetCitiesSortedAlphabetically()
        {
            // Vi laver en kopi, så vi ikke ændrer den originale liste.
            List<By> sortedCities = _byer.ToList();
            int n = sortedCities.Count;
            
            // Ydre løkke: hvor mange gennemløb vi laver.
            for (int i = 0; i < n - 1; i++)
            {
                // Indre løkke: sammenligner to nabo-elementer ad gangen.
                for (int j = 0; j < n - i - 1; j++)
                {
                    // Hvis de står i forkert rækkefølge, bytter vi rundt.
                    if (string.Compare(sortedCities[j].Navn, 
                                      sortedCities[j + 1].Navn, 
                                      StringComparison.OrdinalIgnoreCase) > 0)
                    {
                        By temp = sortedCities[j];
                        sortedCities[j] = sortedCities[j + 1];
                        sortedCities[j + 1] = temp;
                    }
                }
            }
            
            return sortedCities;
        }
    }
}

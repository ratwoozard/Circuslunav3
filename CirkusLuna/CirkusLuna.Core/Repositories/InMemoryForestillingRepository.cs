using CirkusLuna.Core.Interfaces;
using CirkusLuna.Core.Models;

namespace CirkusLuna.Core.Repositories
{
    /// <summary>
    /// Simpelt in-memory repository til forestillinger.
    /// Data gemmes i en liste mens appen kører.
    /// </summary>
    public class InMemoryForestillingRepository : IForestillingRepository
    {
        private List<Forestilling> _forestillinger = new();
        private int _nextId = 1;
        
        public List<Forestilling> GetAll()
        {
            return new List<Forestilling>(_forestillinger);
        }
        
        public Forestilling? GetById(int id)
        {
            foreach (var forestilling in _forestillinger)
            {
                if (forestilling.Id == id)
                {
                    return forestilling;
                }
            }
            return null;
        }
        
        public void Add(Forestilling entity)
        {
            // Sætter id automatisk.
            entity.Id = _nextId++;
            _forestillinger.Add(entity);
        }
        
        public void Update(Forestilling entity)
        {
            var existing = GetById(entity.Id);
            if (existing != null)
            {
                existing.Titel = entity.Titel;
                existing.Dato = entity.Dato;
                existing.Tidspunkt = entity.Tidspunkt;
                existing.ById = entity.ById;
                existing.TotalKapacitet = entity.TotalKapacitet;
                existing.VIPKapacitet = entity.VIPKapacitet;
            }
        }
        
        public void Delete(int id)
        {
            var entity = GetById(id);
            if (entity != null)
            {
                _forestillinger.Remove(entity);
            }
        }
        
        /// <summary>
        /// Finder forestillinger i en by med en simpel foreach-løkke.
        /// </summary>
        public List<Forestilling> SearchByCity(string byNavn)
        {
            // Tom liste til resultater.
            List<Forestilling> results = new List<Forestilling>();
            
            // Går alle forestillinger igennem én ad gangen.
            foreach (var forestilling in _forestillinger)
            {
                // Tjekker om bynavnet passer.
                if (forestilling.By != null && 
                    forestilling.By.Navn.Equals(byNavn, StringComparison.OrdinalIgnoreCase))
                {
                    results.Add(forestilling);
                }
            }
            
            return results;
        }
        
        public List<Forestilling> GetByDate(DateTime dato)
        {
            List<Forestilling> results = new List<Forestilling>();
            foreach (var forestilling in _forestillinger)
            {
                if (forestilling.Dato.Date == dato.Date)
                {
                    results.Add(forestilling);
                }
            }
            return results;
        }
        
        public List<Forestilling> GetUpcomingPerformances()
        {
            List<Forestilling> results = new List<Forestilling>();
            foreach (var forestilling in _forestillinger)
            {
                if (forestilling.Dato >= DateTime.Now)
                {
                    results.Add(forestilling);
                }
            }
            return results;
        }
        
        public List<Forestilling> GetPerformancesInCity(int byId)
        {
            List<Forestilling> results = new List<Forestilling>();
            foreach (var forestilling in _forestillinger)
            {
                if (forestilling.ById == byId)
                {
                    results.Add(forestilling);
                }
            }
            return results;
        }
    }
}

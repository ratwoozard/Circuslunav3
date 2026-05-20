using CirkusLuna.Core.Interfaces;
using CirkusLuna.Core.Models;

namespace CirkusLuna.Core.Repositories
{
    /// <summary>
    /// Simpelt in-memory repository til kunder.
    /// </summary>
    public class InMemoryKundeRepository : IKundeRepository
    {
        private List<Kunde> _kunder = new();
        private int _nextId = 1;
        
        public List<Kunde> GetAll()
        {
            return new List<Kunde>(_kunder);
        }
        
        public Kunde? GetById(int id)
        {
            foreach (var kunde in _kunder)
            {
                if (kunde.Id == id)
                {
                    return kunde;
                }
            }
            return null;
        }
        
        public void Add(Kunde entity)
        {
            // Giver kunden et nyt id.
            entity.Id = _nextId++;
            _kunder.Add(entity);
        }
        
        public void Update(Kunde entity)
        {
            var existing = GetById(entity.Id);
            if (existing != null)
            {
                existing.Navn = entity.Navn;
                existing.Email = entity.Email;
                existing.Telefon = entity.Telefon;
            }
        }
        
        public void Delete(int id)
        {
            var entity = GetById(id);
            if (entity != null)
            {
                _kunder.Remove(entity);
            }
        }
        
        public Kunde? GetByEmail(string email)
        {
            foreach (var kunde in _kunder)
            {
                if (kunde.Email.Equals(email, StringComparison.OrdinalIgnoreCase))
                {
                    return kunde;
                }
            }
            return null;
        }
    }
}

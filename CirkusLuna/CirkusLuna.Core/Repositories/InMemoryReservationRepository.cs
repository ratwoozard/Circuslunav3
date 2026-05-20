using CirkusLuna.Core.Interfaces;
using CirkusLuna.Core.Models;

namespace CirkusLuna.Core.Repositories
{
    /// <summary>
    /// Simpelt in-memory repository til reservationer.
    /// </summary>
    public class InMemoryReservationRepository : IReservationRepository
    {
        private List<Reservation> _reservationer = new();
        private int _nextId = 1;
        
        public List<Reservation> GetAll()
        {
            return new List<Reservation>(_reservationer);
        }
        
        public Reservation? GetById(int id)
        {
            foreach (var reservation in _reservationer)
            {
                if (reservation.Id == id)
                {
                    return reservation;
                }
            }
            return null;
        }
        
        public void Add(Reservation entity)
        {
            // Sætter id automatisk.
            entity.Id = _nextId++;
            _reservationer.Add(entity);
        }
        
        public void Update(Reservation entity)
        {
            var existing = GetById(entity.Id);
            if (existing != null)
            {
                existing.AntalBilletter = entity.AntalBilletter;
                existing.Billettype = entity.Billettype;
                existing.ReservationsDato = entity.ReservationsDato;
            }
        }
        
        public void Delete(int id)
        {
            var entity = GetById(id);
            if (entity != null)
            {
                _reservationer.Remove(entity);
            }
        }
        
        public List<Reservation> GetByForestillingId(int forestillingId)
        {
            // Finder alle reservationer til en bestemt forestilling.
            List<Reservation> results = new List<Reservation>();
            foreach (var reservation in _reservationer)
            {
                if (reservation.ForestillingId == forestillingId)
                {
                    results.Add(reservation);
                }
            }
            return results;
        }
        
        public List<Reservation> GetByKundeId(int kundeId)
        {
            // Finder alle reservationer for en bestemt kunde.
            List<Reservation> results = new List<Reservation>();
            foreach (var reservation in _reservationer)
            {
                if (reservation.KundeId == kundeId)
                {
                    results.Add(reservation);
                }
            }
            return results;
        }
    }
}

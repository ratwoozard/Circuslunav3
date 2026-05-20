using CirkusLuna.Core.Interfaces;
using CirkusLuna.Core.Models;

namespace CirkusLuna.Core.Repositories
{
    /// <summary>
    /// Simpelt in-memory repository til artister.
    /// Data ligger kun i hukommelsen mens appen kører.
    /// </summary>
    public class InMemoryArtistRepository : IArtistRepository
    {
        private List<Artist> _artister = new();
        private int _nextId = 1;
        
        public List<Artist> GetAll()
        {
            return new List<Artist>(_artister);
        }
        
        public Artist? GetById(int id)
        {
            foreach (var artist in _artister)
            {
                if (artist.Id == id)
                {
                    return artist;
                }
            }
            return null;
        }
        
        public void Add(Artist entity)
        {
            // Vi sætter id automatisk, så vi ikke skal gøre det manuelt.
            entity.Id = _nextId++;
            _artister.Add(entity);
        }
        
        public void Update(Artist entity)
        {
            var existing = GetById(entity.Id);
            if (existing != null)
            {
                existing.Navn = entity.Navn;
                existing.Specialitet = entity.Specialitet;
            }
        }
        
        public void Delete(int id)
        {
            var entity = GetById(id);
            if (entity != null)
            {
                _artister.Remove(entity);
            }
        }
    }
}

using CirkusLuna.Core.Models;

namespace CirkusLuna.Core.Interfaces
{
    /// <summary>
    /// Repository interface for Artist (Performer) entities
    /// </summary>
    public interface IArtistRepository : IRepository<Artist>
    {
        // Standard CRUD only - no special algorithms needed
    }
}

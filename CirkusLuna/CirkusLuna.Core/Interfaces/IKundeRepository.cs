using CirkusLuna.Core.Models;

namespace CirkusLuna.Core.Interfaces
{
    /// <summary>
    /// Repository interface for Kunde (Customer) entities
    /// </summary>
    public interface IKundeRepository : IRepository<Kunde>
    {
        Kunde? GetByEmail(string email);
    }
}

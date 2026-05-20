namespace CirkusLuna.Core.Interfaces
{
    /// <summary>
    /// Generic repository interface for basic CRUD operations
    /// </summary>
    /// <typeparam name="T">Entity type</typeparam>
    public interface IRepository<T> where T : class
    {
        List<T> GetAll();
        T? GetById(int id);
        void Add(T entity);
        void Update(T entity);
        void Delete(int id);
    }
}

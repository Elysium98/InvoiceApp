namespace InvoiceAPI.DataAccess.Interfaces
{
    public interface IBaseRepository<T> where T : class
    {
        Task Create(T entity);
        Task Update(T entity);
        Task Delete(T entity);
        Task<T> GetById(int id);
        Task<IEnumerable<T>> FindAll();
    }
}

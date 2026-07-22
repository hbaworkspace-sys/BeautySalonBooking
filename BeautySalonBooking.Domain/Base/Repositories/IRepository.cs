namespace BeautySalonBooking.Domain.Base
{
    public interface IRepository<T> where T : class
    {
        Task<List<T>> GetAll();
        Task<T?> GetById(int id);
        Task<bool> Exists(int id);

        Task Add(T entity);
        Task AddRange(IEnumerable<T> entities);

        void Delete(T entity);
        void DeleteRange(IEnumerable<T> entities);

        void Update(T entity);

        IQueryable<T> Query();
        //IQueryable<T> Query(int psgeSize,int pagenumber);
    }
}
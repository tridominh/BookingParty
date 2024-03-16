namespace BirthdayParty.Repository.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        IEnumerable<T> GetAll(params Func<IQueryable<T>, IQueryable<T>>[] includes);
        T Get(params int[] keys);
        T Add(T entity);
        T Update(T entity);
        T Delete(params int[] keys);
    }
}

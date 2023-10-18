using System.Linq.Expressions;

namespace Books2023.DataLayer.Repository.Interfaces
{
    public interface IRepository<T> where T : class
    {
        T Get(Expression<Func<T,bool>> filter,string? propertiesNames=null);
        IEnumerable<T> GetAll(string? propertiesNames = null);
        void Add(T entity);
        void Delete(T entity);
    }
}

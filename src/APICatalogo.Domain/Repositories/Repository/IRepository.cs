using System.Linq.Expressions;

namespace APICatalogo.Domain.Repositories.Repository;

public interface IRepository<T> {

    Task<IEnumerable<T>> GetAll();
    Task<T?> Get(Expression<Func<T, bool>> predicate);
    Task<T?> GetUpdate(Expression<Func<T, bool>> predicate);
    T Create(T entity);
    void Update(T entity);
    void Delete(T entity);

}

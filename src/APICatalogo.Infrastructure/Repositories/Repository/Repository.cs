using System.Linq.Expressions;
using APICatalogo.Domain.Repositories.Repository;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Infrastructure.Repositories.Repository;

public class Repository<T> : IRepository<T> where T : class {

    protected readonly AppDbContext _context;

    public Repository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<T>> GetAll() {
        return await _context.Set<T>().AsNoTracking().ToListAsync();
    }

    public async Task<T?> Get(Expression<Func<T, bool>> predicate)
    {
        return await _context.Set<T>().AsNoTracking().FirstOrDefaultAsync(predicate);
    }

    public async Task<T?> GetUpdate(Expression<Func<T, bool>> predicate) {
        return await _context.Set<T>().FirstOrDefaultAsync(predicate);
    }

    public T Create(T entity)
    {
        _context.Set<T>().Add(entity);
        return entity;
    }

    public void Update(T entity) {
        _context.Set<T>().Update(entity);
    }
    public void Delete(T entity){
        _context.Set<T>().Remove(entity);
    }
}





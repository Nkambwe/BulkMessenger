using BulkMessager.Data.Entities;
using BulkMessager.Utils;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BulkMessager.Data.Repositories {
    /// <summary>
    /// Generic class implementation
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="V"></typeparam>
    public class Repository<T, V> : IRepository<T, V> where T : DomainEntity<V> {

        private readonly DataContext _context;
        private readonly DbSet<T> _entities;

        private readonly IApplicationLogger<T> _logger;

        public Repository(DataContext context, IApplicationLogger<T> logger) {
            _context = context;
            _entities = context.Set<T>();
            _logger = logger;
        }

        public bool Exists(V Id)
            => _entities.FirstOrDefault(t => t.Id.Equals(Id)) != null;
        
        public async Task<bool> ExistsAsync(V Id) 
            => await _entities.FirstOrDefaultAsync(t => t.Id.Equals(Id)) != null;

        public virtual bool Exists(Expression<Func<T, bool>> expression) 
            => _entities.FirstOrDefault(expression) != null;
        
        public virtual async Task<bool> ExistsAsync(Expression<Func<T, bool>> expression)
            => await _entities.FirstOrDefaultAsync(expression) != null;

        public virtual int Count() 
            => _entities.Count();

        public virtual async Task<int> CountAsync() 
            => await _entities.CountAsync();

        public virtual async Task<int> CountAsync(Expression<Func<T, bool>> predicate) 
            => await _entities.CountAsync(predicate);

        public T FindById(V id) 
            => _entities.SingleOrDefault(t => t.Id.Equals(id));

        public virtual T FindById(V id, params Expression<Func<T, object>>[] include) {
            IQueryable<T> query = _entities;

            if (include != null)
                query = include.Aggregate(query, (start, next) => start.Include(next));

            return query.SingleOrDefault(t => t.Id.Equals(id));
        }

        public async Task<T> FindByIdAsync(V id) 
            => await _entities.SingleOrDefaultAsync(t => t.Id.Equals(id));

        public virtual async Task<T> FindByIdAsync(V id, params Expression<Func<T, object>>[] include) {
            IQueryable<T> query = _entities;

            if (include != null)
                query = include.Aggregate(query, (start, next) => start.Include(next));

            return await query.SingleOrDefaultAsync(t => t.Id.Equals(id));
        }

        public T Find(Expression<Func<T, bool>> expression) 
            => _entities.FirstOrDefault(expression);

        public async Task<T> FindAsync(Expression<Func<T, bool>> expression)
             => await _entities.FirstOrDefaultAsync(expression);

        public virtual async Task<T> FindAsync(Expression<Func<T, bool>> predicate, 
            params Expression<Func<T, object>>[] include) {
            IQueryable<T> query = _entities;

            if (include != null)
                query = include.Aggregate(query, (start, next) => start.Include(next));

            return await query.SingleOrDefaultAsync(predicate);
        }
        
        public IList<T> GetAll()
             => _entities.ToList();
        
        public async Task<IList<T>> GetAllAsync()
             => await _entities.ToListAsync();

        public IList<T> GetAll(Expression<Func<T, bool>> expression)
             => _entities.Where(expression).ToList();

        public async Task<IList<T>> GetAllAsync(Expression<Func<T, bool>> expression) 
           => await _entities.Where(expression).ToListAsync();

        public virtual async Task<IQueryable<T>> GetAllAsync(Expression<Func<T, bool>> predicate, 
             params Expression<Func<T, object>>[] filters) {
            var queryList = await _entities.ToListAsync();
            if (!queryList.Any())
                return new List<T>().AsQueryable();

            var query = queryList.AsQueryable();
            if (filters != null)
                query = filters.Aggregate(query, (current, property) => current.Include(property));

            query = query.Where(predicate);
            return query;
        }

        public bool Insert(T entity) {
            _entities.Add(entity);
           return _context.SaveChanges() > 0;
        }

        public async Task<bool> InsertAsync(T entity) {
            await _entities.AddAsync(entity);
            return await _context.SaveChangesAsync() > 0;
        }
        public bool Insert(T[] entities) {
            _entities.AddRange(entities);
            return _context.SaveChanges() > 0;
        }

        public async Task<bool> InsertAsync(T[] entities) {
            await _entities.AddRangeAsync(entities);
            return await _context.SaveChangesAsync() > 0;
        }

        public bool Delete(T entity) {
             _entities.Remove(entity);
            return _context.SaveChanges() > 0;
        }

        public async Task<bool> DeleteAsync(T entity) {
            _entities.Remove(entity);
            return await _context.SaveChangesAsync() > 0;
        }
        public bool Delete(T[] entities) {
            _entities.RemoveRange(entities);
            return _context.SaveChanges() > 0;
        }
        
        public async Task<bool> DeleteAsync(T[] entities) {
            _entities.RemoveRange(entities);
            return await _context.SaveChangesAsync() > 0;
        }

        public virtual async Task<IQueryable<T>> PageAllAsync(int skip, int take) {
            var queryList = await _entities.Skip(skip).Take(take).ToListAsync();
            return !queryList.Any() ? new List<T>().AsQueryable() : queryList.AsQueryable();
        }

        public virtual async Task<IQueryable<T>> PageAllAsync(CancellationToken token, int skip, int take) {
            var items = await _entities.Skip(skip).Take(take).ToListAsync(token);
            return items.AsQueryable();
        }
    }
}

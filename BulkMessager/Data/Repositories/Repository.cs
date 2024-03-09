using BulkMessager.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using System.Linq.Expressions;

namespace BulkMessager.Data.Repositories {
    /// <summary>
    /// Generic class implementation
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="V"></typeparam>
    public class Repository<T, V> : IRepository<T, V> where T : DomainEntity<V> {
        private readonly DbContext _context;
        private readonly DbSet<T> _entities;

        public Repository(DbContext context) {
            _context = context;
            _entities = context.Set<T>();
        }

        public bool Exists(V Id)
            => _entities.FirstOrDefault(t => t.Id.Equals(Id)) != null;
        
        public async Task<bool> ExistsAsync(V Id) 
            => await _entities.FirstOrDefaultAsync(t => t.Id.Equals(Id)) != null;

        public bool Exists(Expression<Func<T, bool>> expression) 
            => _entities.FirstOrDefault(expression) != null;
        
        public async Task<bool> ExistsAsync(Expression<Func<T, bool>> expression)
            => await _entities.FirstOrDefaultAsync(expression) != null;

        public T FindById(V id) 
            => _entities.SingleOrDefault(t => t.Id.Equals(id));

        public async Task<T> FindByIdAsync(V id) 
            => await _entities.SingleOrDefaultAsync(t => t.Id.Equals(id));

        public T Find(Expression<Func<T, bool>> expression) 
            => _entities.FirstOrDefault(expression);

        public async Task<T> FindAsync(Expression<Func<T, bool>> expression)
             => await _entities.FirstOrDefaultAsync(expression);
        
        public IList<T> GetAll()
             => _entities.ToList();
        
        public async Task<IList<T>> GetAllAsync()
             => await _entities.ToListAsync();

        public IList<T> GetAll(Expression<Func<T, bool>> expression)
             => _entities.Where(expression).ToList();

        public async Task<IList<T>> GetAllAsync(Expression<Func<T, bool>> expression) 
           => await _entities.Where(expression).ToListAsync();

        public void Insert(T entity) {
            _entities.Add(entity);
            _context.SaveChanges();
        }

        public async Task InsertAsync(T entity) {
            await _entities.AddAsync(entity);
            await _context.SaveChangesAsync();
        }
        public void Insert(T[] entities) {
            _entities.AddRange(entities);
            _context.SaveChanges();
        }

        public async Task InsertAsync(T[] entities) {
            await _entities.AddRangeAsync(entities);
            await _context.SaveChangesAsync();
        }

        public void Delete(T entity) {
             _entities.Remove(entity);
            _context.SaveChanges();
        }

        public async Task DeleteAsync(T entity) {
            _entities.Remove(entity);
            await _context.SaveChangesAsync();
        }
        public void Delete(T[] entities) {
            _entities.RemoveRange(entities);
            _context.SaveChanges();
        }
        
        public async Task DeleteAsync(T[] entities) {
            _entities.RemoveRange(entities);
            await _context.SaveChangesAsync();
        }
    }
}

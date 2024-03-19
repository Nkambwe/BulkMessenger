using BulkMessager.Data.Entities;
using BulkMessager.Utils;
using System.Linq.Expressions;

namespace BulkMessager.Data.Repositories {
    /// <summary>
    /// Repository interface
    /// </summary>
    /// <typeparam name="T">Object type</typeparam>
    /// <typeparam name="V">Object Id data type</typeparam>
    public interface IRepository<T,V> where T : DomainEntity<V> {

        /// <summary>
        /// Find Entity By Id
        /// </summary>
        /// <param name="id">Entity Id</param>
        /// <returns>Entity with specified Id, null if not found</returns>
        T FindById(V id);
        /// <summary>
        /// Find Entity By Id
        /// </summary>
        /// <param name="id">Entity Id</param>
        /// <param name="include">Filter expression</param>
        /// <returns>Entity with specified Id, null if not found</returns>
        T FindById(V id, params Expression<Func<T, object>>[] include);
        /// <summary>
        /// Find Entity By Id
        /// </summary>
        /// <param name="id">Entity Id</param>
        /// <returns>Entity with specified Id, null if not found</returns>
        Task<T> FindByIdAsync(V id);
        /// <summary>
        /// Find Entity By Id
        /// </summary>
        /// <param name="id">Entity Id</param>
        /// <param name="include">Filter expression</param>
        /// <returns>Entity with specified Id, null if not found</returns>
        Task<T> FindByIdAsync(V id, params Expression<Func<T, object>>[] include);
        /// <summary>
        /// Find Entity that fits expression
        /// </summary>
        /// <param name="predicate">Filter expression</param>
        /// <returns>Entity which fits expression filter, null if not found</returns>
        T Find(Expression<Func<T, bool>> predicate);
        /// <summary>
        /// Find Entity that fits expression
        /// </summary>
        /// <param name="predicate">Filter expression</param>
        /// <returns>Entity which fits expression filter, null if not found</returns>
        Task<T> FindAsync(Expression<Func<T, bool>> predicate);
        /// <summary>
        /// Find Entity that fits expression
        /// </summary>
        /// <param name="predicate">Filter expression</param>
        /// <param name="include">Filter expression</param>
        /// <returns>Entity which fits expression filter, null if not found</returns>
        Task<T> FindAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] include);
        /// <summary>
        /// Get all entities in the database
        /// </summary>
        /// <returns>List of all existing entities</returns>
        IList<T> GetAll();
        /// <summary>
        /// Get all entities in the database
        /// </summary>
        /// <returns>List of all existing entities</returns>
        Task<IList<T>> GetAllAsync();
        /// <summary>
        /// Get all entities in the database that match expression
        /// </summary>
        /// <param name="predicate">Filter expression</param>
         /// <returns>List of all existing entities</returns>
        IList<T> GetAll(Expression<Func<T, bool>> predicate);
        Task<IList<T>> GetAllAsync(Expression<Func<T, bool>> predicate);
        Task<IQueryable<T>> GetAllAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] filters); 
        bool Insert(T entity);
        Task<bool> InsertAsync(T entity);
        bool Insert(T[] entities);
        Task<bool> InsertAsync(T[] entities);
        bool Delete(T entity);
        Task<bool> DeleteAsync(T entity);
        bool Delete(T[] entities);
        Task<bool> DeleteAsync(T[] entities);
        int Count();
        Task<int> CountAsync();
        Task<int> CountAsync(Expression<Func<T, bool>> predicate);
        bool Exists(V Id);
        Task<bool> ExistsAsync(V Id);
        bool Exists(Expression<Func<T, bool>> expression);
        Task<bool> ExistsAsync(Expression<Func<T, bool>> expression);
        Task<IQueryable<T>> PageAllAsync(int skip, int take);
        Task<IQueryable<T>> PageAllAsync(CancellationToken token, int skip, int take);
    }
}

using BulkMessager.Data.Entities;
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
        /// <returns>Entity with specified Id, null if not found</returns>
        Task<T> FindByIdAsync(V id);
        /// <summary>
        /// Find Entity that fits expression
        /// </summary>
        /// <param name="expression">Filter expression</param>
        /// <returns>Entity which fits expression filter, null if not found</returns>
        T Find(Expression<Func<T, bool>> expression);
        /// <summary>
        /// Find Entity that fits expression
        /// </summary>
        /// <param name="expression">Filter expression</param>
        /// <returns>Entity which fits expression filter, null if not found</returns>
        Task<T> FindAsync(Expression<Func<T, bool>> expression);
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
        /// <param name="expression">Filter expression</param>
         /// <returns>List of all existing entities</returns>
        IList<T> GetAll(Expression<Func<T, bool>> expression);
        Task<IList<T>> GetAllAsync(Expression<Func<T, bool>> expression);
        void Insert(T entity);
        Task InsertAsync(T entity);
        void Insert(T[] entities);
        Task InsertAsync(T[] entities);
        void Delete(T entity);
        Task DeleteAsync(T entity);
        void Delete(T[] entities);
        Task DeleteAsync(T[] entities);
        bool Exists(V Id);
        Task<bool> ExistsAsync(V Id);
        bool Exists(Expression<Func<T, bool>> expression);
        Task<bool> ExistsAsync(Expression<Func<T, bool>> expression);
    }
}

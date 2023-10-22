using System.Linq.Expressions;

namespace Persistence.Repository
{
    public interface IRepository<T> where T : class
    {
        /// <summary>Get all records of a table in database.</summary>
        /// <exception cref="DatabaseException">Thrown when the operation fails.</exception>
        IEnumerable<T> GetAll(Func<IQueryable<T>, IQueryable<T>>? includeProperties = null);

        /// <summary>Get a record from a table by id</summary>
        /// <exception cref="DatabaseException">Thrown when the operation fails.</exception>
        T GetById(object id);

        /// <summary>Insert a record to a table</summary>
        /// <exception cref="DatabaseException">Thrown when the operation fails.</exception>
        void Insert(T entity);

        /// <summary>Update a record of table</summary>
        /// <exception cref="DatabaseException">Thrown when the operation fails.</exception>
        void Update(T entity);

        /// <summary>Delete a record from a table </summary>
        /// <exception cref="DatabaseException">Thrown when the operation fails.</exception>
        void Delete(object id);

        /// <summary>Retrieve a list of record tha match the filter expression</summary>
        /// <exception cref="UnitOfWorkException">Thrown when the operation fails.</exception>
        IEnumerable<T> Find(Expression<Func<T, bool>> filter, params Expression<Func<T, object>>[] includeProperties);
    }
}
using System.Linq.Expressions;
using DistLab2.Persistence.Error;
using Microsoft.EntityFrameworkCore;
using Persistence.Repository;

namespace DistLab2.Persistence
{
    public class Repository<T> : IRepository<T> where T : class
    {

        internal DbContext _context;
        internal DbSet<T> _dbSet;

        public Repository(AuctionDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public IEnumerable<T> GetAll(Func<IQueryable<T>, IQueryable<T>>? includeProperties = null)
        {
            try
            {
                // If we need to use eager loading
                // Ex: repository.GetAll(q => q.Include(a => a.Bids));
                IQueryable<T> query = _dbSet;
                if (includeProperties != null)
                {
                    query = includeProperties(query);
                }

                return query.ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                throw new DatabaseException("Failed to fetch all records\nDetails: " + ex.Message);
            }
        }

        public T GetById(object id)
        {
            try
            {
                return _dbSet.Find(id) ?? throw new DatabaseException($"No record found with id {id}");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                throw new DatabaseException($"Failed to fetch record with id {id}\nDetails: " + ex.Message);
            }
        }


        public IEnumerable<T> Find(Expression<Func<T, bool>> filter, params Expression<Func<T, object>>[] includeProperties)
        {
            try
            {
                IQueryable<T> query = _context.Set<T>().Where(filter);
                foreach (var includeProperty in includeProperties)
                {
                    query = query.Include(includeProperty);
                }
                return query.ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                throw new DatabaseException("Failed to fetch records based on filter\nDetails: " + ex.Message);
            }
        }

        public void Insert(T entity)
        {
            try
            {
             

                _dbSet.Add(entity);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                throw new DatabaseException("Failed to insert record\nDetails: " + ex.Message);
            }
        }

        public void Update(T entity)
        {
            try
            {
                _dbSet.Attach(entity);
                _context.Entry(entity).State = EntityState.Modified;
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                throw new DatabaseException("Failed to update record\nDetails: " + ex.Message);
            }
        }

        public void Delete(object id)
        {
            try
            {
                T entity = _dbSet.Find(id) ?? throw new DatabaseException($"No record found with id {id}");
                _dbSet.Remove(entity);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                throw new DatabaseException($"Failed to delete record with id {id}\nDetails: " + ex.Message);
            }
        }


    }
}

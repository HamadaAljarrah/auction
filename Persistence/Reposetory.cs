using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using SQLitePCL;

namespace DistLab2.Persistence
{
    public class Reposetory<T> : IReposetory<T> where T : class
    {
        private readonly AuctionDbContext _context;
        private readonly DbSet<T> _dbSet;

        public Reposetory(AuctionDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }
        //läg en konstorutor per context typ så en för user osv
        public IEnumerable<T> GetAll()
        {
            return _dbSet.ToList();
        }

        public T GetById(int id)
        {
            Console.WriteLine("id :"+id);
            var a = _dbSet.Find(id);
            return _dbSet.Find(id);
        }

        public IEnumerable<T> Find(Func<T, bool> predicate)
        {
            return _dbSet.Where(predicate);
        }

        public void Add(T entity)
        {
            Console.WriteLine("in entity "+entity.ToString);
            _dbSet.Add(entity);
            _context.SaveChanges();
        }

        public void Update(T entity)
        {
            // Implement update logic as needed
        }

        public void Remove(T entity)
        {
            _dbSet.Remove(entity);
        }
    }
}

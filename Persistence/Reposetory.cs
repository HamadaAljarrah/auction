using System.ComponentModel.DataAnnotations;
namespace DistLab2.Persistence
{
    public class Reposetory<T> : IReposetory<T> where T : class
    {
        private List<T> entities;

        public Reposetory()
        {
            entities = new List<T>();
        }

        public IEnumerable<T> GetAll()
        {
            return entities;
        }

        public T GetById(int id)
        {
            return entities.FirstOrDefault(e => e.GetHashCode() == id);
        }

        public IEnumerable<T> Find(Func<T, bool> predicate)
        {
            return entities.Where(predicate);
        }

        public void Add(T entity)
        {
            entities.Add(entity);
        }

        public void Update(T entity)
        {
            // Implement update logic as needed
        }

        public void Remove(T entity)
        {
            entities.Remove(entity);
        }

    }
}

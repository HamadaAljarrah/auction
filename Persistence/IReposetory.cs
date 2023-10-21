using System.ComponentModel.DataAnnotations;

namespace DistLab2.Persistence
{
    public interface IReposetory<T>
    {
        IEnumerable<T> GetAll();
        T GetById(int id);
        IEnumerable<T> Find(Func<T, bool> predicate);
        void Add(T entity);
        void Update(T entity);
        void Remove(T entity);

        public IEnumerable<T> ExecuteQuery(string query, params object[] parameters);

    }
}

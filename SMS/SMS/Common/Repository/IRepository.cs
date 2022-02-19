using System.Linq;

namespace SMS.Common.Repository
{
    public interface IRepository
    {
        void Add<T>(T entity) where T : class;

        IQueryable<T> All<T>() where T : class;

        int SaveChanges();
    }
}

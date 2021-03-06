using Microsoft.EntityFrameworkCore;
using SharedTrip.Data;
using System.Linq;

namespace SharedTrip.Common
{
    public class Repository : IRepository
    {
        private readonly DbContext data;

        public Repository(ApplicationDbContext _data)
        {
            data = _data;
        }

        public void Add<T>(T entity) where T : class
        {
            DbSet<T>().Add(entity);
        }

        public IQueryable<T> All<T>() where T : class
        {
            return DbSet<T>().AsQueryable();
        }

        public int SaveChanges()
        {
            return data.SaveChanges();
        }

        private DbSet<T> DbSet<T>() where T : class
        {
            return data.Set<T>();
        }
    }
}

using Microsoft.EntityFrameworkCore;
using Puzzle_API.Data;

namespace Puzzle_API.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly DataContext _context;
        internal DbSet<T> dbSet;

        public Repository(DataContext context)
        {
            _context = context;
            this.dbSet = _context.Set<T>();
        }

        public Task<T> Get(List<string> strings)
        {
            throw new NotImplementedException();
        }
    }
}

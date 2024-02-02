using FinanceAPI.DataAccess;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace FinanceAPI.Shared.Repositories
{
    public interface IBaseRepository<T>
        where T : class
    {
        Task<T?> FirstOrDefault(
            Expression<Func<T, bool>> where);
        Task<bool> Any(Expression<Func<T, bool>> wherePredicate = null);
    }

    public class BaseRepository<T> : IBaseRepository<T>
        where T : class
    {
        private readonly DataContext _context;

        public BaseRepository(
            DataContext context)
        {
            _context = context;
        }

        public async Task<T?> FirstOrDefault(
            Expression<Func<T, bool>> where)
        {
            return await _context
                .Set<T>()
                .FirstOrDefaultAsync(where);
        }

        public async Task<bool> Any(
            Expression<Func<T, bool>> where = null)
        {
            return where is not null
                ? await _context.Set<T>().AnyAsync(where)
                : await _context.Set<T>().AnyAsync();
        }
    }
}

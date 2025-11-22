using Microsoft.EntityFrameworkCore;
using SalesManagement.Data;
using SalesManagement.Repositories.Interfaces;
using System.Linq.Expressions;

namespace SalesManagement.Repositories.Implementations
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly SalesManagementContext _context;
        public GenericRepository(SalesManagementContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<T>> GetAllAsync(params Func<IQueryable<T>, IQueryable<T>>[] includeChains)
        {
            IQueryable<T> query = _context.Set<T>();

            foreach (var chain in includeChains)
            {
                query = chain(query);
            }

            return await query.ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id, params Func<IQueryable<T>, IQueryable<T>>[] includeChains)
        {
            IQueryable<T> query = _context.Set<T>();

            foreach (var chain in includeChains)
            {
                query = chain(query);
            }

            return await query.FirstOrDefaultAsync(e => EF.Property<int>(e, "Id") == id);
        }

        public async Task AddAsync(T entity) => await _context.Set<T>().AddAsync(entity);

        public void Update(T entity) => _context.Set<T>().Update(entity);

        public void Delete(T entity) => _context.Set<T>().Remove(entity);
        public async Task<bool> ExistsAsync(int id)
        {
            var entity = await GetByIdAsync(id);
            return entity != null;
        }
    }
}

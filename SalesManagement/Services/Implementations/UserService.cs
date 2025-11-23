using SalesManagement.Data;
using SalesManagement.Models.Entities;
using SalesManagement.Repositories.Interfaces;
using SalesManagement.Services.Interfaces;

namespace SalesManagement.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly IGenericRepository<User> _repository;
        private readonly SalesManagementContext _context;
        public UserService(IGenericRepository<User> repository, SalesManagementContext context)
        {
            _repository = repository;
            _context = context;
        }
        public async Task CreateAsync(User user)
        {
            await _repository.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var item = await _repository.GetByIdAsync(id);
            if (item == null) return;
            _repository.Delete(item);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<User>> GetAllAsync() => await _repository.GetAllAsync();

        public async Task<User?> GetByIdAsync(int id) => await _repository.GetByIdAsync(id);

        public async Task UpdateAsync(User user)
        {
            _repository.Update(user);
            await _context.SaveChangesAsync();
        }
        public async Task<bool> ExistsAsync(int id) => await _repository.ExistsAsync(id);

        public IEnumerable<User> Filter(IEnumerable<User> items, string searchFirstName, string searchLastName)
        {
            if (!string.IsNullOrEmpty(searchFirstName))
            {
                items = items.Where(x => x.FirstName.ToUpper().Contains(searchFirstName.ToUpper()));
            }
            if (!string.IsNullOrEmpty(searchLastName))
            {
                items = items.Where(x => x.LastName.ToUpper().Contains(searchLastName.ToUpper()));
            }
            return items;
        }

    }
}

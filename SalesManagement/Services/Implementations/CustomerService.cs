using Microsoft.EntityFrameworkCore;
using SalesManagement.Data;
using SalesManagement.Models.Entities;
using SalesManagement.Repositories.Interfaces;
using SalesManagement.Services.Interfaces;

namespace SalesManagement.Services.Implementations
{
    public class CustomerService : IService<Customer>
    {
        private readonly IGenericRepository<Customer> _repository;
        private readonly SalesManagementContext _context;
        public CustomerService(IGenericRepository<Customer> repository, SalesManagementContext context)
        {
            _repository = repository;
            _context = context;
        }
        public async Task CreateAsync(Customer customer)
        {
            await _repository.AddAsync(customer);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var item = await _repository.GetByIdAsync(id);
            if (item == null) return;
            _repository.Delete(item);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Customer>> GetAllAsync() => await _repository.GetAllAsync(x => x.Include(x => x.Transactions));

        public async Task<Customer?> GetByIdAsync(int id) => await _repository.GetByIdAsync(id, x => x.Include(x => x.Transactions));

        public async Task UpdateAsync(Customer customer)
        {
            _repository.Update(customer);
            await _context.SaveChangesAsync();
        }
        public async Task<bool> ExistsAsync(int id) => await _repository.ExistsAsync(id);

    }
}

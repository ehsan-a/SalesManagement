using SalesManagement.Models.Entities;

namespace SalesManagement.Services.Interfaces
{
    public interface ICustomerService : IService<Customer>
    {
        IEnumerable<Customer> Filter(IEnumerable<Customer> items, string searchFirstName, string searchLastName);
    }
}

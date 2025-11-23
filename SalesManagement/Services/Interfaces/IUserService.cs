using SalesManagement.Models.Entities;

namespace SalesManagement.Services.Interfaces
{
    public interface IUserService : IService<User>
    {
        IEnumerable<User> Filter(IEnumerable<User> items, string searchFirstName, string searchLastName);
    }
}

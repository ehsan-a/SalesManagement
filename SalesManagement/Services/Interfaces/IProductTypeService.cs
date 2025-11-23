using SalesManagement.Models.Entities;
using System.Net.Sockets;

namespace SalesManagement.Services.Interfaces
{
    public interface IProductTypeService : IService<ProductType>
    {
        IEnumerable<ProductType> Filter(IEnumerable<ProductType> items, string searchString, string productTypeCategory);
    }
}

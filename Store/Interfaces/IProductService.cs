using System.Threading.Tasks;
using Store.Domain.Entity;

namespace Store.Data.EntityFrameworkCore.Services
{
    public interface IProductService
    {
        Task InsertAsync(Product product);
        Task UpdateAsync(Product product);
        Task DeleteAsync(int id);
    }
}
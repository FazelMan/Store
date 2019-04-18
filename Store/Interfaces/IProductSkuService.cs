using System.Collections.Generic;
using System.Threading.Tasks;
using Store.Domain.Dto.ProductSku;
using Store.Domain.Entity;

namespace Store.Data.EntityFrameworkCore.Services
{
    public interface IProductSkuService
    {
        ApiResultList<IEnumerable<ProductSkuGet>> GetAll(int pageIndex);
        Task InsertAsync(ProductSku productSku);
        Task UpdateAsync(ProductSku productSku);
        Task DeleteAsync(int id);
        Task<int> GetExistCountAsync(int productSkuId);
        Task UpdateInventoryAsync(int productSkuId, int quantity);
    }
}
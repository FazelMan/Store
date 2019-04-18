using System.Collections.Generic;
using System.Threading.Tasks;
using Store.Domain.Dto.Purchase;
using Store.Domain.Entity;

namespace Store.Interfaces
{
    public interface IPurchaseService
    {
        Task<Purchase> GetPurchaseAsync(int id);
        ApiResultList<IEnumerable<PurchaseGet>> GetAll(int pageIndex);
        Task InsertAsync(Purchase purchase);
        Task UpdateAsync(Purchase purchase);
        Task DeleteAsync(int id);
    }
}

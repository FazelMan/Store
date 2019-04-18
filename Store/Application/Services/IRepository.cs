using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Store.Domain.Entity;

namespace Store.Application.Services
{
    public interface IRepository<TEntity, TType> where TEntity : Entity<TType>
    {
        Task DeleteAsync(TType id);
        Task<TEntity> FindAsync(TType id);
        IPagedList<TEntity> GetAll(int pageIndex = 1, int pageSize = int.MaxValue);
        Task<TEntity> InsertAsync(TEntity entity);
        IQueryable<TEntity> Table();
        IQueryable<TEntity> TableNoTracking();
        Task UpdateAsync(TEntity entitie);
    }
}

using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Store.Application.Services;
using Store.Data.EntityFrameworkCore.Uow;
using Store.Domain.Entity;
using Store.Extentions;

namespace Store.Data.EntityFrameworkCore.Repositories
{
    public class EFCoreRepository<TEntity, TType> : IRepository<TEntity, TType> where TEntity : Entity<TType>, new()
    {
        private readonly IDbContext _context;
        private readonly DbSet<TEntity> _entities;

        public EFCoreRepository(IDbContext context)
        {
            _context = context;
            _entities = context.Set<TEntity>();
        }

        public async Task<TEntity> FindAsync(TType id)
        {
            return await _entities.FindAsync(id);
        }

        public async Task<TEntity> InsertAsync(TEntity entity)
        {
            await _entities.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(TType id)
        {
            var table = await _entities.FindAsync(id);
            PropertyInfo property = table.GetType().GetProperties().FirstOrDefault(x => x.Name == "IsRemoved");

            //check soft delete or hard delete
            if (property != null)
            {
                SetValueWithReflectionExtention.SetValue(table, "IsRemoved", true);
                _context.SaveChanges();
            }
            else
            {
                _entities.Remove(table);
                _context.SaveChanges();
            }
        }

        public async Task UpdateAsync(TEntity entity)
        {
            var model = await FindAsync(entity.Id);
            if (model == null) return;
            _context.Entry(model).CurrentValues.SetValues(entity);
            await _context.SaveChangesAsync();
        }

        public IQueryable<TEntity> Table()
        {
            return _entities;
        }

        public IQueryable<TEntity> TableNoTracking()
        {
            return _entities.AsNoTracking();
        }

        public IPagedList<TEntity> GetAll(int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var query = _entities.AsNoTracking();
            return new PagedList<TEntity>(query, pageIndex, pageSize);
        }
    }

}
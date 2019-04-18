using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Store.Application.Services;
using Store.Domain.Dto.Purchase;
using Store.Domain.Entity;
using Store.Interfaces;

namespace Store.Data.EntityFrameworkCore.Services
{
    public class PurchaseService : IPurchaseService
    {
        private readonly IRepository<Purchase, int> _purchaseRepository;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public PurchaseService(IRepository<Purchase, int> purchaseRepository,
            IConfiguration configuration,
            IMapper mapper)
        {
            _purchaseRepository = purchaseRepository;
            _configuration = configuration;
            _mapper = mapper;
        }

        public async Task<Purchase> GetPurchaseAsync(int id)
        {
            return await _purchaseRepository.FindAsync(id);
        }

        public ApiResultList<IEnumerable<PurchaseGet>> GetAll(int pageIndex)
        {
            var pageSize = int.Parse(_configuration["Pagination:PageSize"]);

            var query = _purchaseRepository.Table()
                .Include(x => x.ProductSku) //for include productSku detail
                .ThenInclude(x => x.Product); //for include product detail

            //set query pagination
            PagedList<Purchase> result = new PagedList<Purchase>(query, pageIndex, pageSize);

            var purchaseGetList = new List<PurchaseGet>();
            _mapper.Map(result, purchaseGetList);

            return new ApiResultList<IEnumerable<PurchaseGet>>
            {
                Result = purchaseGetList,
                TotalCount = result.TotalCount,
                FilteredCount = result.FilteredCount
            };
        }

        public async Task InsertAsync(Purchase purchase)
        {
            await _purchaseRepository.InsertAsync(purchase);
        }

        public async Task UpdateAsync(Purchase purchase)
        {
            purchase.UpdatedDate = DateTime.Now;
            await _purchaseRepository.UpdateAsync(purchase);
        }

        public async Task DeleteAsync(int id)
        {
            await _purchaseRepository.DeleteAsync(id);
        }
    }
}
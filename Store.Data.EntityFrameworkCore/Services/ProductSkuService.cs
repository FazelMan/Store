using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Store.Application.Services;
using Store.Domain.Dto.ProductSku;
using Store.Domain.Entity;

namespace Store.Data.EntityFrameworkCore.Services
{
    public class ProductSkuService : IProductSkuService
    {
        private readonly IRepository<ProductSku, int> _productSkuRepository;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public ProductSkuService(IRepository<ProductSku, int> productSkuRepository,
            IConfiguration configuration,
            IMapper mapper)
        {
            _productSkuRepository = productSkuRepository;
            _configuration = configuration;
            _mapper = mapper;
        }

        public ApiResultList<IEnumerable<ProductSkuGet>> GetAll(int pageIndex)
        {
            var pageSize = int.Parse(_configuration["Pagination:PageSize"]);

            var query = _productSkuRepository.TableNoTracking()
                .Include(x => x.Product); //for include productTitle

            //set query pagination
            PagedList<ProductSku> result = new PagedList<ProductSku>(query, pageIndex, pageSize);

            var productSkuGetList = new List<ProductSkuGet>();
            _mapper.Map(result, productSkuGetList);

            return new ApiResultList<IEnumerable<ProductSkuGet>>
            {
                Result = productSkuGetList,
                TotalCount = result.TotalCount,
                FilteredCount = result.FilteredCount
            };
        }

        public async Task InsertAsync(ProductSku productSku)
        {
            if (productSku == null)
            {
                throw new ArgumentNullException("ProductSku must be entered!");
            }

            if (string.IsNullOrWhiteSpace(productSku.Title))
            {
                throw new Exception("Title is required!");
            }

            await _productSkuRepository.InsertAsync(productSku);
        }

        public async Task UpdateAsync(ProductSku productSku)
        {
            productSku.UpdatedDate = DateTime.Now;
            await _productSkuRepository.UpdateAsync(productSku);
        }

        public async Task DeleteAsync(int id)
        {
            if (id == 0)
            {
                throw new Exception("Please insert Valid Number!");
            }

            await _productSkuRepository.DeleteAsync(id);
        }

        public async Task<int> GetExistCountAsync(int productSkuId)
        {
            var result = await _productSkuRepository.TableNoTracking()
                .FirstOrDefaultAsync(x => x.Id == productSkuId);

            return result.Quantity;
        }

        public async Task UpdateInventoryAsync(int productSkuId, int quantity)
        {
            var productSku = await _productSkuRepository.FindAsync(productSkuId);
            productSku.Quantity = quantity;
            await _productSkuRepository.UpdateAsync(productSku);
        }
    }
}
using System;
using System.Threading.Tasks;
using AutoMapper;
using Store.Application.Services;
using Store.Domain.Entity;

namespace Store.Data.EntityFrameworkCore.Services
{
    public class ProductService : IProductService
    {
        private readonly IRepository<Product, int> _productRepository;
        private readonly IMapper _mapper;

        public ProductService(IRepository<Product, int> productRepository, 
            IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task InsertAsync(Product product)
        {
             throw new NotImplementedException();
        }

        public async Task UpdateAsync(Product product)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}

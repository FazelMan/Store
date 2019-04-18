using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Moq;
using NUnit.Framework;
using Store.Application.Services;
using Store.Data.EntityFrameworkCore.Services;
using Store.Domain.Dto.ProductSku;
using Store.Domain.Entity;

namespace Store.Tests
{
    public class ProductSkuTest
    {
        private static ProductSkuService _productSkuService;
        private static Mock<IRepository<ProductSku, int>> _mockProductSkuRepository;
        private static IConfiguration _configuration;
        private static Mock<IMapper> _mockMapper;

        [SetUp]
        public void Setup()
        {
            _mockProductSkuRepository = new Mock<IRepository<ProductSku, int>>();
            _configuration = Config.InitConfiguration();
            _mockMapper = new Mock<IMapper>();
            _productSkuService = new ProductSkuService(_mockProductSkuRepository.Object, _configuration, _mockMapper.Object);
        }

        [Test]
        public void Insert_Return_TitleIsRequired()
        {
            //Arrange  
            var productSku = new ProductSku()
            {
                Quantity = 10,
                Price = 1000,
                ProductId = 1
            };

            //Act  
            var ex = Assert.ThrowsAsync<Exception>(() => _productSkuService.InsertAsync(productSku));

            //Assert  
            Assert.That(ex.Message, Is.EqualTo("Title is required!"));
        }
    }
}
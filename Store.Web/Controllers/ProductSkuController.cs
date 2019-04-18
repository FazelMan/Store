using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Store.Data.EntityFrameworkCore.Services;
using Store.Domain.Dto.ProductSku;
using Store.Domain.Entity;

namespace Store.Web.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class ProductSkuController : Controller
    {
        private readonly IProductSkuService _productSkuService;
        private readonly IMapper _mapper;

        public ProductSkuController(IProductSkuService productSkuService,
            IMapper mapper)
        {
            _productSkuService = productSkuService;
            _mapper = mapper;
        }

        /// <summary>
        /// Get list of productSkus. 
        /// </summary>
        /// <param name="pageIndex">The requested pageIndex.</param>
        /// <returns>The requested productSku list.</returns>
        /// <response code="200">The productSku list was successfully retrieved.</response>
        [HttpGet("{pageIndex:int}")]
        [ProducesResponseType(typeof(ApiResultList<IEnumerable<ProductSkuGet>>), (int)HttpStatusCode.OK)]
        public IActionResult Get(int pageIndex = 1)
        {
            var result = _productSkuService.GetAll(pageIndex);

            return Ok(result);
        }

        /// <summary>
        /// Create a new productSku.
        /// </summary>
        /// <param name="productSkuInsert">The productSku to create.</param>
        /// <response code="202">The productSku was successfully created.</response>
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        public async Task<IActionResult> InsertAsync(ProductSkuInsert productSkuInsert)
        {
            var productSku = new ProductSku();

            _mapper.Map(productSkuInsert, productSku);
            await _productSkuService.InsertAsync(productSku);

            return StatusCode((int)HttpStatusCode.Created);
        }

        /// <summary>
        /// Update a productSku.
        /// </summary>
        /// <param name="productSkuUpdate">The productSku to update.</param>
        /// <response code="202">The productSku was successfully updated.</response>
        [HttpPut]
        [ProducesResponseType((int)HttpStatusCode.Accepted)]
        public async Task<IActionResult> UpdateAsync(ProductSkuUpdate productSkuUpdate)
        {
            var productSku = new ProductSku();

            _mapper.Map(productSkuUpdate, productSku);
            await _productSkuService.UpdateAsync(productSku);

            return Accepted();
        }

        /// <summary>
        /// Delete a single productSku. 
        /// </summary>
        /// <param name="id">The requested productSku identifier.</param>
        /// <response code="202">The productSku was successfully deleted.</response>
        [HttpDelete("{id:int}")]
        [ProducesResponseType((int)HttpStatusCode.Accepted)]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            await _productSkuService.DeleteAsync(id);

            return Accepted();
        }
    }
}

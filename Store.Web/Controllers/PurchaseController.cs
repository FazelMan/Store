using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Store.Data.EntityFrameworkCore.Services;
using Store.Domain.Dto.Purchase;
using Store.Domain.Entity;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Store.Interfaces;

namespace Store.Web.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class PurchaseController : Controller
    {
        private readonly IPurchaseService _purchaseService;
        private readonly IProductSkuService _productSkuService;
        private readonly IMapper _mapper;

        public PurchaseController(IPurchaseService purchaseService,
            IProductSkuService productSkuService,
            IMapper mapper)
        {
            _purchaseService = purchaseService;
            _productSkuService = productSkuService;
            _mapper = mapper;
        }

        /// <summary>
        /// Get list of purchases. 
        /// </summary>
        /// <param name="pageIndex">The requested pageIndex.</param>
        /// <returns>The requested purchase list.</returns>
        /// <response code="200">The purchase list was successfully retrieved.</response>
        [HttpGet("{pageIndex:int}")]
        [ProducesResponseType(typeof(ApiResultList<IEnumerable<PurchaseGet>>), (int)HttpStatusCode.OK)]
        public IActionResult Get(int pageIndex = 1)
        {
            var result = _purchaseService.GetAll(pageIndex);

            return Ok(result);
        }

        /// <summary>
        /// Create a new purchase.
        /// </summary>
        /// <param name="purchaseInsert">The purchase to create.</param>
        /// <response code="202">The purchase was successfully created.</response>
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        public async Task<IActionResult> InsertAsync(PurchaseInsert purchaseInsert)
        {
            //بررسی موجودی انبار
            int productSkuExistCount = await _productSkuService.GetExistCountAsync(purchaseInsert.ProductSkuId);
            if (purchaseInsert.Quantity > productSkuExistCount)
            {
                return BadRequest("INVENTORY_IS_NOT_ENOUGH");
            }
         
            var purchase = new Purchase();
            _mapper.Map(purchaseInsert, purchase);
            await _purchaseService.InsertAsync(purchase);

            //برداشت از موجودی انبار
            var newQuantity = purchaseInsert.Quantity - productSkuExistCount;
            await _productSkuService.UpdateInventoryAsync(purchaseInsert.ProductSkuId, newQuantity);

            return StatusCode((int)HttpStatusCode.Created);
        }

        /// <summary>
        /// Update a purchase.
        /// </summary>
        /// <param name="purchaseUpdate">The purchase to update.</param>
        /// <response code="202">The purchase was successfully updated.</response>
        [HttpPut]
        [ProducesResponseType((int)HttpStatusCode.Accepted)]
        public async Task<IActionResult> UpdateAsync(PurchaseUpdate purchaseUpdate)
        {
            // check stock
            int productSkuExistCount = await _productSkuService.GetExistCountAsync(purchaseUpdate.ProductSkuId);
            if (purchaseUpdate.Quantity > productSkuExistCount)
            {
                return BadRequest("INVENTORY_IS_NOT_ENOUGH");
            }

            var purchase = new Purchase();
            _mapper.Map(purchaseUpdate, purchase);
            await _purchaseService.UpdateAsync(purchase);

            // update stock
            await _productSkuService.UpdateInventoryAsync(purchaseUpdate.ProductSkuId, purchaseUpdate.Quantity);

            return Accepted();
        }

        /// <summary>
        /// Delete a single purchase. 
        /// </summary>
        /// <param name="id">The requested purchase identifier.</param>
        /// <response code="202">The purchase was successfully deleted.</response>
        [HttpDelete("{id:int}")]
        [ProducesResponseType((int)HttpStatusCode.Accepted)]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var purchase =await _purchaseService.GetPurchaseAsync(id);
            await _purchaseService.DeleteAsync(id);

            // update stock
            await _productSkuService.UpdateInventoryAsync(purchase.ProductSkuId, purchase.Quantity);
            return Accepted();
        }
    }
}

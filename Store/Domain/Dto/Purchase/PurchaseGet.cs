using System;

namespace Store.Domain.Dto.Purchase
{
    public class PurchaseGet
    {
        public int Id { get; set; }
        public string ProductSkuId { get; set; }
        public string ProductSkuTitle { get; set; }
        public string ProductTitle { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public DateTime CreatedDate { get; set; } 
    }
}

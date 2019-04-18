using System;

namespace Store.Domain.Dto.Purchase
{
    public class PurchaseGet
    {
        public int Id { get; set; }
        public int ProductTitle { get; set; }
        public int ProductSkuTitle { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public DateTime CreatedDate { get; set; } 
    }
}

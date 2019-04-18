namespace Store.Domain.Dto.Purchase
{
    public class PurchaseInsert 
    {
        public int PurchaseId { get; set; }
        public int ProductSkuId { get; set; }
        public int Quantity { get; set; }
    }
}

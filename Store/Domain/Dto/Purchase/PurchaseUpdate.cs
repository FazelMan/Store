namespace Store.Domain.Dto.Purchase
{
    public class PurchaseUpdate
    {
        public int Id { get; set; }
        public int ProductSkuId { get; set; }
        public int Quantity { get; set; }
    }
}
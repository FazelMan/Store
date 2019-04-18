namespace Store.Domain.Dto.ProductSku
{
    public class ProductSkuInsert 
    {
        public int ProductId { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}

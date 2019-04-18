namespace Store.Domain.Dto.ProductSku
{
    public class ProductSkuGet
    {
        public int Id { get; set; }
        public string ProductTitle { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}

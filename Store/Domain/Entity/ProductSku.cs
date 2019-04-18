using Store.Interfaces;
using System;

namespace Store.Domain.Entity
{
    public class ProductSku : Entity, ISoftDelete
    {
        public int ProductId { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime? UpdatedDate { get; set; }
        public bool IsRemoved { get; set; }

        public Product Product { get; set; }
    }
}

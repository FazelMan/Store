using Store.Interfaces;
using System;
using System.Collections.Generic;

namespace Store.Domain.Entity
{
    public class Product : Entity, ISoftDelete
    {
        public Product()
        {
            ProductSkus = new HashSet<ProductSku>();
        }

        public string Title { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public bool IsRemoved { get; set; }

        public virtual ICollection<ProductSku> ProductSkus { get; set; }
    }
}

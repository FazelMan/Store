using System;
using Store.Interfaces;

namespace Store.Domain.Entity
{
    public class Purchase : Entity, ISoftDelete
    {
        public int ProductSkuId { get; set; }
        public int Quantity { get; set; }
        public bool IsRemoved { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime? UpdatedDate { get; set; }

        public virtual ProductSku ProductSku { get; set; }
    }
}

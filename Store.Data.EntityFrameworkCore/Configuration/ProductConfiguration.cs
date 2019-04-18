using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Store.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Store.Data.EntityFrameworkCore.Configuration
{
    internal class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(x => x.Title).HasMaxLength(200);
        }
    }

    internal class ProductSkuConfiguration : IEntityTypeConfiguration<ProductSku>
    {
        public void Configure(EntityTypeBuilder<ProductSku> builder)
        {
            builder.Property(x => x.Title).HasMaxLength(200);
        }
    }
}

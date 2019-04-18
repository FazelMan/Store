using System;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Remotion.Linq.Parsing.ExpressionVisitors;
using Store.Domain.Entity;
using Store.Interfaces;

namespace Store.Data.EntityFrameworkCore
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                .HasData(new Product()
                {
                    Id = 1,
                    Title = "iPhone 7"
                }, new Product()
                {
                    Id = 2,
                    Title = "Samsong A9"
                });

            modelBuilder.Entity<ProductSku>()
                .HasData(new ProductSku()
                {
                    Id = 1,
                    ProductId = 1,
                    Title = "64G",
                    Price = 899,
                    Quantity = 10
                }, new ProductSku()
                {
                    Id = 2,
                    ProductId = 1,
                    Title = "128G",
                    Price = 990,
                    Quantity = 5
                }, new ProductSku()
                {
                    Id = 3,
                    ProductId = 2,
                    Title = "256G",
                    Price = 750,
                    Quantity = 20
                });
        }

        public static void ApplyQueryFilter(this ModelBuilder modelBuilder)
        {
            foreach (IMutableEntityType mutableEntityType in modelBuilder.Model.GetEntityTypes().Where<IMutableEntityType>((Func<IMutableEntityType, bool>)(e => typeof(ISoftDelete).IsAssignableFrom(e.ClrType))))
                modelBuilder.Entity(mutableEntityType.ClrType).HasQueryFilter(ModelBuilderExtensions.ConvertFilterExpression<ISoftDelete>((Expression<Func<ISoftDelete, bool>>)(e => !e.IsRemoved), mutableEntityType.ClrType));
        }

        private static LambdaExpression ConvertFilterExpression<TInterface>(
            Expression<Func<TInterface, bool>> filterExpression,
            Type entityType)
        {
            ParameterExpression parameterExpression = Expression.Parameter(entityType);
            return Expression.Lambda(ReplacingExpressionVisitor.Replace((Expression)filterExpression.Parameters.Single<ParameterExpression>(), (Expression)parameterExpression, filterExpression.Body), parameterExpression);
        }
    }
}
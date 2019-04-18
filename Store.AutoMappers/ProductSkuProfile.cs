using AutoMapper;
using Store.Domain.Dto;
using Store.Domain.Dto.ProductSku;
using Store.Domain.Entity;

namespace Store.AutoMappers
{
    public class ProductSkuProfile : Profile
    {
        public ProductSkuProfile()
        {
            CreateMap<ProductSku, ProductSkuGet>()
                .ForMember(x => x.ProductTitle, m => m.MapFrom(s => s.Product.Title))
                .ReverseMap();

            CreateMap<ProductSku, ProductSkuInsert>().ReverseMap();
            CreateMap<ProductSku, ProductSkuUpdate>().ReverseMap();
        }
    }
}

using AutoMapper;
using Store.Domain.Dto.Purchase;
using Store.Domain.Entity;

namespace Store.AutoMappers
{
    public class PurchaseProfile : Profile
    {
        public PurchaseProfile()
        {
            CreateMap<Purchase, PurchaseGet>()
              .ForMember(x => x.ProductSkuTitle, m => m.MapFrom(s => s.ProductSku.Title))
              .ForMember(x => x.ProductTitle, m => m.MapFrom(s => s.ProductSku.Product.Title))
              .ReverseMap();

            CreateMap<Purchase, PurchaseInsert>().ReverseMap();
            CreateMap<Purchase, PurchaseUpdate>().ReverseMap();
        }
    }
}

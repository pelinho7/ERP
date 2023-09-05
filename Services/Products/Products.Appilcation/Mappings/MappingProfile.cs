using AutoMapper;
using Products.Appilcation.Features.Products;
using Products.Application.Features.Products.Commands.ArchiveProduct;
using Products.Application.Features.Products.Commands.CreateProduct;
using Products.Application.Features.Products.Commands.UpdateProduct;
//using Products.Application.Features.Orders.Commands.CheckoutOrder;
//using Products.Application.Features.Orders.Commands.UpdateOrder;
//using Products.Application.Features.Orders.Queries.GetOrdersList;
using Products.Domain.Entities;

namespace Products.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, CreateProductCommand>().ReverseMap();
            CreateMap<Product, UpdateProductCommand>().ReverseMap();
            CreateMap<Product, ArchiveProductCommand>().ReverseMap();
            CreateMap<Product, ProductHistory>().ReverseMap();
            CreateMap<Product, ProductHistory>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => 0))
            .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.CreatedModifiedBy, opt => opt.MapFrom(src => src.LastModifiedBy == null ? src.CreatedBy : src.LastModifiedBy))
            .ForMember(dest => dest.CreatedModifiedDate, opt => opt.MapFrom(src => src.LastModifiedDate == null ? src.CreatedDate : src.LastModifiedDate));
            CreateMap<Product, ProductVm>().ReverseMap();
            //CreateMap<Product, UpdateOrderCommand>().ReverseMap();
        }
    }
}

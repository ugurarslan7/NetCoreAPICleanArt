using Application.Features.Products.Create;
using Application.Features.Products.Dto;
using Application.Features.Products.Update;
using AutoMapper;
using Domain.Entities;

namespace Application.Features.Products
{
    public class ProductMappingProfile : Profile
    {
        public ProductMappingProfile()
        {
            CreateMap<Product, ProductDto>().ReverseMap();

            CreateMap<CreateProductRequest, Product>().ForMember(dest => dest.Name,
                opt => opt.MapFrom(p => p.Name.ToLowerInvariant()));

            CreateMap<UpdateProductRequest, Product>().ForMember(dest => dest.Name,
              opt => opt.MapFrom(p => p.Name.ToLowerInvariant()));
        }
    }
}

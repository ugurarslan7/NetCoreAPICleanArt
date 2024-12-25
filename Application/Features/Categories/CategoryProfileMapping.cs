using Application.Features.Categories.Create;
using Application.Features.Categories.Dto;
using AutoMapper;
using Domain.Entities;
using Services.Categories.Create;

namespace Application.Features.Categories
{
    public class CategoryProfileMapping : Profile
    {
        public CategoryProfileMapping()
        {
            CreateMap<Category, CategoryWithProductsDto>().ReverseMap();
            CreateMap<Category, CategoryDto>().ReverseMap();

            CreateMap<CreateCategoryRequest, Category>().ForMember(dest => dest.Name,
                opt => opt.MapFrom(p => p.Name.ToLowerInvariant()));

            CreateMap<UpdateCategoryRequest, Category>().ForMember(dest => dest.Name,
              opt => opt.MapFrom(p => p.Name.ToLowerInvariant()));
        }
    }
}

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
            CreateMap<CategoryDto, Category>().ReverseMap();

            CreateMap<Category, CategoryWithProductsDto>().ReverseMap();

            CreateMap<CreateCategoryRequest, Category>().ForMember(dest => dest.Name,
                opt => opt.MapFrom(src => src.Name.ToLowerInvariant()));

            CreateMap<UpdateCategoryRequest, Category>().ForMember(dest => dest.Name,
                opt => opt.MapFrom(src => src.Name.ToLowerInvariant()));

        }
    }
}

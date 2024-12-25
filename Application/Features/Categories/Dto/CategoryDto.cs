namespace Application.Features.Categories.Dto;

public record CategoryDto(int Id, string Name, List<CategoryWithProductsDto> CategoryWithProductsDtos);

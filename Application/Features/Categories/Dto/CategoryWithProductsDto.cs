﻿using Application.Features.Products.Dto;

namespace Application.Features.Categories.Dto;

public record CategoryWithProductsDto(int Id, string Name, List<ProductDto> Products);
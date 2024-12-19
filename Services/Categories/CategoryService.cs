﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Repositories;
using Repositories.Categories;
using Services.Categories.Create;
using Services.Categories.Dto;
using Services.Products.Update;
using System.Net;

namespace Services.Categories
{
    public class CategoryService(ICategoryRepository categoryRepository, IUnitOfWork unitOfWork, IMapper mapper) : ICategoryService
    {

        public async Task<ServiceResult<CategoryWithProductsDto>> GetCategoryWithProductsAsync(int categoryId)
        {
            var category = categoryRepository.GetCategoriyWithProductAsync(categoryId);
            if (category is null)
            {
                return ServiceResult<CategoryWithProductsDto>.Fail("Category not found", HttpStatusCode.NotFound);
            }

            var categoryAsDto = mapper.Map<CategoryWithProductsDto>(category);

            return ServiceResult<CategoryWithProductsDto>.Success(categoryAsDto);
        }

        public async Task<ServiceResult<List<CategoryWithProductsDto>>> GetCategoryWithProductsAsync()
        {
            var category = categoryRepository.GetCategoriyWithProduct().ToListAsync();

            var categoryAsDto = mapper.Map<List<CategoryWithProductsDto>>(category);

            return ServiceResult<List<CategoryWithProductsDto>>.Success(categoryAsDto);
        }

        public async Task<ServiceResult<List<CategoryDto>>> GetAllListAsync()
        {
            var categories = await categoryRepository.GetAll().ToListAsync();

            var categoryAsDto = mapper.Map<List<CategoryDto>>(categories);

            return ServiceResult<List<CategoryDto>>.Success(categoryAsDto);
        }

        public async Task<ServiceResult<CategoryDto>> GetByIdAsync(int id)
        {
            var category = await categoryRepository.GetByIdAsync(id);

            if (category is null)
            {
                return ServiceResult<CategoryDto>.Fail("Category not found", HttpStatusCode.NotFound);
            }

            var categoryAsDto = mapper.Map<CategoryDto>(category);

            return ServiceResult<CategoryDto>.Success(categoryAsDto);
        }

        public async Task<ServiceResult<int>> CreateAsync(CreateCategoryRequest request)
        {
            var anyCategory = await categoryRepository.Where(p => p.Name == request.Name).AnyAsync();
            if (anyCategory)
            {
                return ServiceResult<int>.Fail("category ismi veritabanında bulunmaktadır.", HttpStatusCode.NotFound);
            }

            var newCategory = mapper.Map<Category>(request);
            await categoryRepository.AddAsync(newCategory);
            await unitOfWork.SaveChangesAsyns();
            return ServiceResult<int>.SuccessAsCreated(newCategory.Id,$"api/categories/{newCategory.Id}");
        }

        public async Task<ServiceResult> UpdateAsync(int id, UpdateCategoryRequest request)
        {
            var isCategorytNameExist = await categoryRepository.Where(p => p.Name == request.Name && p.Id != id).AnyAsync();
            if (isCategorytNameExist)
            {
                return ServiceResult.Fail("ürün ismi veritabanında bulunmaktadır.", HttpStatusCode.BadRequest);
            }

            var category = mapper.Map<Category>(request);
            category.Id = id;

            categoryRepository.Update(category);

            await unitOfWork.SaveChangesAsyns();

            return ServiceResult.Success(HttpStatusCode.NoContent);
        }

        public async Task<ServiceResult> DeleteAsync(int id)
        {
            var category = await categoryRepository.GetByIdAsync(id);

            categoryRepository.Delete(category);

            await unitOfWork.SaveChangesAsyns();

            return ServiceResult.Success(HttpStatusCode.NoContent);
        }

    }
}

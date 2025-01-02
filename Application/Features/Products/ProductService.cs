using Application;
using Application.Contracts.Persistance;
using Application.Features.Products.Create;
using Application.Features.Products.Dto;
using Application.Features.Products.Update;
using Application.ServiceBus;
using AutoMapper;
using Domain.Entities;
using Domain.Events;
using System.Net;

namespace Application.Features.Products
{
    public class ProductService(IProductRepository repository, IUnitOfWork unitOfWork, IMapper mapper,IServiceBus serviceBus) : IProductService
    {

        public async Task<ServiceResult<List<ProductDto>>> GetAllListAsync()
        {
            var products = await repository.GetAllAsync();

            var productAsDto = mapper.Map<List<ProductDto>>(products);

            return ServiceResult<List<ProductDto>>.Success(productAsDto);
        }

        public async Task<ServiceResult<List<ProductDto>>> GetPagedAllListAsync(int pageNumber, int pageSize)
        {
            var products = await repository.GetAllPagedAsync(pageNumber, pageSize);

            var productAsDto = mapper.Map<List<ProductDto>>(products);

            return ServiceResult<List<ProductDto>>.Success(productAsDto);
        }

        public async Task<ServiceResult<List<ProductDto>>> GetTopPriceProductAsync(int count)
        {
            var products = await repository.GetTopPriceProductAsync(count);

            var productAsDto = mapper.Map<List<ProductDto>>(products);

            return new ServiceResult<List<ProductDto>> { Data = productAsDto };
        }

        public async Task<ServiceResult<ProductDto>> GetByIdAsync(int id)
        {
            var product = await repository.GetByIdAsync(id);

            if (product is null)
            {
                return ServiceResult<ProductDto>.Fail("Product not found", HttpStatusCode.NotFound);

            }

            var productAsDto = mapper.Map<ProductDto>(product);

            return ServiceResult<ProductDto>.Success(productAsDto);
        }

        public async Task<ServiceResult<CreateProductResponse>> CreateAsync(CreateProductRequest request)
        {
            var anyProduct = await repository.AnyAsync(p => p.Name == request.Name);
            if (anyProduct)
            {
                return ServiceResult<CreateProductResponse>.Fail("ürün ismi veritabanında bulunmaktadır.", HttpStatusCode.NotFound);
            }


            var product = mapper.Map<Product>(request);

            await repository.AddAsync(product);

            await unitOfWork.SaveChangesAsync();

            await serviceBus.PublishAsync(new ProductAddedEvent(product.Id,product.Name,product.Price));

            return ServiceResult<CreateProductResponse>.SuccessAsCreated(new CreateProductResponse(product.Id), $"api/products/{product.Id}");
        }

        public async Task<ServiceResult> UpdateAsync(int id, UpdateProductRequest request)
        {
            //fast fail
            //guard clauses 

            var isProductNameExist = await repository.AnyAsync(p => p.Name == request.Name && p.Id != id);
            if (isProductNameExist)
            {
                return ServiceResult.Fail("ürün ismi veritabanında bulunmaktadır.", HttpStatusCode.BadRequest);
            }

            var product = mapper.Map<Product>(request);
            product.Id = id;

            repository.Update(product);

            await unitOfWork.SaveChangesAsync();

            return ServiceResult.Success(HttpStatusCode.NoContent);
        }

        public async Task<ServiceResult> UpdateStockAsync(int id, int quantity)
        {
            var product = await repository.GetByIdAsync(id);

            if (product is null)
            {
                return ServiceResult.Fail("Product not found", HttpStatusCode.NotFound);

            }

            product.Stock = quantity;

            repository.Update(product);

            await unitOfWork.SaveChangesAsync();

            return ServiceResult.Success(HttpStatusCode.NoContent);
        }

        public async Task<ServiceResult> DeleteAsync(int id)
        {
            var product = await repository.GetByIdAsync(id);

            repository.Delete(product!);

            await unitOfWork.SaveChangesAsync();

            return ServiceResult.Success(HttpStatusCode.NoContent);
        }


    }


}

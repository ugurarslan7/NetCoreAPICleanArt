using Application.Features.Categories;
using Application.Features.Products;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Application.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection serviceDescriptors, IConfiguration configuration)
        {

            //standart olan entity filtresini kapattık
            serviceDescriptors.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });

            serviceDescriptors.AddScoped<IProductService, ProductService>();
            serviceDescriptors.AddScoped<ICategoryService, CategoryService>();

            serviceDescriptors.AddFluentValidationAutoValidation();

            serviceDescriptors.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            serviceDescriptors.AddAutoMapper(Assembly.GetExecutingAssembly());



            return serviceDescriptors;
        }
    }
}

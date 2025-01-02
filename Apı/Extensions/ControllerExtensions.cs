﻿using Apı.Filters;

namespace Apı.Extensions
{
    public static class ControllerExtensions
    {
        public static IServiceCollection AddControllerWithFiltersExt(this IServiceCollection services)
        {
            services.AddScoped(typeof(NotFoundFilter<,>));
            
            services.AddControllers(options =>
            {
                options.Filters.Add<FluentValidationFilter>();
                options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true;
            });
            return services;
        }
    }
}

﻿using Apı.Filters;

namespace Apı.Extensions
{
    public static class SwaggerExtension
    {
        public static IServiceCollection AddSwagerGenExt(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new() { Title = "NetCoreAPIClean.API", Version = "v1" });
            });
            return services;
        }

        public static IApplicationBuilder UseSwaggerExt(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI();
            return app;
        }
    }
}

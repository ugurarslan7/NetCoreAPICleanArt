using Apı.ExceptionHandlers;

namespace Apı.Extensions
{
    public static class ExceptionHandlerExtensions
    {
        public static IServiceCollection AddExceptionHandlerExtensionsExt(this IServiceCollection services)
        {
            services.AddExceptionHandler<CriticalExceptionHandler>();
            services.AddExceptionHandler<GlobalExceptionHandler>();
            return services;
        }
    }
}

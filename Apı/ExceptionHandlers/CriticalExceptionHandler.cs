using Domain.Exceptions;
using Microsoft.AspNetCore.Diagnostics;

namespace Apı.ExceptionHandlers
{
    public class CriticalExceptionHandler : IExceptionHandler
    {
        public ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            if (exception is CriticalException)
            {
                Console.WriteLine("hata ile ilgili işlem yapıldı");
            }

            return ValueTask.FromResult(false);
        }
    }
}

using Apý.Filters;
using Persistence.Extensions;
using Application.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Apý.ExceptionHandlers;
using Apý.Extensions;
using Bus;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllerWithFiltersExt().AddSwagerGenExt().AddExceptionHandlerExtensionsExt();

builder.Services.AddRepositories(builder.Configuration).AddServices(builder.Configuration).
    AddBusExt(builder.Configuration);

var app = builder.Build();

app.UseExceptionHandler(p => { });

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwaggerExt();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

using Microsoft.Extensions.DependencyInjection;
using PcGear.Core.Services;

namespace PcGear.Core;

public static class DIConfig
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<ProductsService>();
        services.AddScoped<CategoriesService>();
        services.AddScoped<ManufacturersService>();
        services.AddScoped<UsersService>();
        services.AddScoped<ProductReviewsService>();

        return services;
    }
}
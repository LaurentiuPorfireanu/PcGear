using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PcGear.Database.Context;
using PcGear.Database.Repos;


namespace PcGear.Database
{
    public static class DIConfig
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddDbContext<PcGearDatabaseContext>();
            services.AddScoped<DbContext, PcGearDatabaseContext>();
            services.AddScoped<ProductsRepository>();
            services.AddScoped<CategoriesRepository>();
            services.AddScoped<ManufacturersRepository>();
            services.AddScoped<UsersRepository>();
            services.AddScoped<ProductReviewsRepository>();


            return services;
        }
    }
}

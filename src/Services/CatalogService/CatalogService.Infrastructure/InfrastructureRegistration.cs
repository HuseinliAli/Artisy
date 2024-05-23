using CatalogService.Application.Repositories;
using CatalogService.Infrastructure.Contexts;
using CatalogService.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.Infrastructure
{
    public static class InfrastructureRegistration
    {
        public static IServiceCollection AddInfrastructureLayer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<CatalogDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("CatalogDb"));
            });
            services.AddScoped<ICatalogArtistRepository, CatalogArtistRepository>();
            services.AddScoped<ICatalogArtistNationalityRepository, CatalogArtistNationalityRepository>();
            services.AddScoped<ICatalogGenreRepository, CatalogGenreRepository>();
            services.AddScoped<ICatalogColorPaletteRepository, CatalogColorPaletteRepository>();
            services.AddScoped<ICatalogItemCatalogColorPaletteRepository, CatalogItemCatalogColorPaletteRepository>();
            services.AddScoped<ICatalogItemRepository, CatalogItemRepository>();
            services.AddScoped<ICatalogMediumRepository, CatalogMediumRepository>();
            return services;
        }
    }
}

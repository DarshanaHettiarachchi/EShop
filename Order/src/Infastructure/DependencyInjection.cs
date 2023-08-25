using Application.Common.Interfaces;
using Application.Data;
using Domain.Orders;
using Infastructure;
using Infastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Repositories;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,
            IConfiguration configuration
        )
        {
            services.AddHttpClient();

            services.AddSingleton<IServiceBusMessageProcessor, ServiceBusMessageProcessor>();

            services.AddScoped<IOrderedProductService, OrderedProductService>();

            services.AddDbContext<ApplicationDbContext>(options =>
            options
                .UseSqlServer(configuration.GetConnectionString("Database")));

            services.AddScoped<IApplicationDbContext>(sp =>
                sp.GetRequiredService<ApplicationDbContext>());

            services.AddScoped<IUnitOfWork>(sp =>
                sp.GetRequiredService<ApplicationDbContext>());

            services.AddScoped<IOrderRepository, OrderRepository>();

            services.AddScoped<IOrderSummaryRepository, OrderSummariesRepository>();

            return services;
        }
    }


}



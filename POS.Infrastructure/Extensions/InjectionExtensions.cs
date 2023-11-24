using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using POS.Infrastructure.FileExcel;
using POS.Infrastructure.FileStorage;
using POS.Infrastructure.Persistences.Context;
using POS.Infrastructure.Persistences.Interfaces;
using POS.Infrastructure.Persistences.Repositories;

namespace POS.Infrastructure.Extensions
{
    public static class InjectionExtensions
    {
        public static IServiceCollection AddInjectionInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var assembly = typeof(POSContext).Assembly.FullName;

            services.AddDbContext<POSContext>(
                options => options.UseSqlServer(
                                                    configuration.GetConnectionString("POSConecction"), b => b.MigrationsAssembly(assembly)
                                               ), ServiceLifetime.Transient
                                             );

            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IAzureStorage, AzureStorage>();
            services.AddTransient<IGenerateExcel, GenerateExcel>();

            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            return services;
        }
    }
}

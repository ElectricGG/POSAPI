using FluentValidation.AspNetCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using POS.Application.Commons.Ordering;
using POS.Application.Extensions.WatchDog;
using POS.Application.Interfaces;
using POS.Application.Services;
using System.Reflection;

namespace POS.Application.Extensions
{
    public static class InjectionExtensions
    {
        
        public static IServiceCollection AddInjectionApplication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton(configuration);

#pragma warning disable CS0618 // El tipo o el miembro están obsoletos
            services.AddFluentValidation(options =>
            {
                options.RegisterValidatorsFromAssemblies(AppDomain.CurrentDomain.GetAssemblies().Where(p => !p.IsDynamic));
            });
#pragma warning restore CS0618 // El tipo o el miembro están obsoletos

            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            services.AddTransient<IOrderingQuery,OrderingQuery>();

            services.AddScoped<ICategoryApplication, CategoryApplication>();
            services.AddScoped<IUserApplication, UserApplication>();
            services.AddScoped<IProviderApplication, ProviderApplication>();
            services.AddScoped<IDocumentTypeApplication, DocumentTypeApplication>();
            services.AddScoped<IGenerateExcelApplication, GenerateExcelApplication>();

            services.AddWAtchDog(configuration);

            return services;
        }
    }
}

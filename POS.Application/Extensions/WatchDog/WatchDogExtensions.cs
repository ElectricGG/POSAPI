using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WatchDog;
using WatchDog.src.Enums;

namespace POS.Application.Extensions.WatchDog
{
    public static class WatchDogExtensions
    {
        public static IServiceCollection AddWAtchDog(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddWatchDogServices(options =>
            {
                //para registrar en base de datos
                options.SetExternalDbConnString = configuration.GetConnectionString("POSConecction");
                options.DbDriverOption = WatchDogDbDriverEnum.MSSQL;
                //-------------------------------

                options.IsAutoClear = true;
                options.ClearTimeSchedule = WatchDogAutoClearScheduleEnum.Daily;
            });

            return services;
        }
    }
}

using Microsoft.Extensions.DependencyInjection;
using RuyaTabircim.Data.Repository;
using RuyaTabircim.Data.Repository.Interface;
using RuyaTabircim.Data.Service;
using RuyaTabircim.Data.Service.Interface;

namespace RuyaTabircim.Api
{
    public static class ServiceExtensions
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services, AppSettings appSettings)
        {
            // Allow Cors
            services.AddCors();

            var data = appSettings.ConnectionString;

            services.AddTransient<ISpiritRepository>(i => new SpiritRepository(data));
            services.AddTransient<IOracleRepository>(i => new OracleRepository(data));
            services.AddTransient<IDreamRepository>(i => new DreamRepository(data));
            services.AddTransient<ISentenceRepository>(i => new SentenceRepository(data));

            services.AddSingleton<ISpiritService, SpiritService>();
            services.AddSingleton<IOracleService, OracleService>();
            services.AddSingleton<IDreamService, DreamService>();
            services.AddSingleton<ISentenceService, SentenceService>();

            return services;
        }
    }
}

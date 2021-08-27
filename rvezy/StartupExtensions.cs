using System.Linq;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using rvezy.Core.Logger;
using rvezy.Core.Mappers;
using rvezy.Data;
using rvezy.Data.Repositories;
using rvezy.Extensions;
using rvezy.Services;
using Serilog;
using Serilog.Events;
using ILogger = rvezy.Core.Logger.ILogger;

namespace rvezy
{
    public static class StartupExtensions
    {
        public static void ConfigureLogs(this IServiceCollection services, IConfiguration configuration,
            IHostEnvironment environment)
        {
            var minLevel = LogEventLevel.Debug;
            var loggerConfiguration = new LoggerConfiguration()
                .MinimumLevel.Is(minLevel)
                .MinimumLevel.Override("Microsoft", LogEventLevel.Error)
                .Enrich.FromLogContext()
                //Sinks configuration
                .WriteTo.File("rvezy.log", rollingInterval: RollingInterval.Infinite);

            if (environment.IsDevelopment()) loggerConfiguration.WriteTo.Console();

            Log.Logger = loggerConfiguration.CreateLogger();
            Log.Information("Logs configured");
        }

        public static void ConfigureDataContext(this IServiceCollection services, IConfiguration configuration,
            IHostEnvironment environment)
        {
            services.AddDbContext<DataContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection")));
        }

        public static void ConfigureDependencies(this IServiceCollection services, IConfiguration configuration,
            IHostEnvironment environment)
        {
            services.TryAddSingleton(configuration);
            services.TryAddSingleton<ILoggerFactory, LoggerFactory>();
            services.TryAddSingleton<ILogger, Logger>();

           // services.TryAddScoped<IRestClient, RestClient>();

            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

            services.AddEntityRepositories(typeof(ListingRepository).Assembly);

            services.TryAddScoped<ICsvProvider, CsvProvider>();

            services.TryAddScoped<IListingService, ListingService>();
            services.TryAddScoped<ICalendarService, CalendarService>();
            services.TryAddScoped<IReviewService, ReviewService>();
            //
//            services.AddTransient<IEmailSender, EmailSender>();
//            services.AddTransient<ISmsSender, SmsSender>();
        }

        public static void ConfigureCache(this IServiceCollection services, IConfiguration configuration,
            IHostEnvironment environment)
        {
            var hostname = configuration.GetValue<string>("Redis:Hostname");
            var instanceName = configuration.GetValue<string>("Redis:InstanceName");

            if (hostname.IsNullOrWhiteSpace() || instanceName.IsNullOrWhiteSpace())
            {
                services.AddMemoryCache();
            }
            else
            {
                services.AddStackExchangeRedisCache(o =>
                {
                    o.Configuration = hostname;
                    o.InstanceName = instanceName;
                });
            }
        }

        public static void RegisterAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(DefaultProfile).Assembly);
        }

        private static void AddEntityRepositories(this IServiceCollection services, Assembly assembly)
        {
            var genericType = typeof(IRepository<>);
            var implementationTypes = assembly.GetTypes()
                .Where(c => c.IsClass && !c.IsNested && c.IsPublic && !c.IsAbstract
                            && c.Name.EndsWith("Repository"))
                .ToList();

            foreach (var implementationType in implementationTypes)
            {
                var interfaceType = implementationType.GetInterface(genericType.Name);
                services.TryAddScoped(interfaceType, implementationType);
            }
        }
    }
}
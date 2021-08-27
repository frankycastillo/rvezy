using System;
using System.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using rvezy.Core;
using ILogger = rvezy.Core.Logger.ILogger;

namespace rvezy.Services
{
    public abstract class BaseService : DisposibleService
    {
        protected readonly Stopwatch Stopwatch;
        protected readonly IConfiguration Configuration;
        protected readonly ILogger Logger;
        protected readonly IHttpContextAccessor ContextAccessor;

        protected BaseService(IConfiguration configuration, ILogger logger, IHttpContextAccessor contextAccessor)
        {
            Logger = logger;
            Configuration = configuration;
            ContextAccessor = contextAccessor;

            Stopwatch = new Stopwatch();
        }

        // protected IIdentity GetCurrentUser() => ContextAccessor.HttpContext?.User?.Identity;

        protected DateTimeOffset GetNow()
        {
            return DateTimeOffset.UtcNow;
        }

//        protected Instant getUtcNow()
//        {
//            return new Instant();
//        }
//
//        protected ZonedDateTime getInstantInZone(DateTimeZone zone)
//        {
//            return getUtcNow().InZone(zone);
//        }
//
//        protected ZonedDateTime getZonedDateTimeSpain()
//        {
//            var zone = DateTimeZoneProviders.Tzdb.GetZoneOrNull("Europe/Madrid");
//            return getUtcNow().InZone(zone);
//        }
//
//        protected DateTimeOffset getDateTimeOffsetSpain()
//        {
//            return getZonedDateTimeSpain().ToDateTimeOffset();
//        }
    }
}
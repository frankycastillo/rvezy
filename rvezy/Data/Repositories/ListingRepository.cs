using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Distributed;
using rvezy.Core.Logger;
using rvezy.Models;

namespace rvezy.Data.Repositories
{
    public class ListingRepository : Repository<Listing>
    {
        public ListingRepository(DataContext context, IHttpContextAccessor contextAccessor, ILogger logger, IDistributedCache cache) :
            base(context, contextAccessor, logger, cache)
        {
        }
    }
}
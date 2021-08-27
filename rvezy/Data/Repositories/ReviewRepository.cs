using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Distributed;
using rvezy.Core.Logger;
using rvezy.Models;

namespace rvezy.Data.Repositories
{
    public class ReviewRepository : Repository<Review>
    {
        public ReviewRepository(DataContext context, IHttpContextAccessor contextAccessor, ILogger logger, IDistributedCache cache) :
            base(context, contextAccessor, logger, cache)
        {
        }
    }
}
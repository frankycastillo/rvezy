using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using rvezy.Models;
using rvezy.Services;

namespace rvezy.Web
{
    [Route("api/[controller]")]
    [ApiController]
    public class ListingController : ControllerBase
    {
        private readonly IListingService _listingService;

        public ListingController(IListingService listingService)
        {
            _listingService = listingService;
        }


        [Route("")]
        [HttpGet]
        public async Task<IEnumerable<Listing>> GetListings()
        {
           // var paginator = Paginator.Small();
            var items = await _listingService.GetAllListings().ConfigureAwait(false);

            return items;
        }

    }
}

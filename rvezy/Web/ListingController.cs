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
    [Route("api/listing")]
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
        public async Task<IEnumerable<Listing>> GetListings(int page, int size)
        {
            var paginator = new Paginator {Page = page, Size = size};
            var items = await _listingService.GetAllListings(paginator).ConfigureAwait(false);

            return items;
        }

        [Route("{id}")]
        [HttpGet]
        public async Task<Listing> GetListingById(int id)
        {
            var item = await _listingService.GetListingById(id).ConfigureAwait(false);

            return item;
        }

        [Route("/filter/property_type/{propertyType}")]
        [HttpGet]
        public async Task<IEnumerable<Listing>> GetListingByPropertyType(string propertyType)
        {
            var items = await _listingService.GetListingByPropertyType(propertyType).ConfigureAwait(false);

            return items;
        }


        [Route("")]
        [HttpPost]
        public async Task<Listing> AddListing(Listing model)
        { 
            await _listingService.AddListing(model);

            return model;
        }

        [Route("")]
        [HttpPut]
        public async Task<Listing> EditListing(Listing model)
        {
            var updated = await _listingService.EditListing(model.id, model);

            return updated;
        }

        [Route("")]
        [HttpDelete]
        public async Task<IActionResult> DeleteListingById(int id)
        {
            var current = await _listingService.GetListingById(id).ConfigureAwait(false);
            await _listingService.Delete(current);

            return Ok();
        }

    }
}
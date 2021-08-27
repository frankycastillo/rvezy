using System;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
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
            var items = await _listingService.GetAllListingsCsv(paginator).ConfigureAwait(false);

            return items;
        }

        [Route("{id}")]
        [HttpGet]
        public async Task<Listing> GetListingById(Guid id)
        {
            var item = await _listingService.GetListingCsvById(id).ConfigureAwait(false);

            return item;
        }

        [Route("/filter/property_type/{propertyType}")]
        [HttpGet]
        public async Task<IEnumerable<Listing>> GetListingByPropertyType(string propertyType)
        {
            var items = await _listingService.GetListingCsvByPropertyType(propertyType).ConfigureAwait(false);

            return items;
        }


        [Route("")]
        [HttpPost]
        public async Task<Listing> AddListing(Listing model)
        { 
            await _listingService.Add(model);

            return model;
        }

        [Route("")]
        [HttpPut]
        public async Task<Listing> EditListing(Listing model)
        {
            var updated = await _listingService.Edit(model.Id, model);

            return updated;
        }

        [Route("")]
        [HttpDelete]
        public async Task<IActionResult> DeleteListingById(Guid id)
        {
            var current = await _listingService.GetListingCsvById(id).ConfigureAwait(false);
            await _listingService.Delete(current);

            return Ok();
        }

    }
}
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CsvHelper;
using CsvHelper.Configuration;
using rvezy.Models;

namespace rvezy.Services
{
    public interface IListingService
    {
        Task<IEnumerable<Listing>> GetAllListings(IPaginator paginator);
        Task<Listing> GetListingById(int id);
        Task<IEnumerable<Listing>> GetListingByPropertyType(string propertyType);
    }
    
    public class ListingService : IListingService
    {
        private readonly ICsvProvider _csvProvider;

        public ListingService(ICsvProvider csvProvider)
        {
            _csvProvider = csvProvider;
        }

        public async Task<IEnumerable<Listing>> GetAllListings(IPaginator paginator)
        {
            var records = _csvProvider.GetListingsFromFile();
            
            return paginator != null 
                ? records.Skip((paginator.Page - 1) * paginator.Size).Take(paginator.Size) 
                : records.ToList();
        }

        public async Task<Listing> GetListingById(int id)
        {
            var records = _csvProvider.GetListingsFromFile();
            var found = records.AsQueryable().FirstOrDefault(x => x.id == id);

            return found;
        }

        public async Task<IEnumerable<Listing>> GetListingByPropertyType(string propertyType)
        {
            var records = _csvProvider.GetListingsFromFile();
            var found = records.AsQueryable().Where(x => x.property_type == propertyType);

            return found;
        }
    }
}

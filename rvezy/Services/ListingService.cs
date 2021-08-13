using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CsvHelper;
using rvezy.Models;

namespace rvezy.Services
{
    public interface IListingService
    {
        Task<IEnumerable<Listing>> GetAllListings();

    }
    
    public class ListingService : IListingService
    {
        public async Task<IEnumerable<Listing>> GetAllListings()
        {
            IEnumerable<Listing> records;

            using (var reader = new StreamReader(@"Csv/listings.csv"))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                records = csv.GetRecords<Listing>().ToList();
            }

            return records;
        }
    }
}

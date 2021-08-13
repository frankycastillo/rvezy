using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using CsvHelper;
using CsvHelper.Configuration;
using rvezy.Models;

namespace rvezy.Services
{
    public interface ICsvProvider
    {
        IEnumerable<Listing> GetListingsFromFile();
    }
    
    public class CsvProvider : ICsvProvider
    {
        private IEnumerable<Listing> _records;
        
        public IEnumerable<Listing> GetListingsFromFile() 
        {
            if (_records == null || !_records.Any())
            {
                var config = new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    HasHeaderRecord = true,
                    BadDataFound = null,
                    MissingFieldFound = null
                };

                var reader = new StreamReader(@"Csv/listings.csv");
                var csv = new CsvReader(reader, config);

                _records = csv.GetRecords<Listing>();
            }

            return _records;
        }
    }
}
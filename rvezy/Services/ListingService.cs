using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CsvHelper;
using CsvHelper.Configuration;
using rvezy.Data;
using rvezy.Models;

namespace rvezy.Services
{
    public interface IListingService
    {
        Task<IEnumerable<Listing>> GetAllListings(IPaginator paginator);
        Task<Listing> GetListingById(int id);
        Task<IEnumerable<Listing>> GetListingByPropertyType(string propertyType);

        Task<Listing> AddListing(Listing model);

        Task<Listing> EditListing(int id, Listing model);

        Task  Delete(Listing model);

    }
    
    public class ListingService : IListingService
    {
        private readonly ICsvProvider _csvProvider;
        private readonly DataContext _dataContext;

        public ListingService(ICsvProvider csvProvider, DataContext dataContext)
        {
            _csvProvider = csvProvider;
            _dataContext = dataContext;
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

        public async Task<Listing> AddListing(Listing model)
        {
            _dataContext.Set<Listing>().Add(model);

            return model;
        }

        public async Task<Listing> EditListing(int id, Listing model)
        {
            var current = await _dataContext.Set<Listing>().FindAsync(id).ConfigureAwait(false);
            if (current != null)
            {
                _dataContext.Entry(current).CurrentValues.SetValues(model);
                await _dataContext.SaveChangesAsync().ConfigureAwait(false);
            }
           
            return current;
        }

        public async Task Delete(Listing model)
        {
            _dataContext.Set<Listing>().Remove(model);
            _dataContext.SaveChanges();
        }
    }
}

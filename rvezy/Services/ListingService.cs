using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using rvezy.Core.Logger;
using rvezy.Data;
using rvezy.Models;

namespace rvezy.Services
{
    public interface IListingService
    {
        Task<IEnumerable<Listing>> GetAllListingsCsv(IPaginator paginator);
        Task<Listing> GetListingCsvById(Guid id);
        Task<IEnumerable<Listing>> GetListingCsvByPropertyType(string propertyType);

        Task<IEnumerable<Listing>> GetAll();

        Task<IEnumerable<Listing>> GetAll(IPaginator paginator);

        Task<Listing> GetById(Guid id);

        Task<Listing> Add(Listing model);

        Task<Listing> Edit(Guid id, Listing model);

        Task Delete(Listing model);
    }
    
    public class ListingService : BaseService, IListingService
    {
        private readonly ICsvProvider _csvProvider;
        private readonly IRepository<Listing> _repository;

        public ListingService(IConfiguration configuration, ILogger logger, IHttpContextAccessor contextAccessor,
            ICsvProvider csvProvider, IRepository<Listing> repository) : base(configuration, logger, contextAccessor)
        {
            _csvProvider = csvProvider;
            _repository = repository;
        }

        #region CSV
        public async Task<IEnumerable<Listing>> GetAllListingsCsv(IPaginator paginator)
        {
            var records = _csvProvider.GetListingsFromFile();

            return paginator != null
                ? records.Skip((paginator.Page - 1) * paginator.Size).Take(paginator.Size)
                : records.ToList();
        }

        public async Task<Listing> GetListingCsvById(Guid id)
        {
            var records = _csvProvider.GetListingsFromFile();
            var found = records.AsQueryable().FirstOrDefault(x => x.Id == id);

            return found;
        }

        public async Task<IEnumerable<Listing>> GetListingCsvByPropertyType(string propertyType)
        {
            var records = _csvProvider.GetListingsFromFile();
            var found = records.AsQueryable().Where(x => x.property_type == propertyType);

            return found;
        }

        public async Task<IEnumerable<Listing>> GetAll()
        {
            return await _repository.GetAll().ConfigureAwait(false);
        }

        public async Task<IEnumerable<Listing>> GetAll(IPaginator paginator)
        {
            return await _repository.GetAll(paginator).ConfigureAwait(false);
        }

        public async Task<Listing> GetById(Guid id)
        {
            return await _repository.Get(id).ConfigureAwait(false);
        }

        public async Task<Listing> Add(Listing model)
        {
            return await _repository.Add(model).ConfigureAwait(false);
        }

        public async Task<Listing> Edit(Guid id, Listing model)
        {
            return await _repository.Update(model.Id, model).ConfigureAwait(false);
        }

        #endregion
        
        public async Task<Listing> AddListing(Listing model)
        {
            return await _repository.Add(model).ConfigureAwait(false);
        }

        public async Task<Listing> EditListing(Guid id, Listing model)
        {
            return await _repository.Update(model.Id, model).ConfigureAwait(false);
        }

        public async Task Delete(Listing model)
        {
            await _repository.SoftDelete(model).ConfigureAwait(false);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using rvezy.Core.Logger;
using rvezy.Data;
using rvezy.Models;

namespace rvezy.Services
{
    public interface IReviewService
    {
        Task<IEnumerable<Review>> GetAll();

        Task<IEnumerable<Review>> GetAll(IPaginator paginator);

        Task<Review> GetById(Guid id);

        Task<Review> Add(Review model);

        Task<Review> Edit(Guid id, Review model);

        Task Delete(Review model);
    }

    public class ReviewService : BaseService, IReviewService
    {
        private readonly IRepository<Review> _repository;

        public ReviewService(IConfiguration configuration, ILogger logger, IHttpContextAccessor contextAccessor,
            IRepository<Review> repository) : base(configuration, logger, contextAccessor)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Review>> GetAll()
        {
            return await _repository.GetAll().ConfigureAwait(false);
        }

        public async Task<IEnumerable<Review>> GetAll(IPaginator paginator)
        {
            return await _repository.GetAll(paginator).ConfigureAwait(false);
        }

        public async Task<Review> GetById(Guid id)
        {
            return await _repository.Get(id).ConfigureAwait(false);
        }

        public async Task<Review> Add(Review model)
        {
            return await _repository.Add(model).ConfigureAwait(false);
        }

        public async Task<Review> Edit(Guid id, Review model)
        {
            return await _repository.Update(model.Id, model).ConfigureAwait(false);
        }

        public async Task Delete(Review model)
        {
            await _repository.SoftDelete(model).ConfigureAwait(false);
        }
    }
}
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
    public interface ICalendarService
    {
        Task<IEnumerable<Calendar>> GetAll();

        Task<IEnumerable<Calendar>> GetAll(IPaginator paginator);

        Task<Calendar> GetById(Guid id);

        Task<Calendar> Add(Calendar model);

        Task<Calendar> Edit(Guid id, Calendar model);

        Task Delete(Calendar model);
    }

    public class CalendarService : BaseService, ICalendarService
    {
        private readonly IRepository<Calendar> _repository;

        public CalendarService(IConfiguration configuration, ILogger logger, IHttpContextAccessor contextAccessor,
            IRepository<Calendar> repository) : base(configuration, logger, contextAccessor)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Calendar>> GetAll()
        {
            return await _repository.GetAll().ConfigureAwait(false);
        }

        public async Task<IEnumerable<Calendar>> GetAll(IPaginator paginator)
        {
            return await _repository.GetAll(paginator).ConfigureAwait(false);
        }

        public async Task<Calendar> GetById(Guid id)
        {
            return await _repository.Get(id).ConfigureAwait(false);
        }

        public async Task<Calendar> Add(Calendar model)
        {
            return await _repository.Add(model).ConfigureAwait(false);
        }

        public async Task<Calendar> Edit(Guid id, Calendar model)
        {
            return await _repository.Update(model.Id, model).ConfigureAwait(false);
        }

        public async Task Delete(Calendar model)
        {
            await _repository.SoftDelete(model).ConfigureAwait(false);
        }
    }
}
using System;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using rvezy.Models;
using rvezy.Services;

namespace rvezy.Web
{
    [Route("api/calendar")]
    [ApiController]
    public class CalendarController : ControllerBase
    {
        private readonly ICalendarService _service;

        public CalendarController(ICalendarService service)
        {
            _service = service;
        }

        [Route("")]
        [HttpGet]
        public async Task<IEnumerable<Calendar>> GetCalendars(int page, int size)
        {
            var paginator = new Paginator {Page = page, Size = size};
            var items = await _service.GetAll(paginator).ConfigureAwait(false);

            return items;
        }

        [Route("{id}")]
        [HttpGet]
        public async Task<Calendar> GetById(Guid id)
        {
            var item = await _service.GetById(id).ConfigureAwait(false);

            return item;
        }


        [Route("")]
        [HttpPost]
        public async Task<Calendar> Create(Calendar model)
        { 
            await _service.Add(model);

            return model;
        }

        [Route("")]
        [HttpPut]
        public async Task<Calendar> Update(Calendar model)
        {
            var updated = await _service.Edit(model.Id, model);

            return updated;
        }

        [Route("")]
        [HttpDelete]
        public async Task<IActionResult> Delete(Guid id)
        {
            var current = await _service.GetById(id).ConfigureAwait(false);
            await _service.Delete(current);

            return Ok();
        }

    }
}
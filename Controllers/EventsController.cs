using System;
using System.Threading.Tasks;
using Evento.Infrastructure.Commands.Events;
using Evento.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace Evento.Api.Controllers
{
    public class EventsController : ApiControllerBase
    {
        private readonly IEventService _service;
        public EventsController(IEventService service)
        {
            _service = service;
        }
        [HttpGet]
        public async Task<IActionResult> Get(string name)
        {
            var events = await _service.BrowseAsync(name);
            return Json(events);
        }

        [HttpGet("{eventId}")]
        public async Task<IActionResult> Get(Guid eventId)
        {
            var events = await _service.GetAsync(eventId);
            if (events == null)
                return NotFound();
            return Json(events);
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]CreateEvents command)
        {
            command.EventId = Guid.NewGuid();
            await _service.CreateAsync(command.EventId, command.Name, command.Description, command.StartDate, command.EndDate);
            await _service.AddTicketdsAsync(command.EventId, command.Tickets, command.Price);

            return Created($"/events/{command.EventId}", null);
        }

        [HttpPut("{eventId}")]
        public async Task<IActionResult> Put(Guid eventId, [FromBody]UpdateEvent command)
        {
            await _service.UpdateAsync(eventId, command.Name, command.Description);
            return NoContent();
        }

        [HttpDelete("{eventId}")]
        public async Task<IActionResult> Delete(Guid eventId)
        {
            await _service.DelteAsync(eventId);
            return NoContent();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Evento.Core.Domain;
using Evento.Core.Repositories;
using Evento.Infrastructure.DTO;
using Evento.Infrastructure.Extensions;
using Evento.Infrastructure.Repositories;

namespace Evento.Infrastructure.Services
{
    public class EventService : IEventService
    {
        public readonly IEventRepository _eventRepository;
        public readonly IMapper _mapper;
        public EventService(IEventRepository eventRepository, IMapper mapper)
        {
            _eventRepository = eventRepository;
            _mapper = mapper;
        }
        public async Task<EventDetailsDto> GetAsync(Guid id)
        {
            var @event = await _eventRepository.GetAsync(id);
            return _mapper.Map<EventDetailsDto>(@event);
        }

        public async Task<EventDetailsDto> GetAsync(string name)
        {
            var @event = await _eventRepository.GetAsync(name);
            return _mapper.Map<EventDetailsDto>(@event);
        }

        public async Task<IEnumerable<EventDto>> BrowseAsync(string name = "")
        {
            var events = await _eventRepository.BrowseAsync(name);
            return _mapper.Map<IEnumerable<EventDto>>(events);

        }

        public async Task AddTicketdsAsync(Guid eventId, int amount, decimal price)
        {
            var @event = await _eventRepository.GetOrFailAnsyc(eventId);
            @event.AddTickets(amount, price);
            await _eventRepository.UpdateAsync(@event);
        }

        public async Task CreateAsync(Guid id, string name, string description, DateTime startDate, DateTime endDate)
        {
            var @event = await _eventRepository.GetAsync(name);
            if (@event != null)
            {
                throw new Exception($"Event name: '{name}' aleardy exists");
            }
            @event = new Event(id, name, description, startDate, endDate);
            await _eventRepository.AddAsync(@event);
        }

        public async Task DelteAsync(Guid id)
        {
            var @event = await _eventRepository.GetOrFailAnsyc(id);
            await _eventRepository.DeleteAsync(@event);
        }

        public async Task UpdateAsync(Guid id, string name, string description)
        {

            var @event = await _eventRepository.GetAsync(name);
            if (@event != null)
            {
                throw new Exception($"Event name: '{id}' aleardy exists");
            }
            @event = await _eventRepository.GetAsync(id);
            @event.SetDescription(description);
            @event.SetName(name);
            await _eventRepository.UpdateAsync(@event);
        }

    }
}
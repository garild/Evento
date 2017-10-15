using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Evento.Core.Domain;
using Evento.Infrastructure.DTO;

namespace Evento.Infrastructure.Services
{
    public interface IEventService
    {
        Task<EventDetailsDto> GetAsync(Guid id);
        Task<EventDetailsDto> GetAsync(string name);
        Task<IEnumerable<EventDto>> BrowseAsync(string name = "");
        Task CreateAsync(Guid id, string name, string description, DateTime startDate, DateTime endDate);
        Task UpdateAsync(Guid id, string name, string description);
        Task AddTicketdsAsync(Guid eventId, int amount, decimal price);
        Task DelteAsync(Guid id);
    }
}
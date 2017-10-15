using System;
using System.Collections.Generic;
using System.Linq;

namespace Evento.Core.Domain
{
    public class Event : Entity
    {
        public ISet<Ticket> _ticets = new HashSet<Ticket>();
        public string Name { get; protected set; }
        public string Description { get; protected set; }
        public DateTime CreatedAd { get; protected set; }
        public DateTime StartDate { get; protected set; }
        public DateTime EndDate { get; protected set; }
        public DateTime UpdateDate { get; protected set; }

        public IEnumerable<Ticket> Tickets => _ticets;

        public IEnumerable<Ticket> PurchasedTickets => Tickets.Where(p=>p.Purchased);

        public IEnumerable<Ticket> AvaibleTickets => Tickets.Except(PurchasedTickets);

        protected Event()
        {

        }
        public Event(Guid id, string name, string description, DateTime startDate, DateTime endDate)
        {
            Id = id;
            SetName(name);
            SetDescription(description);
            StartDate = startDate;
            EndDate = endDate;
            CreatedAd = DateTime.UtcNow;

        }
        public void AddTickets(int amount, decimal price)
        {
            var seating = _ticets.Count + 1;
            for (var i = 0; i < amount; i++)
            {
                _ticets.Add(new Ticket(this, seating, price));
            }
        }

        public void SetName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new Exception($"Event with Id :  {Id} cannot have empty name");
            Name = name;
            UpdateDate = DateTime.UtcNow;
        }

        public void SetDescription(string description)
        {
            if (string.IsNullOrWhiteSpace(description))
                throw new Exception($"Event with Id :  {Id} cannot have empty description");
            Description = description;
            UpdateDate = DateTime.UtcNow;
        }


    }
}
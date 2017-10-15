using System;
using System.Threading.Tasks;
using Evento.Core.Domain;
using Evento.Core.Repositories;
using Evento.Infrastructure.Repositories;

namespace Evento.Infrastructure.Extensions
{
    public static class RepositoryExtensions
    {
        public static async Task<Event> GetOrFailAnsyc(this IEventRepository repository, Guid id)
        {
            var @event = await repository.GetAsync(id);
            if (@event == null)
            {
                throw new Exception($"Event with Id '{id}' aleardy exists");
            }
            return @event;
        }

         public static async Task<User> GetOrFailAnsyc(this IUserRepository repository, Guid id)
        {
            var user = await repository.GetAsync(id);
            if (user == null)
            {
                throw new Exception($"User with Id '{id}' aleardy exists");
            }
            return user;
        }
    }
}
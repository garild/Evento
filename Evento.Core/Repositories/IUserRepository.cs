using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Evento.Core.Domain;

namespace Evento.Core.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetAsync(Guid Id);
        Task<User> GetAsync(string email);
        Task AddAsync(User @user);
        Task UpdateAsync(User @user);
        Task DeleteAsync(User @user);
    }
}
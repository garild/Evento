using System;
using System.Collections.Generic;
using Evento.Core.Repositories;
using Evento.Infrastructure.Repositories;
using Evento.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Evento.Infrastructure.Settings;

namespace Evento.Infrastructure.DI
{
    public static class RepositoryServices
    {
        public static IServiceCollection  AddInternalServices(this IServiceCollection services)
        {
            services.AddScoped<IEventRepository, EventRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IEventService, EventService>();
            services.AddScoped<IUserService, UserService>();
            services.AddSingleton<IJwtHandler,JwtHandler>();
           
            return services;
        }
    }
}
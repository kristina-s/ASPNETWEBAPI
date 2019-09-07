using DataAccess;
using DataModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Helpers
{
    public static class DiModule
    {
        public static IServiceCollection RegisterModule(
            IServiceCollection services, string connectionString)
        {
            services.AddTransient<IRepository<User>, UserRepository>();
            services.AddTransient<IRepository<Ticket>, TicketRepository>();
            services.AddTransient<IRepository<Session>, SessionRepository>();
            services.AddTransient<IRepository<Winner>, WinnerRepository>();
            services.AddDbContext<LottoDbContext>(x =>
            x.UseSqlServer(connectionString));

            return services;
        }
    }
}

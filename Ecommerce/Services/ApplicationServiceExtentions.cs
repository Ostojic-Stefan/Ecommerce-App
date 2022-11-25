using Ecommerce.Data;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

namespace Ecommerce.Services
{
    public static class ApplicationServiceExtentions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection service, IConfiguration config)
        {
            service.AddDbContext<DataContext>(opt =>
            {
                var connString = config.GetConnectionString("DefaultConnection");
                opt.UseSqlite(connString);
            });


            service.AddAutoMapper(typeof(AutoMapperProfiles).Assembly);
            return service;
        }
    }
}

using Ecommerce.Data;
using Microsoft.EntityFrameworkCore;
using Ecommerce.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

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

            service.AddIdentityCore<AppUser>(options =>
            {
                options.Password.RequireNonAlphanumeric = false;
            })
            .AddEntityFrameworkStores<DataContext>()
            .AddSignInManager<SignInManager<AppUser>>();

            service.AddScoped<TokenService>();

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["TokenKey"]));

            service.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(opt =>
                {
                    opt.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = key,
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });

            service.AddAutoMapper(typeof(AutoMapperProfiles).Assembly);

            return service;
        }
    }
}

using Ecommerce.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddApplicationServices(builder.Configuration);

builder.Services.AddControllers(opt =>
{
    //var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
    //opt.Filters.Add(new AuthorizeFilter(policy));
});

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();

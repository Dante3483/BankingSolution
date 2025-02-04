using BankingSolution.Domain.Repositories;
using BankingSolution.Infrastructure.Extensions;
using BankingSolution.Infrastructure.Repositories;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddBankingDbContext(
    $"Data Source={Path.Combine("..", "BankingSolution.Infrastructure", "Bank.db")}"
);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc(
        "v1",
        new OpenApiInfo
        {
            Version = "v1",
            Title = "Account API",
            Description = "An ASP.NET Core Web API for managing Account items",
        }
    );
});

builder.Services.AddScoped<IAccountRepository, AccountRepository>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();

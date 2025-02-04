using BankingSolution.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace BankingSolution.Infrastructure.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddBankingDbContext(
            this IServiceCollection services,
            string connectionString
        )
        {
            return services.AddDbContext<BankingDbContext>(options =>
                options.UseSqlite(connectionString)
            );
        }
    }
}

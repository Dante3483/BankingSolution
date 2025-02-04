using BankingSolution.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace BankingSolution.Infrastructure.Extensions
{
    /// <summary>
    /// Provides extension methods for registering services in the dependency injection container.
    /// </summary>
    public static class ServiceCollectionExtension
    {
        /// <summary>
        /// Adds the <see cref="BankingDbContext"/> to the service collection with the specified connection string.
        /// </summary>
        /// <param name="services">The service collection to add the context to.</param>
        /// <param name="connectionString">The connection string to use for the database context.</param>
        /// <returns>The updated service collection.</returns>
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

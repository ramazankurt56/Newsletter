using GenericRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newsletter.Domain.Models;
using Newsletter.Infrastructure.Context;
using Scrutor;
using System.Reflection;

namespace Newsletter.Infrastructure;
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services,IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("SqlServer"));
        });
        services.AddIdentityCore<AppUser>().AddEntityFrameworkStores<ApplicationDbContext>();
        services.AddScoped<IUnitOfWork>(svr => svr.GetRequiredService<ApplicationDbContext>());
        services.Scan(action =>
        action.
        FromAssemblies(Assembly.GetExecutingAssembly())
        .AddClasses(publicOnly: false)
        .UsingRegistrationStrategy(RegistrationStrategy.Skip)
        .AsMatchingInterface()
        .AsImplementedInterfaces()
        .WithScopedLifetime()) ;
        return services;
    }
    
}

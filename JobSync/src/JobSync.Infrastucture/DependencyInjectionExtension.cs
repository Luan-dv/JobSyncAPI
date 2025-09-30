using JobSync.Domain.Repositories;
using JobSync.Domain.Repositories.User;
using JobSync.Domain.Security.Cryptography;
using JobSync.Infrastucture.DataAcess;
using JobSync.Infrastucture.DataAcess.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace JobSync.Infrastucture;
public class DependencyInjectionExtension
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration) //this é uma extensão de IServiceCollection
    {
        AddDbContext(services, configuration);

        AddRepositories(services);


        services.AddScoped<IPasswordEncripter, Security.Cryptography.Bcrypt>(); 
    }

    private static void AddRepositories(IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IUserReadOnlyRepository, UserRepository>();
        services.AddScoped<IUserWriteOnlyRepository, UserRepository>();
    }

    private static void AddDbContext(IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Connection"); ;
        var version = new Version(8, 0, 42);
        var serverVersion = new MySqlServerVersion(version);



        services.AddDbContext<JobSyncDbContext>(config => config.UseMySql(connectionString, serverVersion));
    }

}


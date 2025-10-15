using JobSync.Aplication.AutoMapper;
using JobSync.Aplication.UseCases.Users.Login.DoLogin;
using JobSync.Aplication.UseCases.Users.Register;
using Microsoft.Extensions.DependencyInjection;

namespace JobSync.Aplication;
public static class DependencyInjectionExtension
{
    public static void AddApplication(this IServiceCollection services)
    {
        AddAutoMapper(services);
        AddUseCases(services);

    }
    private static void AddAutoMapper(IServiceCollection services)
    {
        services.AddAutoMapper(typeof(AutoMapping));

    }
    private static void AddUseCases(IServiceCollection services)
    {
 
        services.AddScoped<IRegisterUserUseCase, RegisterUserUseCase>();
        services.AddScoped<IDoLoginUseCase, DoLoginUseCase>();
    }

}

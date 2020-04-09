using Api.Domain.Interfaces.Services.User;
using Api.Service.Service;
using Domain.Interfaces.Services.User;
using Microsoft.Extensions.DependencyInjection;
using Service.Service;

namespace Api.CrossCutting.DependencyInjection
{
    public class ConfigureService
    {
        public static void ConfigureDependenciesService(IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<IUserServices, UserService>();
            serviceCollection.AddTransient<ILoginService, LoginService>();
        }
    }
}

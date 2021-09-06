using Api.Domain.Interfaces.Services.Cep;
using Api.Domain.Interfaces.Services.Municipio;
using Api.Domain.Interfaces.Services.Uf;
using Api.Domain.Interfaces.Services.User;
using Api.Service.Service;
using Api.Service.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Api.CrossCutting.DependencyInjection
{
    public class ConfigureService
    {
        public static void ConfigureDependenciesServices(IServiceCollection servicesCollection)
        {
            servicesCollection.AddTransient<IUserService, UserService>();
            servicesCollection.AddTransient<IloginService, LoginService>();
            servicesCollection.AddTransient<IUfService, UfService>();
            servicesCollection.AddTransient<ICepService, CepService>();
            servicesCollection.AddTransient<IMunicipioService, MunicipioService>();
        }
    }
}

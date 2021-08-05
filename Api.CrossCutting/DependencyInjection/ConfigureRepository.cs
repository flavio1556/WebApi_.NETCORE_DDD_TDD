using Api.Data.Context;
using Api.Data.Repository;
using Api.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Api.Data.Implementations;
using Api.Domain.Repository;
namespace Api.CrossCutting.DependencyInjection
{
    public class ConfigureRepository
    {
        public static void ConfigureDependenciesRepository(IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));
            serviceCollection.AddScoped<IUserRepository, UserImplementation>();
            serviceCollection.AddDbContext<Mycontext>
             (
                 options => options.UseSqlServer("Server=(local)\\sqlexpress;DataBase=Curso;Trusted_Connection=true;MultipleActiveResultSets=true")
             );
        }
    }
}

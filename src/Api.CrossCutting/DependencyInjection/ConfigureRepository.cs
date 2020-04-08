using Api.Data.Context;
using Api.Data.Repository;
using Api.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Api.CrossCutting.DependencyInjection
{
    public class ConfigureRepository
    {
        public static void ConfigureDependenciesRepository(IServiceCollection serviceCollection)
        {
            serviceCollection.AddDbContext<MyContext>(
                 // options => options.UseMySql("Server=localhost;Port=3306;Database=dbAPI_DDD;Uid=root;Pwd=admin")
                 options => options.UseSqlServer("Server=TOSHIBA-ESTEBAN;Database=dbAPI_DDD;User Id=sa;Password=@dmin123;")
            );
            serviceCollection.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));
        }
    }
}

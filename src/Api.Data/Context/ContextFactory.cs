using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Api.Data.Context
{
    public class ContextFactory : IDesignTimeDbContextFactory<MyContext>
    {
        public MyContext CreateDbContext(string[] args)
        {
            //Usado para criar Migracoes
            //Mysql
            // var connectionString = "Server=localhost;Port=3306;Database=dbAPI_DDD;Uid=root;Pwd=admin";
            //SqlServer
            var connectionString = "Server=TOSHIBA-ESTEBAN; Database=dbAPI_DDD; User Id=sa; Password=@dmin123;";
            var optionsBuilder = new DbContextOptionsBuilder<MyContext>();
            //optionsBuilder.UseMySql(connectionString);
            optionsBuilder.UseSqlServer(connectionString);
            return new MyContext(optionsBuilder.Options);
        }
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Api.Data.Context
{
    public class ContextFactory : IDesignTimeDbContextFactory<MyContext>
    {
        public MyContext CreateDbContext(string[] args)
        {
            var ConnectionString = "Server=(local)\\sqlexpress;DataBase=Curso;Trusted_Connection=true;MultipleActiveResultSets=true";
            var optionsBuilder = new DbContextOptionsBuilder<MyContext>();
            optionsBuilder.UseSqlServer(ConnectionString);
            return new MyContext(optionsBuilder.Options);
        }
    }
}

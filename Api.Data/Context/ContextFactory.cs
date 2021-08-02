using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Api.Data.Context
{
    public class ContextFactory : IDesignTimeDbContextFactory<Mycontext>
    {
        public Mycontext CreateDbContext(string[] args)
        {
            var ConnectionString = "Server=(local)\\sqlexpress;DataBase=Curso;Trusted_Connection=true;MultipleActiveResultSets=true";
            var optionsBuilder = new DbContextOptionsBuilder<Mycontext>();
            optionsBuilder.UseSqlServer(ConnectionString);
            return new Mycontext (optionsBuilder.Options);
        }
    }
}

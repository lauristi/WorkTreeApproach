using WorkTree.Database.Dapper.Interface;

namespace WorkTree.Database.Dapper
{
    public abstract class AbstractConnectionStringProvider : IConnectionStringProvider
    {
        protected readonly IConfiguration Configuration;

        protected AbstractConnectionStringProvider(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public abstract string GetConnectionString();
    }
}
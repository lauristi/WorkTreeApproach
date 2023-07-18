using Microsoft.Extensions.Configuration;

namespace WorkTree.Database.Dapper
{
    public class ConnectionStringProvider : AbstractConnectionStringProvider
    {
        public ConnectionStringProvider(IConfiguration configuration) : base(configuration)
        {
        }

        public override string GetConnectionString()
        {
            return Configuration["Database:SQlServer"];
        }
    }
}

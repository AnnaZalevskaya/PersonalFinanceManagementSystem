using Accounts.DataAccess.Dapper.Settings;
using Microsoft.Extensions.Options;
using Npgsql;
using System.Data;

namespace Accounts.DataAccess.Dapper.Data
{
    public class DapperContext
    {
        private readonly IOptions<ConnectionStrings> _options;

        public DapperContext(IOptions<ConnectionStrings> options)
        {
            _options = options;
        }

        public IDbConnection CreateConnection()
            => new NpgsqlConnection(_options.Value.DefaultConnection);
    }
}

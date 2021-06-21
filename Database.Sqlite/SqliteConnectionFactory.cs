namespace Database.Sqlite
{
    using System;
    using System.Data.Common;
    using System.Data.SQLite;
    using System.Threading;
    using System.Threading.Tasks;
    using Abstractions;
    using Microsoft.Extensions.Options;


    public class SqliteConnectionFactory : IDbConnectionFactory
    {
        private readonly IOptions<SqliteConnectionFactoryOptions> _options;


        public SqliteConnectionFactory(IOptions<SqliteConnectionFactoryOptions> options)
        {
            _options = options ?? throw new ArgumentNullException(nameof(options));
        }


        public async Task<DbConnection> CreateConnectionAsync(CancellationToken cancellationToken = default)
        {
            DbConnection connection = new SQLiteConnection(_options.Value.ConnectionString);

            await connection.OpenAsync(cancellationToken);
            
            return connection;
        }
    }
}
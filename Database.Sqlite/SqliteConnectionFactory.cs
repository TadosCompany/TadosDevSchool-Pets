namespace Database.Sqlite
{
    using System;
    using System.Data.Common;
    using System.Data.SQLite;
    using System.Threading;
    using System.Threading.Tasks;
    using Abstractions;

    public class SqliteConnectionFactory : IDbConnectionFactory
    {
        public static readonly string ConnectionStringParameterName = nameof(_connectionString).TrimStart('_');



        private readonly string _connectionString;

        

        public SqliteConnectionFactory(string connectionString)
        {
            if (string.IsNullOrWhiteSpace(connectionString))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(connectionString));

            _connectionString = connectionString;
        }



        public async Task<DbConnection> CreateConnectionAsync(CancellationToken cancellationToken = default)
        {
            DbConnection connection = new SQLiteConnection(_connectionString);

            await connection.OpenAsync(cancellationToken);
            
            return connection;
        }
    }
}
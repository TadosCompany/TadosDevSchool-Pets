namespace Pets.Persistence
{
    using System;
    using System.Data;
    using System.Data.Common;
    using System.Threading;
    using System.Threading.Tasks;
    using Dapper;
    using global::Database.Abstractions;

    public class Database
    {
        private readonly IDbConnectionFactory _dbConnectionFactory;



        public Database(IDbConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory ?? throw new ArgumentNullException(nameof(dbConnectionFactory));
        }



        public async Task InitAsync(CancellationToken cancellationToken = default)
        {
            await using DbConnection connection = await _dbConnectionFactory.CreateConnectionAsync(cancellationToken);
            await using DbTransaction transaction = await connection.BeginTransactionAsync(IsolationLevel.ReadCommitted, cancellationToken);

            await connection.ExecuteAsync(
                sql: @"
                    CREATE TABLE IF NOT EXISTS Food (
                        Id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                        Name TEXT NOT NULL,
                        AnimalType INTEGER NOT NULL,
                        Count INTEGER NOT NULL
                    );

                    CREATE TABLE IF NOT EXISTS Breed (
                        Id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                        Name TEXT NOT NULL,
                        AnimalType INTEGER NOT NULL);

                    CREATE TABLE IF NOT EXISTS Animal (
                        Id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                        Name TEXT NOT NULL,
                        Type INTEGER NOT NULL,
                        BreedId INTEGER NOT NULL,
                        CONSTRAINT FK_Animal_Breed FOREIGN KEY (BreedId) REFERENCES Breed (Id)
                    );

                    CREATE TABLE IF NOT EXISTS Cat (
                        AnimalId INTEGER NOT NULL,
                        Weight DECIMAL(10, 2),
                        CONSTRAINT UQ_AnimalId UNIQUE (AnimalId),
                        CONSTRAINT FK_Cat_Animal FOREIGN KEY (AnimalId) REFERENCES Animal (Id)
                    );

                    CREATE TABLE IF NOT EXISTS Dog (
                        AnimalId INTEGER NOT NULL,
                        TailLength DECIMAL(10, 2),
                        CONSTRAINT UQ_AnimalId UNIQUE (AnimalId),
                        CONSTRAINT FK_Dog_Animal FOREIGN KEY (AnimalId) REFERENCES Animal (Id)
                    );

                    CREATE TABLE IF NOT EXISTS Feeding (
                        Id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                        AnimalId INTEGER NOT NULL,
                        FoodId INTEGER NOT NULL,
                        DateTimeUtc TEXT NOT NULL,
                        Count INT NOT NULL,
                        CONSTRAINT FK_Feeding_Animal FOREIGN KEY (AnimalId) REFERENCES Animal (Id),
                        CONSTRAINT FK_Feeding_Food FOREIGN KEY (FoodId) REFERENCES Food (Id)
                    );",
                transaction: transaction);

            await transaction.CommitAsync(cancellationToken);
        }
    }
}

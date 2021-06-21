namespace Pets.Initializers
{
    using System.Data.SQLite;

    public static class DatabaseInitializer
    {
        public static string ConnectionString { get; private set; }


        public static void Init(string connectionString)
        {
            ConnectionString = connectionString;
            InitSchema();
        }

        private static void InitSchema()
        {
            using SQLiteConnection connection = new SQLiteConnection(ConnectionString);
            connection.Open();

            using SQLiteCommand command = connection.CreateCommand();

            command.CommandText = @"
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
                );";

            command.ExecuteNonQuery();
        }
    }
}
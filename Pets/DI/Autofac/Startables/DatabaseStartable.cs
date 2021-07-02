namespace Pets.DI.Autofac.Startables
{
    using System;
    using global::Autofac;
    using Persistence;


    public class DatabaseStartable : IStartable
    {
        private readonly Database _database;


        public DatabaseStartable(Database database)
        {
            _database = database ?? throw new ArgumentNullException(nameof(database));
        }


        public void Start()
        {
            _database.InitAsync().GetAwaiter().GetResult();
        }
    }
}
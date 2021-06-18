namespace Pets.Domain.Services
{
    using System.Data.SQLite;
    using System.Threading.Tasks;
    using Entities;
    using Enums;

    public class DogService : AnimalService
    {
        public DogService(SQLiteConnection connection)
            : base(connection)
        {
        }



        public async Task<Dog> CreateDogAsync(string name, Breed breed, decimal tailLength)
        {
            await CheckIsAnimalWithNameExistAsync(AnimalType.Dog, name);

            return new Dog(name, breed, tailLength);
        }
    }
}

namespace Pets.Domain.Services
{
    using System.Data.SQLite;
    using System.Threading.Tasks;
    using Entities;
    using Enums;

    public class CatService : AnimalService
    {
        public CatService(SQLiteConnection connection)
            : base(connection)
        {
        }



        public async Task<Cat> CreateCatAsync(string name, Breed breed, decimal weight)
        {
            await CheckIsAnimalWithNameExistAsync(AnimalType.Cat, name);

            return new Cat(name, breed, weight);
        }
    }
}

namespace Pets.Domain.Services.Animals.Hamsters
{
    using System.Threading;
    using System.Threading.Tasks;
    using Entities;
    using global::Commands.Abstractions;
    using Queries.Abstractions;

    public class HamsterService : AnimalServiceBase, IHamsterService
    {
        public HamsterService(
            IAsyncQueryBuilder asyncQueryBuilder,
            IAsyncCommandBuilder asyncCommandBuilder)
            : base(
                asyncQueryBuilder,
                asyncCommandBuilder)
        {
        }



        public async Task<Hamster> CreateHamsterAsync(
            string name, 
            Breed breed, 
            Food favoriteFood, 
            string eyesColor, 
            CancellationToken cancellationToken = default)
        {
            var hamster = new Hamster(name, breed, favoriteFood, eyesColor);

            await CreateAnimalAsync(hamster, cancellationToken);

            return hamster;
        }
    }
}
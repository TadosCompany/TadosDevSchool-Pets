namespace Pets.Domain.Services.Animals.Cats
{
    using System.Threading;
    using System.Threading.Tasks;
    using Entities;
    using global::Commands.Abstractions;
    using Queries.Abstractions;

    public class CatService : AnimalServiceBase, ICatService
    {
        public CatService(
            IAsyncQueryBuilder asyncQueryBuilder, 
            IAsyncCommandBuilder asyncCommandBuilder) 
            : base(
                asyncQueryBuilder, 
                asyncCommandBuilder)
        {
        }



        public async Task<Cat> CreateCatAsync(
            string name, 
            Breed breed,
            Food favoriteFood,
            decimal weight, 
            CancellationToken cancellationToken = default)
        {
            var cat = new Cat(name, breed, favoriteFood, weight);

            await CreateAnimalAsync(cat, cancellationToken);

            return cat;
        }
    }
}

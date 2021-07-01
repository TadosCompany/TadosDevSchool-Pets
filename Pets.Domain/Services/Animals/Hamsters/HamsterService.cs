namespace Pets.Domain.Services.Animals.Hamsters
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Commands.Contexts;
    using Entities;
    using Enums;
    using global::Commands.Abstractions;
    using Queries.Abstractions;

    public class HamsterService : AnimalServiceBase, IHamsterService
    {
        private readonly IAsyncCommandBuilder _asyncCommandBuilder;



        public HamsterService(IAsyncQueryBuilder asyncQueryBuilder, IAsyncCommandBuilder asyncCommandBuilder) : base(asyncQueryBuilder)
        {
            _asyncCommandBuilder = asyncCommandBuilder ?? throw new ArgumentNullException(nameof(asyncCommandBuilder));
        }



        public async Task<Hamster> CreateHamsterAsync(
            string name, 
            Breed breed, 
            Food favoriteFood, 
            string eyesColor, 
            CancellationToken cancellationToken = default)
        {
            await CheckIsAnimalWithNameExistAsync(AnimalType.Hamster, name, cancellationToken);
            
            var hamster = new Hamster(name, breed, favoriteFood, eyesColor);

            await _asyncCommandBuilder.CreateAsync(hamster, cancellationToken);

            return hamster;
        }
    }
}
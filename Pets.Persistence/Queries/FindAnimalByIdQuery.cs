namespace Pets.Persistence.Queries
{
    using System;
    using System.Collections.Generic;
    using System.Data.Common;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Dapper;
    using Database.Abstractions;
    using Domain.Criteria;
    using Domain.Entities;
    using Domain.ValueObjects;
    using Dto;
    using global::Queries.Abstractions;


    public class FindAnimalByIdQuery : IAsyncQuery<FindById, Animal>
    {
        private readonly IDbTransactionProvider _dbTransactionProvider;


        public FindAnimalByIdQuery(IDbTransactionProvider dbTransactionProvider)
        {
            _dbTransactionProvider =
                dbTransactionProvider ?? throw new ArgumentNullException(nameof(dbTransactionProvider));
        }


        public async Task<Animal> AskAsync(FindById criterion, CancellationToken cancellationToken = default)
        {
            DbTransaction transaction = await _dbTransactionProvider.GetCurrentTransactionAsync(cancellationToken);
            DbConnection connection = transaction.Connection;

            AnimalDto dto = (await connection.QueryAsync<AnimalDto, BreedDto, AnimalDto>(@"
                SELECT
                    Animal.Id,
                    Animal.Name,
                    Animal.Type,
                    Cat.Weight,
                    Dog.TailLength,
                    Breed.Id,
                    Breed.Name,
                    Breed.AnimalType
                FROM Animal
                JOIN Breed ON Breed.Id = Animal.BreedId
                LEFT JOIN Cat ON Cat.AnimalId = Animal.Id
                LEFT JOIN Dog ON Dog.AnimalId = Animal.Id
                WHERE Animal.Id = @Id", (animalDto, breedDto) =>
            {
                Breed breed = breedDto.ToEntity();

                animalDto.Breed = breedDto;
                
                return animalDto;
            }, new
            {
                Id = criterion.Id,
            }, transaction)).SingleOrDefault();

            List<Feeding> feedings = new List<Feeding>();
            
            if (dto != null)
            {
                Dictionary<long, Food> foodsCache = new Dictionary<long, Food>();
                
                feedings.AddRange(
                    await connection.QueryAsync<FeedingDto, FoodDto, Feeding>(@"
                        SELECT
                            Feeding.Id,
                            Feeding.DateTimeUtc,
                            Feeding.Count,
                            Food.Id,
                            Food.AnimalType,
                            Food.Name,
                            Food.Count
                        FROM Feeding
                        JOIN Food ON Food.Id = Feeding.FoodId
                        WHERE Feeding.AnimalId = @AnimalId
                        ORDER BY Feeding.DateTimeUtc DESC",
                        (feedingDto, foodDto) =>
                        {
                            if (!foodsCache.ContainsKey(foodDto.Id))
                            {
                                foodsCache[foodDto.Id] = foodDto.ToEntity();
                            }
                            
                            return new Feeding(
                                feedingDto.Id,
                                Helpers.ParseDateTime(feedingDto.DateTimeUtc),
                                foodsCache[foodDto.Id],
                                feedingDto.Count);
                        },
                        new
                        {
                            AnimalId = dto.Id,
                        }, transaction));
            }

            return dto?.ToEntity(dto.Breed.ToEntity(), feedings);
        }
    }
}
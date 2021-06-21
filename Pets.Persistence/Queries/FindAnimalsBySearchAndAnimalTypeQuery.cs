namespace Pets.Persistence.Queries
{
    using System;
    using System.Collections.Generic;
    using System.Data.Common;
    using System.Dynamic;
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


    public class FindAnimalsBySearchAndAnimalTypeQuery : IAsyncQuery<FindBySearchAndAnimalType, List<Animal>>
    {
        private readonly IDbTransactionProvider _dbTransactionProvider;


        public FindAnimalsBySearchAndAnimalTypeQuery(IDbTransactionProvider dbTransactionProvider)
        {
            _dbTransactionProvider =
                dbTransactionProvider ?? throw new ArgumentNullException(nameof(dbTransactionProvider));
        }


        public async Task<List<Animal>> AskAsync(
            FindBySearchAndAnimalType criterion,
            CancellationToken cancellationToken = default)
        {
            DbTransaction transaction = await _dbTransactionProvider.GetCurrentTransactionAsync(cancellationToken);
            DbConnection connection = transaction.Connection;

            List<string> conditions = new List<string>();
            ExpandoObject parameters = new ExpandoObject();

            IDictionary<string, object> parametersMap = parameters;

            if (!string.IsNullOrWhiteSpace(criterion.Search))
            {
                conditions.Add("Animal.Name LIKE '%' || @Search || '%'");
                parametersMap["Search"] = criterion.Search;
            }

            if (criterion.AnimalType.HasValue)
            {
                conditions.Add("Animal.Type = @AnimalType");
                parametersMap["AnimalType"] = criterion.AnimalType.Value;
            }

            Dictionary<long, Breed> breedsCache = new Dictionary<long, Breed>();
            Dictionary<long, Food> foodsCache = new Dictionary<long, Food>();

            List<AnimalDto> dtos = (await connection.QueryAsync<AnimalDto, BreedDto, AnimalDto>(@$"
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
                {(conditions.Count > 0 ? $"WHERE {string.Join(" AND ", conditions)}" : string.Empty)}
                ORDER BY Animal.Name", (animalDto, breedDto) =>
            {
                animalDto.Breed = breedDto;

                return animalDto;
            }, parameters, transaction)).ToList();

            List<Animal> animals = new List<Animal>();
            
            foreach (AnimalDto dto in dtos)
            {
                List<Feeding> feedings = new List<Feeding>();
                
                feedings.AddRange(
                    await connection.QueryAsync<FeedingDto, FoodDto, Feeding>(@"
                        SELECT
                            Feeding.Id,
                            Feeding.DateTimeUtc,
                            Feeding.Count,
                            Food.Id,
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

                if (!breedsCache.ContainsKey(dto.Breed.Id))
                {
                    breedsCache.Add(dto.Breed.Id, dto.Breed.ToEntity());
                }
                
                animals.Add(dto.ToEntity(breedsCache[dto.Breed.Id], feedings));
            }
            
            return animals;
        }
    }
}
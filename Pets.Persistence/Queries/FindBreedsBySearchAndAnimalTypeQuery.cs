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
    using global::Queries.Abstractions;


    public class FindBreedsBySearchAndAnimalTypeQuery : IAsyncQuery<FindBySearchAndAnimalType, List<Breed>>
    {
        private readonly IDbTransactionProvider _dbTransactionProvider;


        public FindBreedsBySearchAndAnimalTypeQuery(IDbTransactionProvider dbTransactionProvider)
        {
            _dbTransactionProvider =
                dbTransactionProvider ?? throw new ArgumentNullException(nameof(dbTransactionProvider));
        }


        public async Task<List<Breed>> AskAsync(
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
                conditions.Add("Breed.Name LIKE '%' || @Search || '%'");
                parametersMap["Search"] = criterion.Search;
            }

            if (criterion.AnimalType.HasValue)
            {
                conditions.Add("Breed.AnimalType = @AnimalType");
                parametersMap["AnimalType"] = criterion.AnimalType.Value;
            }

            List<Breed> breeds = (await connection.QueryAsync<Breed>(@$"
                SELECT
                    Breed.Id,
                    Breed.AnimalType,
                    Breed.Name
                FROM Breed
                {(conditions.Count > 0 ? $"WHERE {string.Join(" AND ", conditions)}" : string.Empty)}
                ORDER BY Breed.Name", parameters, transaction)).ToList();

            return breeds;
        }
    }
}
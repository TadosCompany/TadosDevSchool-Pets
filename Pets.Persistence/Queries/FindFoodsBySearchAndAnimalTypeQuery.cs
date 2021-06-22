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


    public class FindFoodsBySearchAndAnimalTypeQuery : IAsyncQuery<FindBySearchAndAnimalType, List<Food>>
    {
        private readonly IDbTransactionProvider _dbTransactionProvider;


        public FindFoodsBySearchAndAnimalTypeQuery(IDbTransactionProvider dbTransactionProvider)
        {
            _dbTransactionProvider =
                dbTransactionProvider ?? throw new ArgumentNullException(nameof(dbTransactionProvider));
        }


        public async Task<List<Food>> AskAsync(
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
                conditions.Add("Food.Name LIKE '%' || @Search || '%'");
                parametersMap["Search"] = criterion.Search;
            }

            if (criterion.AnimalType.HasValue)
            {
                conditions.Add("Food.AnimalType = @AnimalType");
                parametersMap["AnimalType"] = criterion.AnimalType.Value;
            }

            List<Food> breeds = (await connection.QueryAsync<Food>(@$"
                SELECT
                    Food.Id,
                    Food.AnimalType,
                    Food.Name,
                    Food.Count
                FROM Food
                {(conditions.Count > 0 ? $"WHERE {string.Join(" AND ", conditions)}" : string.Empty)}
                ORDER BY Food.Name", parameters, transaction)).ToList();

            return breeds;
        }
    }
}
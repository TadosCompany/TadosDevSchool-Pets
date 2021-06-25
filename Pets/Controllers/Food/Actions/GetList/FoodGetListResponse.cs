namespace Pets.Controllers.Food.Actions.GetList
{
    using System.Collections.Generic;
    using Api.Requests.Abstractions;
    using Dto;

    public record FoodGetListResponse : IResponse
    {
        public IEnumerable<FoodDto> Foods { get; init; }
    }
}

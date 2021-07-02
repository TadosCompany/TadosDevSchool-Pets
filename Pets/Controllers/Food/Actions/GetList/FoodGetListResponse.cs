namespace Pets.Controllers.Food.Actions.GetList
{
    using System.Collections.Generic;
    using Api.Requests.Abstractions;
    using Dto;

    public record FoodGetListResponse(

        IEnumerable<FoodDto> Foods

    ) : IResponse;
}

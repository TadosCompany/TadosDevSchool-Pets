namespace Pets.Controllers.Breed.Actions.GetList
{
    using System.Collections.Generic;
    using Api.Requests.Abstractions;
    using Dto;

    public class BreedGetListResponse : IResponse
    {
        public IEnumerable<BreedDto> Breeds { get; set; }
    }
}

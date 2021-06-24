namespace Pets.Controllers.Animal.Actions.GetList
{
    using System.Collections.Generic;
    using Dto;

    public class AnimalGetListResponse
    {
        public IEnumerable<AnimalListItemDto> Animals { get; set; }
    }
}

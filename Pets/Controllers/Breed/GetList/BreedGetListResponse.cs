namespace Pets.Controllers.Breed.GetList
{
    using System.Collections.Generic;
    using Domain.Entities;

    public class BreedGetListResponse
    {
        public IEnumerable<Breed> Breeds { get; set; }
    }
}

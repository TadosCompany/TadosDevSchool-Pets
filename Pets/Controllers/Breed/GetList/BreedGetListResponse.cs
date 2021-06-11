namespace Pets.Controllers.Breed.GetList
{
    using Models;
    using System.Collections.Generic;

    public class BreedGetListResponse
    {
        public IEnumerable<Breed> Breeds { get; set; }
    }
}

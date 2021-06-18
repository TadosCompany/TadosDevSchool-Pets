namespace Pets.Controllers.Animal.GetList
{
    using System.Collections.Generic;
    using Domain.Entities;

    public class AnimalGetListResponse
    {
        public IEnumerable<Animal> Animals { get; set; }
    }
}

namespace Pets.Controllers.Animal.GetList
{
    using Models;
    using System.Collections.Generic;

    public class AnimalGetListResponse
    {
        public IEnumerable<Animal> Animals { get; set; }
    }
}

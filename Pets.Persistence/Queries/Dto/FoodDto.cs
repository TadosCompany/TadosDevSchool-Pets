namespace Pets.Persistence.Queries.Dto
{
    using Domain.Entities;
    using Domain.Enums;

    internal class FoodDto
    {
        public long Id { get; set; }

        public AnimalType AnimalType { get; set; }

        public string Name { get; set; }

        public int Count { get; set; }


        public Food ToEntity()
        {
            return new Food(Id, AnimalType, Name, Count);
        }
    }
}
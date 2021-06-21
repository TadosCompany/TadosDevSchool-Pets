namespace Pets.Domain.Commands.Contexts
{
    using Entities;
    using global::Commands.Abstractions;


    public class CreateDogCommandContext : ICommandContext
    {
        public CreateDogCommandContext(Dog dog)
        {
            Dog = dog;
        }


        public Dog Dog { get; }
    }
}
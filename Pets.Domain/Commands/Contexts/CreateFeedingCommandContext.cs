namespace Pets.Domain.Commands.Contexts
{
    using System;
    using Entities;
    using global::Commands.Abstractions;
    using ValueObjects;


    public class CreateFeedingCommandContext : ICommandContext
    {
        public CreateFeedingCommandContext(Animal animal, Feeding feeding)
        {
            Animal = animal ?? throw new ArgumentNullException(nameof(animal));
            Feeding = feeding ?? throw new ArgumentNullException(nameof(feeding));
        }


        public Animal Animal { get; }

        public Feeding Feeding { get; }
    }
}
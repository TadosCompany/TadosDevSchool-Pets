namespace Pets.Domain.Commands.Contexts
{
    using System;
    using Entities;
    using global::Commands.Abstractions;


    public class CreateBreedCommandContext : ICommandContext
    {
        public CreateBreedCommandContext(Breed breed)
        {
            Breed = breed ?? throw new ArgumentNullException(nameof(breed));
        }
        
        
        public Breed Breed { get; }
    }
}
namespace Pets.Domain.Commands.Contexts
{
    using System;
    using Entities;
    using global::Commands.Abstractions;


    public class CreateFoodCommandContext : ICommandContext
    {
        public CreateFoodCommandContext(Food food)
        {
            Food = food ?? throw new ArgumentNullException(nameof(food));
        }


        public Food Food { get; }
    }
}
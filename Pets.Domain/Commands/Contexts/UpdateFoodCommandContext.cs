namespace Pets.Domain.Commands.Contexts
{
    using System;
    using Entities;
    using global::Commands.Abstractions;


    public class UpdateFoodCommandContext : ICommandContext
    {
        public UpdateFoodCommandContext(Food food)
        {
            Food = food ?? throw new ArgumentNullException(nameof(food));
        }


        public Food Food { get; }
    }
}
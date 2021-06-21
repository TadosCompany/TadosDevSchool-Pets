namespace Pets.Domain.Commands.Contexts
{
    using Entities;
    using global::Commands.Abstractions;


    public class CreateCatCommandContext : ICommandContext
    {
        public CreateCatCommandContext(Cat cat)
        {
            Cat = cat;
        }


        public Cat Cat { get; }
    }
}
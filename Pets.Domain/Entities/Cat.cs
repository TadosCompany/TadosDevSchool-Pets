namespace Pets.Domain.Entities
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using Enums;

    [SuppressMessage("CodeQuality", "IDE0079:Remove unnecessary suppression")]
    [SuppressMessage("ReSharper", "VirtualMemberCallInConstructor")]
    public class Cat : Animal
    {
        [Obsolete("Only for reflection", true)]
        public Cat()
        {
        }
        
        protected internal Cat(string name, Breed breed, decimal weight)
            : base(AnimalType.Cat, name, breed)
        {
            if (weight < 0) 
                throw new ArgumentOutOfRangeException(nameof(weight));

            Weight = weight;
        }



        public virtual decimal Weight { get; protected set; }
    }
}

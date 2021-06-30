namespace Pets.Domain.Entities
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using Enums;

    [SuppressMessage("CodeQuality", "IDE0079:Remove unnecessary suppression")]
    [SuppressMessage("ReSharper", "VirtualMemberCallInConstructor")]
    public class Dog : Animal
    {
        [Obsolete("Only for reflection", true)]
        public Dog()
        {
        }

        protected internal Dog(string name, Breed breed, decimal tailLength)
            : base(AnimalType.Dog, name, breed)
        {
            if (tailLength < 0)
                throw new ArgumentOutOfRangeException(nameof(tailLength));

            if (breed.AnimalType != AnimalType.Dog)
                throw new ArgumentException("Invalid breed animal type", nameof(breed));
            
            TailLength = tailLength;
        }



        public virtual decimal TailLength { get; protected set; }
    }
}
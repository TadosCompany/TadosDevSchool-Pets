namespace Pets.Domain.Entities
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using global::Domain.Abstractions;
    using Enums;

    [SuppressMessage("CodeQuality", "IDE0079:Remove unnecessary suppression")]
    [SuppressMessage("ReSharper", "VirtualMemberCallInConstructor")]
    public class Breed : IEntity
    {
        [Obsolete("Only for reflection", true)]
        public Breed()
        {
        }
        
        protected internal Breed(AnimalType animalType, string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(name));

            AnimalType = animalType;
            Name = name;
        }



        public virtual long Id { get; protected set; }

        public virtual AnimalType AnimalType { get; protected set; }

        public virtual string Name { get; protected set; }
    }
}

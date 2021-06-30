namespace Pets.Domain.Exceptions
{
    using System;
    using global::Domain.Abstractions;

    public class AnotherFoodRequiredException : Exception, IDomainException
    {
        private const string DefaultMessage = "Another food required";



        public AnotherFoodRequiredException()
            : base(DefaultMessage)
        {
        }
    }
}

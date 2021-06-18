namespace Pets.Domain.Exceptions
{
    using System;
    using global::Domain.Abstractions;

    public class NotEnoughFoodException : Exception, IDomainException
    {
        private const string DefaultMessage = "Not enough food";



        public NotEnoughFoodException()
            : base(DefaultMessage)
        {
        }
    }
}

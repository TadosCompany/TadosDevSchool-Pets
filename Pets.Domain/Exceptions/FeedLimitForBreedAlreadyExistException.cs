namespace Pets.Domain.Exceptions
{
    using System;
    using global::Domain.Abstractions;

    public class FeedLimitForBreedAlreadyExistException : Exception, IDomainException
    {
        private const string DefaultMessage = "Feed limit for breed already exist";



        public FeedLimitForBreedAlreadyExistException()
            : base(DefaultMessage)
        {
        }
    }
}

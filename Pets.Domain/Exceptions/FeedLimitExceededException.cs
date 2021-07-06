namespace Pets.Domain.Exceptions
{
    using System;
    using global::Domain.Abstractions;

    public class FeedLimitExceededException : Exception, IDomainException
    {
        private const string DefaultMessage = "Feed limit exceeded";



        public FeedLimitExceededException()
            : base(DefaultMessage)
        {
        }
    }
}

namespace Pets.Domain.Exceptions
{
    using System;
    using global::Domain.Abstractions;

    public class FeedLimitExceeded : Exception, IDomainException
    {
        private const string DefaultMessage = "Feed limit exceeded";



        public FeedLimitExceeded()
            : base(DefaultMessage)
        {
        }
    }
}

namespace Pets.Domain.Exceptions
{
    using System;
    using global::Domain.Abstractions;

    public class NameAlreadyExistsException : Exception, IDomainException
    {
        private const string DefaultMessage = "Name already exists";



        public NameAlreadyExistsException()
            : base(DefaultMessage)
        {
        }
    }
}

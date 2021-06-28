namespace Pets.Persistence.ORM.Commands
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Domain.Commands.Contexts;
    using global::Commands.Abstractions;
    using global::Domain.Abstractions;
    using Repositories.Abstractions;


    public class CreateObjectWithIdCommand<THasId> : IAsyncCommand<CreateObjectWithIdCommandContext<THasId>>
        where THasId : class, IHasId, new()
    {
        private readonly IAsyncRepository<THasId> _repository;


        public CreateObjectWithIdCommand(IAsyncRepository<THasId> repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }


        public Task ExecuteAsync(
            CreateObjectWithIdCommandContext<THasId> commandContext,
            CancellationToken cancellationToken = default)
        {
            return _repository.AddAsync(commandContext.ObjectWithId, cancellationToken);
        }
    }
}
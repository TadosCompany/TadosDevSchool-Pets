namespace AspnetCore.ApiControllers.Abstractions
{
    using Persistence.Transactions.Behaviors;

    public interface IShouldPerformCommit
    {
        IExpectCommit CommitPerformer { get; }
    }
}
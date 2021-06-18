namespace Domain.Abstractions
{
    public interface IValueObjectWithId : IValueObject
    {
        long Id { get; }
    }
}

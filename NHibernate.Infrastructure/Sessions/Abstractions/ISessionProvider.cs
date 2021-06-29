namespace NHibernate.Infrastructure.Sessions.Abstractions
{
    public interface ISessionProvider
    {
        ISession CurrentSession { get; }
    }
}
namespace Pets.Domain.Entities
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using global::Domain.Abstractions;

    [SuppressMessage("CodeQuality", "IDE0079:Remove unnecessary suppression")]
    [SuppressMessage("ReSharper", "VirtualMemberCallInConstructor")]
    public class FeedLimit : IEntity
    {
        [Obsolete("Only for reflection", true)]
        public FeedLimit()
        {
        }

        protected internal FeedLimit(Breed breed, int maxPerDay)
        {
            Breed = breed ?? throw new ArgumentNullException(nameof(breed));
            SetMaxPerDay(maxPerDay);
        }



        public virtual long Id { get; protected set; }

        public virtual Breed Breed { get; protected set; }

        public virtual int MaxPerDay { get; protected set; }



        public virtual void SetMaxPerDay(int maxPerDay)
        {
            if (maxPerDay <= 0)
                throw new ArgumentOutOfRangeException(nameof(maxPerDay));

            MaxPerDay = maxPerDay;
        }
    }
}

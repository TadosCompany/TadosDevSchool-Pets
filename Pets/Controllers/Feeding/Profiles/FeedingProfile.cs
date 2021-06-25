namespace Pets.Controllers.Feeding.Profiles
{
    using AutoMapper;
    using Domain.ValueObjects;
    using Dto;

    public class FeedingProfile : Profile
    {
        public FeedingProfile()
        {
            CreateMap<Feeding, FeedingDto>();
        }
    }
}

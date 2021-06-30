namespace Pets.Controllers.FeedLimit.Profiles
{
    using AutoMapper;
    using Domain.Entities;
    using Dto;

    public class FeedLimitProfile : Profile
    {
        public FeedLimitProfile()
        {
            CreateMap<FeedLimit, FeedLimitDto>();
        }
    }
}

namespace Pets.Controllers.Food.Profiles
{
    using AutoMapper;
    using Domain.Entities;
    using Dto;

    public class FoodProfile : Profile
    {
        public FoodProfile()
        {
            CreateMap<Food, FoodDto>();
        }
    }
}

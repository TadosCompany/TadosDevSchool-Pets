namespace Pets.Controllers.Breed.Profiles
{
    using AutoMapper;
    using Domain.Entities;
    using Dto;

    public class BreedProfile : Profile
    {
        public BreedProfile()
        {
            CreateMap<Breed, BreedDto>();
        }
    }
}

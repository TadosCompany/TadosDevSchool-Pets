namespace Pets.Controllers.Animal.Profiles
{
    using AutoMapper;
    using Domain.Entities;
    using Dto;

    public class AnimalProfile : Profile
    {
        public AnimalProfile()
        {
            CreateMap<Animal, AnimalListItemDto>()
                .Include<Cat, CatListItemDto>()
                .Include<Dog, DogListItemDto>();

            CreateMap<Cat, CatListItemDto>();
            CreateMap<Dog, DogListItemDto>();


            CreateMap<Animal, AnimalDto>()
                .Include<Cat, CatDto>()
                .Include<Dog, DogDto>();

            CreateMap<Cat, CatDto>();
            CreateMap<Dog, DogDto>();
        }
    }
}

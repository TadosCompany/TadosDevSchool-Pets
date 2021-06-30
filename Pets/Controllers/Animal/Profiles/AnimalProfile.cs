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
                .Include<Dog, DogListItemDto>()
                .Include<Hamster, HamsterListItemDto>();

            CreateMap<Cat, CatListItemDto>();
            CreateMap<Dog, DogListItemDto>();
            CreateMap<Hamster, HamsterListItemDto>();


            CreateMap<Animal, AnimalDto>()
                .ForMember(
                    x => x.Feedings, 
                    x => x.MapFrom(y => y.OrderedFeedings))
                .Include<Cat, CatDto>()
                .Include<Dog, DogDto>()
                .Include<Hamster, HamsterDto>();

            CreateMap<Cat, CatDto>();
            CreateMap<Dog, DogDto>();
            CreateMap<Hamster, HamsterDto>();
        }
    }
}

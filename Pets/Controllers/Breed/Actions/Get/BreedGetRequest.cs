namespace Pets.Controllers.Breed.Actions.Get
{
    using Api.Requests.Abstractions;

    public class BreedGetRequest : IRequest<BreedGetResponse>
    {
        public long Id { get; set; }
    }
}

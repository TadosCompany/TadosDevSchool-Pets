namespace Pets.Controllers.Breed.Actions.Add
{
    using Api.Requests.Abstractions;

    public class BreedAddResponse : IResponse
    {
        public long Id { get; set; }
    }
}

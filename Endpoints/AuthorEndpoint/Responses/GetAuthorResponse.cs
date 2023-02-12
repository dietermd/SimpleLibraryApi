namespace SimpleLibraryApi.Endpoints.AuthorEndpoint.Responses
{
    public class GetAuthorResponse
    {
        public Guid AuthorId { get; set; }
        public string Name { get; set; } = null!;
        public string Country { get; set; } = null!;
        public IEnumerable<Guid> Books { get; set; } = Enumerable.Empty<Guid>();
    }
}

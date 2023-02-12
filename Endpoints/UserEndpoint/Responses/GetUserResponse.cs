namespace SimpleLibraryApi.Endpoints.UserEndpoin.Responses
{
    public class GetUserResponse
    {
        public Guid UserId { get; set; }
        public string Email { get; set; } = null!;
        public IEnumerable<Guid> BookBorrows { get; set; } = Enumerable.Empty<Guid>();
    }
}

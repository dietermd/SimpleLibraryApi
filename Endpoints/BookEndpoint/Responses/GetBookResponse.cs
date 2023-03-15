namespace SimpleLibraryApi.Endpoints.BookEndpoint.Responses
{
    public class GetBookResponse
    {
        public Guid BookId { get; set; }
        public string Title { get; set; } = null!;
        public string ISNB { get; set; } = null!;
        public int Copies { get; set; }
        public int Loaned { get; set; }
        public IEnumerable<Guid> Authors { get; set; } = Enumerable.Empty<Guid>();
    }
}

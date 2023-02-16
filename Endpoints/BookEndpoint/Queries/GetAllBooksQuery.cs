using SimpleLibraryApi.Abstractions;
using SimpleLibraryApi.Endpoints.BookEndpoint.Responses;

namespace SimpleLibraryApi.Endpoints.BookEndpoint.Queries
{
    public class GetAllABooksQuery : IQuery<List<GetBookResponse>>
    {
        public int? Limit { get; set; }
    }
}

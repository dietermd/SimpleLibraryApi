using SimpleLibraryApi.Abstractions;
using SimpleLibraryApi.Endpoints.BookEndpoint.Responses;

namespace SimpleLibraryApi.Endpoints.BookEndpoint.Queries
{
    public class GetBookQuery : IQuery<GetBookResponse?>
    {
        public Guid BookId { get; set; }
    }
}

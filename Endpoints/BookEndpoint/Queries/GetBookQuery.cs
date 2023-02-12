using MediatR;
using SimpleLibraryApi.Endpoints.BookEndpoint.Responses;

namespace SimpleLibraryApi.Endpoints.BookEndpoint.Queries
{
    public class GetBookQuery : IRequest<GetBookResponse?>
    {
        public Guid BookId { get; set; }
    }
}

using MediatR;
using SimpleLibraryApi.Endpoints.BookEndpoint.Responses;

namespace SimpleLibraryApi.Endpoints.BookEndpoint.Queries
{
    public class GetAllABooksQuery : IRequest<List<GetBookResponse>>
    {
        public int? Limit { get; set; }
    }
}

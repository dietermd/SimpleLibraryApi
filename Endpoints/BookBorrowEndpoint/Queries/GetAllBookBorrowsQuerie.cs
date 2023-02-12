using MediatR;
using SimpleLibraryApi.Endpoints.BookBorrowEndpoint.Responses;

namespace SimpleLibraryApi.Endpoints.BookBorrowEndpoint.Queries
{
    public class GetAllBookBorrowsQuerie : IRequest<List<GetBookBorrowResponse>>
    {
        public int? Limit { get; set; }
    }
}

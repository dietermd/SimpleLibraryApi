using MediatR;
using SimpleLibraryApi.Endpoints.BookBorrowEndpoint.Responses;

namespace SimpleLibraryApi.Endpoints.BookBorrowEndpoint.Queries
{
    public class GetBookBorrowQuerie : IRequest<GetBookBorrowResponse?>
    {
        public Guid BookBorrowId { get; set; }
    }
}

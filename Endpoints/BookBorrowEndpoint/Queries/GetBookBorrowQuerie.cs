using SimpleLibraryApi.Abstractions;
using SimpleLibraryApi.Endpoints.BookBorrowEndpoint.Responses;

namespace SimpleLibraryApi.Endpoints.BookBorrowEndpoint.Queries
{
    public class GetBookBorrowQuerie : IQuery<GetBookBorrowResponse?>
    {
        public Guid BookBorrowId { get; set; }
    }
}

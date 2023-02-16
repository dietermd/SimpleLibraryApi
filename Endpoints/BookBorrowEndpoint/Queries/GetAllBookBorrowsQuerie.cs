using SimpleLibraryApi.Abstractions;
using SimpleLibraryApi.Endpoints.BookBorrowEndpoint.Responses;

namespace SimpleLibraryApi.Endpoints.BookBorrowEndpoint.Queries
{
    public class GetAllBookBorrowsQuerie : IQuery<List<GetBookBorrowResponse>>
    {
        public int? Limit { get; set; }
    }
}

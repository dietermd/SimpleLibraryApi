using SimpleLibraryApi.Application.Abstractions;
using SimpleLibraryApi.Endpoints.BookBorrowEndpoint.Responses;

namespace SimpleLibraryApi.Endpoints.BookBorrowEndpoint.Commands
{
    public record UpdateBookBorrowCommand(Guid BookBorrowId) : ICommand<GetBookBorrowResponse?> { }
}

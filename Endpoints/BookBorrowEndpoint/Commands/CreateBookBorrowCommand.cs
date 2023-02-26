using SimpleLibraryApi.Application.Abstractions;
using SimpleLibraryApi.Endpoints.BookBorrowEndpoint.Responses;

namespace SimpleLibraryApi.Endpoints.BookBorrowEndpoint.Commands
{
    public record CreateBookBorrowCommand(Guid UserId, Guid BookId) : ICommand<GetBookBorrowResponse> { }
}

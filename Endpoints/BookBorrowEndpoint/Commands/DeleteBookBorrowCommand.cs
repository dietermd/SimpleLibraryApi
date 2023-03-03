using SimpleLibraryApi.Application.Abstractions;

namespace SimpleLibraryApi.Endpoints.BookBorrowEndpoint.Commands
{
    public record DeleteBookBorrowCommand(Guid BookBorrowId) : ICommand<bool> { }
}

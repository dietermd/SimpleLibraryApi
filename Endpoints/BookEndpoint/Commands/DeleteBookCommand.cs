using SimpleLibraryApi.Application.Abstractions;

namespace SimpleLibraryApi.Endpoints.BookEndpoint.Commands
{
    public record DeleteBookCommand(Guid BookId) : ICommand<bool> { }
}

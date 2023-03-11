using SimpleLibraryApi.Application.Abstractions;

namespace SimpleLibraryApi.Endpoints.AuthorEndpoint.Commands
{
    public record DeleteAuthorCommand(Guid AuthorId) : ICommand<bool> { }
}

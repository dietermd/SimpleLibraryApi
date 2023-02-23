using SimpleLibraryApi.Application.Abstractions;

namespace SimpleLibraryApi.Endpoints.UserEndpoint.Commands
{
    public record DeleteUserCommand(Guid userId) : ICommand<bool> { }
}

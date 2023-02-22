using SimpleLibraryApi.Application.Abstractions;
using SimpleLibraryApi.Endpoints.UserEndpoin.Responses;

namespace SimpleLibraryApi.Endpoints.UserEndpoint.Commands
{
    public sealed record UpdateUserCommand(Guid UserId, string Email, string Password) : ICommand<GetUserResponse?>;
}

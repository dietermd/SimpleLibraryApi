using SimpleLibraryApi.Abstractions;
using SimpleLibraryApi.Endpoints.UserEndpoin.Responses;

namespace SimpleLibraryApi.Endpoints.UserEndpoint.Commands
{
    public sealed record CreateUserCommand(string Email, string Password) : ICommand<GetUserResponse>;
}

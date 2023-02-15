using MediatR;
using SimpleLibraryApi.Endpoints.UserEndpoin.Responses;

namespace SimpleLibraryApi.Endpoints.UserEndpoint.Commands
{
    public class CreateUserCommand : IRequest<GetUserResponse>
    {
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}

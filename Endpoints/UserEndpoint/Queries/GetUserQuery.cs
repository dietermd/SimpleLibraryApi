using MediatR;
using SimpleLibraryApi.Endpoints.UserEndpoin.Responses;

namespace SimpleLibraryApi.Endpoints.UserEndpoint.Queries
{
    public class GetUserQuery : IRequest<GetUserResponse?>
    {
        public Guid UserId { get; set; }
    }
}

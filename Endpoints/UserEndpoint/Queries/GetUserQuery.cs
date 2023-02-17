using SimpleLibraryApi.Application.Abstractions;
using SimpleLibraryApi.Endpoints.UserEndpoin.Responses;

namespace SimpleLibraryApi.Endpoints.UserEndpoint.Queries
{
    public class GetUserQuery : IQuery<GetUserResponse?>
    {
        public Guid UserId { get; set; }
    }
}

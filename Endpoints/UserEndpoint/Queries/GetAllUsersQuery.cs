using MediatR;
using SimpleLibraryApi.Endpoints.UserEndpoin.Responses;

namespace SimpleLibraryApi.Endpoints.UserEndpoint.Queries
{
    public class GetAllUsersQuery : IRequest<List<GetUserResponse>>
    {
        public int? Limit { get; set; }
    }
}

using SimpleLibraryApi.Abstractions;
using SimpleLibraryApi.Endpoints.UserEndpoin.Responses;

namespace SimpleLibraryApi.Endpoints.UserEndpoint.Queries
{
    public class GetAllUsersQuery : IQuery<List<GetUserResponse>>
    {
        public int? Limit { get; set; }
    }
}

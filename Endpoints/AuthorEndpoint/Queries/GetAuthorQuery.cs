using SimpleLibraryApi.Abstractions;
using SimpleLibraryApi.Endpoints.AuthorEndpoint.Responses;

namespace SimpleLibraryApi.Endpoints.AuthorEndpoint.Queries
{
    public class GetAuthorQuery : IQuery<GetAuthorResponse?>
    {
        public Guid AuthorId { get; set; }
    }
}

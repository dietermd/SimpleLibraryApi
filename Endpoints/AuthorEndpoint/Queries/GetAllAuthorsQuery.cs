using SimpleLibraryApi.Abstractions;
using SimpleLibraryApi.Endpoints.AuthorEndpoint.Responses;

namespace SimpleLibraryApi.Endpoints.AuthorEndpoint.Queries
{
    public class GetAllAuthorsQuery : IQuery<List<GetAuthorResponse>>
    {
        public int? Limit { get; set; }
    }
}

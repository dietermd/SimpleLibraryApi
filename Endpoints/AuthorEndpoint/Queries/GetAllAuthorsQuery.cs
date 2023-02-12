using MediatR;
using SimpleLibraryApi.Endpoints.AuthorEndpoint.Responses;

namespace SimpleLibraryApi.Endpoints.AuthorEndpoint.Queries
{
    public class GetAllAuthorsQuery : IRequest<List<GetAuthorResponse>>
    {
        public int? Limit { get; set; }
    }
}

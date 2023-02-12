using MediatR;
using SimpleLibraryApi.Endpoints.AuthorEndpoint.Responses;

namespace SimpleLibraryApi.Endpoints.AuthorEndpoint.Queries
{
    public class GetAuthorQuery : IRequest<GetAuthorResponse?>
    {
        public Guid AuthorId { get; set; }
    }
}

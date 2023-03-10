using SimpleLibraryApi.Application.Abstractions;
using SimpleLibraryApi.Endpoints.AuthorEndpoint.Responses;

namespace SimpleLibraryApi.Endpoints.AuthorEndpoint.Commands
{
    public record UpdateAuthorCommand(Guid AuthorId, string Name, string Country) : ICommand<GetAuthorResponse?> { }
}

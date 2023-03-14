using SimpleLibraryApi.Application.Abstractions;
using SimpleLibraryApi.Endpoints.BookEndpoint.Responses;

namespace SimpleLibraryApi.Endpoints.BookEndpoint.Commands
{
    public record CreateBookCommand(string Title, string ISBN, int Copies, List<Guid> Authors) : ICommand<GetBookResponse> { }
}

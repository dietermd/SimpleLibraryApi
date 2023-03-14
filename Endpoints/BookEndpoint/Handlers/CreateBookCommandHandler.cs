using MediatR;
using SimpleLibraryApi.Endpoints.BookEndpoint.Commands;
using SimpleLibraryApi.Endpoints.BookEndpoint.Responses;

namespace SimpleLibraryApi.Endpoints.BookEndpoint.Handlers
{
    public class CreateBookCommandHandler : IRequestHandler<CreateBookCommand, GetBookResponse>
    {
        public Task<GetBookResponse> Handle(CreateBookCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}

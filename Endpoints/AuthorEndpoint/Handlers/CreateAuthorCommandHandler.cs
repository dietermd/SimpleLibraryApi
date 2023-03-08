using MediatR;
using SimpleLibraryApi.Endpoints.AuthorEndpoint.Commands;
using SimpleLibraryApi.Endpoints.AuthorEndpoint.Responses;
using SimpleLibraryApi.Models;
using SimpleLibraryApi.Models.Context;

namespace SimpleLibraryApi.Endpoints.AuthorEndpoint.Handlers
{
    public class CreateAuthorCommandHandler : IRequestHandler<CreateAuthorCommand, GetAuthorResponse>
    {
        private readonly ApiDbContext _dbContext;

        public CreateAuthorCommandHandler(ApiDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<GetAuthorResponse> Handle(CreateAuthorCommand request, CancellationToken cancellationToken)
        {
            var author = new Author { Name = request.Name, Country = request.Country };

            await _dbContext.Author.AddAsync(author, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return new GetAuthorResponse
            {
                AuthorId = author.AuthorId,
                Name = author.Name,
                Country = author.Country,
                Books = author.BookAuthor.Select(x => x.BookId)
            };
        }
    }
}

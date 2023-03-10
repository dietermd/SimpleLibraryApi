using MediatR;
using Microsoft.EntityFrameworkCore;
using SimpleLibraryApi.Endpoints.AuthorEndpoint.Commands;
using SimpleLibraryApi.Endpoints.AuthorEndpoint.Responses;
using SimpleLibraryApi.Models;
using SimpleLibraryApi.Models.Context;

namespace SimpleLibraryApi.Endpoints.AuthorEndpoint.Handlers
{
    public class UpdateAuthorCommandHandler : IRequestHandler<UpdateAuthorCommand, GetAuthorResponse?>
    {
        private readonly ApiDbContext _dbContext;

        public UpdateAuthorCommandHandler(ApiDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<GetAuthorResponse?> Handle(UpdateAuthorCommand request, CancellationToken cancellationToken)
        {
            var author = await _dbContext.Author.FirstOrDefaultAsync(x => x.AuthorId == request.AuthorId, cancellationToken);

            if (author is null)
                return null;

            author.Name = request.Name;
            author.Country = request.Country;

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

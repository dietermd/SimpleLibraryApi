using MediatR;
using Microsoft.EntityFrameworkCore;
using SimpleLibraryApi.Endpoints.AuthorEndpoint.Queries;
using SimpleLibraryApi.Endpoints.AuthorEndpoint.Responses;
using SimpleLibraryApi.Models.Context;

namespace SimpleLibraryApi.Endpoints.AuthorEndpoint.Handlers
{
    public class GetAuthorQueryHandler : IRequestHandler<GetAuthorQuery, GetAuthorResponse?>
    {
        private readonly ApiDbContext _dbContext;

        public GetAuthorQueryHandler(ApiDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<GetAuthorResponse?> Handle(GetAuthorQuery request, CancellationToken cancellationToken)
        {
            _dbContext.Database.EnsureCreated();
            return await _dbContext.Author.Include(x => x.BookAuthor).Where(x => x.AuthorId == request.AuthorId)
                .Select(x =>
                new GetAuthorResponse
                {
                    AuthorId = x.AuthorId,
                    Name = x.Name,
                    Country = x.Country,
                    Books = x.BookAuthor.Select(y => y.BookId)
                }).FirstOrDefaultAsync(cancellationToken);
        }
    }
}

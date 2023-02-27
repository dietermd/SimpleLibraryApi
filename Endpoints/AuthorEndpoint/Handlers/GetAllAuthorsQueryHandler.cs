using MediatR;
using Microsoft.EntityFrameworkCore;
using SimpleLibraryApi.Endpoints.AuthorEndpoint.Queries;
using SimpleLibraryApi.Endpoints.AuthorEndpoint.Responses;
using SimpleLibraryApi.Models.Context;

namespace SimpleLibraryApi.Endpoints.AuthorEndpoint.Handlers
{
    public class GetAllAuthorsQueryHandler : IRequestHandler<GetAllAuthorsQuery, List<GetAuthorResponse>>
    {
        private readonly ApiDbContext _dbContext;

        public GetAllAuthorsQueryHandler(ApiDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<GetAuthorResponse>> Handle(GetAllAuthorsQuery request, CancellationToken cancellationToken)
        {
            var authors = request.Limit.HasValue ? _dbContext.Author.Include(x => x.BookAuthor).Take(request.Limit.Value) : _dbContext.Author.Include(x => x.BookAuthor);

            return await authors
                .Select(x => new GetAuthorResponse
                {
                    AuthorId = x.AuthorId,
                    Name = x.Name,
                    Country = x.Country,
                    Books = x.BookAuthor.Select(y => y.BookId)
                })
                .ToListAsync(cancellationToken);
        }
    }
}

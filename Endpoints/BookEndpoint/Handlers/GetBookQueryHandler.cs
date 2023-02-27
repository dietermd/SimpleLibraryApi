using MediatR;
using Microsoft.EntityFrameworkCore;
using SimpleLibraryApi.Endpoints.BookEndpoint.Queries;
using SimpleLibraryApi.Endpoints.BookEndpoint.Responses;
using SimpleLibraryApi.Models.Context;

namespace SimpleLibraryApi.Endpoints.BookEndpoint.Handlers
{
    public class GetBookQueryHandler : IRequestHandler<GetBookQuery, GetBookResponse?>
    {
        private readonly ApiDbContext _dbContext;
        public GetBookQueryHandler(ApiDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<GetBookResponse?> Handle(GetBookQuery request, CancellationToken cancellationToken)
        {
            return await _dbContext.Book.Include(x => x.BookAuthor).Include(x => x.BookBorrow)
                .Where(x => x.BookId == request.BookId)
                .Select(x => new GetBookResponse
                {
                    BookId = x.BookId,
                    Title = x.Title,
                    Copies = x.Copies,
                    Loaned = x.BookBorrow.Where(y => !y.ReturnDate.HasValue).Count(),
                    Authors = x.BookAuthor.Select(y => y.AuthorId).ToList()
                })
                .FirstOrDefaultAsync(cancellationToken: cancellationToken);
        }
    }
}

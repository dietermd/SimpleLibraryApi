using MediatR;
using Microsoft.EntityFrameworkCore;
using SimpleLibraryApi.Endpoints.BookEndpoint.Queries;
using SimpleLibraryApi.Endpoints.BookEndpoint.Responses;
using SimpleLibraryApi.Models.Context;

namespace SimpleLibraryApi.Endpoints.BookEndpoint.Handlers
{
    public class GetAllBooksQueryHandler : IRequestHandler<GetAllABooksQuery, List<GetBookResponse>>
    {
        private readonly ApiDbContext _dbContext;
        public GetAllBooksQueryHandler(ApiDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<GetBookResponse>> Handle(GetAllABooksQuery request, CancellationToken cancellationToken)
        {
            _dbContext.Database.EnsureCreated();

            var books = request.Limit.HasValue ? _dbContext.Book.Include(x => x.BookAuthor).Include(x => x.BookBorrow).Take(request.Limit.Value) : _dbContext.Book.Include(x => x.BookAuthor).Include(x => x.BookBorrow);

            return await books
                .Select(x => new GetBookResponse
                {
                    BookId = x.BookId,
                    Title = x.Title,
                    Copies = x.Copies,
                    Loaned = x.BookBorrow.Where(y => !y.ReturnDate.HasValue).Count(),
                    Authors = x.BookAuthor.Select(y => y.AuthorId).ToList()
                })
                .ToListAsync(cancellationToken: cancellationToken);
        }
    }
}

using MediatR;
using Microsoft.EntityFrameworkCore;
using SimpleLibraryApi.Endpoints.BookBorrowEndpoint.Queries;
using SimpleLibraryApi.Endpoints.BookBorrowEndpoint.Responses;
using SimpleLibraryApi.Models.Context;

namespace SimpleLibraryApi.Endpoints.BookBorrowEndpoint.Handlers
{
    public class GetAllBookBorrowsQuerieHandler : IRequestHandler<GetAllBookBorrowsQuerie, List<GetBookBorrowResponse>>
    {
        private readonly ApiDbContext _dbContext;

        public GetAllBookBorrowsQuerieHandler(ApiDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<GetBookBorrowResponse>> Handle(GetAllBookBorrowsQuerie request, CancellationToken cancellationToken)
        {
            _dbContext.Database.EnsureCreated();
            var bookBorrows = request.Limit.HasValue ? _dbContext.BookBorrow.Take(request.Limit.Value) : _dbContext.BookBorrow;

            return await bookBorrows
                .Select(x => new GetBookBorrowResponse
                {
                    BookBorrowId = x.BookBorrowId,
                    UserId = x.UserId,
                    BookId = x.BookId,
                    BorrowDate = x.BorrowDate,
                    ReturnDate = x.ReturnDate
                })
                .ToListAsync(cancellationToken);
        }
    }
}

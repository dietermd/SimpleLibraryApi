using MediatR;
using Microsoft.EntityFrameworkCore;
using SimpleLibraryApi.Endpoints.BookBorrowEndpoint.Queries;
using SimpleLibraryApi.Endpoints.BookBorrowEndpoint.Responses;
using SimpleLibraryApi.Models.Context;

namespace SimpleLibraryApi.Endpoints.BookBorrowEndpoint.Handlers
{
    public class GetBookBorrowQuerieHandler : IRequestHandler<GetBookBorrowQuerie, GetBookBorrowResponse?>
    {
        private readonly ApiDbContext _dbContext;

        public GetBookBorrowQuerieHandler(ApiDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<GetBookBorrowResponse?> Handle(GetBookBorrowQuerie request, CancellationToken cancellationToken)
        {
            return await _dbContext.BookBorrow.Where(x => x.BookBorrowId == request.BookBorrowId)
                .Select(x => new GetBookBorrowResponse 
                {
                    BookBorrowId = x.BookBorrowId,
                    UserId = x.UserId,
                    BookId = x.BookId,
                    BorrowDate = x.BorrowDate,
                    ReturnDate = x.ReturnDate
                })
                .FirstOrDefaultAsync(cancellationToken);
        }
    }
}

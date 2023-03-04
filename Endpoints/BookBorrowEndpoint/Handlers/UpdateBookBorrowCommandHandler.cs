using MediatR;
using Microsoft.EntityFrameworkCore;
using SimpleLibraryApi.Endpoints.BookBorrowEndpoint.Commands;
using SimpleLibraryApi.Endpoints.BookBorrowEndpoint.Responses;
using SimpleLibraryApi.Models;
using SimpleLibraryApi.Models.Context;

namespace SimpleLibraryApi.Endpoints.BookBorrowEndpoint.Handlers
{
    public class UpdateBookBorrowCommandHandler : IRequestHandler<UpdateBookBorrowCommand, GetBookBorrowResponse?>
    {
        private readonly ApiDbContext _dbContext;

        public UpdateBookBorrowCommandHandler(ApiDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<GetBookBorrowResponse?> Handle(UpdateBookBorrowCommand request, CancellationToken cancellationToken)
        {
            var bookBorrow = await _dbContext.BookBorrow.Include(x => x.Book).FirstOrDefaultAsync(x => x.BookBorrowId == request.BookBorrowId, cancellationToken);

            if (bookBorrow is null)
                return null;

            bookBorrow.ReturnDate = DateTime.Now;
            bookBorrow.Book.Copies += 1;

            await _dbContext.SaveChangesAsync(cancellationToken);

            return new GetBookBorrowResponse
            {
                BookBorrowId = bookBorrow.BookBorrowId,
                BorrowDate = bookBorrow.BorrowDate,
                ReturnDate = bookBorrow.ReturnDate,
                UserId = bookBorrow.UserId,
                BookId = bookBorrow.BookId
            };
        }
    }
}

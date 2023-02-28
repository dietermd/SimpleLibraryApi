using MediatR;
using Microsoft.EntityFrameworkCore;
using SimpleLibraryApi.Endpoints.BookBorrowEndpoint.Commands;
using SimpleLibraryApi.Endpoints.BookBorrowEndpoint.Responses;
using SimpleLibraryApi.Models;
using SimpleLibraryApi.Models.Context;

namespace SimpleLibraryApi.Endpoints.BookBorrowEndpoint.Handlers
{
    public class CreateBookBorrowCommandHandler : IRequestHandler<CreateBookBorrowCommand, GetBookBorrowResponse>
    {
        private readonly ApiDbContext _dbContext;

        public CreateBookBorrowCommandHandler(ApiDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<GetBookBorrowResponse> Handle(CreateBookBorrowCommand request, CancellationToken cancellationToken)
        {
            var book = await _dbContext.Book.FirstAsync(x => x.BookId == request.BookId, cancellationToken);
            book.Copies -= 1;

            var bookBorrow = new BookBorrow
            {
                UserId = request.UserId,
                BookId = request.BookId,
                BorrowDate = DateTime.Now,
            };

            await _dbContext.BookBorrow.AddAsync(bookBorrow, cancellationToken);
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

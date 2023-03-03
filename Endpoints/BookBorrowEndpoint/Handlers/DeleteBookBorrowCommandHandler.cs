using MediatR;
using Microsoft.EntityFrameworkCore;
using SimpleLibraryApi.Endpoints.BookBorrowEndpoint.Commands;
using SimpleLibraryApi.Models.Context;
using ApplicationException = SimpleLibraryApi.Application.Exceptions.ApplicationException;

namespace SimpleLibraryApi.Endpoints.BookBorrowEndpoint.Handlers
{
    public class DeleteBookBorrowCommandHandler : IRequestHandler<DeleteBookBorrowCommand, bool>
    {
        private readonly ApiDbContext _dbContext;

        public DeleteBookBorrowCommandHandler(ApiDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> Handle(DeleteBookBorrowCommand request, CancellationToken cancellationToken)
        {
            var bookBorrow = await _dbContext.BookBorrow.FirstOrDefaultAsync(x => x.BookBorrowId == request.BookBorrowId, cancellationToken);

            if (bookBorrow == null)
                return false;

            if (!bookBorrow.ReturnDate.HasValue)
                throw new ApplicationException("Unable to delete BookBorrow", "The book has not been returned");

            _dbContext.BookBorrow.Remove(bookBorrow);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}

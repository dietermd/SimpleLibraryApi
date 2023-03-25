using MediatR;
using Microsoft.EntityFrameworkCore;
using SimpleLibraryApi.Endpoints.BookEndpoint.Commands;
using SimpleLibraryApi.Models.Context;
using ApplicationException = SimpleLibraryApi.Application.Exceptions.ApplicationException;

namespace SimpleLibraryApi.Endpoints.BookEndpoint.Handlers
{
    public class DeleteBookCommandHandler : IRequestHandler<DeleteBookCommand, bool>
    {
        private readonly ApiDbContext _dbContext;

        public DeleteBookCommandHandler(ApiDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> Handle(DeleteBookCommand request, CancellationToken cancellationToken)
        {
            var book = await _dbContext.Book.Include(x => x.BookBorrow).FirstOrDefaultAsync(x => x.BookId == request.BookId,cancellationToken);

            if (book is null)
                return false;

            if (book.BookBorrow.Any(x => !x.ReturnDate.HasValue))
                throw new ApplicationException("Unable to delete book", "All loaned books must be returned first");

            _dbContext.Book.Remove(book);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}

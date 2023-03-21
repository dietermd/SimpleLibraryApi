using MediatR;
using Microsoft.EntityFrameworkCore;
using SimpleLibraryApi.Endpoints.BookEndpoint.Commands;
using SimpleLibraryApi.Endpoints.BookEndpoint.Responses;
using SimpleLibraryApi.Models.Context;

namespace SimpleLibraryApi.Endpoints.BookEndpoint.Handlers
{
    public class UpdateBookCommandHandler : IRequestHandler<UpdateBookCommand, GetBookResponse?>
    {
        private readonly ApiDbContext _dbContext;

        public UpdateBookCommandHandler(ApiDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<GetBookResponse?> Handle(UpdateBookCommand request, CancellationToken cancellationToken)
        {
            var book = await _dbContext.Book.Include(x => x.BookAuthor).Include(x => x.BookBorrow).FirstOrDefaultAsync(x => x.BookId == request.BookId, cancellationToken);

            if (book is null)
                return null;

            book.Title = request.Title;
            book.ISBN = request.ISBN;
            book.Copies = request.Copies;
            //authors logic here

            await _dbContext.SaveChangesAsync(cancellationToken);

            return new GetBookResponse
            {
                BookId = book.BookId,
                Title = book.Title,
                ISNB = book.ISBN,
                Copies = book.Copies,
                Loaned = book.BookBorrow.Count(),
                Authors = book.BookAuthor.Select(x => x.AuthorId)
            };
        }
    }
}

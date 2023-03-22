using MediatR;
using Microsoft.EntityFrameworkCore;
using SimpleLibraryApi.Endpoints.BookEndpoint.Commands;
using SimpleLibraryApi.Endpoints.BookEndpoint.Responses;
using SimpleLibraryApi.Models;
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
            var removedAuthors = book.BookAuthor.Where(x => !request.Authors.Contains(x.AuthorId));
            _dbContext.BookAuthor.RemoveRange(removedAuthors);
            request.Authors.RemoveAll(x => book.BookAuthor.Select(y => y.AuthorId).Contains(x));
            
            foreach (var authorId in request.Authors)
            {
                book.BookAuthor.Add(new BookAuthor
                {
                    AuthorId = authorId,
                    BookId = book.BookId,
                });
            }

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

using MediatR;
using Microsoft.EntityFrameworkCore;
using SimpleLibraryApi.Endpoints.BookEndpoint.Commands;
using SimpleLibraryApi.Endpoints.BookEndpoint.Responses;
using SimpleLibraryApi.Models;
using SimpleLibraryApi.Models.Context;

namespace SimpleLibraryApi.Endpoints.BookEndpoint.Handlers
{
    public class CreateBookCommandHandler : IRequestHandler<CreateBookCommand, GetBookResponse>
    {
        private readonly ApiDbContext _dbContext;

        public CreateBookCommandHandler(ApiDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<GetBookResponse> Handle(CreateBookCommand request, CancellationToken cancellationToken)
        {
            var book = new Book
            {
                Title = request.Title,
                ISBN = request.ISBN,
                Copies = request.Copies,
            };

            if (request.Authors.Any())
            {
                var bookAuthors = request.Authors.Select(id => new BookAuthor
                {
                    AuthorId = id,
                    BookId = book.BookId
                }).ToList();

                book.BookAuthor = bookAuthors;
            }

            await _dbContext.Book.AddAsync(book, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return new GetBookResponse
            {
                BookId = book.BookId,
                Title = book.Title,
                ISNB = book.ISBN,
                Copies = book.Copies,
                Authors = book.BookAuthor.Select(x => x.AuthorId),
                Loaned = book.BookBorrow.Count()
            };
        }
    }
}

using MediatR;
using Microsoft.EntityFrameworkCore;
using SimpleLibraryApi.Endpoints.AuthorEndpoint.Commands;
using SimpleLibraryApi.Models.Context;
using ApplicationException = SimpleLibraryApi.Application.Exceptions.ApplicationException;

namespace SimpleLibraryApi.Endpoints.AuthorEndpoint.Handlers
{
    public class DeleteAuthorCommandHandler : IRequestHandler<DeleteAuthorCommand, bool>
    {
        private readonly ApiDbContext _dbContext;

        public DeleteAuthorCommandHandler(ApiDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> Handle(DeleteAuthorCommand request, CancellationToken cancellationToken)
        {
            var author = await _dbContext.Author.Include(x => x.BookAuthor).FirstOrDefaultAsync(x => x.AuthorId == request.AuthorId, cancellationToken);

            if (author is null)
                return false;

            _dbContext.Author.Remove(author);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}

using MediatR;
using Microsoft.EntityFrameworkCore;
using SimpleLibraryApi.Endpoints.UserEndpoint.Commands;
using SimpleLibraryApi.Models.Context;
using ApplicationException = SimpleLibraryApi.Application.Exceptions.ApplicationException;

namespace SimpleLibraryApi.Endpoints.UserEndpoint.Handlers
{
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, bool>
    {
        private readonly ApiDbContext _dbContext;

        public DeleteUserCommandHandler(ApiDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            await _dbContext.Database.EnsureCreatedAsync(cancellationToken);

            var user = await _dbContext.User.Include(x => x.BookBorow).FirstOrDefaultAsync(x => x.UserId == request.userId, cancellationToken);

            if (user == null)
                return false;

            if (user.BookBorow.Any(x => !x.ReturnDate.HasValue))
                throw new ApplicationException("Unable to delete user", "The user has one or more book to be returned.");

            _dbContext.User.Remove(user);
            _dbContext.SaveChanges();

            return true;
        }
    }
}

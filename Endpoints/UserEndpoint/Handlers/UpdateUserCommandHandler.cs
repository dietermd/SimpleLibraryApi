using MediatR;
using Microsoft.EntityFrameworkCore;
using SimpleLibraryApi.Endpoints.UserEndpoin.Responses;
using SimpleLibraryApi.Endpoints.UserEndpoint.Commands;
using SimpleLibraryApi.Models.Context;

namespace SimpleLibraryApi.Endpoints.UserEndpoint.Handlers
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, GetUserResponse?>
    {
        private readonly ApiDbContext _dbContext;

        public UpdateUserCommandHandler(ApiDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<GetUserResponse?> Handle(UpdateUserCommand command, CancellationToken cancellationToken)
        {
            var user = await _dbContext.User.Include(x => x.BookBorow).FirstOrDefaultAsync(x => x.UserId == command.UserId, cancellationToken);
            if (user is null)
                return null;

            user.Email = command.Email;
            user.Password = command.Password;

            _dbContext.Update(user);
            _dbContext.SaveChanges();

            return new GetUserResponse
            {
                UserId = user.UserId,
                Email = user.Email,
                BookBorrows = user.BookBorow.Select(x => x.BookBorrowId)
            };
        }
    }
}

using MediatR;
using ValidationException = SimpleLibraryApi.Application.Exceptions.ValidationException;
using SimpleLibraryApi.Endpoints.UserEndpoin.Responses;
using SimpleLibraryApi.Endpoints.UserEndpoint.Commands;
using SimpleLibraryApi.Models;
using SimpleLibraryApi.Models.Context;

namespace SimpleLibraryApi.Endpoints.UserEndpoint.Handlers
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, GetUserResponse>
    {
        private readonly ApiDbContext _dbContext;

        public CreateUserCommandHandler(ApiDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<GetUserResponse> Handle(CreateUserCommand command, CancellationToken cancellationToken)
        {
            _dbContext.Database.EnsureCreated();

            var user = new User { Email = command.Email, Password = command.Password };

            await _dbContext.User.AddAsync(user, cancellationToken);
            _dbContext.SaveChanges();

            return new GetUserResponse
            {
                UserId = user.UserId,
                Email = user.Email,
                BookBorrows = user.BookBorow.Select(x => x.BookBorrowId),
            };
        }
    }
}

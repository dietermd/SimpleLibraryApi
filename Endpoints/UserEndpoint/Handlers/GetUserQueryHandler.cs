using MediatR;
using Microsoft.EntityFrameworkCore;
using SimpleLibraryApi.Endpoints.UserEndpoin.Responses;
using SimpleLibraryApi.Endpoints.UserEndpoint.Queries;
using SimpleLibraryApi.Models.Context;

namespace SimpleLibraryApi.Endpoints.UserEndpoint.Handlers
{
    public class GetUserQueryHandler : IRequestHandler<GetUserQuery, GetUserResponse?>
    {
        private readonly ApiDbContext _dbContext;
        public GetUserQueryHandler(ApiDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<GetUserResponse?> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            _dbContext.Database.EnsureCreated();
            return await _dbContext.User.Include(x => x.BookBorow).Where(x => x.UserId == request.UserId)
                .Select(x => new GetUserResponse
                {
                    UserId = x.UserId,
                    Email = x.Email,
                    BookBorrows = x.BookBorow.Select(y => y.BookBorrowId).ToList()
                }).FirstOrDefaultAsync(cancellationToken: cancellationToken);
        }
    }
}

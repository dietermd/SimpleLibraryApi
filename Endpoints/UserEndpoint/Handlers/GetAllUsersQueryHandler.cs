using MediatR;
using Microsoft.EntityFrameworkCore;
using SimpleLibraryApi.Endpoints.UserEndpoin.Responses;
using SimpleLibraryApi.Endpoints.UserEndpoint.Queries;
using SimpleLibraryApi.Models.Context;

namespace SimpleLibraryApi.Endpoints.UserEndpoint.Handlers
{
    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, List<GetUserResponse>>
    {
        private readonly ApiDbContext _dbContext;
        public GetAllUsersQueryHandler(ApiDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<GetUserResponse>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            _dbContext.Database.EnsureCreated();
            var users = request.Limit.HasValue ? _dbContext.User.Include(x => x.BookBorow).Take(request.Limit.Value) : _dbContext.User.Include(x => x.BookBorow);

            return await users
                .Select( x => new GetUserResponse
                {
                    UserId = x.UserId,
                    Email = x.Email,
                    BookBorrows = x.BookBorow.Select(y => y.BookBorrowId)
                })
                .ToListAsync(cancellationToken);
        }
    }
}

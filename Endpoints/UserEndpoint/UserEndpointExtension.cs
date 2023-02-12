using MediatR;
using SimpleLibraryApi.Endpoints.UserEndpoint.Queries;

namespace SimpleLibraryApi.Endpoints.UserEndpoint
{
    public static class UserEndpointExtension
    {
        public static WebApplication MapUserEndpoints(this WebApplication app)
        {
            app.MapGet("/users", async (int? limit, IMediator mediator, CancellationToken cancellationToken) =>
            {
                var response = await mediator.Send(new GetAllUsersQuery { Limit = limit }, cancellationToken);
                return Results.Ok(response);
            });

            app.MapGet("/users/{userId}", async (Guid userId, IMediator mediator, CancellationToken cancellationToken) =>
            {
                var response = await mediator.Send(new GetUserQuery { UserId = userId }, cancellationToken);
                return response is null ? Results.NotFound() : Results.Ok(response);
            });

            return app;
        }
    }
}

using MediatR;
using SimpleLibraryApi.Endpoints.UserEndpoint.Commands;
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
            })
            .WithName("GetUserById");

            app.MapPost("/users", async (CreateUserCommand createUserCommand, IMediator mediator, CancellationToken cancellationToken) =>
            {
                var result = await mediator.Send(createUserCommand, cancellationToken);
                return Results.CreatedAtRoute("GetUserById", new { result.UserId }, result);
            });

            app.MapPut("/users/{userId}", async (Guid userId, CreateUserCommand createUserCommand, IMediator mediator, CancellationToken cancellationToken) =>
            {
                var response = await mediator.Send(new UpdateUserCommand(userId, createUserCommand.Email, createUserCommand.Password), cancellationToken);
                return response is null ? Results.NotFound() : Results.Ok(response);
            });

            return app;
        }
    }
}

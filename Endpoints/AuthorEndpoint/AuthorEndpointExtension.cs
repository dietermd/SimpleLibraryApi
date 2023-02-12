using MediatR;
using SimpleLibraryApi.Endpoints.AuthorEndpoint.Queries;

namespace SimpleLibraryApi.Endpoints.AuthorEndpoint
{
    public static class AuthorEndpointExtension
    {
        public static WebApplication MapAuthorEndpoints(this WebApplication app)
        {
            app.MapGet("/authors", async (int? limit, IMediator mediator, CancellationToken cancellationToken) =>
            {
                var response = await mediator.Send(new GetAllAuthorsQuery { Limit = limit }, cancellationToken);
                return Results.Ok(response);
            });

            app.MapGet("/authors/{authorId}", async (Guid authorId, IMediator mediator, CancellationToken cancellationToken) =>
            {
                var response = await mediator.Send(new GetAuthorQuery { AuthorId = authorId }, cancellationToken);
                return response is null ? Results.NotFound() : Results.Ok(response);
            });

            return app;
        }
    }
}

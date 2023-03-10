using MediatR;
using SimpleLibraryApi.Endpoints.BookEndpoint.Queries;

namespace SimpleLibraryApi.Endpoints.BookEndpoint
{
    public static class BookEndpointExtension
    {
        public static WebApplication MapBookEndpoints(this WebApplication app)
        {
            app.MapGet("/books", async (int? limit, IMediator mediator, CancellationToken cancellationToken) =>
            {
                var response = await mediator.Send(new GetAllABooksQuery { Limit = limit }, cancellationToken);
                return Results.Ok(response);
            });

            app.MapGet("/books/{bookId}", async (Guid authorId, IMediator mediator, CancellationToken cancellationToken) =>
            {
                var response = await mediator.Send(new GetBookQuery { BookId = authorId }, cancellationToken);
                return response is null ? Results.NotFound() : Results.Ok(response);
            });

            return app;
        }
    }
}

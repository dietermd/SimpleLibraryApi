using MediatR;
using SimpleLibraryApi.Endpoints.BookEndpoint.Commands;
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

            app.MapGet("/books/{bookId}", async (Guid bookId, IMediator mediator, CancellationToken cancellationToken) =>
            {
                var response = await mediator.Send(new GetBookQuery { BookId = bookId }, cancellationToken);
                return response is null ? Results.NotFound() : Results.Ok(response);
            })
            .WithName("GetBookById");

            app.MapPost("/books", async (CreateBookCommand createBookCommand, IMediator mediator, CancellationToken cancellationToken) =>
            {
                var result = await mediator.Send(createBookCommand, cancellationToken);
                return Results.CreatedAtRoute("GetBookById", new { result.BookId }, result);
            });

            app.MapPut("/books/{bookId}", async (Guid bookId, UpdateBookCommand updateBookCommand, IMediator mediator, CancellationToken cancellationToken) =>
            {
                var result = await mediator.Send(updateBookCommand, cancellationToken);
                return result is null ? Results.NotFound() : Results.Ok(result);
            });

            return app;
        }
    }
}

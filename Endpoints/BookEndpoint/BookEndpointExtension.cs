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
            })
            .RequireAuthorization("admin");

            app.MapPut("/books/{bookId}", async (Guid bookId, CreateBookCommand createBookCommand, IMediator mediator, CancellationToken cancellationToken) =>
            {
                var result = await mediator.Send(new UpdateBookCommand(bookId, createBookCommand.Title, createBookCommand.ISBN, createBookCommand.Copies, createBookCommand.Authors), cancellationToken);
                return result is null ? Results.NotFound() : Results.Ok(result);
            })
            .RequireAuthorization("admin");

            app.MapDelete("/books/{bookId}", async (Guid bookId, IMediator mediator, CancellationToken cancellationToken) =>
            {
                var deleted = await mediator.Send(new DeleteBookCommand(bookId), cancellationToken);
                return deleted ? Results.Ok() : Results.NotFound();
            })
            .RequireAuthorization("admin");

            return app;
        }
    }
}

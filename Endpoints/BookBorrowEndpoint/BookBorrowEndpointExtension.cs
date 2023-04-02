using MediatR;
using SimpleLibraryApi.Endpoints.BookBorrowEndpoint.Commands;
using SimpleLibraryApi.Endpoints.BookBorrowEndpoint.Queries;

namespace SimpleLibraryApi.Endpoints.BookBorrowEndpoint
{
    public static class BookBorrowEndpointExtension
    {
        public static WebApplication MapBookBorrowEnpoints(this WebApplication app)
        {
            app.MapGet("/bookBorrows", async (int? limit, IMediator mediator, CancellationToken cancellationToken) =>
            {
                var response = await mediator.Send(new GetAllBookBorrowsQuerie {  Limit = limit }, cancellationToken);
                return Results.Ok(response);
            });

            app.MapGet("/bookBorrows/{bookBorrowId}", async (Guid bookBorrowId, IMediator mediator, CancellationToken cancellationToken) =>
            {
                var response = await mediator.Send(new GetBookBorrowQuerie { BookBorrowId = bookBorrowId}, cancellationToken);
                return response is null ? Results.NotFound() : Results.Ok(response);
            })
            .WithName("CreateBookBorrowById");

            app.MapPost("/bookBorrows", async (CreateBookBorrowCommand createBookBorrowCommand, IMediator mediator, CancellationToken cancellationToken) =>
            {
                var result = await mediator.Send(createBookBorrowCommand, cancellationToken);
                return Results.CreatedAtRoute("CreateBookBorrowById", new { result.BookBorrowId }, result);
            })
            .RequireAuthorization("admin");

            app.MapPut("/bookBorrows/{bookBorrowId}", async (Guid bookBorrowId, IMediator mediator, CancellationToken cancellationToken) =>
            {
                var response = await mediator.Send(new UpdateBookBorrowCommand(bookBorrowId), cancellationToken);
                return response is null ? Results.NotFound() : Results.Ok(response);
            })
            .RequireAuthorization("admin");

            app.MapDelete("/bookBorrows/{bookBorrowId}", async (Guid bookBorrowId, IMediator mediator, CancellationToken cancellationToken) =>
            {
                var deleted = await mediator.Send(new DeleteBookBorrowCommand(bookBorrowId), cancellationToken);
                return deleted ? Results.NoContent() : Results.NotFound();
            })
            .RequireAuthorization("admin");

            return app;
        }
    }
}

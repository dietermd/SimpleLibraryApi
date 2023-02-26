using MediatR;
using SimpleLibraryApi.Endpoints.BookBorrowEndpoint.Commands;
using SimpleLibraryApi.Endpoints.BookBorrowEndpoint.Queries;

namespace SimpleLibraryApi.Endpoints.BookBorrowEndpoint
{
    public static class BookBorrowEndpointExtension
    {
        public static WebApplication MapBookBorrowEnpoints(this WebApplication app)
        {
            app.MapGet("/BookBorrows", async (int? limit, IMediator mediator, CancellationToken cancellationToken) =>
            {
                var response = await mediator.Send(new GetAllBookBorrowsQuerie {  Limit = limit }, cancellationToken);
                return Results.Ok(response);
            });

            app.MapGet("/BookBorrows/{bookBorrowId}", async (Guid bookBorrowId, IMediator mediator, CancellationToken cancellationToken) =>
            {
                var response = await mediator.Send(new GetBookBorrowQuerie { BookBorrowId = bookBorrowId}, cancellationToken);
                return response is null ? Results.NotFound() : Results.Ok(response);
            })
            .WithName("CreateBookBorrowById");

            app.MapPost("/BookBorrows", async (CreateBookBorrowCommand createBookBorrowCommand, IMediator mediator, CancellationToken cancellationToken) =>
            {
                var result = await mediator.Send(createBookBorrowCommand, cancellationToken);
                return Results.CreatedAtRoute("CreateBookBorrowById", new { result.BookBorrowId }, result);
            });

            return app;
        }
    }
}

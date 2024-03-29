﻿using MediatR;
using SimpleLibraryApi.Endpoints.AuthorEndpoint.Commands;
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
            })
            .RequireAuthorization()
            .WithTags("Authors");

            app.MapGet("/authors/{authorId}", async (Guid authorId, IMediator mediator, CancellationToken cancellationToken) =>
            {
                var response = await mediator.Send(new GetAuthorQuery { AuthorId = authorId }, cancellationToken);
                return response is null ? Results.NotFound() : Results.Ok(response);
            })
            .WithName("CreateAuthorById")
            .RequireAuthorization()
            .WithTags("Authors");

            app.MapPost("/authors", async (CreateAuthorCommand createAuthorCommand, IMediator mediator, CancellationToken cancellationToken) =>
            {
                var result = await mediator.Send(createAuthorCommand, cancellationToken);
                return Results.CreatedAtRoute("CreateAuthorById",  new { result.AuthorId }, result);
            })
            .RequireAuthorization("admin_role")
            .WithTags("Authors");

            app.MapPut("/authors/{authorId}", async (Guid authorId, CreateAuthorCommand createAuthorCommand, IMediator mediator, CancellationToken cancellationToken) =>
            {
                var response = await mediator.Send(new UpdateAuthorCommand(authorId, createAuthorCommand.Name, createAuthorCommand.Country), cancellationToken);
                return response is null ? Results.NotFound() : Results.Ok(response);
            })
            .RequireAuthorization("admin_role")
            .WithTags("Authors");

            app.MapDelete("/authors/{authorId}", async (Guid authorId, IMediator mediator, CancellationToken cancellationToken) =>
            {
                var deleted = await mediator.Send(new DeleteAuthorCommand(authorId), cancellationToken);
                return deleted ? Results.NoContent() : Results.NotFound();
            })
            .RequireAuthorization("admin_role")
            .WithTags("Authors");

            return app;
        }
    }
}

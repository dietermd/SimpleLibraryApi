using Microsoft.EntityFrameworkCore;
using SimpleLibraryApi.Application.Behaviors;
using SimpleLibraryApi.Application.Middlewares;
using SimpleLibraryApi.Endpoints.AuthorEndpoint;
using SimpleLibraryApi.Endpoints.BookBorrowEndpoint;
using SimpleLibraryApi.Endpoints.BookBorrowEndpoint.Commands.Validators;
using SimpleLibraryApi.Endpoints.BookEndpoint;
using SimpleLibraryApi.Endpoints.UserEndpoint;
using SimpleLibraryApi.Endpoints.UserEndpoint.Commands.Validators;
using SimpleLibraryApi.Models.Context;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Db
builder.Services.AddDbContext<ApiDbContext>(options => options.UseInMemoryDatabase("InMemory"));

// Mediatr
builder.Services.AddMediatR(cfg => {
    cfg.RegisterServicesFromAssembly(typeof(Program).Assembly);
    cfg.AddOpenBehavior(typeof(ValidationBahavior<,>));
});

// FluentValidation
builder.AddUserCommandValidators()
    .AddBookBorrowCommandValidators();

// Middleware
builder.Services.AddTransient<ExceptionHandlingMiddleware>();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    // Create In-Memmory database
    using (var scope = app.Services.CreateScope())
    {
        scope.ServiceProvider.GetRequiredService<ApiDbContext>().Database.EnsureCreated();
    }
}

// Middleware
app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseHttpsRedirection();

// Endpoint mapping.
app.MapUserEndpoints()
    .MapAuthorEndpoints()
    .MapBookEndpoints()
    .MapBookBorrowEnpoints();

app.Run();


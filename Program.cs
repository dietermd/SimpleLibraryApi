using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using SimpleLibraryApi.Endpoints.AuthorEndpoint;
using SimpleLibraryApi.Endpoints.BookBorrowEndpoint;
using SimpleLibraryApi.Endpoints.BookEndpoint;
using SimpleLibraryApi.Endpoints.UserEndpoint;
using SimpleLibraryApi.Models.Context;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Db
builder.Services.AddDbContext<ApiDbContext>(options => options.UseInMemoryDatabase("InMemory"));

// Mediatr
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Endpoint mapping.
app.MapUserEndpoints()
    .MapAuthorEndpoints()
    .MapBookEndpoints()
    .MapBookBorrowEnpoints();

app.Run();


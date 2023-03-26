using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SimpleLibraryApi.Application.Behaviors;
using SimpleLibraryApi.Application.Middlewares;
using SimpleLibraryApi.Endpoints.AuthorEndpoint;
using SimpleLibraryApi.Endpoints.AuthorEndpoint.Commands.Validators;
using SimpleLibraryApi.Endpoints.BookBorrowEndpoint;
using SimpleLibraryApi.Endpoints.BookBorrowEndpoint.Commands.Validators;
using SimpleLibraryApi.Endpoints.BookEndpoint;
using SimpleLibraryApi.Endpoints.BookEndpoint.Commands.Validators;
using SimpleLibraryApi.Endpoints.UserEndpoint;
using SimpleLibraryApi.Endpoints.UserEndpoint.Commands.Validators;
using SimpleLibraryApi.Models.Context;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add JWT configuration
builder.Services.AddAuthentication(o =>
{
    o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    o.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(o =>
{
    o.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = false,
        ValidateIssuerSigningKey = true
    };
});

builder.Services.AddAuthorization();
// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

var securityScheme = new OpenApiSecurityScheme()
{
    Name = "Authorization",
    Type = SecuritySchemeType.ApiKey,
    Scheme = "Bearer",
    BearerFormat = "JWT",
    In = ParameterLocation.Header,
    Description = "JSON Web Token based security",
};
var securityReq = new OpenApiSecurityRequirement()
{
    {
        new OpenApiSecurityScheme
        {
            Reference = new OpenApiReference
            {
                Type = ReferenceType.SecurityScheme,
                Id = "Bearer"
            }
        },
        new string[] {}
    }
};
builder.Services.AddSwaggerGen(x =>
{
    x.AddSecurityDefinition("Bearer", securityScheme);
    x.AddSecurityRequirement(securityReq);
});

// Db
builder.Services.AddDbContext<ApiDbContext>(options => options.UseInMemoryDatabase("InMemory"));

// Mediatr
builder.Services.AddMediatR(cfg => {
    cfg.RegisterServicesFromAssembly(typeof(Program).Assembly);
    cfg.AddOpenBehavior(typeof(ValidationBahavior<,>));
});

// FluentValidation
builder.AddUserCommandValidators()
    .AddBookBorrowCommandValidators()
    .AddAuthorCommandValidatorExtension()
    .AddBookCommandValidators();

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

app.UseAuthentication();
app.UseAuthorization();

// Middleware
app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseHttpsRedirection();

// Endpoint mapping.
app.MapUserEndpoints()
    .MapAuthorEndpoints()
    .MapBookEndpoints()
    .MapBookBorrowEnpoints();

app.Run();


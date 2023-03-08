using FluentValidation;

namespace SimpleLibraryApi.Endpoints.AuthorEndpoint.Commands.Validators
{
    public static class AuthorCommandValidatorExtension
    {
        public static WebApplicationBuilder AddAuthorCommandValidatorExtension(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IValidator<CreateAuthorCommand>, CreateAuthorCommandValidator>();

            return builder;
        }
    }
}

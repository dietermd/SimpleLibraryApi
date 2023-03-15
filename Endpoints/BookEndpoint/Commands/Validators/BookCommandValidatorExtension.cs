using FluentValidation;

namespace SimpleLibraryApi.Endpoints.BookEndpoint.Commands.Validators
{
    public static class BookCommandValidatorExtension
    {
        public static WebApplicationBuilder AddBookCommandValidators(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IValidator<CreateBookCommand>, CreateBookCommandValidator>();

            return builder;
        }
    }
}

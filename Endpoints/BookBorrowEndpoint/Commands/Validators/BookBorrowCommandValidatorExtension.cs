using FluentValidation;

namespace SimpleLibraryApi.Endpoints.BookBorrowEndpoint.Commands.Validators
{
    public static class BookBorrowCommandValidatorExtension
    {
        public static WebApplicationBuilder AddBookBorrowCommandValidators(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IValidator<CreateBookBorrowCommand>, CreateBookBorrowCommandValidator>();

            return builder;
        }
    }
}

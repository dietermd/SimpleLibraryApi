using FluentValidation;

namespace SimpleLibraryApi.Endpoints.UserEndpoint.Commands.Validators
{
    public static class UserCommandValidatorExtension
    {
        public static WebApplicationBuilder AddUserCommandValidators(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IValidator<CreateUserCommand>, CreateUserCommandValidator>()
                .AddScoped<IValidator<UpdateUserCommand>, UpdateUserCommandValidator>();

            return builder;
        }
    }
}

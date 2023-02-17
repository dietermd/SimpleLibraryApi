using FluentValidation;

namespace SimpleLibraryApi.Endpoints.UserEndpoint.Commands.Validators
{
    public sealed class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            RuleFor(x => x.Email).NotEmpty().MaximumLength(200);

            RuleFor(x => x.Password).NotEmpty().MaximumLength(200);
        }
    }
}

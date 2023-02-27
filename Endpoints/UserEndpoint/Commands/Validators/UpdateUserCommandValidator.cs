using FluentValidation;
using SimpleLibraryApi.Models.Context;

namespace SimpleLibraryApi.Endpoints.UserEndpoint.Commands.Validators
{
    public sealed class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
    {
        private readonly ApiDbContext _dbContext;

        public UpdateUserCommandValidator(ApiDbContext dbContext)
        {
            _dbContext = dbContext;

            RuleFor(x => x.Email).NotEmpty().MaximumLength(200);
            RuleFor(x => x.Email).Must((command, email) => EmailMustBeUnique(command.UserId, email)).WithMessage("The email is already in use");

            RuleFor(x => x.Password).NotEmpty().MaximumLength(200);
        }

        private bool EmailMustBeUnique(Guid userId, string email)
        {
            return !_dbContext.User.Any(x => x.Email == email && x.UserId != userId);
        }


    }
}

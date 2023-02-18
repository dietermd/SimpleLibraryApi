using FluentValidation;
using SimpleLibraryApi.Models.Context;

namespace SimpleLibraryApi.Endpoints.UserEndpoint.Commands.Validators
{
    public sealed class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        private readonly ApiDbContext _dbContext;

        public CreateUserCommandValidator(ApiDbContext dbContext)
        {
            _dbContext = dbContext;

            RuleFor(x => x.Email).NotEmpty().MaximumLength(200);
            RuleFor(x => x.Email).Must(EmailMustBeUnique).WithMessage("User already exists");

            RuleFor(x => x.Password).NotEmpty().MaximumLength(200);
        }

        private bool EmailMustBeUnique(string email)
        {
            _dbContext.Database.EnsureCreated();
            return !_dbContext.User.Any(x => x.Email == email);
        }


    }
}

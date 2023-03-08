using FluentValidation;
using SimpleLibraryApi.Models.Context;

namespace SimpleLibraryApi.Endpoints.AuthorEndpoint.Commands.Validators
{
    public class CreateAuthorCommandValidator : AbstractValidator<CreateAuthorCommand>
    {
        private readonly ApiDbContext _dbContext;

        public CreateAuthorCommandValidator(ApiDbContext dbContext) 
        {
            _dbContext = dbContext;

            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Country).NotEmpty();

            RuleFor(x => x.Name).Must(NameMustBeUnique).WithMessage("Author already exists");
        }

        private bool NameMustBeUnique(string name)
        {
            return !_dbContext.Author.Any(a => a.Name == name);
        }
    }
}

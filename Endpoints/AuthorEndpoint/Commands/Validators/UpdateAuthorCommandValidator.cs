using FluentValidation;
using SimpleLibraryApi.Models.Context;

namespace SimpleLibraryApi.Endpoints.AuthorEndpoint.Commands.Validators
{
    public class UpdateAuthorCommandValidator : AbstractValidator<UpdateAuthorCommand>
    {
        private readonly ApiDbContext _dbContext;

        public UpdateAuthorCommandValidator(ApiDbContext dbContext) 
        {
            _dbContext = dbContext;

            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Country).NotEmpty();

            RuleFor(x => x.Name).Must((command, name) => NameMustBeUnique(command.AuthorId, command.Name)).WithMessage("Author already exists");
        }

        private bool NameMustBeUnique(Guid authorId, string name)
        {
            return !_dbContext.Author.Any(x => x.AuthorId != authorId && x.Name == name);
        }
    }
}

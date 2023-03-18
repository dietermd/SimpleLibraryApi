using FluentValidation;
using SimpleLibraryApi.Models.Context;

namespace SimpleLibraryApi.Endpoints.BookEndpoint.Commands.Validators
{
    public class CreateBookCommandValidator : AbstractValidator<CreateBookCommand>
    {
        private readonly ApiDbContext _dbContext;
        public CreateBookCommandValidator(ApiDbContext dbContext) 
        {
            _dbContext = dbContext;

            RuleFor(x => x.Title).NotEmpty().MaximumLength(250);
            RuleFor(x => x.Copies).GreaterThan(0);

            RuleFor(x => x.ISBN).NotEmpty().MinimumLength(10).MaximumLength(13);
            RuleFor(x => x.ISBN).Must(ISBNMustBeUnique).WithMessage("There is already a book with the same ISBN in the database");

            RuleFor(x => x.Authors).Cascade(CascadeMode.Stop)
                .Must(AuthorsMustNotHaveDuplicates).WithMessage("Authos must not have duplicates")
                .Must(AuthorsMustExist).WithMessage("One or more author not found in the database");

        }

        private bool ISBNMustBeUnique(string ISBN)
        {
            return !_dbContext.Book.Any(x => x.ISBN == ISBN);
        }

        private bool AuthorsMustNotHaveDuplicates(List<Guid> authors)
        {
            return authors.Count() == authors.Distinct().Count();
        }

        private bool AuthorsMustExist(List<Guid> authors)
        {
            if (!authors.Any()) return true;

            var count = _dbContext.Author.Count(x => authors.Contains(x.AuthorId));
            return count == authors.Count();
        }
    }
}

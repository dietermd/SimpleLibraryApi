using FluentValidation;
using Microsoft.EntityFrameworkCore;
using SimpleLibraryApi.Models.Context;

namespace SimpleLibraryApi.Endpoints.BookEndpoint.Commands.Validators
{
    public class UpdateBookCommandValidator : AbstractValidator<UpdateBookCommand>
    {
        private readonly ApiDbContext _dbContext;
        public UpdateBookCommandValidator(ApiDbContext dbContext) 
        {
            _dbContext = dbContext;

            RuleFor(x => x.Title).NotEmpty().MaximumLength(250);

            RuleFor(x => x.Copies).GreaterThan(0)
                .Must((command, copies) => CopiesMustBeGreaterOrEqualThanLoaned(command.BookId, copies)).WithMessage("The number of copies available must be equal or greater then the number of currently loaned books");

            RuleFor(x => x.ISBN).NotEmpty().MinimumLength(10).MaximumLength(13);
            RuleFor(x => x.ISBN).Must((command, isbn) => ISBNMustBeUnique(command.BookId, isbn)).WithMessage("There is already a book with the same ISBN in the database");

            RuleFor(x => x.Authors).Cascade(CascadeMode.Stop)
                .Must(AuthorsMustNotHaveDuplicates).WithMessage("Authos must not have duplicates")
                .Must(AuthorsMustExist).WithMessage("One or more author not found in the database");

        }

        private bool ISBNMustBeUnique(Guid bookId, string ISBN)
        {
            return !_dbContext.Book.Any(x => x.BookId == bookId && x.ISBN == ISBN);
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

        private bool CopiesMustBeGreaterOrEqualThanLoaned(Guid bookId, int copies)
        {
            var loanedQnt = _dbContext.Book.Include(x => x.BookBorrow).Where(x => x.BookId == bookId).Select(x => x.BookBorrow).Count();
            return copies >= loanedQnt;
        }
    }
}

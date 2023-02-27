using FluentValidation;
using SimpleLibraryApi.Models.Context;

namespace SimpleLibraryApi.Endpoints.BookBorrowEndpoint.Commands.Validators
{
    public sealed class CreateBookBorrowCommandValidator : AbstractValidator<CreateBookBorrowCommand>
    {
        private readonly ApiDbContext _dbContext;

        public CreateBookBorrowCommandValidator(ApiDbContext dbContext)
        {
            _dbContext = dbContext;

            RuleFor(x => x.UserId).NotEmpty();
            RuleFor(x => x.UserId).Must(UserMustExist).WithMessage("Invalid user.");

            RuleFor(x => x.BookId).NotEmpty();
            RuleFor(x => x.BookId).Must(BookMustExist).WithMessage("Invalid book.");
            RuleFor(x => x.BookId).Must(BookMustHaveCopiesAvailable).WithMessage("No copies available.");
        }

        private bool UserMustExist(Guid userId)
        {
            return _dbContext.User.Any(x => x.UserId == userId);
        }

        private bool BookMustExist(Guid bookId)
        {
            return _dbContext.Book.Any(x => x.BookId == bookId);
        }

        private bool BookMustHaveCopiesAvailable(Guid bookId)
        {
            var copies = _dbContext.Book.Where(x => x.BookId == bookId).Select(x => x.Copies).FirstOrDefault();

            return copies > 0;
        }
    }
}

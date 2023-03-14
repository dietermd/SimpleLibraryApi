﻿using FluentValidation;
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

        }

        private bool ISBNMustBeUnique(string ISBN)
        {
            return _dbContext.Book.Any(x => x.ISBN == ISBN);
        }
    }
}
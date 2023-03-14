using Microsoft.EntityFrameworkCore;

namespace SimpleLibraryApi.Models.Context
{
    public class ApiDbContext : DbContext
    {
        public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<BookAuthor>().HasKey(e => new { e.BookId, e.AuthorId });

            var authors = new Author[]
            {
                new Author { Name = "J.D. Salinger", Country = "USA" },
                new Author { Name = "F. Scott. Fitzgerald", Country = "USA" },
                new Author { Name = "Jane Austen", Country = "UK" },
                new Author { Name = "Scott Hanselman", Country = "USA" },
                new Author { Name = "Jason N. Gaylord", Country = "USA" },
                new Author { Name = "Pranav Rastogi", Country = "India" },
                new Author { Name = "Todd Miranda", Country = "USA" },
                new Author { Name = "Christian Wenz", Country = "USA" }
            };

            modelBuilder.Entity<Author>().HasData(authors);

            var books = new Book[]
            {
                new Book { Title = "The Catcher in the Rye", ISBN = "9780316769532", Copies = 3 },
                new Book { Title = "Nine Stories", ISBN = "9780241911464", Copies = 2 },
                new Book { Title = "Franny and Zooey", ISBN = "9780140237528", Copies = 3 },
                new Book { Title = "The Great Gatsby", ISBN = "9780743273565", Copies = 4 },
                new Book { Title = "Tender id the Night", ISBN = "9780370005249", Copies = 3 },
                new Book { Title = "Pride and Prejudice", ISBN = "9788466810265",  Copies = 1},
                new Book { Title = "Professional ASP.NET 4.5 in C# and VB", ISBN = "9781118311820", Copies = 2 },
            };

            modelBuilder.Entity<Book>().HasData(books);


            var bookAuthors = new BookAuthor[]
            {
                new BookAuthor { BookId = books[0].BookId, AuthorId = authors[0].AuthorId },
                new BookAuthor { BookId = books[1].BookId, AuthorId = authors[0].AuthorId },
                new BookAuthor { BookId = books[2].BookId, AuthorId = authors[0].AuthorId },

                new BookAuthor { BookId = books[3].BookId, AuthorId = authors[1].AuthorId },
                new BookAuthor { BookId = books[4].BookId, AuthorId = authors[1].AuthorId },

                new BookAuthor { BookId = books[5].BookId, AuthorId = authors[2].AuthorId },

                new BookAuthor { BookId = books[6].BookId, AuthorId = authors[3].AuthorId },
                new BookAuthor { BookId = books[6].BookId, AuthorId = authors[4].AuthorId },
                new BookAuthor { BookId = books[6].BookId, AuthorId = authors[5].AuthorId },
                new BookAuthor { BookId = books[6].BookId, AuthorId = authors[6].AuthorId },
                new BookAuthor { BookId = books[6].BookId, AuthorId = authors[7].AuthorId },
            };

            modelBuilder.Entity<BookAuthor>().HasData(bookAuthors);

            var user = new User
            {
                Email = "bob@simplelibrary.com",
                Password = "password"
            };

            modelBuilder.Entity<User>().HasData(user);

            var bookBorrow = new BookBorrow
            {
                BorrowDate = DateTime.Now,
                UserId = user.UserId,
                BookId = books[0].BookId,
            };

            modelBuilder.Entity<BookBorrow>().HasData(bookBorrow);
        }

        public DbSet<Book> Book { get; set; } = null!;
        public DbSet<Author> Author { get; set; } = null!;
        public DbSet<BookAuthor> BookAuthor { get; set; } = null!;
        public DbSet<User> User { get; set; } = null!;
        public DbSet<BookBorrow> BookBorrow { get; set; } = null!;
    }
}

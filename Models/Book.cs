using System.ComponentModel.DataAnnotations;

namespace SimpleLibraryApi.Models
{
    public class Book
    {
        public Book()
        {
            BookAuthor = new HashSet<BookAuthor>();
            BookBorrow = new HashSet<BookBorrow>();
        }
        public Guid BookId { get; set; } = Guid.NewGuid();
        public string Title { get; set; } = null!;
        public int Copies { get; set; }

        public ICollection<BookAuthor> BookAuthor { get; set; }
        public ICollection<BookBorrow> BookBorrow { get; set; }
    }
}

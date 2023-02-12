using System.ComponentModel.DataAnnotations.Schema;

namespace SimpleLibraryApi.Models
{
    public class BookAuthor
    {
        [ForeignKey(nameof(Book))]
        public Guid BookId { get; set; }
        public Book Book { get; set; } = null!;

        [ForeignKey(nameof(Author))]
        public Guid AuthorId { get; set; }
        public Author Author { get; set; } = null!;
    }
}

using System.ComponentModel.DataAnnotations;

namespace SimpleLibraryApi.Models
{
    public class Author
    {
        public Author()
        {
            BookAuthor = new HashSet<BookAuthor>();
        }

        public Guid AuthorId { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = null!;
        public string Country { get; set; } = null!;

        public ICollection<BookAuthor> BookAuthor { get; set; }
    }
}

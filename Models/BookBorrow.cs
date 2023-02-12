using System.ComponentModel.DataAnnotations.Schema;

namespace SimpleLibraryApi.Models
{
    public class BookBorrow
    {
        public Guid BookBorrowId { get; set; } = Guid.NewGuid();
        public DateTime BorrowDate { get; set; }
        public DateTime? ReturnDate { get; set; }

        [ForeignKey(nameof(User))]
        public Guid UserId { get; set; }
        public User User { get; set; } = null!;

        [ForeignKey(nameof(Book))]
        public Guid BookId { get; set; }
        public Book Book { get; set; } = null!;
    }
}

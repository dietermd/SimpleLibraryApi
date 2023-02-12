namespace SimpleLibraryApi.Models
{
    public class User
    {
        public User()
        {
            BookBorow = new HashSet<BookBorrow>();
        }

        public Guid UserId { get; set; } = Guid.NewGuid();
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;

        public ICollection<BookBorrow> BookBorow { get; set; }
    }
}

namespace SimpleLibraryApi.Endpoints.BookBorrowEndpoint.Responses
{
    public class GetBookBorrowResponse
    {
        public Guid BookBorrowId { get; set; }
        public Guid UserId { get; set; }
        public Guid BookId { get; set; }
        public DateTime BorrowDate { get; set; }
        public DateTime? ReturnDate { get; set; }
    }
}

using System;

namespace Library.Application.DTOs
{
    public class RentalDto
    {
        public int Id { get; set; }
        public BookDto Book { get; set; }
        public RenterDto Renter { get; set; }
        public DateTime RentedAt { get; set; }
        public DateTime? ReturnedAt { get; set; }
        public StatusDto Status { get; set; }
        public LibrarianDto Librarian { get; set; }
        public string? Review { get; set; }
    }
    public class CreateRentalDto
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public int RenterId { get; set; }
        public int LibrarianId { get; set; }
        public int StatusId { get; set; }
        public DateTime RentedAt { get; set; }
        public DateTime? ReturnedAt { get;set; }
    }

}

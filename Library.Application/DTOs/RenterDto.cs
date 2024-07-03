namespace Library.Application.DTOs
{
    public class RenterDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Patronymic { get; set; }
        public string Address { get; set; }
        public string ContactNumber { get; set; }
        public ICollection<RentalDto> Rentals { get; set; }
    }
}

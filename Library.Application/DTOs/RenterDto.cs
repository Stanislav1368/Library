using System.Text.Json.Serialization;

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
        public bool IsRenter { get; set; } = true;
    }
    public class NewRenterDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Patronymic { get; set; }
        public string Address { get; set; }
        public string ContactNumber { get; set; }
        public ICollection<RentalDto> Rentals { get; set; }
        public string Password { get; set; }
        public bool IsRenter { get; set; } = true;
    }
    public class CreateRenterDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Patronymic { get;  set; }
        public string Address { get; set; }
        public string ContactNumber { get; set; }
    }
}

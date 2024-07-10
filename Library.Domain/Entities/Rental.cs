using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Domain.Entities
{
    public class Rental
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public Book Book { get; set; }
        public int RenterId { get; set; }
        public Renter Renter { get; set; }
        public int LibrarianId { get; set; }
        public Librarian Librarian { get; set; }
        public DateTime RentedAt { get; set; } = DateTime.Now;
        public DateTime? ReturnedAt { get; set; }
        public DateTime? ActualReturnedAt { get; set; }
        public int StatusId { get; set; }
        public Status Status { get; set; }
        public string? Review { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Domain.Entities
{
    public class Status
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Rental> Rentals { get; set; }
    }
}

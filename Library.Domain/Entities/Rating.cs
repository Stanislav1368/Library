using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Domain.Entities
{
    public class Rating
    {
        public int Id { get; set; }
        public int RenterId { get; set; }
        public int BookId { get; set; }
        public double RatingValue { get; set; }  

        public virtual Renter Renter { get; set; }
        public virtual Book Book { get; set; }
    }
}

using Library.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.DTOs
{
    public class RatingDto
    {
        public int Id { get; set; }
        public int RenterId { get; set; }
        public int BookId { get; set; }
        public double RatingValue { get; set; }

    }
    public class AddRatingDto
    {
        public double RatingValue { get; set; }
        public int RenterId { get; set; }
    }
}

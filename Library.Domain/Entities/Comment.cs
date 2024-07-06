using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Domain.Entities
{
    public class Comment
    {
        public int Id { get; set; }
        public int RenterId { get; set; }
        public int BookId { get; set; }
        public string CommentText { get; set; }  
        public DateTime CommentedAt { get; set; }
        public Renter Renter { get; set; } 
        public Book Book { get; set; }
    }
}

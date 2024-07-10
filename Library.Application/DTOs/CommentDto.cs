using Library.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.DTOs
{
    public class CommentDto
    {
        public string CommentText { get; set; }
        public DateTime CommentedAt { get; set; }
        public virtual Renter Renter { get; set; }
        //public virtual Book Book { get; set; }
    }
    public class CreateCommentDto
    {
        public string CommentText { get; set; }
        public int RenterId { get; set; }
    }
}

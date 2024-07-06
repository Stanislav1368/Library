using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.DTOs
{
    public class AuthRequestDto
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public bool IsRenter { get; set; }
    }
}

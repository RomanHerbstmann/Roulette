using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roulette
{
    public class LoggedInUser
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public decimal Money { get; set; }
        public int UserId { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Entities
{
    public class Login
    {
        public string Email { get; set; }
        public string Password { get; set; }

        
        public string Roles { get; set; }
    }
}

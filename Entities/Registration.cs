using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace Entities
{
    public class Registration
    {
        public int RegisteredUsersId { get; set; }

        public string Name { get; set; }

 
        public string Email { get; set; }

      
        public string Password { get; set; }

        
        public string Role { get; set; }

        public string Salt { get; set; }
    }
}

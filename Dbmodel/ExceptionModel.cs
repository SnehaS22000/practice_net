using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dbmodel
{
    public class ExceptionModel
    {
        [Key]
        public int ExceptionDetailsId { get; set; }
        public string ExceptionMessage { get; set; }
        public DateTime Time { get; set; }
    }
}

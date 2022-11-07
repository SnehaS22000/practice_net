using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Employee
    {

  
        public int EmployeesDetailId { get; set; }
        public string Name { get; set; }
        public string Designation { get; set; }
        public string Grade { get; set; }
        public string Skills { get; set; }
        public string BenchDuration { get; set; }
        public string Location { get; set; }
        public int PsNo { get; set; }

    }
}

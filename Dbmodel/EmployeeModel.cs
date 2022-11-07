using System.ComponentModel.DataAnnotations;

namespace Dbmodel
{
    public class EmployeeModel
    {
        [Key]
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
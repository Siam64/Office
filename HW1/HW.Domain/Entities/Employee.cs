using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW.Domain.Entities
{
    public class Employee
    {
        [Key]
        public int EmpId { get; set; }
        public int DepId { get; set; }
        public string EmpName { get; set; }
        public double? Salary { get; set; }
    }
}

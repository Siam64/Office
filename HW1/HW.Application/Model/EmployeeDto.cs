using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW.Application.Model
{
    public class EmployeeDto
    {
        public int EmpId { get; set; }
        public int DepId { get; set; }
        public string EmpName { get; set; }
        public string DepName { get; set; }
        public double Salary { get; set; }
    }
}

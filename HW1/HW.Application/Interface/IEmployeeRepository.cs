using HW.Application.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW.Application.Interface
{
    public interface IEmployeeRepository
    {
        IEnumerable<EmployeeDto> GetAll();
        EmployeeDto GetById(int id);
        int Add(EmployeeDto employeeDto);
        EmployeeDto Update(EmployeeDto employeeDto);
        bool Delete(int id);
    }
}

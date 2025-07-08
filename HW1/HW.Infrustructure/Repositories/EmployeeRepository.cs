using HW.Application.Interface;
using HW.Application.Model;
using HW.Domain.Entities;
using HW.Infrustructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW.Infrustructure.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        
            private readonly ApplicationDbContext _context;

            public EmployeeRepository(ApplicationDbContext context)
            {
                _context = context;
            }

        public IEnumerable<EmployeeDto> GetAll()
        {
            var employees = from e in _context.Employees
                            join d in _context.Departments
                            on e.DepId equals d.DepId
                            select new EmployeeDto
                            {
                                EmpId = e.EmpId,
                                DepId = e.DepId,
                                EmpName = e.EmpName,
                                Salary = (double)e.Salary,
                                DepName = d.DepName 
                            };

            return employees.ToList();
        }


        public EmployeeDto GetById(int id)
        {
            var employee = _context.Employees.Find(id);
            if (employee == null) return null;

           
            var depName = _context.Departments
                .Where(d => d.DepId == employee.DepId)
                .Select(d => d.DepName)
                .FirstOrDefault();

            return new EmployeeDto
            {
                EmpId = employee.EmpId,
                DepId = employee.DepId,
                EmpName = employee.EmpName,
                Salary = (double)employee.Salary,
                DepName = depName
            };
        }


        public int Add(EmployeeDto employeeDto)
        {
            var depId = _context.Departments
                .Where(d => d.DepName == employeeDto.DepName)
                .Select(d => d.DepId)
                .FirstOrDefault();

            if (depId == 0)
            {
                throw new Exception("Department not found");
            }

            var employee = new Employee
            {
                DepId = depId,
                EmpName = employeeDto.EmpName,
                Salary = employeeDto.Salary
            };

            _context.Employees.Add(employee);
            _context.SaveChanges();
            return employee.EmpId;
        }


        public EmployeeDto Update(EmployeeDto employeeDto)
        {
            var depId = _context.Departments
                .Where(x => x.DepName == employeeDto.DepName)
                .Select(x => x.DepId)
                .FirstOrDefault();

            var employee = _context.Employees.Find(employeeDto.EmpId);
            if (employee == null) return null;

            employee.DepId = depId;
            employee.EmpName = employeeDto.EmpName;
            employee.Salary = employeeDto.Salary;

            _context.Employees.Update(employee);
            _context.SaveChanges();

            var depName = _context.Departments
                .Where(x => x.DepId == depId)
                .Select(x => x.DepName)
                .FirstOrDefault() ?? string.Empty;

            return new EmployeeDto
            {
                DepName = depName,
                EmpId = employee.EmpId,
                DepId = depId,
                EmpName = employee.EmpName,
                Salary = (double)employee.Salary
            };
        }

            public bool Delete(int id)
            {
                try
                {
                    var employee = _context.Employees.Find(id);
                    if (employee != null)
                    {
                        _context.Employees.Remove(employee);
                        _context.SaveChanges();
                        return true;
                    }
                    return false;
                }
                catch
                {
                    // Log error if necessary
                    return false;
                }
            }
    }
}


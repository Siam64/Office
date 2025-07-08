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
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly ApplicationDbContext _context;

        public DepartmentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<DepartmentDto> GetAll()
        {
            var departments = _context.Departments.ToList();
            return departments.Select(d => new DepartmentDto
            {
                DepId = d.DepId,
                DepName = d.DepName,
                Remarks = d.Remarks
            }).ToList();
        }




        public DepartmentDto GetById(int id)
        {
            var department = _context.Departments.Find(id);
            if (department == null) return null;

            return new DepartmentDto
            {
                DepId = department.DepId,
                DepName = department.DepName,
                Remarks = department.Remarks
            };
        }

        public int Add(DepartmentDto departmentDto)
        {
            var department = new Department
            {
                DepName = departmentDto.DepName,
                Remarks = departmentDto.Remarks
            };

            _context.Departments.Add(department);
            _context.SaveChanges();
            return department.DepId;
        }

        public DepartmentDto Update(DepartmentDto departmentDto)
        {
            var department = _context.Departments.Find(departmentDto.DepId);
            if (department == null) return null;

            department.DepName = departmentDto.DepName;
            department.Remarks = departmentDto.Remarks;

            _context.Departments.Update(department);
            _context.SaveChanges();
            return new DepartmentDto
            {
                DepId = department.DepId,
                DepName= department.DepName,
                Remarks = department.Remarks
            };
        }

        public bool Delete(int id)
        {
            try
            {
                var department = _context.Departments.Find(id);
                if (department != null)
                {
                    _context.Departments.Remove(department);
                    _context.SaveChanges();
                    return true;
                }
                return false;
            }
            catch
            {
                // Log the exception or handle it as needed
                return false;
            }

        }
    }

}

using HW.Application.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW.Application.Interface
{
    public interface IDepartmentRepository
    {
            IEnumerable<DepartmentDto> GetAll();
            DepartmentDto GetById(int id);
            int Add(DepartmentDto department);
            DepartmentDto Update(DepartmentDto department);
            bool Delete(int id);
        

    }
}

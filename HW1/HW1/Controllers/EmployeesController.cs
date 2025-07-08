using HW.Application.Interface;
using HW.Application.Model;
using Microsoft.AspNetCore.Mvc;

namespace HW1.Project.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IDepartmentRepository _departmentRepository;

        public EmployeesController(IDepartmentRepository departmentRepository, IEmployeeRepository employeeRepository)
        {
            _departmentRepository = departmentRepository;
            _employeeRepository = employeeRepository;
        }
        public IActionResult EmpIndex()
        {
            var result = _departmentRepository.GetAll();
            ViewBag.Departments = result.Select(d => d.DepName).ToList();

            var Employees = _employeeRepository.GetAll() ?? new List<EmployeeDto>();
            return View(Employees);
        }


        public IActionResult AddEmployee(EmployeeDto model) {

            if (model == null)
            {
                return Json(new { success = false, message = "No data to save" });
            }

            try 
            {
                var result = _employeeRepository.Add(model);
                if (result == 0)
                {
                    return Json(new { success = false, message = "Saving Error" });
                }
                var dataResult = _employeeRepository.GetById(result);
                return Json(new { success = true, message = "Employee created successfully", data = dataResult });
            }
            catch
            {
                return Json(new { success = false, message = "Error" });

            }
        
        }


        public IActionResult GetDataForUpdate(int id)
        {

            if (id == 0)
            {
                return Json(new { success = false, message = "Error" });
            }

            try
            {
                var data = _employeeRepository.GetById(id);
                if (data == null)
                {
                    return Json(new { success = false, message = "Employee not found" });
                }
                return Json(new { success = true, data = data });
            }
            catch
            {
               return Json(new { success = false, message = "Error" });
            }
            
        }

        public IActionResult Update(EmployeeDto model)
        {
            if(model == null)
            {
                return Json(new {success = false, message = "Error"});
            }

            try
            {
                var data = _employeeRepository.Update(model);
                if (data == null)
                {
                    return Json(new { success = false, message = "Failed" });
                }
                return Json(new { success = true, data = data });
            }
            catch
            {
                return Json(new { success = false, message = "Error" });
            }
        }

        public IActionResult Delete(int id)
        {
            if(id == 0)
            {
                return Json(new { success = false, message = "Error" });
            }

            try
            {
                var result = _employeeRepository.Delete(id);

                if (!result)
                {
                    return Json(new { success = false, message = "Error deleting employee" });
                }

                return Json(new { success = true, message = "Employee deleted successfully" });
            }
            catch
            {
                return Json(new { success = false, message = "Error deleting employee" });
            }
        }

    }
}

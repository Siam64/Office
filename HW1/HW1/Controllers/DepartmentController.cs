using HW.Application.Interface;
using HW.Application.Model;
using Microsoft.AspNetCore.Mvc;

namespace HW1.Project.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IDepartmentRepository _departmentRepository;

        public DepartmentController(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }
        public IActionResult DepIndex()
        {
            var departments = _departmentRepository.GetAll() ?? new List<DepartmentDto>();
            return View(departments);
        }

        public IActionResult AddDepartment(DepartmentDto model)
        {
            if (model == null)
            {
                return Json(new { success = false, message = "No data to save" });
            }

            try
            {
                var result = _departmentRepository.Add(model);
                if (result == 0)
                {
                    return Json(new { success = false, message = "Saving Error" });
                }

                var dataResult = _departmentRepository.GetById(result);
                return Json(new { success = true, message = "Vauchar created successfully", data = dataResult });

            }
            catch
            {
                return Json(new { success = false, message = "Error" });
            }
        }


        public IActionResult GetDataForUpdate(int id)
        {
            if (id <= 0)
            {
                return Json(new { success = false, message = "Invalid ID" });
            }

            try
            {
                var data = _departmentRepository.GetById(id);
                if (data == null)
                {
                    return Json(new { success = false, message = "Department not found" });
                }
                return Json(new { success = true, data = data });
            }
            catch
            {
                return Json(new { success = false, message = "Error retrieving department" });

            }   
        
        }

        public IActionResult Update(DepartmentDto model)
        {
            if (model == null)
            {
                return Json(new { success = false, message = "Error retrieving department" });
            }

            try
            {
                var data = _departmentRepository.Update(model);
                if (data == null)
                {
                    return Json(new { success = false, message = "Error updating department" });
                }

                return Json(new { success = true, data = data });
            }
            catch
            {
                return Json(new { success = false, message = "Exception occurred while updating department" });
            }
        }


        [HttpPost]
        public IActionResult Delete(int id)
        {
            if (id <= 0)
            {
                return Json(new { success = false, message = "Invalid ID" });
            }

            try
            {
                var result = _departmentRepository.Delete(id);

                if (!result)
                {
                    return Json(new { success = false, message = "Error deleting department" });
                }

                return Json(new { success = true, message = "Department deleted successfully" });
            }
            catch
            {
                return Json(new { success = false, message = "Error deleting department" });
            }
        }

    }
}

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sunrise.Data;
using Sunrise.Models;

namespace Sunrise.Controllers
{
    public class DepartmentController : Controller
    {
		ApplicationDbContext _context;
		public DepartmentController(ApplicationDbContext context)
		{
			_context = context;
		}

		[HttpGet]
        public IActionResult GetIndexView()
        {
            return View("Index", _context.Departments.ToList());
        }
        [HttpGet]
        public IActionResult GetDetailsView(int id)
        {
            Department department= _context.Departments.Include(d=>d.Employees).FirstOrDefault(x => x.Id == id);
            return View("Details", department);
        }
        [HttpGet]
        public IActionResult GetCreateView()
        {

            return View("Create");
        }
        [HttpGet]
        public IActionResult GetEditView(int id)
        {
            Department department = _context.Departments.FirstOrDefault(x => x.Id == id);

            if (department == null)
            {
                return NotFound();
            }
            else
            {
                return View("Edit", department);
            }
        }

        [HttpGet]
        public IActionResult GetDeleteView(int id)
        {
			Department department = _context.Departments.Include(d => d.Employees).FirstOrDefault(x => x.Id == id);

			if (department == null)
            {
                return NotFound();
            }
            else
            {
                return View("Delete", department);
            }
        }

        [HttpPost]
        public IActionResult AddNew(Department dp)
        {
			if (ModelState.IsValid)
			{
				_context.Departments.Add(dp);
				_context.SaveChanges();
				return RedirectToAction("GetIndexView");
			}
			else
				return View("Create");
			
        }
        [HttpPost]
        public IActionResult EditCurrent(Department dp)
        {
			if (ModelState.IsValid)
			{
				_context.Departments.Update(dp);
				_context.SaveChanges();
				return RedirectToAction("GetIndexView");
			}
			else
				return View("Edit");
			
        }
        [HttpPost]
        public IActionResult DeleteCurrent(int id)
        {
            Department department = _context.Departments.FirstOrDefault(e => e.Id == id);
            if (department == null)
            {
                return NotFound();
            }
            else
            {
                _context.Departments.Remove(department);
                _context.SaveChanges(true);
                return RedirectToAction("GetIndexView");
            }
        }

    }
}

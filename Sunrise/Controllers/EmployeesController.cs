using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Sunrise.Data;
using Sunrise.Models;

namespace Sunrise.Controllers
{
    public class EmployeesController : Controller
    {
        ApplicationDbContext _context;
        IWebHostEnvironment _webHostEnvironment;
        public EmployeesController(IWebHostEnvironment WebHostEnvironment,ApplicationDbContext context)
        {
            _webHostEnvironment = WebHostEnvironment;
            _context = context;
        }
        [HttpGet]
        public IActionResult GetIndexView()
        {
            return View("Index", _context.Employees.ToList());
        }
        [HttpGet]
        public IActionResult GetDetailsView(int id)
        {
			Employee employee = _context.Employees.Include(e => e.Department).FirstOrDefault(e => e.Id == id); 
			return View("Details", employee);
        }
        [HttpGet]
        public IActionResult GetCreateView()
        {
            //ViewBag.AllDepartments=_context.Departments.ToList();
            ViewBag.DeptSelectItems=new SelectList(_context.Departments.ToList(), "Id", "FullName");

			return View("Create");
        }
         [HttpPost]
        public IActionResult AddNew(Employee emp , IFormFile? imageFormFile)
        {
            if (imageFormFile != null)
            {
                string imageExtension = Path.GetExtension(imageFormFile.FileName);
                Guid imageGuid = Guid.NewGuid();
                string imageName = imageGuid + imageExtension;
                string imgUrl = "\\images\\" + imageName;
                emp.ImageUrl = imgUrl;
                string imgPath = _webHostEnvironment.WebRootPath + imgUrl;
                //fileStream
                FileStream imgStream = new FileStream(imgPath, FileMode.Create);
                imageFormFile.CopyTo(imgStream);
                imgStream.Dispose();

            }
            else
            {
                emp.ImageUrl = "\\images\\No_Image.png";
            }

            if (((emp.JoinDate - emp.BirthDate).Days / 365) < 18)
            {
                ModelState.AddModelError(string.Empty, "Illegel hiring age (under 18 years old)");
            }
            if(emp.Salary >55000 || emp.Salary <5500)
            {
                ModelState.AddModelError(string.Empty, "Salary must be between 5500 EGP and 55000 EGP");
            }
            if (ModelState.IsValid)
            {
                _context.Employees.Add(emp);
                _context.SaveChanges();
                return RedirectToAction("GetIndexView");
            }
            else
            {
				ViewBag.DeptSelectItems = new SelectList(_context.Departments.ToList(), "Id", "FullName");
                return View("Create");
			}
        }
        [HttpPost]
        public IActionResult EditCurrent(Employee emp, IFormFile? imageFormFile)
        {
			if (imageFormFile != null)
			{
                if(emp.ImageUrl!= "\\images\\No_Image.png")
                {
                    string oldImgPath=_webHostEnvironment.WebRootPath+emp.ImageUrl;

                    if (System.IO.File.Exists(oldImgPath))
                    { 
                        System.IO.File.Delete(oldImgPath);
                    }
                }
				string imageExtension = Path.GetExtension(imageFormFile.FileName);
				Guid imageGuid = Guid.NewGuid();
				string imageName = imageGuid + imageExtension;
				string imgUrl = "\\images\\" + imageName;
				emp.ImageUrl = imgUrl;
				string imgPath = _webHostEnvironment.WebRootPath + imgUrl;
				//fileStream
				FileStream imgStream = new FileStream(imgPath, FileMode.Create);
				imageFormFile.CopyTo(imgStream);
				imgStream.Dispose(); 
			}


			if (((emp.JoinDate - emp.BirthDate).Days / 365) < 18)
            {
                ModelState.AddModelError(string.Empty, "Illegel hiring age (under 18 years old)");
            }
            if (emp.Salary > 55000 || emp.Salary < 5500)
            {
                ModelState.AddModelError(string.Empty, "Salary must be between 5500 EGP and 55000 EGP");
            }
            if (ModelState.IsValid)
            {
                _context.Employees.Update(emp);
                _context.SaveChanges();
                return RedirectToAction("GetIndexView");
            }
            else
            {
                ViewBag.DeptSelectItems = new SelectList(_context.Departments.ToList(), "Id", "FullName");
                return View("Edit");
            }
		}
        [HttpGet]
        public IActionResult GetEditView(int id)
        {
            Employee employee =_context.Employees.FirstOrDefault(e=>e.Id== id);
            if (employee == null)
            {
                return NotFound();
            }
            else
            {
				ViewBag.DeptSelectItems = new SelectList(_context.Departments.ToList(), "Id", "FullName");
				return View("Edit",employee);
            }
        }

        [HttpGet]
        public IActionResult GetDeleteView(int id)
        {
            Employee employee = _context.Employees.Include(e=>e.Department).FirstOrDefault(e => e.Id == id);
            if (employee == null)
            {
                return NotFound();
            }
            else
            {
                return View("Delete", employee);
            }
        }
        [HttpPost]
        public IActionResult DeleteCurrent(int id)
        {
            Employee employee = _context.Employees.FirstOrDefault(e => e.Id == id);
            if (employee == null)
            {
                return NotFound();
            }
            else
            {
				if (employee.ImageUrl != "\\images\\No_Image.png")
				{
                    string imgPath=_webHostEnvironment.WebRootPath+employee.ImageUrl;

                    if(System.IO.File.Exists(imgPath))
                    {
						System.IO.File.Delete(imgPath);    
                    }
				}
				_context.Employees.Remove(employee);
                _context.SaveChanges(true);
                return RedirectToAction("GetIndexView");
            }
        }
        public string GreetVisitor()
        {
            return " welcome to sunrise";
        }
        public string GreetUser(string name)
        {
            return $"Hi {name}\nHow are you";
        }
        public string GetAge(string name, int birthYear)
        {

            int age = DateTime.Now.Year - birthYear;
            return $"Hi {name} \nyour age is {age}";
        }
    }
}

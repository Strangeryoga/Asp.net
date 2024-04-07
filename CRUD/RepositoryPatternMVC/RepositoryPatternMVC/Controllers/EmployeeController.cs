using Microsoft.AspNetCore.Mvc;
using RepositoryPatternMVC.Models;
using RepositoryPatternMVC.Repo;

namespace RepositoryPatternMVC.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly EmpRepo repo;
        public EmployeeController(EmpRepo repo) 
        { 
            this.repo=repo;
        }
        public IActionResult Index()
        {
            var dt=repo.GetAllEmps();
            return View(dt);
        }

        public IActionResult AddEmp()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddEmp(Emp emp)
        {
            if (ModelState.IsValid)
            {
                repo.NewEmp(emp);
                TempData["msg"] = "Emp Added Sucessfully";
                return RedirectToAction("Index");
            }
            else 
            {
                return View();
            }
            
        }

        public IActionResult DeleteEmp(int id)
        {
            repo.RemoveEmp(id);
            return RedirectToAction("Index");
        }
    }
}

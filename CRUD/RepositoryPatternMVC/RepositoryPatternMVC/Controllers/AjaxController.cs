using Microsoft.AspNetCore.Mvc;
using RepositoryPatternMVC.Data;
using RepositoryPatternMVC.Models;

namespace RepositoryPatternMVC.Controllers
{
    public class AjaxController : Controller
    {
        private readonly ApplicationDbContext db;
        public AjaxController(ApplicationDbContext db) 
        { 
            this.db = db;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ShowEmp()
        {
            var data = db.emps.ToList();
            return new JsonResult(data);
        }


        [HttpPost]
        public IActionResult AddEmp(Emp e)
        {
            db.emps.Add(e);
            db.SaveChanges();
            return new JsonResult("");
        }
    }
}

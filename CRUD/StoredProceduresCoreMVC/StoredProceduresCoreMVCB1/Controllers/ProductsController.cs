using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StoredProceduresCoreMVCB1.Data;
using StoredProceduresCoreMVCB1.Models;

namespace StoredProceduresCoreMVCB1.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext db;
        public ProductsController(ApplicationDbContext db) 
        { 
            this.db = db;
        } 
        public IActionResult Index()
        {
            var data = db.products.FromSqlRaw("exec sp_show").ToList();
            return View(data);
        }

        public IActionResult AddProduct()
        {
            return View();
        }

        //[HttpPost]
        //public IActionResult AddProduct(Product p)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Database.ExecuteSqlRaw($"exec sp_insert '{p.Pname}','{p.Pcat}',{p.Price}");
        //        TempData["success"] = "Product Added Success";
        //        return RedirectToAction("Index");
        //    }
        //    else
        //    {
        //        return View();
        //    }
        //}

        public IActionResult DeleteProduct(int id)
        {
            db.Database.ExecuteSqlRaw($"exec sp_del {id}");
            return RedirectToAction("Index");
        }

        public IActionResult EditProduct(int id) 
        {
            var data=db.products.FromSqlRaw($"exec sp_findbyid {id}").ToList().FirstOrDefault();
            return View(data);
        }

        //[HttpPost]
        //public IActionResult EditProduct(Product p)
        //{
        //    db.Database.ExecuteSqlRaw($"exec sp_update {p.Pid},'{p.Pname}','{p.Pcat}',{p.Price}");
        //    return RedirectToAction("Index");
        //}

        [HttpPost]
        public IActionResult AddOrUpdateProduct(Product p)
        {
            db.Database.ExecuteSqlRaw($"exec sp_insertorupdate {p.Pid},'{p.Pname}','{p.Pcat}',{p.Price}");
            return RedirectToAction("Index");
        }
    }
}

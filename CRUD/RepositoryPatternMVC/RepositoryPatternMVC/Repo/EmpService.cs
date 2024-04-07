using RepositoryPatternMVC.Data;
using RepositoryPatternMVC.Models;

namespace RepositoryPatternMVC.Repo
{
    public class EmpService : EmpRepo
    {
        private readonly ApplicationDbContext db;
        public EmpService(ApplicationDbContext db) 
        { 
            this.db = db;
        }

        public List<Emp> GetAllEmps()
        {
            var data=db.emps.ToList();
            return data;
        }

        public void NewEmp(Emp e)
        {
            db.emps.Add(e);
            db.SaveChanges();
        }

        public void RemoveEmp(int id)
        {
            //var data=db.emps.Where(x=>x.Id==id).SingleOrDefault();
            var data = db.emps.Find(id); 
            if (data != null)
            {
                db.emps.Remove(data);
                db.SaveChanges();
            }
        }
    }
}

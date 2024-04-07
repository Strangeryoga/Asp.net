using RepositoryPatternMVC.Models;

namespace RepositoryPatternMVC.Repo
{
    public interface EmpRepo
    {
        void NewEmp(Emp e);
        List<Emp> GetAllEmps();
        void RemoveEmp(int id);
    }
}

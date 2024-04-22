using UserRegistration.Models;

namespace UserRegistration.Repo
{
    public interface UserRepo
    {

        void NewUser(User user);

        List<User> GetUsers();

        User GetUserById(int userId);

        void UpdateUser(User user);


        void DeleteUser(int userId);
    }
}

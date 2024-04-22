using Microsoft.AspNetCore.Mvc;
using UserRegistration.Models;
using UserRegistration.Repo;

namespace UserRegistration.Controllers
{
    public class UserController : Controller
    {
        private readonly UserRepo _repo;

        public UserController(UserRepo repo)
        {
            _repo = repo;
        }

        // Action to display the list of users
        public IActionResult Index()
        {
            var data = _repo.GetUsers(); // Get the list of users from the repository
            return View(data); // Pass the list of users to the view
        }

        // Action to display the form for adding a new user (GET)
        public IActionResult AddUser()
        {
            return View(); // Return the view for adding a new user
        }

        // Action to handle the submission of the form for adding a new user (POST)
        [HttpPost]
        public IActionResult AddUser(User user)
        {
            if (ModelState.IsValid) // Check if the submitted user data is valid
            {
                _repo.NewUser(user); // Add the new user to the repository
                TempData["msg"] = "User Added Successfully"; // Set a success message in TempData
                return RedirectToAction("Index"); // Redirect to the index page
            }
            else
            {
                return View(); // If the submitted data is not valid, return the add user view again
            }
        }

        // Action to display the form for editing a user (GET)
        public IActionResult EditUser(int id)
        {
            var user = _repo.GetUserById(id); // Get the user to be edited from the repository
            if (user == null)
            {
                return NotFound(); // If user is not found, return 404 Not Found
            }
            return View(user); // Return the view for editing the user
        }

        // Action to handle the submission of the form for editing a user (POST)
        [HttpPost]
        public IActionResult EditUser(int id, User user)
        {
            if (id != user.Id)
            {
                return BadRequest(); // If the user ID in the URL doesn't match the user ID in the submitted data, return 400 Bad Request
            }

            if (ModelState.IsValid) // Check if the submitted user data is valid
            {
                _repo.UpdateUser(user); // Update the user in the repository
                TempData["msg"] = "User Updated Successfully"; // Set a success message in TempData
                return RedirectToAction("Index"); // Redirect to the index page
            }
            else
            {
                return View(user); // If the submitted data is not valid, return the edit user view again
            }
        }

        // Action to delete a user
        public IActionResult DeleteUser(int id)
        {
            _repo.DeleteUser(id); // Delete the user from the repository
            return RedirectToAction("Index"); // Redirect to the index page
        }
    }
}

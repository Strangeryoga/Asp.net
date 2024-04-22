using System;
using System.Collections.Generic;
using System.Linq;
using UserRegistration.Data;
using UserRegistration.Models;
using UserRegistration.Repo;

namespace UserRegistration.Service
{
    public class UserService : UserRepo
    {
        private readonly UserDbContext _context;

        public UserService(UserDbContext context)
        {
            _context = context;
        }

        // Retrieve all users from the database
        public List<User> GetUsers()
        {
            return _context.user.ToList();
        }

        // Retrieve a user by their ID from the database
        public User GetUserById(int userId)
        {
            return _context.user.FirstOrDefault(u => u.Id == userId);
        }

        // Add a new user to the database
        public void NewUser(User user)
        {
            _context.user.Add(user);
            _context.SaveChanges();
        }

        // Update an existing user in the database
        public void UpdateUser(User user)
        {
            var existingUser = _context.user.Find(user.Id);
            if (existingUser != null)
            {
                // Update existing user properties
                existingUser.FirstName = user.FirstName;
                existingUser.LastName = user.LastName;
                existingUser.Email = user.Email;
                existingUser.PhoneNumber = user.PhoneNumber;

                _context.SaveChanges();
            }
            else
            {
                throw new InvalidOperationException("User not found for update.");
                // Or handle the error according to your application's requirements
            }
        }

        // Delete a user from the database
        public void DeleteUser(int userId)
        {
            var userToDelete = _context.user.Find(userId);
            if (userToDelete != null)
            {
                _context.user.Remove(userToDelete);
                _context.SaveChanges();
            }
            else
            {
                throw new InvalidOperationException("User not found for deletion.");
                // Or handle the error according to your application's requirements
            }
        }
    }
}

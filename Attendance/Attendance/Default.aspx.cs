using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Attendance
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // This method is called when the page is loaded.
            // It can be used for initialization tasks.
        }


        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            // This method is called when the submit button is clicked.

            // Retrieve username and password from the input fields.
            string username = txtUsername.Text;
            string password = txtPassword.Text;

            // Check if the user is authenticated.
            if (AuthenticateUser(username, password))
            {
                // If authenticated, update check-in time and redirect to the index page.

                // Update check-in time in the database.
                UpdateCheckinTime(username);

                // Store the username in the session for future use.
                Session["Username"] = username;

                // Redirect to the index page.
                Response.Redirect("Index.aspx"); 
            }
            else
            {
                // If not authenticated, display an error message.
                Response.Write("Invalid username or password!");
            }
        }


        // Method to authenticate the user against hardcoded credentials.
        private bool AuthenticateUser(string username, string password)
        {
            // Hardcoded username-password pairs.
            Dictionary<string, string> hardcodedUsers = new Dictionary<string, string>
            {
                {"user1", "password1"},
                {"user2", "password2"},
            };

            // Check if the provided credentials match any hardcoded pair.
            if (hardcodedUsers.ContainsKey(username) && hardcodedUsers[username] == password)
            {
                return true; // Authentication successful.
            }
            return false;  // Authentication failed.
        }


        // Method to update check-in time in the database.
        private void UpdateCheckinTime(string username)
        {
            // Database connection string.
            string connectionString = "Data Source=DESKTOP-FROJFGN\\SQLEXPRESS;Initial Catalog=UserDB1;Integrated Security=True;Encrypt=False";

            // Open a connection to the database.
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Check if the user already exists in the database.
                if (!UserExistsInDatabase(username))
                {
                    // If the user does not exist, insert a new record with check-in time.
                    string insertQuery = "INSERT INTO Attendance (Username, Checkin) VALUES (@username, @checkin)";
                    using (SqlCommand insertCommand = new SqlCommand(insertQuery, connection))
                    {
                        insertCommand.Parameters.AddWithValue("@username", username);
                        insertCommand.Parameters.AddWithValue("@checkin", DateTime.Now);
                        insertCommand.ExecuteNonQuery();
                    }
                }
            }
        }


        // Method to check if a user already exists in the database.
        private bool UserExistsInDatabase(string username)
        {
            // Database connection string.
            string connectionString = "Data Source=DESKTOP-FROJFGN\\SQLEXPRESS;Initial Catalog=UserDB1;Integrated Security=True;Encrypt=False";

            // Open a connection to the database.
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Query to check the existence of the user in the database.
                string query = "SELECT COUNT(*) FROM Attendance WHERE Username = @username";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Set the parameter for the username.
                    command.Parameters.AddWithValue("@username", username);

                    // Execute the query and get the count.
                    int count = (int)command.ExecuteScalar();

                    // Return true if the count is greater than 0 (user exists), otherwise false.
                    return count > 0; 
                }
            }
        }  
    }
}
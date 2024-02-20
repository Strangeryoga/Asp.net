using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;

namespace Youtubeudemy
{
    public partial class Login : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Check if the form was submitted
            if (IsPostBack)
            {
                // Get the email and password entered by the user
                string email = txtEmail.Value;
                string password = txtPassword.Value;

                // Validate the email and password against the database
                if (IsValidUser(email, password))
                {
                    Session["UserID"] = GetUserID(email); 
                    Session["Email"] = email;

                    // Assuming GetUserID is a method to retrieve the user's ID based on email
                    // Authentication successful
                    Response.Redirect("UserDashboard.aspx"); // Redirect to the dashboard or another page
                }
                else
                {
                    // Authentication failed
                    // You can display an error message to the user
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Invalid email or password');", true);
                }
            }
           
        }

        // Function to validate user credentials against the database
        private bool IsValidUser(string email, string password)
        {
            // Define connection string
            string connectionString = "Data Source=DESKTOP-FROJFGN\\SQLEXPRESS;Initial Catalog=udemy;Integrated Security=True;";

            // Define SQL query to check if the user exists in the database and has an active status
            string query = "SELECT COUNT(*) FROM Users WHERE Email = @Email AND Upass = @Password AND Ustatus = 1";

            // Use a try-catch block to handle exceptions
            try
            {
                // Create a new SqlConnection using the connection string
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    // Create a new SqlCommand using the query and connection
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Add parameters to the query to prevent SQL injection
                        command.Parameters.AddWithValue("@Email", email);
                        command.Parameters.AddWithValue("@Password", password);

                        // Open the connection
                        connection.Open();

                        // Execute the query and get the result
                        int count = (int)command.ExecuteScalar();

                        // Check if the user exists and has an active status (count > 0)
                        if (count > 0)
                        {
                            return true; // User exists, credentials are correct, and status is active
                        }
                        else
                        {
                            return false; // User does not exist, credentials are incorrect, or status is inactive
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions (e.g., database connection errors)
                ClientScript.RegisterStartupScript(this.GetType(), "alert", $"alert('An error occurred: {ex.Message}');", true);
                return false; // Return false to indicate authentication failure
            }
        }


        private int GetUserID(string email)
        {
            // Define connection string
            string connectionString = "Data Source=DESKTOP-FROJFGN\\SQLEXPRESS;Initial Catalog=udemy;Integrated Security=True;";

            // Define SQL query to retrieve the UserID based on email
            string query = "SELECT UserID FROM Users WHERE Email = @Email";

            // Use a try-catch block to handle exceptions
            try
            {
                // Create a new SqlConnection using the connection string
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    // Create a new SqlCommand using the query and connection
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Add parameters to the query to prevent SQL injection
                        command.Parameters.AddWithValue("@Email", email);

                        // Open the connection
                        connection.Open();

                        // Execute the query and get the result
                        object result = command.ExecuteScalar();

                        // Check if the result is not null
                        if (result != null)
                        {
                            // Return the UserID
                            return Convert.ToInt32(result);
                        }
                        else
                        {
                            // If no user found with the provided email, return -1 or handle it accordingly
                            return -1; // Return a default value or handle it as appropriate for your application.
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions (e.g., database connection errors)
                ClientScript.RegisterStartupScript(this.GetType(), "alert", $"alert('An error occurred: {ex.Message}');", true);
                return -1; // Return -1 to indicate an error or handle it as appropriate for your application.
            }
        }

       


    }
}

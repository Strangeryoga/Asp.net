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

            // Check if the current time is within the allowed time range (9 am to 6 pm).
            if (IsCurrentTimeWithinRange())
            {
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
            else
            {
                // Display an error message if login is attempted outside the allowed time range.
                Response.Write("Login is only allowed between 9 am and 6 pm.");
            }
        }


        // Method to check if the current time is within the allowed time range.
        private bool IsCurrentTimeWithinRange()
        {
            TimeSpan currentTime = DateTime.Now.TimeOfDay;
            TimeSpan startTime = new TimeSpan(9, 0, 0);  // 9 am
            TimeSpan endTime = new TimeSpan(18, 0, 0);   // 6 pm

            return currentTime >= startTime && currentTime <= endTime;
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
            string connectionString = "Data Source=DESKTOP-8hlb8i7\\SQLEXPRESS;Initial Catalog=UserDB1;Integrated Security=True;Encrypt=False";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                if (!UserExistsInDatabase(username))
                {
                    // User doesn't exist, insert a new record with the initial check-in time.
                    string insertQuery = "INSERT INTO Attendance (Username, Checkin, TotalWorkHours) VALUES (@username, @checkin, 0)";
                    using (SqlCommand insertCommand = new SqlCommand(insertQuery, connection))
                    {
                        insertCommand.Parameters.AddWithValue("@username", username);
                        insertCommand.Parameters.AddWithValue("@checkin", DateTime.Now);
                        insertCommand.ExecuteNonQuery();
                    }
                }
                else
                {
                    // User exists, get the last check-in time and total work hours.
                    string lastCheckinQuery = "SELECT TOP 1 Checkin, TotalWorkHours FROM Attendance WHERE Username = @username ORDER BY Checkin DESC";
                    using (SqlCommand lastCheckinCommand = new SqlCommand(lastCheckinQuery, connection))
                    {
                        lastCheckinCommand.Parameters.AddWithValue("@username", username);

                        using (SqlDataReader reader = lastCheckinCommand.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                DateTime lastCheckin = reader.GetDateTime(0);
                                int totalWorkHours = reader.GetInt32(1);

                                // Close the reader before executing the update command.
                                reader.Close();

                                // Check if the last check-in was on a different day.
                                if (lastCheckin.Date < DateTime.Now.Date)
                                {
                                    // Update the reset the total work hours.
                                    string updateQuery = "UPDATE Attendance SET Checkin = @checkin, TotalWorkHours = 0 WHERE Username = @username";
                                    using (SqlCommand updateCommand = new SqlCommand(updateQuery, connection))
                                    {
                                        updateCommand.Parameters.AddWithValue("@username", username);
                                        updateCommand.Parameters.AddWithValue("@checkin", DateTime.Now);
                                        updateCommand.ExecuteNonQuery();
                                    }
                                }
                                else
                                {
                                    // Update add the time difference to total work hours.
                                    TimeSpan timeDifference = DateTime.Now - lastCheckin;
                                    int minutesWorked = (int)timeDifference.TotalMinutes;

                                    string updateQuery = "UPDATE Attendance SET TotalWorkHours = TotalWorkHours + @minutesWorked WHERE Username = @username";
                                    using (SqlCommand updateCommand = new SqlCommand(updateQuery, connection))
                                    {
                                        updateCommand.Parameters.AddWithValue("@username", username);
                                        updateCommand.Parameters.AddWithValue("@checkin", DateTime.Now);
                                        updateCommand.Parameters.AddWithValue("@minutesWorked", minutesWorked);
                                        updateCommand.ExecuteNonQuery();
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }


        // Method to check if a user already exists in the database.
        private bool UserExistsInDatabase(string username)
        {
            // Database connection string.
            string connectionString = "Data Source=DESKTOP-8hlb8i7\\SQLEXPRESS;Initial Catalog=UserDB1;Integrated Security=True;Encrypt=False";

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
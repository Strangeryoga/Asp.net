using System;
using System.Data.SqlClient;

namespace Attendance
{
    public partial class Index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // This method is called when the page is loaded.
            // It can be used for initialization tasks.
            // Check if the username is available in the session.
            if (Session["Username"] != null)
            {
                // If yes, display it in the label.
                lblUsername.Text = "Logged in as: " + Session["Username"].ToString();
            }
            else
            {
                // If not, redirect to the login page (or take appropriate action).
                Response.Redirect("Default.aspx");
            }
        }


        protected void btnSignOut_Click(object sender, EventArgs e)
        {
            // This method is called when the sign-out button is clicked.

            // Retrieve the username from the session.
            string username = Session["Username"] as string;

            // Check if the username is not null or empty.
            if (!string.IsNullOrEmpty(username))
            {
                // If a valid username is present, update the checkout time and perform sign-out.

                // Update checkout time in the database.
                UpdateCheckoutTime(username);

                // Clear the session to sign out the user.
                Session.Clear();

                // Redirect to the login page or any other appropriate page.
                Response.Redirect("Default.aspx");
            }
        }


        // Method to update the checkout time in the database.
        private void UpdateCheckoutTime(string username)
        {
            // Database connection string.
            string connectionString = "Data Source=DESKTOP-8hlb8i7\\SQLEXPRESS;Initial Catalog=UserDB1;Integrated Security=True;Encrypt=False";

            // Open a connection to the database.
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // SQL query to update the checkout time for the specified username.
                string updateQuery = "UPDATE Attendance SET Checkout = @checkout WHERE Username = @username";
                using (SqlCommand updateCommand = new SqlCommand(updateQuery, connection))
                {
                    // Set parameters for the SQL query.
                    updateCommand.Parameters.AddWithValue("@username", username);
                    updateCommand.Parameters.AddWithValue("@checkout", DateTime.Now);

                    // Execute the SQL query to update checkout time.
                    updateCommand.ExecuteNonQuery();
                }
            }
        }
    }
}
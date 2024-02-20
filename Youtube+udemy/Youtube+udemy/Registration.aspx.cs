using System;
using System.Configuration;
using System.Data.SqlClient;

namespace Youtubeudemy
{
    public partial class Register : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            string name = txtName.Text;
            string email = txtEmail.Text;
            string password = txtPassword.Text;
            int status = 0; // Assuming 0 represents 'unblocked'

            string connectionString = "Data Source=DESKTOP-FROJFGN\\SQLEXPRESS;Initial Catalog=udemy;Integrated Security=True;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO Users (UName, Email, Upass, Ustatus) VALUES (@Name, @Email, @Password, @Status)";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Name", name);
                command.Parameters.AddWithValue("@Email", email);
                command.Parameters.AddWithValue("@Password", password);
                command.Parameters.AddWithValue("@Status", status); // Add status parameter

                try
                {
                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        // Registration successful
                        Response.Redirect("Login.aspx"); // Redirect to login page
                    }
                    else
                    {
                        // Registration failed
                        // Handle error appropriately
                        // For example, you can display an error message
                        Response.Write("<script>alert('Registration failed.');</script>");
                    }
                }
                catch (Exception ex)
                {
                    // Handle exceptions
                    // Print the exception message for debugging
                    Response.Write("<script>alert('Exception: " + ex.Message + "');</script>");
                }
            }
        }
    }
}

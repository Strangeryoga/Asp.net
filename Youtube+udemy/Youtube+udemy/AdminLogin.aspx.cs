using System;
using System.Data.SqlClient;
using System.Configuration;

namespace Youtubeudemy
{
    public partial class AdminLogin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["AdminLoggedIn"] != null && (bool)Session["AdminLoggedIn"])
            {
                Response.Redirect("Dashboard.aspx");
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text.Trim();
            string password = txtPassword.Text;

            // Check if email and password match the admin credentials (Replace with your authentication logic)
            if (CheckAdminCredentials(email, password))
            {
                Session["AdminLoggedIn"] = true;
                Response.Redirect("Dashboard.aspx");
            }
            else
            {
                lblError.Text = "Invalid email or password";
            }
        }

        private bool CheckAdminCredentials(string email, string password)
        {
            // Replace "YourConnectionString" with your actual connection string name in web.config
            string connectionString = "Data Source=DESKTOP-FROJFGN\\SQLEXPRESS;Initial Catalog=udemy;Integrated Security=True;";
            string query = "SELECT COUNT(*) FROM AdminUsers WHERE Email = @Email AND Password = @Password";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Email", email);
                    command.Parameters.AddWithValue("@Password", password);
                    connection.Open();
                    int count = (int)command.ExecuteScalar();
                    return count > 0;
                }
            }
        }
    }
}

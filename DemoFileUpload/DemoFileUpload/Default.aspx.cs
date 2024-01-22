using System;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;

namespace DemoFileUpload
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            string connectionString = "Data Source=DESKTOP-M29L4T5\\SQLEXPRESS;Initial Catalog=dbuser;Integrated Security=True;Encrypt=False";

            // Capture the full name and store it in a session variable
            string fullName = txtFullName.Text;
            Session["FullName"] = fullName;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string email = txtEmail.Text;
                string password = HashPassword(txtPassword.Text);

                string query = "insert into Users (FullName, Email, Password) VALUES (@FullName, @Email, @Password)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@FullName", fullName);
                    command.Parameters.AddWithValue("@Email", email);
                    command.Parameters.AddWithValue("@Password", password);

                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        Response.Write("Registration successful!");
                    }
                    else
                    {
                        Response.Write("Registration failed!");
                    }
                }
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string connectionString = "Data Source=DESKTOP-FROJFGN\\SQLEXPRESS;Initial Catalog=UserDB1;Integrated Security=True;Encrypt=False";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string email = txtLoginEmail.Text;
                string password = HashPassword(txtLoginPassword.Text);

                string query = "SELECT ID, FullName FROM Users WHERE Email = @Email AND Password = @Password";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Email", email);
                    command.Parameters.AddWithValue("@Password", password);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // Login successful
                            string userId = reader["ID"].ToString();
                            string fullName = reader["FullName"].ToString();

                            Session["FullName"] = fullName;

                            Response.Write($"Login successful! Welcome, {fullName}");
                            Response.Redirect("index.aspx");
                        }
                        else
                        {
                            // Login failed
                            Response.Write("Login failed. Please check your email and password.");
                        }
                    }
                }
            }
        }

        private string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));

                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < hashedBytes.Length; i++)
                {
                    builder.Append(hashedBytes[i].ToString("x2"));
                }

                return builder.ToString();
            }
        }
    }
}

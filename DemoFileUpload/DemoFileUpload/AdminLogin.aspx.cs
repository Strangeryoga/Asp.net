using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DemoFileUpload
{
    public partial class AdminLogin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click1(object sender, EventArgs e)
        {
            string EMAIL, PASS, Error;

            EMAIL = TextBox1.Text;
            PASS = TextBox2.Text;
            Error = Label1.Text;


            // Update connection string with your database details
            string connectionString = "Data Source=DESKTOP-8HLB8I7\\SQLEXPRESS;Initial Catalog=demo;Integrated Security=True;Encrypt=False";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Replace "YourTableName" with the actual table name
                string query = "SELECT * FROM Admin WHERE Email = '" + EMAIL + "' AND Password = '" + PASS + "'";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Email", EMAIL);
                    command.Parameters.AddWithValue("@Password", PASS);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            // Fetch and display the records
                            while (reader.Read())
                            {
                                // Access the columns using the column name or index
                                string Lemail = reader["Email"].ToString();
                                string Lpass = reader["Password"].ToString();

                                if (EMAIL == Lemail & PASS == Lpass)
                                {
                                    Response.Redirect("WebForm1.aspx");
                                }

                            }
                        }
                        else
                        {
                            Response.Redirect("AdminLogin.aspx");

                            // Example: Response.Write("No matching records found.<br/>");
                        }
                    }
                }
            }
        }
    }
}


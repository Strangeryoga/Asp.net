using System;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;

namespace InterviewSchedule
{
    public partial class Page1 : System.Web.UI.Page
    {
        protected void Button1_Click(object sender, EventArgs e)
        {
            string name = TextBox1.Text;
            string address = TextBox2.Text;

            
            if (FileUpload1.HasFile)
            {
                byte[] resumeData = FileUpload1.FileBytes;
                SaveToDatabase(name, address, resumeData);
                string userName = TextBox1.Text;
                Session["UserName"] = userName;

                Response.Write("<script>alert('File Uploaded Successfully')</script>");
                Response.Redirect("Page2.aspx");
            }
            else
            {
                
            }
        }

        private void SaveToDatabase(string name, string address, byte[] resumeData)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["dbconfig"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = $"INSERT INTO Resumes (Name, Address, ResumeData) VALUES ('{name}', '{address}', @ResumeData)";
                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@ResumeData", resumeData);

                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                   
                }
            }
        }

    }
}

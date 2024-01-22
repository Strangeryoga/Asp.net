using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Mime;
using System.Web.Services.Description;
using System.Data.SqlClient;

namespace EmailOTPAttachment
{
    public partial class Dashboard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Check if the page is not being loaded in response to a postback
            if (!IsPostBack)
            {
                // If not a postback, populate the email dropdown list
                PopulateEmailDropDown();
            }

        }

        private void PopulateEmailDropDown()
        {
            // Database connection string
            string connectionString = "Data Source=DESKTOP-FROJFGN\\SQLEXPRESS;Initial Catalog=UserDB1;Integrated Security=True;Encrypt=False";

            // Using statement ensures proper resource disposal
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // SQL query to select email addresses from the database
                string query = "SELECT Email FROM EmailPass";

                // Using statement ensures proper resource disposal
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Open the database connection
                    connection.Open();

                    // Execute the query and retrieve the data reader
                    SqlDataReader reader = command.ExecuteReader();

                    // Bind the data to the dropdown list
                    DropDownList1.DataSource = reader;
                    DropDownList1.DataTextField = "Email";
                    DropDownList1.DataValueField = "Email";
                    DropDownList1.DataBind();

                    // Close the data reader
                    reader.Close();
                }
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            // Get the selected email address from the dropdown list
            string to = DropDownList1.SelectedValue;

            // Call the method to send the email with attachment
            sentAttachment(to);

            // Display a JavaScript alert indicating that the email has been sent successfully
            Response.Write("<script>alert('Send Successfully')</script>");

        }

        public void sentAttachment(string toemail)
        {
            // Create a MailMessage object
            MailMessage mail = new MailMessage();

            // Set the sender, recipient, subject, and body of the email
            mail.From = new MailAddress("stalefold37@gmail.com");
            mail.To.Add(toemail);
            mail.Subject = TextBox1.Text.ToString();
            mail.Body = TextBox2.Text.ToString();

            // Check if a file has been uploaded
            if (FileUpload1.HasFile)
            {
                // If a file is uploaded, create an Attachment object and add it to the email
                Attachment attachment = new Attachment(FileUpload1.PostedFile.InputStream, FileUpload1.FileName);
                mail.Attachments.Add(attachment);
            }

            // Configure the SMTP client for Gmail
            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com");
            smtpClient.Port = 587;
            smtpClient.Credentials = new NetworkCredential("stalefold37@gmail.com", "mbysugodvmpiqrrj");
            smtpClient.EnableSsl = true;

            try
            {
                // Try sending the email
                smtpClient.Send(mail);
                Response.Write("Email sent successfully!");
            }
            catch (Exception ex)
            {
                // Handle any exceptions that occur during the email sending process
                Response.Write("Error: " + ex.Message);
            }
            finally
            {
                // Dispose of the MailMessage and SmtpClient objects
                mail.Dispose();
                smtpClient.Dispose();
            }

        }
    }
}
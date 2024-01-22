using System;
using System.IO;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using System.Net;
using System.Net.Mail;
using System.Data.SqlClient;

namespace EmailPdf
{
    public partial class NewPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // No logic in Page_Load for generating PDF and sending email
        }

        protected void btnGeneratePDF_Click(object sender, EventArgs e)
        {
            // Check if query parameters exist
            if (!String.IsNullOrEmpty(Request.QueryString["Name"]) &&
                !String.IsNullOrEmpty(Request.QueryString["Department"]) &&
                !String.IsNullOrEmpty(Request.QueryString["Salary"]) &&
                !String.IsNullOrEmpty(Request.QueryString["Email"]))
            {
                // Retrieve values from query parameters
                string Name = Request.QueryString["Name"];
                string Department = Request.QueryString["Department"];
                string Salary = Request.QueryString["Salary"];
                string Email = Request.QueryString["Email"];

                // Generate PDF and send email
                GeneratePDFAndSendEmail(Name, Department, Salary, Email);
            }
            else
            {
                // Handle the case where query parameters are missing
                Response.Write("Invalid URL. Missing parameters.");
            }
        }

        private void GeneratePDFAndSendEmail(string name, string dept, string salary, string email)
        {
            // Set the file path for the generated PDF
            string filePath = Server.MapPath("~/EmployeeDetails3.pdf");

            // Create a PdfWriter instance
            using (PdfWriter writer = new PdfWriter(filePath))
            {
                // Create a PdfDocument instance
                using (PdfDocument pdf = new PdfDocument(writer))
                {
                    // Create a Document instance
                    Document document = new Document(pdf);

                    // Add content to the document
                    document.Add(new Paragraph($"Name: {name}"));
                    document.Add(new Paragraph($"Department: {dept}"));
                    document.Add(new Paragraph($"Salary: {salary}"));
                    document.Add(new Paragraph($"Email: {email}"));
                }
            }

            // Insert into the database
            InsertIntoDatabase(name, filePath);

            // Send email with the generated PDF as an attachment
            SendEmail(email, filePath);
        }

        private void InsertIntoDatabase(string name, string filePath)
        {
            string connectionString = "Data Source=DESKTOP-FROJFGN\\SQLEXPRESS;Initial Catalog=UserDB1;Integrated Security=True;Encrypt=False"; // Replace with your database connection string

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Use parameterized query to prevent SQL injection
                string query = "INSERT INTO EmployeeDetails (Name, FilePath) VALUES (@Name, @FilePath)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Name", name);
                    command.Parameters.AddWithValue("@FilePath", filePath);

                    command.ExecuteNonQuery();
                }
            }
        }

        private void SendEmail(string toEmail, string attachmentPath)
        {
            using (MailMessage mail = new MailMessage())
            {
                mail.From = new MailAddress("stalefold37@gmail.com"); // Set your email address
                mail.To.Add(toEmail);
                mail.Subject = "Employee Details PDF";
                mail.Body = "Please find the attached Employee Details PDF.";

                // Attach the PDF file
                Attachment attachment = new Attachment(attachmentPath);
                mail.Attachments.Add(attachment);

                using (SmtpClient smtp = new SmtpClient("smtp.gmail.com")) // Set your SMTP server
                {
                    smtp.Port = 587; // Set your SMTP port
                    smtp.Credentials = new NetworkCredential("stalefold37@gmail.com", "mbysugodvmpiqrrj"); // Set your email credentials
                    smtp.EnableSsl = true;

                    // Send the email
                    smtp.Send(mail);
                }
            }

            // Optionally, you can redirect the user to a confirmation page
            Response.Write("Email Sent");
        }
    }
}

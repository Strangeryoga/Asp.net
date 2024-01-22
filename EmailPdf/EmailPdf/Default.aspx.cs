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
    public partial class Default : System.Web.UI.Page
    {
        protected void Button1_Click(object sender, EventArgs e)
        {
            // Get values from textboxes
            string Name = TextBox1.Text;
            string Department = TextBox2.Text;
            string Salary = TextBox3.Text;
            string Email = TextBox4.Text;

            // Generate PDF and send email
            GeneratePDFAndSendEmail(Name, Department, Salary, Email);
        }

        private void GeneratePDFAndSendEmail(string name, string dept, string salary, string email)
        {
            // Set the file path for the generated PDF
            string filePath = Server.MapPath("~/EmployeeDetails.pdf");

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

            // Optionally, you can redirect the user to a confirmation page
            Response.Write("PDF generated and Email Sent");
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
        }
    }
}

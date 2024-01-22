using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EmailOTPAttachment
{
    public partial class LoginOTP : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // This method is executed when the page is loaded, but it's currently empty.
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            // Get the email address entered by the user
            string to = TextBox1.Text;

            // Generate a random OTP (One-Time Password)
            string otp = GenerateOTP();

            // Send the OTP to the entered email address
            sentOTP(to, otp);

            // Store the email and OTP in session variables for verification on the next page
            Session["GT"] = to;
            Session["GOT"] = otp;
            Response.Redirect("VerifyOTP.aspx");

        }

        // Method to generate a random OTP
        public string GenerateOTP()
        {
            // Create a random number generator
            Random random = new Random();

            // Generate a random 6-digit OTP
            return random.Next(100000, 999999).ToString();
        }

        // Method to send the OTP via email
        public void sentOTP(string toemail, string otp)
        {
            // Create a MailMessage object
            MailMessage mail = new MailMessage();

            // Set the sender, recipient, subject, and body of the email
            mail.From = new MailAddress("stalefold37@gmail.com");
            mail.To.Add(toemail);
            mail.Subject = "Your OTP for Login";
            mail.Body = $"Your OTP is: {otp}";

            // Configure the SMTP client for Gmail
            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com");
            smtpClient.Port = 587;
            smtpClient.Credentials = new NetworkCredential("stalefold37@gmail.com", "mbysugodvmpiqrrj");
            smtpClient.EnableSsl = true;

            // Send the email containing the OTP
            smtpClient.Send(mail);

        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EmailOTP
{
    public partial class LoginOTP : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string to = TextBox1.Text;
            string otp = GenerateOTP();
            sentOTP(to, otp);

            Session["GOT"] = otp;
            Response.Redirect("VerifyOTP.aspx");

        }

        public string GenerateOTP()
        {
            Random random = new Random();
            return random.Next(100000, 999999).ToString();
        }

        public void sentOTP(string toemail, string otp)
        {
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress("stalefold37@gmail.com");
            mail.To.Add(toemail);
            mail.Subject = "Your OTP for Login";
            mail.Body = $"Your OTP is: {otp}";

            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com");
            smtpClient.Port = 587;
            smtpClient.Credentials = new NetworkCredential("stalefold37@gmail.com", "mbysugodvmpiqrrj");

            smtpClient.EnableSsl = true;

            smtpClient.Send(mail);

        }
    }
}
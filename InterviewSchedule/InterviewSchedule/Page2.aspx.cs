using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Net;
using System.Net.Mail;
using System.Web.UI.WebControls;

namespace InterviewSchedule
{
    public partial class Page2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["UserName"] != null)
                {
                    string userName = Session["UserName"].ToString();
                    LabelUserName.Text = $"Welcome, {userName}!";
                }
                else
                {
                    Response.Write("<script>alert('You are not allowed to access this page')</script>");
                    Response.End();
                    return;

                }

                for (int i = 0; i <= 6; i++)
                {
                    DateTime day = DateTime.Today.AddDays(i);

                    if (day.DayOfWeek >= DayOfWeek.Monday && day.DayOfWeek <= DayOfWeek.Saturday)
                    {
                        DropDownList1.Items.Add(day.ToString("dddd"));
                    }
                }

                for (int hour = 10; hour <= 13; hour++)
                {
                    for (int minute = 0; minute <= 30; minute += 30)
                    {
                        string time = $"{hour:D2}:{minute:D2} {(hour < 12 ? "AM" : "PM")}";
                        DropDownList2.Items.Add(time);
                    }
                }
            }
        }


        protected void Button1_Click(object sender, EventArgs e)
        {
            string selectedDay = DropDownList1.SelectedValue;
            string selectedSlot = DropDownList2.SelectedValue;
            string userEmail = TextBox1.Text;
            string userName = Session["UserName"].ToString();


            if (IsSlotAvailable(selectedDay, selectedSlot))
            {
                string emailBody = $"Your interview is scheduled for {selectedDay} at {selectedSlot}.";
                SaveToDatabase(userName, selectedDay, selectedSlot, userEmail);
                SendEmail(userEmail, "Interview Schedule", emailBody);
            }
            else
            {
                Response.Write("<script>alert('Selected slot is not available. Please choose another slot.')</script>");
            }
        }

        private bool IsSlotAvailable(string scheduledDay, string scheduledSlot)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["dbconfig"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = $"SELECT COUNT(*) FROM InterviewSchedule WHERE ScheduledDay = '{scheduledDay}' AND ScheduledSlot = '{scheduledSlot}'";
                SqlCommand command = new SqlCommand(query, connection);

                try
                {
                    connection.Open();
                    int count = Convert.ToInt32(command.ExecuteScalar());
                    return count == 0;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
        }


        private void SaveToDatabase(string userName, string scheduledDay, string scheduledSlot, string userEmail)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["dbconfig"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = $"INSERT INTO InterviewSchedule (UserName, ScheduledDay, ScheduledSlot, UserEmail) " +
                               $"VALUES ('{userName}', '{scheduledDay}', '{scheduledSlot}', '{userEmail}')";

                SqlCommand command = new SqlCommand(query, connection);

                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error saving to database: {ex.Message}");
                }
            }
        }

        private void SendEmail(string toEmail, string subject, string body)
        {
            string smtpServer = "smtp.gmail.com";
            string smtpUsername = "stalefold37@gmail.com";
            string smtpPassword = "mbysugodvmpiqrrj";
            int smtpPort = 587;

            using (MailMessage mail = new MailMessage())
            {
                mail.From = new MailAddress("stalefold37@gmail.com");
                mail.To.Add(toEmail);
                mail.Subject = subject;
                mail.Body = body;
                mail.IsBodyHtml = false;

                using (SmtpClient smtp = new SmtpClient(smtpServer, smtpPort))
                {
                    smtp.Credentials = new NetworkCredential(smtpUsername, smtpPassword);
                    smtp.EnableSsl = true;

                    try
                    {
                        smtp.Send(mail);
                        Response.Write("<script>alert('Your slot is booked')</script>");
                    }
                    catch (Exception ex)
                    {
                        Response.Write($"<script>alert('Error sending email: {ex.Message}')</script>");
                    }
                }
            }
        }
    }
}
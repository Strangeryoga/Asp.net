using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Xml.Linq;

namespace AppointmentSetter
{
    public partial class Appointment : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // This method is executed when the page is loaded.
            // You can perform any initialization here if needed.
        }

        protected void btnSetAppointment_Click(object sender, EventArgs e)
        {
            // Event handler for the Set Appointment button click.

            // Retrieve date and time from the TextBoxes.
            string Name = TextBox1.Text.ToString();
            string Type = DropDownList1.SelectedValue;
            string date = txtDate.Text;
            string time = txtTime.Text;

            // Perform validation (you can add more validation logic here).
            if (string.IsNullOrEmpty(Name) || string.IsNullOrEmpty(Type) ||  string.IsNullOrEmpty(date) || string.IsNullOrEmpty(time))
            {
                lblMessage.Text = "Please enter your details.";
            }
            else
            {
                // Display the confirmation message.
                lblMessage.Text = $"{Name} your appointment for {Type} is set on {date} at {time}.";
                SaveToDatabase(Name, Type, date, time);

            }
        }

        private void SaveToDatabase(string name, string type, string date, string time)
        {
            string cs = ConfigurationManager.ConnectionStrings["dbconfig"].ConnectionString;

            using(SqlConnection connection = new SqlConnection(cs))
            {
                connection.Open();

                string query = $"INSERT INTO Appointment (Name, Type, UploadDate, UploadTime ) VALUES ('{name}', '{type}', '{date}', '{time}')";

                using (SqlCommand command = new SqlCommand(query, connection)) 
                {
                    command.ExecuteNonQuery();

                }
                
            }
        }
    }
}

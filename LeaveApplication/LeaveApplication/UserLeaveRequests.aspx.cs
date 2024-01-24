using System;
using System.Data.SqlClient;
using System.Linq;

namespace LeaveApplication
{
    public partial class UserLeaveRequests : System.Web.UI.Page
    {
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            string connectionString = "Data Source=DESKTOP-FROJFGN\\SQLEXPRESS;Initial Catalog=UserDB;Integrated Security=True;Encrypt=False";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "INSERT INTO LeaveRequests (UserID, StartDate, EndDate, Reason, Status) " +
                               "VALUES (@UserID, @StartDate, @EndDate, @Reason, 'Pending')";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Assuming you have a User ID for the leave applicant
                    command.Parameters.AddWithValue("@UserID", "User123");
                    command.Parameters.AddWithValue("@StartDate", txtStartDate.Text);
                    command.Parameters.AddWithValue("@EndDate", txtEndDate.Text);
                    command.Parameters.AddWithValue("@Reason", txtReason.Text);

                    command.ExecuteNonQuery();
                }
            }

            lblMessage.Text = "Leave request submitted successfully!";
            lblMessage.ForeColor = System.Drawing.Color.Green;
            lblMessage.Visible = true;

            UpdateLeaveLabels();

            // Set the "absent" session after updating labels
            Session["absent"] = lblAbsentDays.Text;
        }

        protected void txtStartDate_TextChanged(object sender, EventArgs e)
        {
            // Update labels based on leave days selected
            UpdateLeaveLabels();
        }

        protected void txtEndDate_TextChanged(object sender, EventArgs e)
        {
            // Update labels based on leave days selected
            UpdateLeaveLabels();
        }

        private void UpdateLeaveLabels()
        {
            DateTime startDate, endDate;

            if (DateTime.TryParse(txtStartDate.Text, out startDate) && DateTime.TryParse(txtEndDate.Text, out endDate))
            {
                // Calculate the number of leave days
                int leaveDays = (int)(endDate - startDate).TotalDays + 1;

                // Update Pending Leaves label
                lblPendingLeaves.Text = "Pending Leaves: " + (leaveDays > 2 ? 0 : 2 - leaveDays);

                // Update Absent Days Remaining label
                lblAbsentDays.Text = " " + (leaveDays > 2 ? leaveDays - 2 : 0);

                // Extract the numeric part from the label's text
                string numericPart = new string(lblAbsentDays.Text.Where(char.IsDigit).ToArray());

                // Set the "absent" session with the extracted numeric part
                Session["absent"] = numericPart;
            }
        }
    }
}

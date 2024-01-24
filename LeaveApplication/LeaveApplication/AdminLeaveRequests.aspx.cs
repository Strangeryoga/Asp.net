using System;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.InteropServices.ComTypes;

namespace LeaveApplication
{
    public partial class AdminLeaveRequests : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadLeaveRequests();
            }
        }

        private void LoadLeaveRequests()
        {
            string connectionString = "Data Source=DESKTOP-FROJFGN\\SQLEXPRESS;Initial Catalog=UserDB;Integrated Security=True;Encrypt=False";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT * FROM LeaveRequests";
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                DataSet ds = new DataSet();
                adapter.Fill(ds);

                GridView1.DataSource = ds;
                GridView1.DataBind();

                GridView1.DataKeyNames = new string[] { "RequestID" };
               
            }
        }

        protected void GridView1_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Approve" || e.CommandName == "Reject")
            {
                int rowIndex = Convert.ToInt32(e.CommandArgument);

                if (rowIndex >= 0 && rowIndex < GridView1.Rows.Count)
                {
                    int requestID = Convert.ToInt32(GridView1.DataKeys[rowIndex].Value);

                    string connectionString = "Data Source=DESKTOP-FROJFGN\\SQLEXPRESS;Initial Catalog=UserDB;Integrated Security=True;Encrypt=False";

                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();

                        if (e.CommandName == "Approve")
                        {
                            UpdateLeaveStatus(connection, requestID, "Approved");
                        }
                        else if (e.CommandName == "Reject")
                        {
                            UpdateLeaveStatus(connection, requestID, "Rejected");
                        }

                        LoadLeaveRequests(); 
                    }
                }
            }
            else if (e.CommandName == "Details")
            {
                Response.Redirect("EnterDetails.aspx");

            }
        }

        private void UpdateLeaveStatus(SqlConnection connection, int requestID, string status)
        {
            string query = "UPDATE LeaveRequests SET Status = @Status WHERE RequestID = @RequestID";

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Status", status);
                command.Parameters.AddWithValue("@RequestID", requestID);

                command.ExecuteNonQuery();
            }
        }
    }
}

using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Youtubeudemy
{
    public partial class UserCourses : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Check if the UserID query parameter exists
                if (Request.QueryString["UserID"] != null)
                {
                    int userId;
                    if (int.TryParse(Request.QueryString["UserID"], out userId))
                    {
                        // Bind user courses data based on the UserID
                        BindUserCoursesData(userId);
                        Session["userId"] = userId;
                    }
                    else
                    {
                        ShowMessage("Invalid user ID.");
                    }
                }
                else
                {
                    ShowMessage("User ID is not provided.");
                }
            }
        }


        protected void btnAddCourse_Click(object sender, EventArgs e)
        {
            Response.Redirect($"AddCourse.aspx");
        }


        private void BindUserCoursesData(int userId)
        {
            string connectionString = "Data Source=DESKTOP-FROJFGN\\SQLEXPRESS;Initial Catalog=udemy;Integrated Security=True;";
            string query = @"
        SELECT UC.UserCourseID, U.UName, U.Email, MC.MasterCourseName, UC.Status
        FROM UserCourses UC
        INNER JOIN Users U ON UC.UserID = U.UserID
        INNER JOIN MasterCourse MC ON UC.MasterCourseID = MC.MasterCourseID
        WHERE U.UserID = @UserID
    ";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserID", userId);

                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        gridViewUserCourses.DataSource = dataTable;
                        gridViewUserCourses.DataBind();
                    }
                }
            }
        }



        private void ShowMessage(string message)
        {
            // Display a message using a JavaScript alert
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", $"alert('{message}')", true);
        }

        private void PopulateCoursesCheckBoxList(CheckBoxList cblCourses)
        {
            string connectionString = "Data Source=DESKTOP-FROJFGN\\SQLEXPRESS;Initial Catalog=udemy;Integrated Security=True;";
            string query = "SELECT MasterCourseID, MasterCourseName FROM MasterCourse";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        cblCourses.DataSource = reader;
                        cblCourses.DataTextField = "MasterCourseName";
                        cblCourses.DataValueField = "MasterCourseID";
                        cblCourses.DataBind();
                    }
                }
            }
        }
        protected void gridViewUserCourses_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // Find the CheckBoxList in the current row
                CheckBoxList cblCourses = (CheckBoxList)e.Row.FindControl("cblCourses");

                // Populate CheckBoxList with course options from the database
                PopulateCoursesCheckBoxList(cblCourses);
            }
        }

        protected void btnStatus_Command(object sender, CommandEventArgs e)
        {
            // Handle button click to toggle status
            if (e.CommandName == "ToggleStatus")
            {
                int userId = Convert.ToInt32(e.CommandArgument);
                ToggleUserStatus(userId);
                BindUserData(userId); // Rebind GridView after status update
                Response.Redirect("Dashboard.aspx"); // Redirect to UserCourses page with UserID query parameter
            }
        }

        private void ToggleUserStatus(int userId)
        {
            // Replace "YourConnectionString" with your actual connection string name in web.config
            string connectionString = "Data Source=DESKTOP-FROJFGN\\SQLEXPRESS;Initial Catalog=udemy;Integrated Security=True;";
            string query = "UPDATE UserCourses SET Status = ~Status WHERE UserCourseID = @UserId";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserId", userId);
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        private void BindUserData(int userId)
        {
            string connectionString = "Data Source=DESKTOP-FROJFGN\\SQLEXPRESS;Initial Catalog=udemy;Integrated Security=True;";
            string query = @"
        SELECT UC.UserCourseID, U.UName, U.Email, MC.MasterCourseName, UC.Status
        FROM UserCourses UC
        INNER JOIN Users U ON UC.UserID = U.UserID
        INNER JOIN MasterCourse MC ON UC.MasterCourseID = MC.MasterCourseID
        WHERE U.UserID = @UserID
    ";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserID", userId); // Add this line to set the @UserID parameter
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        gridViewUserCourses.DataSource = dataTable;
                        gridViewUserCourses.DataBind();
                    }
                }
            }
        }

    }
}

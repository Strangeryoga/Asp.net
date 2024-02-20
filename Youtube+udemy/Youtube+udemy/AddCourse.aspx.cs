using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Youtubeudemy
{
    public partial class AddCourse : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Populate the DropDownList with course names from the database
                PopulateMasterCoursesDropdown();

                
            }
        }

        private void PopulateMasterCoursesDropdown()
        {
            try
            {
                // Define connection string
                string connectionString = "Data Source=DESKTOP-FROJFGN\\SQLEXPRESS;Initial Catalog=udemy;Integrated Security=True;";

                // Define SQL query to retrieve master courses
                string query = "SELECT MasterCourseID, MasterCourseName FROM MasterCourse";

                // Create a new DataTable to store the results
                DataTable masterCourses = new DataTable();

                // Use a try-catch block to handle exceptions
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Open the connection
                        connection.Open();

                        // Execute the query and fill the DataTable with the results
                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            adapter.Fill(masterCourses);
                        }
                    }
                }

                // Bind the DataTable to the dropdown list
                ddlMasterCourses.DataSource = masterCourses;
                ddlMasterCourses.DataTextField = "MasterCourseName";
                ddlMasterCourses.DataValueField = "MasterCourseID";
                ddlMasterCourses.DataBind();

                // Select the first master course by default
                if (ddlMasterCourses.Items.Count > 0)
                {
                    // Fetch and bind videos for the first master course by default
                    //BindVideos(int.Parse(ddlMasterCourses.SelectedValue));
                }
            }
            catch (Exception ex)
            {
                // Handle exception
                throw ex;
            }
        }


        protected void btnAddCourse_Click(object sender, EventArgs e)
        {
            string selectedCourseId = ddlMasterCourses.SelectedValue;
            if (!string.IsNullOrEmpty(selectedCourseId))
            {
                // Insert the selected course into the database
                InsertCourseIntoDatabase(selectedCourseId);

                // Redirect the user back to the UserCourses page or any other page as needed
                Response.Redirect($"UserCourses.aspx?UserID={Session["userId"]}");
            }
            else
            {
                // Show error message or handle the case where no course is selected
            }
        }

        private void InsertCourseIntoDatabase(string masterCourseId)
        {
            string connectionString = "Data Source=DESKTOP-FROJFGN\\SQLEXPRESS;Initial Catalog=udemy;Integrated Security=True;";
            string query = "INSERT INTO UserCourses (UserID, MasterCourseID, Status) VALUES (@UserID, @MasterCourseID, @Status)";

            // For demonstration purposes, assuming UserID and Status will be fixed or default values
            int userId = 1;
            bool status = true; // Assuming the status is active by default

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserID", Session["userId"]);
                    command.Parameters.AddWithValue("@MasterCourseID", masterCourseId);
                    command.Parameters.AddWithValue("@Status", status);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

    }
}

using System;
using System.Data.SqlClient;
using System.Data;
using System.Web.UI;
using System.Web;
using System.Web.UI.WebControls;
using System.IO;
using System.Text.RegularExpressions;

namespace Youtubeudemy
{
    public partial class UserDashboard : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Add click event handler for the logout button
            btnLogout.ServerClick += BtnLogout_Click;
            btnLogout.ServerClick += BtnLogout_Click;

            if (!IsPostBack)
            {
                string userEmail = Session["Email"] as string;
                PopulateMasterCoursesDropdown();
            }
           
        }

        private string GetConnectionString()
        {
            return "Data Source=DESKTOP-FROJFGN\\SQLEXPRESS;Initial Catalog=udemy;Integrated Security=True;";
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            // Register script for handling video click event
            ScriptManager.RegisterStartupScript(this, GetType(), "VideoClickScript", "AttachVideoClickHandler();", true);
        }

        private void BtnLogout_Click(object sender, EventArgs e)
        {
            // Perform logout operation (e.g., clear session, redirect to login page)
            Session.Clear(); // Clear session data
            Response.Redirect("Login.aspx"); // Redirect to login page
        }


        protected void ddlMasterCourses_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selectedMasterCourseId;
            if (int.TryParse(ddlMasterCourses.SelectedValue, out selectedMasterCourseId))
            {
                PopulateCoursesDropdown(selectedMasterCourseId);
            }
        }

        protected void ddlCourses_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selectedCourseId;
            if (int.TryParse(ddlCourses.SelectedValue, out selectedCourseId))
            {
                BindVideos(selectedCourseId);
            }
        }


        private void PopulateMasterCoursesDropdown()
        {
            // Get the current user's ID from the session
            int userId = Convert.ToInt32(Session["UserID"]);


            // Define your SQL query to retrieve master courses based on user enrollment
            string query = @"
        SELECT mc.MasterCourseID, mc.MasterCourseName 
        FROM MasterCourse mc
        INNER JOIN UserCourses uc ON mc.MasterCourseID = uc.MasterCourseID
        WHERE uc.UserID = @UserID AND uc.Status = 1
    ";

            // Create a new DataTable to store the results
            DataTable masterCoursesTable = new DataTable();

            // Use a try-with-resources block to automatically dispose of resources
            using (SqlConnection connection = new SqlConnection(GetConnectionString()))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Add parameter for UserID
                    command.Parameters.AddWithValue("@UserID", userId);

                    // Open the connection
                    connection.Open();

                    // Execute the query and fill the DataTable with the results
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(masterCoursesTable);
                    }
                }
            }

            // Bind the DataTable to the master course dropdown
            ddlMasterCourses.DataSource = masterCoursesTable;
            ddlMasterCourses.DataTextField = "MasterCourseName";
            ddlMasterCourses.DataValueField = "MasterCourseID";
            ddlMasterCourses.DataBind();

            // Add a default item
            ddlMasterCourses.Items.Insert(0, new ListItem("Select Master Course", ""));
        }


       

        private void PopulateCoursesDropdown(int masterCourseId)
        {

            // Define your SQL query to retrieve courses based on the selected master course
            string query = "SELECT CourseID, CourseName FROM Courses WHERE MasterCourseID = @MasterCourseID";

            // Create a new DataTable to store the results
            DataTable coursesTable = new DataTable();

            // Use a try-with-resources block to automatically dispose of resources
            using (SqlConnection connection = new SqlConnection(GetConnectionString()))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Add the parameter to the command
                    command.Parameters.AddWithValue("@MasterCourseID", masterCourseId);

                    // Open the connection
                    connection.Open();

                    // Execute the query and fill the DataTable with the results
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(coursesTable);
                    }
                }
            }

            // Bind the DataTable to the courses dropdown
            ddlCourses.DataSource = coursesTable;
            ddlCourses.DataTextField = "CourseName";
            ddlCourses.DataValueField = "CourseID";
            ddlCourses.DataBind();

            // Add a default item
            ddlCourses.Items.Insert(0, new ListItem("Select Course", ""));
        }



        

       


       

       

        
        


        private void BindVideos(int courseId)
        {
            string query = "SELECT VideoName, YouTubeEmbedCode FROM Videos WHERE CourseID = @CourseId";

            using (SqlConnection connection = new SqlConnection(GetConnectionString()))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@CourseId", courseId);
                    connection.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable videos = new DataTable();
                    adapter.Fill(videos);

                    gvVideos.DataSource = videos;
                    gvVideos.DataBind();

                   
                }
            }
        }

        protected void gvVideos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Watch")
            {
                string youTubeURL = e.CommandArgument.ToString();
                string youTubeVideoID = ExtractYouTubeVideoID(youTubeURL);

                if (!string.IsNullOrEmpty(youTubeVideoID))
                {
                    string youTubeEmbedURL = $"https://www.youtube.com/embed/{youTubeVideoID}";

                    // Open the YouTube video in a new tab
                    videoPlayer.Src = youTubeEmbedURL;
                }
                else
                {
                    ShowErrorMessage("Invalid YouTube URL.");
                }
            }
        }


        // Method to extract the YouTube video ID from the video path
        private string ExtractYouTubeVideoID(string videoPath)
        {
            // Regular expression pattern to match YouTube video IDs
            string pattern = @"(?:https?:\/\/)?(?:www\.)?(?:youtube\.com\/(?:[^\/\n\s]+\/\S+\/|(?:v|e(?:mbed)?)\/|\S*?[?&]v=)|youtu\.be\/)([a-zA-Z0-9_-]{11})";

            // Create a regular expression object
            Regex regex = new Regex(pattern);

            // Match the video path against the regular expression pattern
            Match match = regex.Match(videoPath);

            // If a match is found, return the captured YouTube video ID
            if (match.Success)
            {
                return match.Groups[1].Value;
            }
            else
            {
                return null; // Return null if no match is found
            }
        }

       

        private void ShowErrorMessage(string message)
        {
            // Implement logic to display an error message to the user
            // (e.g., display in a label, alert box, toast notification)
            // Example: Display alert box with JavaScript
            ClientScript.RegisterStartupScript(this.GetType(), "alert", $"alert('{message}');", true);
        }




    }
}

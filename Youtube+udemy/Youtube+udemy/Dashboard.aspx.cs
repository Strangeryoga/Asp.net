using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Youtubeudemy
{
    public partial class Dashboard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindUserData();
                BindVideosGrid();
                BindCoursesDropDown();
                LoadMasterCourses();
                LoadCourses();
                PopulateMasterCoursesDropdown();
            }
        }

        private string GetConnectionString()
        {
            return "Data Source=DESKTOP-FROJFGN\\SQLEXPRESS;Initial Catalog=udemy;Integrated Security=True;";
        }

        private void BindCoursesDropDown()
        {

            // Define the SQL query to fetch courses
            string query = "SELECT [CourseID], [CourseName] FROM [Courses]";

            // Create a new SqlConnection and SqlCommand
            using (SqlConnection connection = new SqlConnection(GetConnectionString()))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Open the connection
                    connection.Open();

                    // Execute the query and load results into the dropdown list
                    ddlCourses.DataSource = command.ExecuteReader();
                    ddlCourses.DataTextField = "CourseName";
                    ddlCourses.DataValueField = "CourseID";
                    ddlCourses.DataBind();
                }
            }
        }

        private void BindUserData()
        {
            string query = "SELECT UserID, UName, Email, Ustatus FROM Users";

            using (SqlConnection connection = new SqlConnection(GetConnectionString()))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        gridViewUsers.DataSource = dataTable;
                        gridViewUsers.DataBind();
                    }
                }
            }
        }

        protected void btnStatus_Command(object sender, CommandEventArgs e)
        {
            // Handle button click to toggle status
            if (e.CommandName == "ToggleStatus")
            {
                int userId = Convert.ToInt32(e.CommandArgument);
                ToggleUserStatus(userId);
                BindUserData(); // Rebind GridView after status update
            }
        }

        private void ToggleUserStatus(int userId)
        {
            string query = "UPDATE Users SET Ustatus = ~Ustatus WHERE UserID = @UserId";

            using (SqlConnection connection = new SqlConnection(GetConnectionString()))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserId", userId);
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        protected void btnAddCourse_Click(object sender, EventArgs e)
        {
            string courseName = txtCourseName.Text;

            // Check if a master course is selected
            if (!string.IsNullOrEmpty(ddlMasterCourses.SelectedValue))
            {
                int masterCourseID = int.Parse(ddlMasterCourses.SelectedValue);

                // Insert the course into the Courses table with the selected master course ID
                InsertCourse(courseName, masterCourseID);
            }
            else
            {
                // If no master course is selected, add the course directly
                AddCourse(courseName);
            }

            // Clear the input field after adding the course
            txtCourseName.Text = "";

            // Refresh the page to update the course list
            Response.Redirect(Request.Url.AbsoluteUri);
        }


        private void AddCourse(string courseName)
        {
            string query = "INSERT INTO Courses (CourseName) VALUES (@CourseName)";

            using (SqlConnection connection = new SqlConnection(GetConnectionString()))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@CourseName", courseName);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtVideoCode.Text) && ddlCourses.SelectedIndex >= 0)
            {
                string videoName = txtVideoName.Text;
                string videoCode = txtVideoCode.Text; // Assuming txtVideoCode is the TextBox for video code
                int courseId = Convert.ToInt32(ddlCourses.SelectedValue);
                int? masterCourseId = !string.IsNullOrEmpty(DropDownList1.SelectedValue) ? Convert.ToInt32(DropDownList1.SelectedValue) : (int?)null;

                // Construct the YouTube embed code using the video code
                string embedCode = $"https://www.youtube.com/embed/{videoCode}";

                // Insert video into the database with the embed code
                InsertVideoIntoDatabase(videoName, embedCode, courseId, masterCourseId);

                // Clear the text boxes after upload
                txtVideoName.Text = "";
                txtVideoCode.Text = "";

                // Bind the videos grid after upload
                BindVideosGrid();
            }
        }






        protected void gvVideos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "ViewVideo")
            {
                int rowIndex = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = gvVideos.Rows[rowIndex];
                string youtubeURL = row.Cells[2].Text; // Assuming YouTubeURL is in the third column

                // Check if the YouTube URL is not empty
                if (!string.IsNullOrEmpty(youtubeURL))
                {
                    // Redirect to the YouTube video URL
                    Response.Redirect(youtubeURL);
                }
            }
        }


        protected string GetYouTubeVideoId(string url)
        {
            // Extract video ID from YouTube URL
            string videoId = "";

            try
            {
                Uri videoUri = new Uri(url);
                string queryString = videoUri.Query;
                if (!string.IsNullOrEmpty(queryString))
                {
                    string[] queryParams = queryString.TrimStart('?').Split('&');
                    foreach (string param in queryParams)
                    {
                        string[] keyValue = param.Split('=');
                        if (keyValue.Length == 2 && keyValue[0].Equals("v", StringComparison.OrdinalIgnoreCase))
                        {
                            videoId = keyValue[1];
                            break;
                        }
                    }
                }
            }
            catch (UriFormatException ex)
            {
                // Handle invalid URL format
                // You may want to log the exception or display an error message
            }

            return videoId;
        }


        protected string GetYouTubeEmbedUrl(string videoId)
        {
            return "https://www.youtube.com/embed/" + videoId;
        }



        private DataTable GetVideosFromDatabase()
        {
            DataTable dtVideos = new DataTable();

            using (SqlConnection connection = new SqlConnection(GetConnectionString()))
            {
                string query = @"
            SELECT v.VideoID, v.VideoName, v.YouTubeEmbedCode, c.CourseName, mc.MasterCourseName 
            FROM Videos v 
            INNER JOIN Courses c ON v.CourseID = c.CourseID
            INNER JOIN MasterCourse mc ON c.MasterCourseID = mc.MasterCourseID";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        dtVideos.Load(reader);
                    }
                }
            }
            return dtVideos;
        }



        private void BindVideosGrid()
        {
            DataTable dtVideos = GetVideosFromDatabase();

            if (dtVideos != null && dtVideos.Rows.Count > 0)
            {
                gvVideos.DataSource = dtVideos;
                gvVideos.DataBind();
            }
            else
            {
                gvVideos.DataSource = null;
                gvVideos.DataBind();
            }
        }

        protected void btnAddMasterCourse_Click(object sender, EventArgs e)
        {
            string masterCourseName = txtMasterCourseName.Text;

            if (!string.IsNullOrEmpty(masterCourseName))
            {
                // Insert master course into the database
                InsertMasterCourse(masterCourseName);

                LoadMasterCourses();

                LoadCourses();

                // Refresh data after adding master course
                LoadData();
            }
            else
            {
                // Handle case where master course name is empty
                // Display error message or take appropriate action
            }
        }

        // Method to insert master course into the database
        private void InsertMasterCourse(string masterCourseName)
        {

            // Your SQL query to insert into MasterCourse table
            string query = "INSERT INTO MasterCourse (MasterCourseName) VALUES (@MasterCourseName)";

            try
            {
                using (SqlConnection connection = new SqlConnection(GetConnectionString()))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Add parameters to prevent SQL injection
                        command.Parameters.AddWithValue("@MasterCourseName", masterCourseName);

                        // Open connection and execute query
                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions
                // You may want to log the exception or display an error message to the user
            }
        }


        // Method to load data into the GridView and DropDownList
        private void LoadData()
        {
            // Load data into GridView

            // Load data into DropDownList
        }

        // Method to load master course data into dropdown list
        private void LoadMasterCourses()
        {
           

                // Your SQL query to select master course data
                string query = "SELECT MasterCourseID, MasterCourseName FROM MasterCourse";

                // Create a new connection
                using (SqlConnection connection = new SqlConnection(GetConnectionString()))
                {
                    // Create a new command with the query and connection
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Open the connection
                        connection.Open();

                        // Execute the query and get the data
                        SqlDataReader reader = command.ExecuteReader();

                        // Bind the data to the dropdown list
                        ddlMasterCourses.DataSource = reader;
                        ddlMasterCourses.DataTextField = "MasterCourseName";
                        ddlMasterCourses.DataValueField = "MasterCourseID";
                        ddlMasterCourses.DataBind();

                        // Add the "Select Master Course" option manually after data binding
                        ddlMasterCourses.Items.Insert(0, new ListItem("Select Master Course", ""));
                    }
                }
            
            
                // Handle any exceptions
                // You may want to log the exception or display an error message to the user
            
        }


        // Method to load course data into dropdown list
        private void LoadCourses()
        {
            

                // Your SQL query to select master course data
                string query = "SELECT MasterCourseID, MasterCourseName FROM MasterCourse";

                // Create a new connection
                using (SqlConnection connection = new SqlConnection(GetConnectionString()))
                {
                    // Create a new command with the query and connection
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Open the connection
                        connection.Open();

                        // Execute the query and get the data
                        SqlDataReader reader = command.ExecuteReader();

                        // Bind the data to the dropdown list
                        DropDownList1.DataSource = reader;
                        DropDownList1.DataTextField = "MasterCourseName";
                        DropDownList1.DataValueField = "MasterCourseID";
                        DropDownList1.DataBind();

                        // Add the "Select Master Course" option manually after data binding
                        DropDownList1.Items.Insert(0, new ListItem("Select Master Course", ""));
                    }
                }
           
        }

        // Method to insert course into the database
        private void InsertCourse(string courseName, int masterCourseID)
        {

            // Your SQL query to insert into Courses table
            string query = "INSERT INTO Courses (CourseName, MasterCourseID) VALUES (@CourseName, @MasterCourseID)";

            try
            {
                using (SqlConnection connection = new SqlConnection(GetConnectionString()))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Add parameters to prevent SQL injection
                        command.Parameters.AddWithValue("@CourseName", courseName);
                        command.Parameters.AddWithValue("@MasterCourseID", masterCourseID);

                        // Open connection and execute query
                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions
                // You may want to log the exception or display an error message to the user
            }

        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Get the selected master course ID
            int selectedMasterCourseID;
            if (int.TryParse(DropDownList1.SelectedValue, out selectedMasterCourseID))
            {
                // Load courses based on the selected master course
                if (selectedMasterCourseID != 0) // Check if a master course is selected
                {
                    LoadRelatedCourses(selectedMasterCourseID);
                }
                else // If no master course is selected, load all courses with MasterCourseID as null
                {
                    LoadCoursesWithNullMasterCourse();
                }
            }
        }


        // Method to load courses related to the selected master course
        private void LoadRelatedCourses(int selectedMasterCourseID)
        {
            try
            {

                // Your SQL query to select courses based on master course ID

                // SQL query to select courses related to the selected master course
                string query = "SELECT CourseID, CourseName FROM Courses WHERE MasterCourseID = @MasterCourseID";
                
               

                // Create a new connection
                using (SqlConnection connection = new SqlConnection(GetConnectionString()))
                {
                    // Create a new command with the query and connection
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Add parameter for the selected master course ID, if applicable
                        if (selectedMasterCourseID != 0)
                        {
                            command.Parameters.AddWithValue("@MasterCourseID", selectedMasterCourseID);
                        }

                        // Open the connection
                        connection.Open();

                        // Execute the query and get the data
                        SqlDataReader reader = command.ExecuteReader();

                        // Bind the data to the ddlCourses dropdown list
                        ddlCourses.DataSource = reader;
                        ddlCourses.DataTextField = "CourseName";
                        ddlCourses.DataValueField = "CourseID";
                        ddlCourses.DataBind();

                        // Add the "Select Course" option manually after data binding
                        ddlCourses.Items.Insert(0, new ListItem("Select Course", ""));
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions
                // You may want to log the exception or display an error message to the user
            }
        }

        private void LoadCoursesWithNullMasterCourse()
        {

                // Your SQL query to select courses where MasterCourseID is null
                string query = "SELECT CourseID, CourseName FROM Courses WHERE MasterCourseID IS NULL";

                // Create a new connection
                using (SqlConnection connection = new SqlConnection(GetConnectionString()))
                {
                    // Create a new command with the query and connection
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Open the connection
                        connection.Open();

                        // Execute the query and get the data
                        SqlDataReader reader = command.ExecuteReader();

                        // Bind the data to the ddlCourses dropdown list
                        ddlCourses.DataSource = reader;
                        ddlCourses.DataTextField = "CourseName";
                        ddlCourses.DataValueField = "CourseID";
                        ddlCourses.DataBind();

                        // Add the "Select Course" option manually after data binding
                        ddlCourses.Items.Insert(0, new ListItem("Select Course", ""));
                    }
                }
            }

        private void InsertVideoIntoDatabase(string videoName, string embedCode, int courseId, int? masterCourseId)
        {

            // Your SQL query to insert into the Videos table
            string query = "INSERT INTO Videos (VideoName, YouTubeEmbedCode, CourseID, MasterCourseID) VALUES (@VideoName, @YouTubeEmbedCode, @CourseID, @MasterCourseID)";

            try
            {
                using (SqlConnection connection = new SqlConnection(GetConnectionString()))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Add parameters to prevent SQL injection
                        command.Parameters.AddWithValue("@VideoName", videoName);
                        command.Parameters.AddWithValue("@YouTubeEmbedCode", embedCode);
                        command.Parameters.AddWithValue("@CourseID", courseId);
                        command.Parameters.AddWithValue("@MasterCourseID", (object)masterCourseId ?? DBNull.Value);

                        // Open connection and execute query
                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions
                // You may want to log the exception or display an error message to the user
            }
        }

        protected void ddlMasterCourses_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Get the selected master course ID
            int selectedMasterCourseId;
            if (int.TryParse(ddlSelectMasterCourse.SelectedValue, out selectedMasterCourseId))
            {
                // Fetch courses and their video counts based on the selected master course
                DataTable coursesWithVideoCounts = GetCoursesWithVideoCounts(selectedMasterCourseId);

                // Bind the fetched data to the GridView
                gridViewCourses.DataSource = coursesWithVideoCounts;
                gridViewCourses.DataBind();
            }
        }

        private DataTable GetCoursesWithVideoCounts(int selectedMasterCourseId)
        {
            DataTable coursesWithVideoCounts = new DataTable();

            string query = @"
        SELECT c.CourseID, c.CourseName, COUNT(v.VideoID) AS VideoCount
        FROM Courses c
        LEFT JOIN Videos v ON c.CourseID = v.CourseID
        WHERE c.MasterCourseID = @MasterCourseID
        GROUP BY c.CourseID, c.CourseName";

            using (SqlConnection connection = new SqlConnection(GetConnectionString()))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@MasterCourseID", selectedMasterCourseId);
                    connection.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    adapter.Fill(coursesWithVideoCounts);
                }
            }

            return coursesWithVideoCounts;
        }

        private void PopulateMasterCoursesDropdown()
        {
            // Fetch master courses from the database
            DataTable masterCourses = GetMasterCoursesFromDatabase(); // Implement this method to retrieve master courses

            // Bind the DataTable to the dropdown
            ddlSelectMasterCourse.DataSource = masterCourses;
            ddlSelectMasterCourse.DataTextField = "MasterCourseName"; // Change to the actual field name in your database
            ddlSelectMasterCourse.DataValueField = "MasterCourseID"; // Change to the actual field name in your database
            ddlSelectMasterCourse.DataBind();

            // Add a default "Select Master Course" option
            ddlSelectMasterCourse.Items.Insert(0, new ListItem("Select Master Course", ""));
        }

        private DataTable GetMasterCoursesFromDatabase()
        {
            // Create a DataTable to store master courses
            DataTable masterCourses = new DataTable();

            // Implement your database connection and query to fetch master courses
            // Example:
            string query = "SELECT MasterCourseID, MasterCourseName FROM MasterCourse";

            using (SqlConnection connection = new SqlConnection(GetConnectionString()))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    adapter.Fill(masterCourses);
                }
            }

            return masterCourses;
        }

    }

}


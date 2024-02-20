using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Youtubeudemy
{
    public partial class VideoPlayer : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                if (Session["SelectedVideoPath"] != null)
                {
                    string videoPath = Session["SelectedVideoPath"].ToString();

                    // Ensure video path is accessible by the web server
                    if (!TryEnsureVideoAccessibility(videoPath))
                    {
                        ShowErrorMessage("The video could not be found or accessed.");
                        return;
                    }

                    // Use Server.MapPath to resolve the physical path to a virtual path
                    string virtualPath = ResolveVirtualPath(videoPath);

                    // Use virtual path instead of physical path:
                    videoSource.Src = virtualPath;

                    // Use MediaElementPlayer library for enhanced playback:
                    ScriptManager.RegisterStartupScript(this, GetType(), "videoPlayerScript",
                        "$('#videoPlayer').mediaelementplayer();", true);
                }
            }
        }
        private bool TryEnsureVideoAccessibility(string videoPath)
        {
            // Check if the video file exists and is accessible
            return File.Exists(videoPath);
        }

        private string ResolveVirtualPath(string physicalPath)
        {
            // Resolve physical path to virtual path
            string virtualPath = physicalPath.Replace(Server.MapPath("~"), "~/").Replace("\\", "/");

            return virtualPath;
        }
        private void ShowErrorMessage(string message)
        {
            // Implement logic to display an error message to the user
            // (e.g., display in a label, alert box, toast notification)
            // Example: Display alert box with JavaScript
            ClientScript.RegisterStartupScript(this.GetType(), "alert", $"alert('{message}');", true);
        }

       

        private DataTable GetVideosFromDatabase()
        {
            DataTable dtVideos = new DataTable();
            string connectionString = "Data Source=DESKTOP-FROJFGN\\SQLEXPRESS;Initial Catalog=UserDB1;Integrated Security=True;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT VideoID, VideoName, VideoPath FROM Videos";

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

        
    }
}
using System;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using System.Web.UI;

namespace Youtubeudemy
{
    public partial class VideoPlayer2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["SelectedVideoName"] != null)
                {
                    string videoName = Session["SelectedVideoName"].ToString();

                    // Get video path based on video name
                    string videoPath = GetVideoPath(videoName);

                    // Ensure video path is accessible by the web server
                    if (!TryEnsureVideoAccessibility(videoPath))
                    {
                        ShowErrorMessage("The video could not be found or accessed.");
                        return;
                    }

                    // Use Server.MapPath to resolve the physical path to a virtual path
                    string virtualPath = ResolveVirtualPath(videoPath);

                    // Set video source
                    videoSource.Src = virtualPath;

                    // Use MediaElementPlayer library for enhanced playback
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

        private string GetVideoPath(string videoName)
        {
            // Query database to get the video path based on video name
            string videoFolderPath = Server.MapPath("~/Videos/");
            string videoPath = Path.Combine(videoFolderPath, videoName);

            if (File.Exists(videoPath))
            {
                return videoPath;
            }
            else
            {
                throw new InvalidOperationException("Video path not found.");
            }
        }



    }
}

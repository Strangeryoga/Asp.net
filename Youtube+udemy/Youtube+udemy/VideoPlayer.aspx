<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="VideoPlayer.aspx.cs" Inherits="Youtubeudemy.VideoPlayer" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Video Player</title>
    <style>
           body, html {
            margin: 0;
            padding: 0;
            height: 100%;
        }

        #videoWrapper {
            width: 50%;
            height: 100%;
            display: flex;
            justify-content: center;
            align-items: center;
            background-color: #000; /* Add background color */
            margin-left: 25%;
            margin-top: 15%;
        }

        #videoPlayer {
            max-width: 50%;
            max-height: 50%;
            margin: auto;
            display: block;
        }

        #tableWrapper {
            width: 50%; /* Set width for the table wrapper */
            height: 100%;
            display: flex;
            overflow: auto; /* Add overflow property for scrolling if needed */
            margin-left: 50%;
             margin-top: -14%;
        }

        #gvVideos {
            width: 100%;
        }

        /* Optional: Adjust table styles as needed */
        #gvVideos tr:nth-child(even) {
            background-color: #f2f2f2;
        }

        #gvVideos th, #gvVideos td {
            padding: 8px;
        }

        #gvVideos th {
            background-color: #4CAF50;
            color: white;
        }

    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div id="videoWrapper">
            <video id="videoPlayer" controls="controls">
                <source id="videoSource" runat="server" />
                Your browser does not support the video tag.
            </video>
        </div>
                <div id="tableWrapper">
       
                    </div>
    </form>
</body>
</html>

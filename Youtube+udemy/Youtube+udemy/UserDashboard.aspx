<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserDashboard.aspx.cs" Inherits="Youtubeudemy.UserDashboard" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>User Dashboard</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-T3c6CoIi6uLrA9TneNEoa7RxnatzjcDSCmG1MXxSR1GAsXEV/Dwwykc2MPK8M2HN" crossorigin="anonymous" />
<style>
    .col-md-8 {
        flex: 0 0 auto;
        width: 75.666667%;
    }
</style>
</head>

<body>
    <form id="formDashboard" runat="server">
       <div class="container-fluid mt-5"> <!-- Use container-fluid for full-width -->
    <div class="row justify-content-end">
        <div class="col-md-6 col-lg-6 col-xl-4"> <!-- Adjust column sizes for different screen sizes -->
            <div class="card">
                <div class="card-header d-flex justify-content-between align-items-center">
                    <h5>Welcome to the User Dashboard</h5>
                    <a href="#" class="btn btn-primary" id="btnLogout" runat="server">Logout</a>
                </div>
                <div class="card-body">
                       <div class="form-group">
                                <label for="ddlMasterCourses">Select Master Course:</label>
                                <asp:DropDownList ID="ddlMasterCourses" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlMasterCourses_SelectedIndexChanged" >
                                </asp:DropDownList>
                        </div>

                     <div class="form-group">
                                <label for="ddlCourses">Select Course:</label>
                                <asp:DropDownList ID="ddlCourses" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlCourses_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                     <hr />
                 <asp:GridView ID="gvVideos" runat="server" AutoGenerateColumns="False" CssClass="table" EmptyDataText="No videos available" OnRowCommand="gvVideos_RowCommand">
    <Columns>
        <asp:BoundField DataField="VideoName" HeaderText="Video Name" />
        <asp:TemplateField HeaderText="Video">
            <ItemTemplate>
                <div class="video-title">
<asp:Button ID="btnWatch" runat="server" CommandName="Watch" CommandArgument='<%# Eval("YouTubeEmbedCode") %>' Text='Watch' />
                </div>
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
</asp:GridView>

                    
     
                </div>
            </div>
        </div>
    </div>
</div>

       <div class="container mt-5 position-absolute top-0 start-0 style="width: 810px; height: 460px;">
    <div class="row">
        <div class="col-md-8">
            <div class="card">
                <div class="card-header">
                    Video Player
                </div>
                <div class="card-body">
                    <iframe id="videoPlayer" runat="server" style="width: 100%;" height="450" frameborder="0" allowfullscreen></iframe>      
                          </div>
            </div>
        </div>
    </div>
</div>




    </form>

    </body>
</html>

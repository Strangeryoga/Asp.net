<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddCourse.aspx.cs" Inherits="Youtubeudemy.AddCourse" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Add Course</title>
    <link href="https://maxcdn.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <h2>Add Course</h2>
            <div class="form-group">
                <label for="ddlMasterCourses">Course Name:</label>
                <asp:DropDownList ID="ddlMasterCourses" runat="server" CssClass="form-control" AppendDataBoundItems="true">
                    <asp:ListItem Text="--Select Master Course--" Value=""></asp:ListItem>
                </asp:DropDownList>
            </div>
            <!-- Add more input fields for other course details as needed -->
            <div class="form-group">
                <asp:Button ID="btnAddCourse" runat="server" Text="Add Course" CssClass="btn btn-primary" OnClick="btnAddCourse_Click" />
            </div>
        </div>
    </form>
    <!-- Bootstrap JavaScript (Optional, if needed) -->
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.5.2/js/bootstrap.min.js"></script>
</body>
</html>

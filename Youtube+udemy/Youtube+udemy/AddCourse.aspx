<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddCourse.aspx.cs" Inherits="Youtubeudemy.AddCourse" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Add Course</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h2>Add Course</h2>
            <div>
                Course Name: 
               <asp:DropDownList ID="ddlMasterCourses" runat="server" AppendDataBoundItems="true">
                <asp:ListItem Text="--Select Master Course--" Value=""></asp:ListItem>
                 </asp:DropDownList>

            </div>
            <!-- Add more input fields for other course details as needed -->
            <div>
                <asp:Button ID="btnAddCourse" runat="server" Text="Add Course" OnClick="btnAddCourse_Click" />
            </div>
        </div>
    </form>
</body>
</html>

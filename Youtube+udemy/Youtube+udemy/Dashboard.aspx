<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="Youtubeudemy.Dashboard" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Dashboard</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h2>User Dashboard</h2>
            <asp:GridView ID="gridViewUsers" runat="server" AutoGenerateColumns="False">
                <Columns>
                    <asp:BoundField DataField="UserId" HeaderText="ID" />
                    <asp:BoundField DataField="UName" HeaderText="Name" />
                    <asp:HyperLinkField DataTextField="Email" HeaderText="Email" DataNavigateUrlFields="UserID" DataNavigateUrlFormatString="UserCourses.aspx?UserID={0}" />                    
                    <asp:TemplateField HeaderText="Status">
                        <ItemTemplate>
                            <asp:Button ID="btnStatus" runat="server" Text='<%# Convert.ToBoolean(Eval("Ustatus")) ? "Inactive" : "Active" %>' CommandName="ToggleStatus" CommandArgument='<%# Eval("UserID") %>' OnCommand="btnStatus_Command" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>

         <div>
            <h2>Add Master Course</h2>
            <div>
                <label for="txtMasterCourseName">Master Course Name:</label>
                <asp:TextBox ID="txtMasterCourseName" runat="server"></asp:TextBox>
            </div>
            <div>
                <asp:Button ID="btnAddMasterCourse" runat="server" Text="Add Master Course" OnClick="btnAddMasterCourse_Click" />
            </div>
        </div>

         <div>
            <h2>Add Course</h2>

             <div>
                <label for="ddlMasterCourses">Select Master Course:</label>
            <asp:DropDownList ID="ddlMasterCourses" runat="server" DataTextField="MasterCourseName" DataValueField="MasterCourseID">
                <asp:ListItem Text="Select Master Course" Value=""></asp:ListItem>
            </asp:DropDownList>
            </div>

            <div>
                <label for="txtCourseName">Course Name:</label>
                <asp:TextBox ID="txtCourseName" runat="server"></asp:TextBox>
            </div>
            <div>
                <asp:Button ID="btnAddCourse" runat="server" Text="Add Course" OnClick="btnAddCourse_Click" />
            </div>
        </div>

     <div>
        <h2>Upload Videos</h2>
         <label for="txtVideoName">Video Name:</label>
    <asp:TextBox ID="txtVideoName" runat="server" placeholder="Enter Video Name"></asp:TextBox>
             <label for="txtVideoCode">YouTube Code:</label>
           <asp:TextBox ID="txtVideoCode" runat="server" placeholder="Enter YouTube Code"></asp:TextBox>

        <label for="ddlMasterCourses">Select Master Course:</label>

     <asp:DropDownList ID="DropDownList1" runat="server" DataTextField="MasterCourseName" DataValueField="MasterCourseID" AutoPostBack="true" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged">
    <asp:ListItem Text="Select Master Course" Value=""></asp:ListItem>
     </asp:DropDownList>


           <label for="ddlCourses">Select Course:</label>

                
        <asp:DropDownList ID="ddlCourses" runat="server"  DataTextField="CourseName" DataValueField="CourseID">


         <asp:ListItem Text="Select Course" Value=""></asp:ListItem>
        </asp:DropDownList>
       
     </div>

<asp:Button ID="btnUpload" runat="server" Text="Upload" OnClick="btnUpload_Click" />        <br /><br />

        <div>
        <asp:GridView ID="gvVideos" runat="server" AutoGenerateColumns="False" OnRowCommand="gvVideos_RowCommand">
            <Columns>
                <asp:BoundField DataField="VideoID" HeaderText="Video ID" />
                <asp:BoundField DataField="VideoName" HeaderText="Video Name" />
                <asp:BoundField DataField="YouTubeEmbedCode" HeaderText="Video Url" />
                        <asp:BoundField DataField="CourseName" HeaderText="Course Name" />
                        <asp:BoundField DataField="MasterCourseName" HeaderText="Master Course Name" />

                <asp:ButtonField ButtonType="Button" Text="View" CommandName="ViewVideo" />
            </Columns>
        </asp:GridView>
    </div>
                <br />

         <div>
                <label for="ddlSelectMasterCourse">Select Master Course:</label>
                <asp:DropDownList ID="ddlSelectMasterCourse" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlMasterCourses_SelectedIndexChanged">
                    <asp:ListItem Text="Select Master Course" Value=""></asp:ListItem>
                </asp:DropDownList>
            </div>
        <br />
            <!-- GridView to display courses and their video counts -->
            <asp:GridView ID="gridViewCourses" runat="server" AutoGenerateColumns="False">
                <Columns>
                    <asp:BoundField DataField="CourseID" HeaderText="ID" />
                    <asp:BoundField DataField="CourseName" HeaderText="Course Name" />
                    <asp:BoundField DataField="VideoCount" HeaderText="Video Count" />
                </Columns>
            </asp:GridView>

      </form>
</body>
</html>

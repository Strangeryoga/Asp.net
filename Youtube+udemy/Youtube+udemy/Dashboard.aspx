<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="Youtubeudemy.Dashboard" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Dashboard</title>
     <script src="https://code.jquery.com/jquery-3.5.1.slim.min.js"></script>
  <script src="https://cdn.jsdelivr.net/npm/@popperjs/core@2.5.4/dist/umd/popper.min.js"></script>
  <script src="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/js/bootstrap.min.js"></script>
   <link href="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css" rel="stylesheet">
  <style>
  /* Custom styles */
    .userDashboard {
      padding: 20px;
    }
    .btn-status {
      width: 100%;
    }
    .table thead th {
      background-color: #007bff;
      color: #fff;
      border-color: #007bff;
      text-align: center;
    }
    .table tbody tr:nth-of-type(odd) {
      background-color: rgba(0, 123, 255, 0.1);
    }
    .table tbody tr:hover {
      background-color: rgba(0, 123, 255, 0.2);
    }
    .table {
      width: 100%;
    }
    .table-responsive {
      overflow-x: auto;
    }
     .AddMaster {
      padding: 20px;
      border: 1px solid #ccc;
      border-radius: 5px;
      background-color: #f9f9f9;
    }
    .AddMaster h2 {
      margin-bottom: 20px;
    }
    .AddMaster label {
      font-weight: bold;
    }
    .AddMaster input[type="text"] {
      width: 100%;
      padding: 8px;
      border: 1px solid #ccc;
      border-radius: 4px;
      box-sizing: border-box;
      margin-bottom: 10px;
    }
    .AddMaster button {
      padding: 10px 20px;
      background-color: #007bff;
      color: #fff;
      border: none;
      border-radius: 4px;
      cursor: pointer;
    }
    .AddMaster button:hover {
      background-color: #0056b3;
    }

     .AddCourse {
      padding: 20px;
      border: 1px solid #ccc;
      border-radius: 5px;
      background-color: #f9f9f9;
      margin-top: 20px;
    }
    .AddCourse h2 {
      margin-bottom: 20px;
    }
    .AddCourse label {
      font-weight: bold;
    }
    .AddCourse input[type="text"],
    .AddCourse select {
      width: 100%;
      padding: 8px;
      border: 1px solid #ccc;
      border-radius: 4px;
      box-sizing: border-box;
      margin-bottom: 10px;
    }
    .AddCourse button {
      padding: 10px 20px;
      background-color: #007bff;
      color: #fff;
      border: none;
      border-radius: 4px;
      cursor: pointer;
    }
    .AddCourse button:hover {
      background-color: #0056b3;
    }

     .uploadVi {
      padding: 20px;
      border: 1px solid #ccc;
      border-radius: 5px;
      background-color: #f9f9f9;
      margin-bottom: 20px;
    }
    .uploadVi h2 {
      margin-bottom: 20px;
    }
    .uploadVi label {
      font-weight: bold;
    }
    .uploadVi input[type="text"],
    .uploadVi select {
      width: 100%;
      padding: 8px;
      border: 1px solid #ccc;
      border-radius: 4px;
      box-sizing: border-box;
      margin-bottom: 10px;
    }
    .btn-upload {
      padding: 10px 20px;
      background-color: #007bff;
      color: #fff;
      border: none;
      border-radius: 4px;
      cursor: pointer;
    }
    .btn-upload:hover {
      background-color: #0056b3;
    }

      .GridViweTable {
      margin-top: 20px;
    }
    .GridViweTable .table {
      width: 100%;
      border-collapse: collapse;
    }
    .GridViweTable .table th,
    .GridViweTable .table td {
      padding: 8px;
      vertical-align: top;
      border-top: 1px solid #dee2e6;
    }
    .GridViweTable .table th {
      background-color: #f8f9fa;
      font-weight: bold;
      text-align: center;
    }
    .GridViweTable .table tbody tr:nth-of-type(odd) {
      background-color: rgba(0, 0, 0, 0.05);
    }
    .GridViweTable .table tbody tr:hover {
      background-color: rgba(0, 0, 0, 0.075);
    }
    .GridViweTable .btn-view {
      padding: 6px 12px;
      font-size: 14px;
      line-height: 1.5;
      border-radius: 4px;
      color: #fff;
      background-color: #007bff;
      border-color: #007bff;
    }
    .GridViweTable .btn-view:hover {
      background-color: #0056b3;
      border-color: #0056b3;
    }

     .selectMC {
      margin-bottom: 20px;
    }
    .selectMC label {
      font-weight: bold;
    }
    .selectMC .form-control {
      width: 100%;
      padding: 6px 12px;
      font-size: 14px;
      line-height: 1.5;
      color: #495057;
      background-color: #fff;
      background-image: none;
      border: 1px solid #ced4da;
      border-radius: 4px;
      transition: border-color ease-in-out .15s, box-shadow ease-in-out .15s;
    }
    .GridViweC {
      margin-top: 20px;
    }
    .GridViweC .table {
      width: 100%;
      border-collapse: collapse;
    }
    .GridViweC .table th,
    .GridViweC .table td {
      padding: 8px;
      vertical-align: top;
      border-top: 1px solid #dee2e6;
    }
    .GridViweC .table th {
      background-color: #f8f9fa;
      font-weight: bold;
      text-align: center;
    }
  </style>
</head>

<body>
    <form id="form1" runat="server">
     <div class="container userDashboard">
    <h2 class="text-center">User Dashboard</h2>
    <div class="table-responsive">
      <asp:GridView ID="gridViewUsers" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-hover">
        <Columns>
          <asp:BoundField DataField="UserId" HeaderText="ID" ItemStyle-CssClass="text-center" />
          <asp:BoundField DataField="UName" HeaderText="Name" />
          <asp:HyperLinkField DataTextField="Email" HeaderText="Email" DataNavigateUrlFields="UserID" DataNavigateUrlFormatString="UserCourses.aspx?UserID={0}" />
          <asp:TemplateField HeaderText="Status">
            <ItemTemplate>
              <asp:Button ID="btnStatus" runat="server" Text='<%# Convert.ToBoolean(Eval("Ustatus")) ? "Inactive" : "Active" %>' CssClass="btn btn-status btn-block" CommandName="ToggleStatus" CommandArgument='<%# Eval("UserID") %>' OnCommand="btnStatus_Command" />
            </ItemTemplate>
          </asp:TemplateField>
        </Columns>
      </asp:GridView>
    </div>
  </div>

         <div class="container">
    <div class="AddMaster">
      <h2>Add Master Course</h2>
      <div>
        <label for="txtMasterCourseName">Master Course Name:</label>
        <asp:TextBox ID="txtMasterCourseName" runat="server" CssClass="form-control"></asp:TextBox>
      </div>
      <div>
        <asp:Button ID="btnAddMasterCourse" runat="server" Text="Add Master Course" CssClass="btn btn-primary" OnClick="btnAddMasterCourse_Click" />
      </div>
    </div>
  </div>

         <div class="container">
    <div class="AddCourse">
      <h2>Add Course</h2>

      <div class="form-group MCourse">
        <label for="ddlMasterCourses">Select Master Course:</label>
        <asp:DropDownList ID="ddlMasterCourses" runat="server" CssClass="form-control" DataTextField="MasterCourseName" DataValueField="MasterCourseID">
          <asp:ListItem Text="Select Master Course" Value=""></asp:ListItem>
        </asp:DropDownList>
      </div>

      <div class="form-group CourseN">
        <label for="txtCourseName">Course Name:</label>
        <asp:TextBox ID="txtCourseName" runat="server" CssClass="form-control"></asp:TextBox>
      </div>

      <div class="form-group">
        <asp:Button ID="btnAddCourse" runat="server" Text="Add Course" CssClass="btn btn-primary" OnClick="btnAddCourse_Click" />
      </div>
    </div>
  </div>

   <div class="container" style="margin-top:20px;">
    <div class="uploadVi">
      <h2>Upload Videos</h2>

      <div class="form-group">
        <label for="txtVideoName">Video Name:</label>
        <asp:TextBox ID="txtVideoName" runat="server" CssClass="form-control" placeholder="Enter Video Name"></asp:TextBox>
      </div>

      <div class="form-group">
        <label for="txtVideoCode">YouTube Code:</label>
        <asp:TextBox ID="txtVideoCode" runat="server" CssClass="form-control" placeholder="Enter YouTube Code"></asp:TextBox>
      </div>

      <div class="form-group">
        <label for="ddlMasterCourses">Select Master Course:</label>
        <asp:DropDownList ID="DropDownList1" runat="server" CssClass="form-control" DataTextField="MasterCourseName" DataValueField="MasterCourseID" AutoPostBack="true" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged">
          <asp:ListItem Text="Select Master Course" Value=""></asp:ListItem>
        </asp:DropDownList>
      </div>

      <div class="form-group">
        <label for="ddlCourses">Select Course:</label>
        <asp:DropDownList ID="ddlCourses" runat="server" CssClass="form-control" DataTextField="CourseName" DataValueField="CourseID">
          <asp:ListItem Text="Select Course" Value=""></asp:ListItem>
        </asp:DropDownList>
      </div>
    </div>

    <div class="text-center">
      <asp:Button ID="btnUpload" runat="server" Text="Upload" CssClass="btn btn-upload" OnClick="btnUpload_Click" />
    </div>
  </div>

       <div class="container">
    <div class="GridViweTable">
      <asp:GridView ID="gvVideos" runat="server" AutoGenerateColumns="False" OnRowCommand="gvVideos_RowCommand" CssClass="table">
        <Columns>
          <asp:BoundField DataField="VideoID" HeaderText="Video ID" />
          <asp:BoundField DataField="VideoName" HeaderText="Video Name" />
          <asp:BoundField DataField="YouTubeEmbedCode" HeaderText="Video Url" />
          <asp:BoundField DataField="CourseName" HeaderText="Course Name" />
          <asp:BoundField DataField="MasterCourseName" HeaderText="Master Course Name" />
          <asp:ButtonField ButtonType="Button" Text="View" CommandName="ViewVideo" ControlStyle-CssClass="btn btn-view" />
        </Columns>
      </asp:GridView>
    </div>
  </div>


         <div class="container">
    <div class="selectMC">
      <label for="ddlSelectMasterCourse">Select Master Course:</label>
      <asp:DropDownList ID="ddlSelectMasterCourse" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlMasterCourses_SelectedIndexChanged">
        <asp:ListItem Text="Select Master Course" Value=""></asp:ListItem>
      </asp:DropDownList>
    </div>
    <div class="GridViweC text-center">
      <asp:GridView ID="gridViewCourses" runat="server" AutoGenerateColumns="False" CssClass="table">
        <Columns>
          <asp:BoundField DataField="CourseID" HeaderText="ID" />
          <asp:BoundField DataField="CourseName" HeaderText="Course Name" />
          <asp:BoundField DataField="VideoCount" HeaderText="Video Count" />
        </Columns>
      </asp:GridView>
    </div>
  </div>
      </form>
</body>
</html>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserCourses.aspx.cs" Inherits="Youtubeudemy.UserCourses" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>User Courses</title>
    <link href="https://maxcdn.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <h2>User Courses</h2>
            <div class="mb-3">
                <asp:Button ID="btnAddCourse" runat="server" Text="Add Course" CssClass="btn btn-primary" OnClick="btnAddCourse_Click" />
            </div>
            <div>
                <asp:GridView ID="gridViewUserCourses" runat="server" AutoGenerateColumns="False" DataKeyNames="UserCourseID" CssClass="table">
                    <Columns>
                        <asp:BoundField DataField="UserCourseID" HeaderText="UC ID" />
                        <asp:TemplateField HeaderText="User">
                            <ItemTemplate>
                                <asp:Label ID="lblUser" runat="server" Text='<%# Eval("UName") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Master Course">
                            <ItemTemplate>
                                <asp:Label ID="lblMasterCourse" runat="server" Text='<%# Eval("MasterCourseName") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Status">
                            <ItemTemplate>
                                <asp:Button ID="btnStatus" runat="server" Text='<%# Convert.ToBoolean(Eval("Status")) ? "Inactive" : "Active" %>' CommandName="ToggleStatus" CommandArgument='<%# Eval("UserCourseID") %>' OnCommand="btnStatus_Command" CssClass='<%# Convert.ToBoolean(Eval("Status")) ? "btn btn-danger" : "btn btn-success" %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </form>

    <!-- Bootstrap JavaScript (Optional, if needed) -->
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.5.2/js/bootstrap.min.js"></script>
</body>
</html>

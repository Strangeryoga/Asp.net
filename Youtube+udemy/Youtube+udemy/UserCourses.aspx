<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserCourses.aspx.cs" Inherits="Youtubeudemy.UserCourses" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>User Courses</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h2>User Courses</h2>

                <asp:Button ID="btnAddCourse" runat="server" Text="Add Course" OnClick="btnAddCourse_Click" />
                <asp:GridView ID="gridViewUserCourses" runat="server" AutoGenerateColumns="False" DataKeyNames="UserCourseID">
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
                        <asp:Button ID="btnStatus" runat="server" Text='<%# Convert.ToBoolean(Eval("Status")) ? "Inactive" : "Active" %>' CommandName="ToggleStatus" CommandArgument='<%# Eval("UserCourseID") %>' OnCommand="btnStatus_Command" />

                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>

        </div>
    </form>

    
</body>
</html>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserLeaveRequests.aspx.cs" Inherits="LeaveApplication.UserLeaveRequests" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Leave Application</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h2>Leave Application</h2>
            <asp:Label ID="lblMessage" runat="server" ForeColor="Green" Visible="false"></asp:Label>
            <table>
                <tr>
                    <td>
                        <asp:Label ID="lblStartDate" runat="server" Text="From:"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtStartDate" runat="server" TextMode="Date" Required="true" AutoPostBack="true" OnTextChanged="txtStartDate_TextChanged"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblEndDate" runat="server" Text="To:"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtEndDate" runat="server" TextMode="Date" Required="true" AutoPostBack="true" OnTextChanged="txtEndDate_TextChanged"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblReason" runat="server" Text="Reason:"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtReason" runat="server" TextMode="MultiLine" Rows="4" Required="true"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblPendingLeaves" runat="server" Text="Pending Leaves: 2"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        Absent Days: <asp:Label ID="lblAbsentDays" runat="server" Text=" 0"></asp:Label>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>

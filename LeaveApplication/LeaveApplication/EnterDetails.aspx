<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EnterDetails.aspx.cs" Inherits="LeaveApplication.EnterDetails" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Enter Details</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h2>Enter Details</h2>
            <asp:Label ID="lblMessage" runat="server" ForeColor="Green" Visible="false"></asp:Label>
            <asp:TextBox ID="txtName" runat="server" placeholder="Name" Required="true"></asp:TextBox>
            <asp:TextBox ID="txtSalary" runat="server" placeholder="Salary" Required="true"></asp:TextBox>
            <asp:TextBox ID="txtAbsentDays" runat="server" placeholder="Absent Days" Required="true" Visible="false"></asp:TextBox>
            <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" />
            <asp:Button ID="btnDownloadPDF" runat="server" Text="Generate Slip" OnClick="btnDownloadPDF_Click" />
        </div>
    </form>
</body>
</html>

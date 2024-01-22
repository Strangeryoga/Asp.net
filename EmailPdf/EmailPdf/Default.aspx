<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="EmailPdf.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            Name <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
        </div>
        <p>
            Dept <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
        </p>
        <p>
            Salary <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
        </p>
        <p>
            Email <asp:TextBox ID="TextBox4" runat="server"></asp:TextBox>
        </p>
        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Submit" />
    </form>
</body>
</html>

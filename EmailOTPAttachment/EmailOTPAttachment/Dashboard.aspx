<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="EmailOTPAttachment.Dashboard" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <p>
            Email
            <asp:DropDownList ID="DropDownList1" runat="server" DataTextField="Email" DataValueField="Email"></asp:DropDownList>
        </p>
        <p>
            Subject<asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
        </p>
        <p>
            Body<asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
        </p>
        <p>
            Attachment<asp:FileUpload ID="FileUpload1" runat="server" />
        </p>
        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Submit" />
    </form>
</body>
</html>

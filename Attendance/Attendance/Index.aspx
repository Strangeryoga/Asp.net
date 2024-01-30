<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="Attendance.Index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Index</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h1>Welcome to the Index page!</h1>
            <asp:Label ID="lblUsername" runat="server" Text=""></asp:Label>
             <br />
            <asp:Button ID="btnSignOut" runat="server" Text="Sign Out" OnClick="btnSignOut_Click" />
        </div>
    </form>
</body>
</html>

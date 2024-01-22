<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NewPage.aspx.cs" Inherits="EmailPdf.NewPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Your Page Title</title>
</head>
<body>
    <form id="form2" runat="server">
        <!-- Your existing ASP.NET controls go here -->

        <!-- Add this button with OnClick attribute -->
        <asp:Button ID="btnGeneratePDF" runat="server" Text="Generate PDF and Send Email" OnClick="btnGeneratePDF_Click" />

    </form>
</body>
</html>

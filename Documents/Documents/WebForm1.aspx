<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="Documents.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>File Upload Example</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h2>File Upload Example</h2>
            <div>
                <label for="ddlDocumentType">Select Document Type:</label>
                <asp:DropDownList ID="ddlDocumentType" runat="server">
                    <asp:ListItem Text="Aadhar" Value="Aadhar" />
                    <asp:ListItem Text="PAN" Value="PAN" />
                    <asp:ListItem Text="Voter ID" Value="VoterID" />
                </asp:DropDownList>
            </div>
            <div>
                <label for="fileUpload">Choose File:</label>
                <asp:FileUpload ID="fileUpload" runat="server" />
            </div>
            <div>
                <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" />
                <asp:Button ID="btnShowDocuments" runat="server" Text="Show Documents" OnClick="btnShowDocuments_Click" />
                <asp:GridView ID="gvDocuments" runat="server" AutoGenerateColumns="False" DataKeyNames="DocumentID">
                    <Columns>
                        <asp:BoundField DataField="DocumentType" HeaderText="Document Type" SortExpression="DocumentType" />
                        <asp:BoundField DataField="FileName" HeaderText="File Name" SortExpression="FileName" />
                        <asp:BoundField DataField="UploadDate" HeaderText="Upload Date" SortExpression="UploadDate" DataFormatString="{0:yyyy-MM-dd HH:mm:ss}" />
                        <asp:TemplateField HeaderText="Download Document">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkDownload" runat="server" Text="Download" OnClick="DownloadFile"
                                CommandArgument='<%# Eval("DocumentId") %>'></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="View Document">
                            <ItemTemplate>
                            <asp:LinkButton ID="lnkView" runat="server" Text="View" OnClick="ViewDocument" CommandArgument='<%# Eval("DocumentID") %>'></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </form>
</body>
</html>

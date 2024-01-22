<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="DemoFileUpload.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-T3c6CoIi6uLrA9TneNEoa7RxnatzjcDSCmG1MXxSR1GAsXEV/Dwwykc2MPK8M2HN" crossorigin="anonymous"/>
    <script type="text/javascript">
        function toggleProductFields() {
            var productFields = document.getElementById("productFields");
            productFields.style.display = (productFields.style.display === "none" || productFields.style.display === "") ? "block" : "none";
        }
    </script>
    <title></title>
</head>
<body>
    <div>
        <h1>Admin Panel</h1>
    </div>
    <form id="form1" runat="server">
         <div>
            <asp:Button ID="ButtonToggleFields" runat="server" OnClientClick="toggleProductFields(); return false;" Text="Add Products" />
             <br />
             <br />
        </div>
        
        <div id="productFields" style="display: none;">
            Product Name
            <asp:TextBox ID="TextBox1" runat="server" OnTextChanged="TextBox1_TextChanged"></asp:TextBox>
            Price
            <asp:TextBox ID="TextBoxPrice" runat="server" OnTextChanged="TextBoxPrice_TextChanged"></asp:TextBox>
            File Upload
            <asp:FileUpload ID="FileUpload1" runat="server" />
            <p>
                <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Save" />
            </p>
        </div>

        <asp:Button ID="Button2" runat="server" Text="Shop" OnClick="Button2_Click" />
        <br />
        <asp:DataList ID="DataList1" runat="server" RepeatDirection="Horizontal" CellPadding="5" OnDataList1_ItemCommand="DataList1_ItemCommand" OnItemCommand="DataList1_ItemCommand" DataKeyField="ID" CellSpacing="3">
            <ItemTemplate>
                <!-- Display product details -->
                <div class="card" style="width: 18rem;">
                    <div class="card-body">
                        <h5 class="card-title"><%# Eval("Name") %></h5>
                        <p class="card-text">Price: ₹ <%# Eval("Price") %></p>
                        <img id="imgProduct" runat="server" src='<%# Eval("Photo") %>' class="card-img-top img-fluid" />

                        <asp:Button ID="Button3" runat="server" OnClick="Button3_Click" Text="Edit" CommandName="EditItem" />
                        <asp:Button ID="ButtonDelete" runat="server" OnClick="ButtonDelete_Click" Text="Delete" CommandName="DeleteItem" />
                    </div>
                </div>
            </ItemTemplate>

          <EditItemTemplate>
    <!-- Provide input controls for editing -->
    <div class="card" style="width: 18rem;">   
        <div class="card-body">
            <h5 class="card-title">
                <asp:TextBox ID="TextBoxEditName" runat="server" Text='<%# Bind("Name") %>'></asp:TextBox>
            </h5>
            <p class="card-text">
                Price: ₹
                <asp:TextBox ID="TextBoxEditPrice" runat="server" Text='<%# Bind("Price") %>'></asp:TextBox>
            </p>
                <img id="imgProduct" runat="server" src='<%# Eval("Photo") %>' class="card-img-top img-fluid" />

            <asp:Button ID="ButtonUpdate" runat="server" OnClick="ButtonUpdate_Click" Text="Update" CommandName="UpdateItem" />
            <asp:Button ID="ButtonCancel" runat="server" OnClick="ButtonCancel_Click" Text="Cancel" CommandName="CancelEdit" />
        </div>
    </div>
</EditItemTemplate>


        </asp:DataList>
    </form>
</body>
</html>
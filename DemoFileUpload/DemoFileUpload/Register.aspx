<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="DemoFileUpload.Register" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Register</title>
    <style>
    body {
        font-family: 'Arial', sans-serif;
        background-image: url("https://t4.ftcdn.net/jpg/05/96/62/65/360_F_596626503_jrzjZNYStDexiWxQFqO7oCh6M8PdMlJs.jpg");
        margin: 0;
        padding: 0;
        display: flex;
        align-items: center;
        justify-content: center;
        height: 100vh;
    }

    form {
        background-color: #fff;
        padding: 20px;
        border-radius: 8px;
        box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
        width: 300px;
        border: 2px solid #ddd;
        box-sizing: border-box;
    }

    p {
        margin: 10px 0;
    }

    label {
        display: block;
        font-weight: bold;
        margin-bottom: 6px;
    }

    input {
        width: 100%;
        padding: 8px;
        margin-top: 4px;
        box-sizing: border-box;
        border: 1px solid #ccc;
        border-radius: 4px;
    }

    button {
        background-color: #4caf50;
        color: #fff;
        padding: 10px 15px;
        border: none;
        border-radius: 4px;
        cursor: pointer;
    }

    button:hover {
        background-color: #45a049;
    }
</style>

</head>
<body>
    <form id="form1" runat="server">
    <p>
        Name
        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
    </p>
        <p>
        &nbsp; Email<asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
    </p>
        <p>
        &nbsp; pass&nbsp;
            <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
    </p>
        <p>
            &nbsp;&nbsp;<asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="submit" />
&nbsp;&nbsp;
            <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="Login" />
    </p>
</form>
</body>
</html>

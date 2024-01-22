<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="DemoFileUpload.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Registration and Login</title>
   <style type="text/css">
        .hidden {
            display: none;
        }

        body {
        background-image: url("https://images.unsplash.com/photo-1445205170230-053b83016050?q=80&w=1771&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D");
        overflow: hidden;
        display: flex;
        justify-content: center;
        align-items: center;
        height: 100vh;
        margin: 0;
        }

        ::selection {
            background-image: url(https://images.unsplash.com/photo-1445205170230-053b83016050?q=80&w=1771&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D)
        }

        .container {
            max-width: 200px;
            margin: 170px auto;
        }

        .wrapper {
            width: 100%;
            background: #fff;
            border-radius: 5px;
            box-shadow: 0px 4px 10px 1px rgba(0,0,0,0.1);
        }

        .wrapper .title {
            height: 90px;
            background: #16a085;
            border-radius: 5px 5px 0 0;
            color: #fff;
            font-size: 30px;
            font-weight: 600;
            display: flex;
            align-items: center;
            justify-content: center;
        }

        .wrapper form {
            padding: 30px 25px 25px 25px;
        }

        .wrapper form .row {
            height: 45px;
            margin-bottom: 15px;
            position: relative;
        }

        .wrapper form .row input {
            height: 100%;
            width: 100%;
            outline: none;
            padding-left: 60px;
            border-radius: 5px;
            border: 1px solid lightgrey;
            font-size: 16px;
            transition: all 0.3s ease;
        }

        form .row input:focus {
            border-color: #16a085;
            box-shadow: inset 0px 0px 2px 2px rgba(26,188,156,0.25);
        }

        form .row input::placeholder {
            color: #999;
        }

        .wrapper form .row i {
            position: absolute;
            width: 47px;
            height: 100%;
            color: #fff;
            font-size: 18px;
            background: #16a085;
            border: 1px solid #16a085;
            border-radius: 5px 0 0 5px;
            display: flex;
            align-items: center;
            justify-content: center;
        }

        .wrapper form .pass {
            margin: -8px 0 20px 0;
        }

        .wrapper form .pass a {
            color: #16a085;
            font-size: 17px;
            text-decoration: none;
        }

        .wrapper form .pass a:hover {
            text-decoration: underline;
        }

        .wrapper form .button input {
            color: #fff;
            font-size: 20px;
            font-weight: 500;
            padding-left: 0px;
            background: #16a085;
            border: 1px solid #16a085;
            cursor: pointer;
        }

        form .button input:hover {
            background: #12876f;
        }

        .wrapper form .signup-link {
            text-align: center;
            margin-top: 20px;
            font-size: 17px;
        }

        .wrapper form .signup-link a {
            color: #16a085;
            text-decoration: none;
        }

        form .signup-link a:hover {
            text-decoration: underline;
        }

       #form1 {
        /* Add your specific styles for the form with ID form1 */
        width: 100%;
        max-width: 250px;
        padding: 20px;
        background: #fff;
        border-radius: 5px;
        box-shadow: 0px 4px 10px 1px rgba(0,0,0,0.1);
    }

    /* Add any other specific styles for form elements within form1 if needed */
    #form1 input {
        /* Styles for form inputs within form1 */
        margin-bottom: 15px;
    }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div id="divRegistration" runat="server" class="hidden">
                <!-- Registration Form -->
                <div>
                    <asp:Label ID="lblFullName" runat="server" Text="Full Name:"></asp:Label>
                    <asp:TextBox ID="txtFullName" runat="server"></asp:TextBox>
                </div>
                <div>
                    <asp:Label ID="lblEmail" runat="server" Text="Email:"></asp:Label>
                    <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox>
                </div>
                <div>
                    <asp:Label ID="lblPassword" runat="server" Text="Password:"></asp:Label>
                    <asp:TextBox ID="txtPassword" runat="server" TextMode="Password"></asp:TextBox>
                </div>
                <div>
                    <asp:Button ID="btnRegister" runat="server" Text="Register" OnClick="btnRegister_Click" />
                </div>
            </div>

            <div id="divLogin" runat="server" class="hidden">
                <!-- Login Form -->
                <div>
                    <asp:Label ID="lblLoginEmail" runat="server" Text="Email:"></asp:Label>
                    <asp:TextBox ID="txtLoginEmail" runat="server"></asp:TextBox>
                </div>
                <div>
                    <asp:Label ID="lblLoginPassword" runat="server" Text="Password:"></asp:Label>
                    <asp:TextBox ID="txtLoginPassword" runat="server" TextMode="Password"></asp:TextBox>
                </div>
                <div>
                    <asp:Button ID="btnLogin" runat="server" Text="Login" OnClick="btnLogin_Click" />
                </div>
            </div>

            <!-- Toggle Buttons -->
            <div>
                <asp:Button ID="btnShowRegistration" runat="server" Text="Register" OnClientClick="toggleForms('divRegistration', 'divLogin'); return false;" />
                <asp:Button ID="btnShowLogin" runat="server" Text="Login" OnClientClick="toggleForms('divLogin', 'divRegistration'); return false;" />
            </div>
        </div>
    </form>

    <script type="text/javascript">
        function toggleForms(showDiv, hideDiv) {
            document.getElementById(showDiv).style.display = 'block';
            document.getElementById(hideDiv).style.display = 'none';
            return false;
        }
    </script>
</body>
</html>
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Youtubeudemy.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Login</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-T3c6CoIi6uLrA9TneNEoa7RxnatzjcDSCmG1MXxSR1GAsXEV/Dwwykc2MPK8M2HN" crossorigin="anonymous" />
</head>
<body>
    <form id="formLogin" runat="server">
        <div class="container mt-5">
            <div class="row justify-content-center">
                <div class="col-md-6">
                    <div class="card">
                        <div class="card-header">
                            Login
                        </div>
                        <div class="card-body">
                            <div class="form-group">
                                <label for="txtEmail">Email address</label>
                                <input type="email" class="form-control" id="txtEmail" runat="server" placeholder="Enter email" />
                            </div>
                            <div class="form-group">
                                <label for="txtPassword">Password</label>
                                <input type="password" class="form-control" id="txtPassword" runat="server" placeholder="Password" />
                            </div>
                            <button type="submit" class="btn btn-primary" runat="server">Login</button>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>

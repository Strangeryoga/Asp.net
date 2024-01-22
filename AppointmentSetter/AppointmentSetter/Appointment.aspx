<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Appointment.aspx.cs" Inherits="AppointmentSetter.Appointment" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Appointment Setter</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h2>Appointment Setter</h2>
            <label>Name<asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
            <br />
            Type<asp:DropDownList ID="DropDownList1" runat="server">
                <asp:ListItem Text="Doctor" Value="Doctor"></asp:ListItem>
                <asp:ListItem Text="Principal" Value="Principal"></asp:ListItem>
                <asp:ListItem Text="Lawyer" Value="Doctor"></asp:ListItem>
            </asp:DropDownList>
            <br />
            Date:</label>
            <asp:TextBox ID="txtDate" runat="server" type="date" /><br />
            <label>Time:</label>
            <asp:TextBox ID="txtTime" runat="server" type="time" /><br />
            <asp:Button ID="btnSetAppointment" runat="server" Text="Set Appointment" OnClick="btnSetAppointment_Click" />
            <br />
            <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
        </div>
    </form>
</body>
</html>

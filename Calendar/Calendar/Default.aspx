<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Calendar.Default" EnableEventValidation="false" ViewStateMode="Enabled" Async="true" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Event Calendar</title>
    <script src="https://code.jquery.com/jquery-3.6.4.min.js"></script>
    <link rel="stylesheet" href="https://code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css"/>
    <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h2>Event Calendar</h2>
            <label>Select Date:</label>
            <asp:TextBox ID="txtDate" runat="server" TextMode="Date" Required="false" AutoPostBack="true"></asp:TextBox>
            <br />
            <label>Event:</label>
            <asp:TextBox ID="txtEvent" runat="server"></asp:TextBox>
            <br />
            <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" />
            <br />
            <asp:Calendar ID="calEvents" runat="server" OnDayRender="calEvents_DayRender" OnSelectionChanged="calEvents_SelectionChanged" style="height: 300px;"></asp:Calendar>
            <br />
        <asp:Button ID="btnShowEvents" runat="server" Text="Show Events" OnClick="btnShowEvents_Click" CausesValidation="false" UseSubmitBehavior="false" />
            <asp:GridView ID="gvEvents" runat="server" AutoGenerateColumns="false">
                <Columns>
                    <asp:BoundField DataField="EventDate" HeaderText="Event Date" />
                    <asp:BoundField DataField="EventText" HeaderText="Event Text" />
                </Columns>
            </asp:GridView>

        </div>
    </form>
</body>
</html>

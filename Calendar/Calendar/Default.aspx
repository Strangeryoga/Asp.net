<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Calendar.Default" EnableEventValidation="false" ViewStateMode="Enabled" Async="true" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Event Calendar</title>
    <script src="https://code.jquery.com/jquery-3.6.4.min.js"></script>
    <link rel="stylesheet" href="https://code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css"/>
    <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>
    <style>
        body {
            font-family: 'Arial', sans-serif;
            background-color: #f4f4f4;
            margin: 0;
            padding: 0;
            text-align: center;
        }

        #container {
            width: 60%;
            margin: 0 auto;
            background-color: #fff;
            padding: 20px;
            border-radius: 10px;
            box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
            text-align: left;
        }

        h2 {
            color: #333;
        }

        label {
            display: block;
            margin-bottom: 5px;
            font-weight: bold;
        }

        input[type="text"],
        input[type="date"] {
            width: 100%;
            padding: 8px;
            margin-bottom: 10px;
            box-sizing: border-box;
        }

        #form1 {
            display: inline-block;
            width: 100%;
        }

        #calendarContainer {
            width: 100%;
            margin-top: 20px;
        }

        #calEvents {
            width: 100%;
            margin-top: 10px;
        }

        #btnShowEvents {
            margin-top: 10px;
        }

        #gvEvents {
            width: 100%;
            margin-top: 20px;
            border-collapse: collapse;
        }

        #gvEvents th,
        #gvEvents td {
            border: 1px solid #ddd;
            padding: 8px;
            text-align: left;
        }

        #gvEvents th {
            background-color: #f2f2f2;
        }

        #gvEvents tr:nth-child(even) {
            background-color: #f9f9f9;
        }

        #gvEvents tr:hover {
            background-color: #f5f5f5;
        }
    </style>
</head>
<body>
       <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <div id="container">
            <h2>Event Calendar</h2>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div>
                        <label>Select Date:</label>
                        <asp:TextBox ID="txtDate" runat="server" TextMode="Date" Required="false" AutoPostBack="true"></asp:TextBox>
                        <br />
                        <div>
                            <label>Select Event Type:</label>
                            <asp:DropDownList ID="ddlEventType" runat="server">
                                <asp:ListItem Text="Holiday" Value="Holiday"></asp:ListItem>
                                <asp:ListItem Text="Birthday" Value="Birthday"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <br />
                        <label>Event:</label>
                        <asp:TextBox ID="txtEvent" runat="server"></asp:TextBox>
                        <br />
                        <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" />
                        <br />
                        <asp:Calendar ID="calEvents" runat="server" OnDayRender="calEvents_DayRender" OnSelectionChanged="calEvents_SelectionChanged" style="height: 300px;" BackColor="White" BorderColor="White" BorderWidth="1px" Font-Names="Verdana" Font-Size="9pt" ForeColor="Black" Height="190px" NextPrevFormat="FullMonth" Width="100%">
                        </asp:Calendar>
                        <br />
                        <asp:Button ID="btnShowEvents" runat="server" Text="Show Events" OnClick="btnShowEvents_Click" CausesValidation="false" UseSubmitBehavior="false" />
                        <asp:GridView ID="gvEvents" runat="server" AutoGenerateColumns="false"  DataKeyNames="ID" OnRowEditing="gvEvents_RowEditing" OnRowUpdating="gvEvents_RowUpdating" OnRowCancelingEdit="gvEvents_RowCancelingEdit" OnRowDeleting="gvEvents_RowDeleting" >
                            <Columns>
                                <asp:CommandField ShowEditButton="True" HeaderText="Edit"/>
                                <asp:CommandField ShowDeleteButton="True" HeaderText="Delete"/>
                                <asp:BoundField DataField="EventDate" HeaderText="Event Date" />
                                <asp:BoundField DataField="EventText" HeaderText="Event Text" />
                            </Columns>
                        </asp:GridView>
                    </div>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="calEvents" EventName="SelectionChanged" />
                    <asp:AsyncPostBackTrigger ControlID="btnShowEvents" EventName="Click" />
                </Triggers>
            </asp:UpdatePanel>
        </div>
    </form>
</body>
</html>

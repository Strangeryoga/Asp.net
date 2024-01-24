<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdminLeaveRequests.aspx.cs" Inherits="LeaveApplication.AdminLeaveRequests" EnableEventValidation="false" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Admin Leave Requests</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h2>Admin Leave Requests</h2>
<asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="RequestID" OnRowCommand="GridView1_RowCommand" AutoPostBack="true">
    <Columns>
                    <asp:BoundField DataField="RequestID" HeaderText="RequestID" SortExpression="RequestID" ReadOnly="True" />
                    <asp:BoundField DataField="UserID" HeaderText="UserID" SortExpression="UserID" />
                    <asp:BoundField DataField="StartDate" HeaderText="StartDate" SortExpression="StartDate" />
                    <asp:BoundField DataField="EndDate" HeaderText="EndDate" SortExpression="EndDate" />
                    <asp:BoundField DataField="Reason" HeaderText="Reason" SortExpression="Reason" />
                    <asp:BoundField DataField="Status" HeaderText="Status" SortExpression="Status" />
                    <asp:TemplateField HeaderText="Action">
                        <ItemTemplate>
                           <asp:Button ID="btnApprove" runat="server" CommandName="Approve" CommandArgument='<%# Container.DataItemIndex %>' Text="Approve" />
                            <asp:Button ID="btnReject" runat="server" CommandName="Reject" CommandArgument='<%# Container.DataItemIndex %>' Text="Reject" />

                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="SD">
                <ItemTemplate>
                    <asp:Button ID="btnDetails" runat="server" CommandName="Details" CommandArgument='<%# Container.DataItemIndex %>' Text="Slip" />
                </ItemTemplate>
            </asp:TemplateField>

                </Columns>
            </asp:GridView>
        </div>
    </form>
</body>
</html>

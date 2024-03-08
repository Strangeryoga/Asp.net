<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="MobilePage.aspx.cs" Inherits="ProjectWeMate.MobilePage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MenuContentHolder" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContentHolder" runat="server">
<div style="text-align:center">
    <h4 class="alert alert-danger" >Mobile Broswer Detected</h4>
</div>
<div class="alert alert-info">
    Sorry to say that this site is not designed for mobile browser.
    For best browsing exprerience, I recommend you to use Desktop/laptop.
    <div>But if you still want to continue browsering with mobile device <a href="Default.aspx" style="color:red">click here</a>. (not recommended)</div>
</div>
</asp:Content>

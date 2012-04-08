<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Login.ascx.cs" Inherits="Mobile.Controls.Account.Login" %>
<div class="frame_container">
    <div class="frame_row">
        <label class="frame_row_lable"><%= Mobile.Core.Web.ResxManager.GetResx("Email") %>:</label>
        <asp:TextBox ID="txtEmail" runat="server" CssClass="frame_row_input text" />
    </div>
    <div class="frame_row">
        <label class="frame_row_lable"><%= Mobile.Core.Web.ResxManager.GetResx("Password") %>:</label>
        <asp:TextBox CssClass="frame_row_input password" ID="txtPassword" runat="server" TextMode=Password/>
    </div>
    <div class="frame_row">
        <asp:Button ID="btnLogin" runat="server" CssClass="frame_row_input submit" OnClick="btnLogin_Click" />
    </div>
</div>
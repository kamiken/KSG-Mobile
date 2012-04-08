<%@ Page Title="Log In" Language="C#" MasterPageFile="~/Default.master" AutoEventWireup="true"
    CodeBehind="Login.aspx.cs" Inherits="Mobile.Account.Login" %>

<%@ Register Src="/Controls/Account/Login.ascx" TagName="Login" TagPrefix="uc1" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <h2>
        Log In
    </h2>
    <p>
        <uc1:Login ID="Login1" runat="server" />
        </p>
    
</asp:Content>

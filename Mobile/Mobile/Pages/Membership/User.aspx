<%@ Page Title="" Language="C#" MasterPageFile="~/Default.Master" AutoEventWireup="true" CodeBehind="User.aspx.cs" Inherits="Mobile.Pages.Membership.User" %>
<%@ Register Src="/Controls/Membership/User.ascx" TagName="User" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <uc1:User ID="User1" runat="server" />
</asp:Content>

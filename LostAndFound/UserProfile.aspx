<%@ Page Title="" Language="C#" MasterPageFile="~/LostAndFound.Master" AutoEventWireup="true" CodeBehind="UserProfile.aspx.cs" Inherits="LostAndFound.UserProfile" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   <div class="page-container">
    <div class="container" style="width:60%;">
        <h1 style="text-align:center"><asp:Label ID="Username" runat="server" Text="Label"></asp:Label></h1>
        <br />
        <br />
        <asp:Label ID="Label1" runat="server" Text="Email:"></asp:Label>
&nbsp;<asp:Label ID="EmailLabel" runat="server"></asp:Label>
        <br />
        <br />
        <asp:Label ID="Label2" runat="server" Text="Phone number:"></asp:Label>
&nbsp;<asp:Label ID="PhoneNumberLabel" runat="server"></asp:Label>
        </div>
       </div>
</asp:Content>

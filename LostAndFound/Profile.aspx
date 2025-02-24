<%@ Page Title="" Language="C#" MasterPageFile="~/LostAndFound.Master" AutoEventWireup="true" CodeBehind="Profile.aspx.cs" Inherits="LostAndFound.Profile" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="page-container">
    <div class="container" style="width:60%;">
        <h1 style="text-align:center"><asp:Label ID="Username" runat="server" Text="Label"></asp:Label></h1>
        <br />
        <br />
        <asp:Label ID="Label1" runat="server" Text="Email:"></asp:Label>
&nbsp;<asp:Label ID="SessionEmail" runat="server" Text="Label"></asp:Label>
        <br />
        <br />
        <asp:Label ID="Label2" runat="server" Text="Phone number:"></asp:Label>
&nbsp;<asp:Label ID="SessionPhoneNumber" runat="server" Text="Label"></asp:Label>
        <br />
        <br />
        <br />
        <div class="button-container">
        <asp:Button ID="Edit" CssClass="btn" runat="server" OnClick="Edit_Click" Text="Edit profile" />
        <asp:Button ID="LogOut" CssClass="btn" runat="server" OnClick="LogOut_Click" Text="Log out" />
        </div>
        <br />

    </div>
    </div>
</asp:Content>

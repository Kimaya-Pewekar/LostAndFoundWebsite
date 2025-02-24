<%@ Page Language="C#" MasterPageFile="~/LostAndFound.Master" AutoEventWireup="true" CodeBehind="HomePage.aspx.cs" Inherits="LostAndFound.HomePage" %>

<asp:Content ContentPlaceHolderID="ContentPlaceHolder1" ID="Content1" runat="server">
    <div class="page-container">
    <div class="container"style="width:70%; height:60vh; align-items:center;">

        <h1 style=" font-size: 50px; font-weight: bold; margin-bottom: 100px;"><label id="tagline">Your stuff called. It wants you back.</label></h1>
        <br />
        <br />
        <div class="button-container">
        <asp:Button CssClass="btn" ID="Button1" runat="server" Text="List a lost item" OnClick="Button1_Click" />
        <asp:Button CssClass="btn" ID="Button2" runat="server" Text="Report a found item" OnClick="Button2_Click" />
        </div>
        <br />


    </div>
        </div>
</asp:Content>
<%@ Page Title="Report a lost item" Language="C#" MasterPageFile="~/LostAndFound.Master" AutoEventWireup="true" CodeBehind="ReportLostItem.aspx.cs" Inherits="LostAndFound.ReportLostItem" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   <div class="page-container">
    <div class="container" style="width:80%">

        Item name *<br />
        <asp:TextBox ID="lostItemName" runat="server"></asp:TextBox>
        <br />
        <br />
        Item description<br />
        <asp:TextBox ID="lostItemDescription" runat="server" TextMode="MultiLine"></asp:TextBox>
        <br />
        <br />
        Last location *<br />
        <asp:TextBox ID="lostItemLocation" runat="server"></asp:TextBox>
        <br />
        <br />
        Last date *<br />
        <asp:TextBox ID="lostItemDate" runat="server" TextMode="Date"></asp:TextBox>
        <br />
        <br />
        Item type&nbsp;*&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:DropDownList ID="lostItemCategories" runat="server">
            <asp:ListItem>Select a category</asp:ListItem>
            <asp:ListItem>Electronics</asp:ListItem>
            <asp:ListItem>Keys</asp:ListItem>
            <asp:ListItem>Wallet</asp:ListItem>
            <asp:ListItem>Phone</asp:ListItem>
            <asp:ListItem>ID</asp:ListItem>
            <asp:ListItem>Jewelry</asp:ListItem>
            <asp:ListItem>Others</asp:ListItem>
        </asp:DropDownList>
        <br />
        <br />
        Upload an image<br />
        <asp:FileUpload ID="lostImageUpload" runat="server" />
        <br />
        <asp:Label ID="ImageError" runat="server" Text="Label" Visible="False"></asp:Label>
        <br />
        <br />
        <asp:Label ID="FieldsNotFilledError" runat="server" Text="Label" Visible="False"></asp:Label>
        <br />
        <asp:Button ID="submitLostReport" Cssclass="btn" runat="server" Text="Submit" OnClick="submitLostReport_Click" />

    </div>
     </div>
</asp:Content>

<%@ Page Title="Report a found item" Language="C#" MasterPageFile="~/LostAndFound.Master" AutoEventWireup="true" CodeBehind="ReportFoundItem.aspx.cs" Inherits="LostAndFound.ReportFoundItem" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="page-container">
    <div class="container" style="width:80%;">

    Item name *<br />
    <asp:TextBox ID="foundItemName" style="width:60%" runat="server"></asp:TextBox>
    <br />
    <br />
    Item description<br />
    <asp:TextBox ID="foundItemDescription" runat="server" TextMode="MultiLine"></asp:TextBox>
    <br />
    <br />
    Location found at *<br />
    <asp:TextBox ID="foundItemLocation" runat="server"></asp:TextBox>
    <br />
    <br />
    Date found on *<br />
    <asp:TextBox ID="foundItemDate" runat="server" TextMode="Date"></asp:TextBox>
    <br />
    <br />
    Item type *&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:DropDownList ID="foundItemCategory" runat="server">
        <asp:ListItem>Select a category</asp:ListItem>
        <asp:ListItem>Electronics</asp:ListItem>
        <asp:ListItem>Keys</asp:ListItem>
        <asp:ListItem>Wallet</asp:ListItem>
        <asp:ListItem>Phone</asp:ListItem>
        <asp:ListItem>ID</asp:ListItem>
        <asp:ListItem>Jewelry</asp:ListItem>
        <asp:ListItem>Others</asp:ListItem>
        <asp:ListItem></asp:ListItem>
    </asp:DropDownList>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <br />
    <br />
    Upload an image<br />
    <asp:FileUpload ID="foundImageUpload" runat="server" />
&nbsp;
    <br />
    <asp:Label ID="ImageTypeError" runat="server" Text="Label" Visible="False"></asp:Label>
    <br />
    <br />
    <asp:Label ID="FieldsNotFilledError" runat="server" Text="Label" Visible="False"></asp:Label>
    <br />
    <asp:Button ID="SubmitFoundReport" Cssclass="btn" runat="server" OnClick="SubmitFoundReport_Click" Text="Submit" />

    </div>
        </div>
</asp:Content>

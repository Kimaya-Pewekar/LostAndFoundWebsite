<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditProfile.aspx.cs" Inherits="LostAndFound.WebForm5" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="stylesheet" type="text/css" href="StyleSheet1.css" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div class="page-container">
        <div class="container" width="60%;">
            <h1><asp:Label ID="Label1" runat="server" Text="Edit profile"></asp:Label></h1>
            <br />
            <br />
            <asp:Label ID="Label2" runat="server" Text="Enter new username"></asp:Label>
            <br />
            <asp:TextBox ID="UsernameField" runat="server"></asp:TextBox>
            <br />
            <br />
            <asp:Label ID="UsernameValidation" runat="server" Text="Label" Visible="False"></asp:Label>
            <br />
            <br />
            <br />
            <br />
            <asp:Label ID="Label3" runat="server" Text="Enter new phone number"></asp:Label>
            <br />
            <asp:TextBox ID="PhoneNumberField" runat="server"></asp:TextBox>
            <br />
            <br />
            <asp:Label ID="PhonenumberValidation" runat="server" Text="Label" Visible="False"></asp:Label>
            <br />
            <br />
            <br />
            <br />
            <br />
            <asp:Button ID="Submit" runat="server" CssClass="btn" OnClick="Submit_Click" Text="Submit" />
        </div>
            </div>
    </form>
</body>
</html>

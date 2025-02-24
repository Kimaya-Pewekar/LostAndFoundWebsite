<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="LostAndFound.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="stylesheet" type="text/css" href="StyleSheet1.css" />
    <title>Sign up</title>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container" style="display: flex; flex-direction: column;">
            <h1>Sign up</h1><br />
            <h3>Create an account or <asp:HyperLink runat="server" NavigateUrl="~/LoginPage.aspx">sign in.</asp:HyperLink></h3><br />
            <br />
            Email<br />
            <asp:TextBox ID="EmailField" runat="server"></asp:TextBox>
            <br />
            <asp:Label ID="EmailValidationMessage" runat="server" Text="Label" Visible="False"></asp:Label>
            <br />
            <br />
            Username<br />
            <asp:TextBox ID="UsernameField" runat="server"></asp:TextBox>
            <br />
            <asp:Label ID="UsernameValidationMessage" runat="server" Text="Label" Visible="False"></asp:Label>
            <br />
            <br />
            Password<br />
            <asp:TextBox ID="PasswordField" runat="server" TextMode="Password"></asp:TextBox>
            <br />
            <asp:Label ID="PasswordValidationMessage" runat="server" Text="Label" Visible="False"></asp:Label>
            <br />
            <br />
            Phone number<br />
            <asp:TextBox ID="PhoneNumberField" runat="server"></asp:TextBox>
            <br />
            <asp:Label ID="PhoneNumberValidationMessage" runat="server" Text="Label" Visible="False"></asp:Label>
            <br />
            <br />
            <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Sign up" CssClass="btn" />
            <br />
            <br />
            <asp:Label ID="UserExistsMessage" runat="server" Text="Label" Visible="False"></asp:Label>
               
        </div>
    </form>
</body>
</html>

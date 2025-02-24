<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LoginPage.aspx.cs" Inherits="LostAndFound.WebForm2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Login page</title>
    <link rel="stylesheet" type="text/css" href="StyleSheet1.css" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="container" style="width:70%; height:90vh; display: flex; flex-direction: column;">
            <h1>Login</h1><br />
            <h3><asp:Label ID="SignUpHyperlink" runat="server">Or <asp:HyperLink runat="server" NavigateUrl="~/Default.aspx" Text="sign up" /> instead.</asp:Label></h3>
            <br />
            <br />
            Email<br />
            <asp:TextBox ID="EmailLoginField" runat="server"></asp:TextBox>
            <br />
            <asp:Label ID="EmailValidationLine" runat="server" Text="Label" Visible="False"></asp:Label>
            <br />
            <br />
            Password<br />
            <asp:TextBox ID="PasswordLoginField" runat="server" TextMode="Password"></asp:TextBox>
            <br />
            <asp:Label ID="PasswordValidationMessage" runat="server" Text="Label" Visible="False"></asp:Label>
            <br />
            <br />
            <asp:HyperLink ID="ForgotPassword" runat="server" NavigateUrl="~/ResetPassword.aspx">Forgot password?</asp:HyperLink>
            <br />
            <br />
            <asp:Button ID="LoginButton" CssClass="btn" runat="server" OnClick="LoginButton_Click" Text="Log in" />
        </div>
    </form>
</body>
</html>

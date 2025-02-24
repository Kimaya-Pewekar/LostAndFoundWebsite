<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ResetPassword.aspx.cs" Inherits="LostAndFound.WebForm3" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="stylesheet" type="text/css" href="StyleSheet1.css" />
    <title>Reset password</title>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <h1>Reset password</h1><br />
            <br />
            Enter email<br />
            <asp:TextBox ID="EmailField" runat="server"></asp:TextBox>
            <br />
            <br />
            Enter new password<br />
            <asp:TextBox ID="NewPasswordField" runat="server" TextMode="Password"></asp:TextBox>
            <br />
            <asp:Label ID="PasswordValidationMessage" runat="server" Text="Label" Visible="False"></asp:Label>
            <br />
            <br />
            Confirm new password<br />
            <asp:TextBox ID="NewPasswordConfirmField" runat="server" TextMode="Password"></asp:TextBox>
            <br />
            <asp:Label ID="PasswordConfirmationMessage" runat="server" Text="Label" Visible="False"></asp:Label>
            <br />
            <br />
            <br />
            <asp:Button ID="ResetPasswordButton" CssClass="btn" runat="server" OnClick="ResetPasswordButton_Click" Text="Reset" />
        </div>
    </form>
</body>
</html>

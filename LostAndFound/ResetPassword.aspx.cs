using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LostAndFound
{
    public partial class WebForm3 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void ResetPasswordButton_Click(object sender, EventArgs e)
        {
            string email = EmailField.Text;
            string newPassword = NewPasswordField.Text;
            string newPasswordConfirm = NewPasswordConfirmField.Text;

            if (IsValidPassword(newPassword) && newPassword == newPasswordConfirm)
            {
                PasswordValidationMessage.Visible = false;
                PasswordConfirmationMessage.Visible = false;

                UpdatePassword(email, newPassword);
                Response.Redirect("Default.aspx");
            }
            else
            {
                if (!IsValidPassword(newPassword))
                {
                    PasswordValidationMessage.Visible = true;
                    PasswordValidationMessage.Text = "Password must be at least 10 characters long.";
                }
                if (newPassword != newPasswordConfirm)
                {
                    PasswordConfirmationMessage.Visible = true;
                    PasswordConfirmationMessage.Text = "Passwords do not match.";
                }
            }
        }

        private bool IsValidPassword(string password)
        {
            return password.Length >= 10;
        }

        private void UpdatePassword(string email, string newPassword)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["LostAndFoundDB"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "UPDATE Users SET password = @Password WHERE email = @Email";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Email", email);
                    command.Parameters.AddWithValue("@Password", newPassword);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
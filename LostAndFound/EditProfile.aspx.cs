using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LostAndFound
{
    public partial class WebForm5 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadUserProfile();
            }
        }
        private void LoadUserProfile()
        {
            if (Session["UserId"] != null)
            {
                int userId = Convert.ToInt32(Session["UserId"]);
                string connectionString = ConfigurationManager.ConnectionStrings["LostAndFoundDB"].ConnectionString;

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = "SELECT username, phone_number FROM [dbo].[Users] WHERE UserIdNumber = @UserId";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@UserId", userId);

                        conn.Open();
                        SqlDataReader reader = cmd.ExecuteReader();
                        if (reader.Read())
                        {
                            UsernameField.Text = reader["username"].ToString();
                            PhoneNumberField.Text = reader["phone_number"].ToString();
                        }
                    }
                }
            }
            else
            {
                Response.Redirect("LoginPage.aspx");
            }
        }
        private bool IsValidUsername(string username)
        {
            if (username.Length < 5)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private bool IsValidPhoneNumber(string phoneNumber)
        {
            string pattern = @"^\d{10}$";
            Regex regex = new Regex(pattern);
            return regex.IsMatch(phoneNumber);
        }

        private bool UsernameIsTaken(string username)
        {
            int userId = Convert.ToInt32(Session["UserId"]);
            string connectionString = ConfigurationManager.ConnectionStrings["LostAndFoundDB"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT COUNT(*) FROM Users WHERE username = @Username AND UserIdNumber != @UserId";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Username", username);
                    command.Parameters.AddWithValue("@UserId", userId);
                    connection.Open();
                    int count = (int)command.ExecuteScalar();

                    return count > 0;
                }
            }
        }

        protected void Submit_Click(object sender, EventArgs e)
        {
            int userId = Convert.ToInt32(Session["UserId"]);
            string newUsername = UsernameField.Text.Trim();
            string newPhoneNumber = PhoneNumberField.Text.Trim();
            if (IsValidPhoneNumber(newPhoneNumber) && IsValidUsername(newUsername) && !UsernameIsTaken(newUsername))
            {
                string connectionString = ConfigurationManager.ConnectionStrings["LostAndFoundDB"].ConnectionString;
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = "UPDATE [dbo].[Users] SET username = @Username, phone_number = @PhoneNumber WHERE UserIdNumber = @UserId";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Username", newUsername);
                        cmd.Parameters.AddWithValue("@PhoneNumber", newPhoneNumber);
                        cmd.Parameters.AddWithValue("@UserId", userId);

                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
                Response.Redirect("Profile.aspx");
            }
            else
            {
                if (!IsValidUsername(newUsername))
                {
                    UsernameValidation.Text = "Username must be at least 5 characters long";
                    UsernameValidation.Visible = true;
                }
                if (!IsValidPhoneNumber(newPhoneNumber))
                {
                    PhonenumberValidation.Text = "Enter valid phone number";
                    PhonenumberValidation.Visible = true;
                }
                if (UsernameIsTaken(newUsername))
                {
                    UsernameValidation.Text = "Username is taken";
                    UsernameValidation.Visible = true;
                }
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;

namespace LostAndFound
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string email = EmailField.Text;
            string username = UsernameField.Text;
            string password = PasswordField.Text;
            string phoneNumber = PhoneNumberField.Text;
            if (IsValidEmail(email) && IsValidUsername(username) && IsValidPassword(password))
            {
                EmailValidationMessage.Text = string.Empty;
                UsernameValidationMessage.Text = string.Empty;
                PasswordValidationMessage.Text = string.Empty;

                if (UserExists(email, username))
                {
                    UserExistsMessage.Visible = true;
                    UserExistsMessage.Text = "An account with this email or username already exists.";
                }
                else
                {
                    InsertUserData(email, username, password, phoneNumber);
                    StoreUserInfoInSession(email);
                    Response.Redirect("HomePage.aspx");
                }
            }
            else
            {
                if (!IsValidEmail(email))
                {
                    EmailValidationMessage.Visible = true;
                    EmailValidationMessage.Text = "Invalid email address.";
                }
                if (!IsValidUsername(username))
                {
                    UsernameValidationMessage.Visible = true;
                    UsernameValidationMessage.Text = "Username must be at least 5 characters long.";
                }
                if (!IsValidPassword(password))
                {
                    PasswordValidationMessage.Visible = true;
                    PasswordValidationMessage.Text = "Password must be at least 10 characters long.";
                }
                if (!IsValidPhoneNumber(phoneNumber))
                {
                    PhoneNumberValidationMessage.Visible = true;
                    PhoneNumberValidationMessage.Text = "Invalid phone number.";
                }
            }
        }
        private bool IsValidEmail(string email)
        {
            string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            Regex regex = new Regex(pattern);
            return regex.IsMatch(email);
        }

        private bool IsValidUsername(string username)
        {
            if(username.Length < 5)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private bool IsValidPassword(string username)
        {
            if (username.Length < 10)
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
        private bool UserExists(string email, string username)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["LostAndFoundDB"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT COUNT(*) FROM Users WHERE email = @Email OR username = @Username";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Email", email);
                    command.Parameters.AddWithValue("@Username", username);

                    connection.Open();
                    int count = (int)command.ExecuteScalar();
                    return count > 0;
                }
            }
        }
        private void InsertUserData(string email, string username, string password, string phonenumber)
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["LostAndFoundDB"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO Users (email, username, password,phone_number) VALUES (@Email, @Username, @Password, @PhoneNumber)";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Email", email);
                    command.Parameters.AddWithValue("@Username", username);
                    command.Parameters.AddWithValue("@Password", password);
                    command.Parameters.AddWithValue("@PhoneNumber", phonenumber);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        private void StoreUserInfoInSession(string email)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["LostAndFoundDB"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT UserIdNumber, username FROM Users WHERE email = @Email";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Email", email);

                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        Session["UserId"] = reader["UserIdNumber"];
                        Session["Username"] = reader["username"];
                    }
                }
            }
        }

    }
}

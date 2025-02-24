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
    public partial class WebForm2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                
            }

        }

        protected void LoginButton_Click(object sender, EventArgs e)
        {
            string email = EmailLoginField.Text;
            string password = PasswordLoginField.Text;
            if (IsValidEmail(email))
            {
                if(IsValidPassword(email,password))
                {   
                    StoreUserInfoInSession(email);
                    Response.Redirect("HomePage.aspx");
                }
                else
                {
                    PasswordValidationMessage.Visible = true;
                    PasswordValidationMessage.Text = "Invalid password.";
                }
            }
            else
            {
                EmailValidationLine.Visible = true;
                EmailValidationLine.Text = "Invalid email or password.";
            }
        }

        private bool IsValidEmail(string email)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["LostAndFoundDB"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT COUNT(*) FROM Users WHERE email = @Email";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Email", email);

                    connection.Open();
                    int count = (int)command.ExecuteScalar();
                    return count > 0;
                }
            }
        }

        private bool IsValidPassword(string email, string password)
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["LostAndFoundDB"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT COUNT(*) FROM Users WHERE email = @email AND password = @password";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@email", email);
                    command.Parameters.AddWithValue("@password", password);

                    connection.Open();
                    int count = (int)command.ExecuteScalar();
                    return count > 0;
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
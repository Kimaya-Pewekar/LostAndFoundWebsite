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
    public partial class Profile : System.Web.UI.Page
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
                    string query = "SELECT username, email, phone_number FROM [dbo].[Users] WHERE UserIdNumber = @UserId";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@UserId", userId);

                        conn.Open();
                        SqlDataReader reader = cmd.ExecuteReader();
                        if (reader.Read())
                        {
                            Username.Text = reader["Username"] + "'s profile";
                            SessionEmail.Text = reader["email"].ToString();
                            SessionPhoneNumber.Text = reader["phone_number"].ToString();
                        }
                    }
                }
            }
            else
            {

                Response.Redirect("LoginPage.aspx");
            }
        }

        protected void Edit_Click(object sender, EventArgs e)
        {
            Response.Redirect("EditProfile.aspx");
        }
        protected void LogOut_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Response.Redirect("LoginPage.aspx");
        }
    }
}
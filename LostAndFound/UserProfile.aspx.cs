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
    public partial class UserProfile : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string username = Request.QueryString["username"];
                if (!string.IsNullOrEmpty(username))
                {
                    LoadUserProfile(username);
                }
                else
                {
                    Response.Redirect("LostItemsForum.aspx"); 
                }
            }
        }

        private void LoadUserProfile(string username)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["LostAndFoundDB"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT username, phone_number, email FROM [dbo].[Users] WHERE username = @Username";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Username", username);

                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        Username.Text = reader["username"].ToString()+"'s profile";
                        PhoneNumberLabel.Text = reader["phone_number"].ToString();
                        EmailLabel.Text = reader["email"].ToString();
                    }
                    else
                    {
                        Response.Redirect("LostItemsForum.aspx");
                    }
                }
            }
        }

    }
}
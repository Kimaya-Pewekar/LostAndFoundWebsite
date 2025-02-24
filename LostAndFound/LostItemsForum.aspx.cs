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
    public partial class LostItemsForum : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            LoadLostItems();

        }
        protected void Page_Init(object sender, EventArgs e)
        {
            RegisterDynamicControls();
        }

        private void LoadLostItems(string category = "All")
        {
            string connectionString = ConfigurationManager.ConnectionStrings["LostAndFoundDB"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT LostReportIdNumber, item_name AS ItemName, Item_image AS ImageUrl, username, LostDate AS DateLost, LastLocation, item_description AS Description, item_type AS ItemType, " +
                               "CASE WHEN username = @Username THEN CAST(1 AS BIT) ELSE CAST(0 AS BIT) END AS IsDeletable FROM [dbo].[LostReport] WHERE report_status = 'Pending'";

                if (category != "All")
                {
                    query += " AND item_type = @ItemType";
                }

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Username", Session["Username"].ToString());

                    if (category != "All")
                    {
                        cmd.Parameters.AddWithValue("@ItemType", category);
                    }

                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    var lostItems = new List<LostItem>();

                    while (reader.Read())
                    {
                        var lostItem = new LostItem
                        {
                            ReportId = reader.GetInt32(0),
                            ItemName = reader.GetString(1),
                            ImageUrl = "data:image/png;base64," +Convert.ToBase64String((byte[])reader["ImageUrl"]),
                            Username = reader.GetString(3),
                            DateLost = reader.GetDateTime(4),
                            LastLocation = reader.GetString(5),
                            Description = reader.GetString(6),
                            ItemType = reader.GetString(7),
                            IsDeletable = reader.GetBoolean(8),
                            Comments = GetComments(reader.GetInt32(0))
                        };
                        lostItems.Add(lostItem);
                    }

                    LostItemsRepeater.DataSource = lostItems;
                    LostItemsRepeater.DataBind();
                }
            }
        }

        protected void RegisterDynamicControls()
        {
            foreach (RepeaterItem item in LostItemsRepeater.Items)
            {
                TextBox commentBox = item.FindControl("CommentTextBox") as TextBox;
                Button addCommentButton = item.FindControl("AddCommentButton") as Button;

                if (commentBox != null)
                {
                    ClientScript.RegisterForEventValidation(commentBox.UniqueID);
                }

                if (addCommentButton != null)
                {
                    ClientScript.RegisterForEventValidation(addCommentButton.UniqueID, addCommentButton.CommandArgument);
                }
            }
        }


        private List<Comment> GetComments(int lostReportId)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["LostAndFoundDB"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT UserId, CommentText, CommentDate FROM [dbo].[LostReportComments] WHERE LostReportId = @LostReportId";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@LostReportId", lostReportId);

                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    var comments = new List<Comment>();

                    while (reader.Read())
                    {
                        comments.Add(new Comment
                        {
                            Username = GetUsername(reader.GetInt32(0)),
                            CommentText = reader.GetString(1),
                            CommentDate = reader.GetDateTime(2)
                        });
                    }

                    return comments;
                }
            }
        }

        private string GetUsername(int userId)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["LostAndFoundDB"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT Username FROM [dbo].[Users] WHERE UserIdNumber = @UserId";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@UserId", userId);

                    conn.Open();
                    return cmd.ExecuteScalar().ToString();
                }
            }
        }

        protected void DeleteButton_Click(object sender, EventArgs e)
        {
            int reportId = Convert.ToInt32((sender as Button).CommandArgument);

            string connectionString = ConfigurationManager.ConnectionStrings["LostAndFoundDB"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "DELETE FROM [dbo].[LostReport] WHERE LostReportIdNumber = @ReportId AND username = @Username";
                string deleteCommentsQuery = "DELETE FROM [dbo].[LostReportComments] WHERE LostReportId = @ReportId";
                using (SqlCommand deleteCommentsCmd = new SqlCommand(deleteCommentsQuery, conn))
                {
                    deleteCommentsCmd.Parameters.AddWithValue("@ReportId", reportId);
                    deleteCommentsCmd.ExecuteNonQuery();
                }
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@ReportId", reportId);
                    cmd.Parameters.AddWithValue("@Username", Session["Username"].ToString());
                    cmd.ExecuteNonQuery();
                }
            }

            LoadLostItems();
        }

        protected void AddCommentButton_Click(object sender, EventArgs e)
        {
            int reportId = Convert.ToInt32((sender as Button).CommandArgument);
            TextBox commentTextBox = (sender as Button).Parent.FindControl("CommentTextBox") as TextBox;
            string commentText = commentTextBox.Text.Trim();

            if (!string.IsNullOrEmpty(commentText))
            {
                int userId = Convert.ToInt32(Session["UserId"]);

                string connectionString = ConfigurationManager.ConnectionStrings["LostAndFoundDB"].ConnectionString;
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = "INSERT INTO [dbo].[LostReportComments] (LostReportId, UserId, CommentText) VALUES (@LostReportId, @UserId, @CommentText)";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@LostReportId", reportId);
                        cmd.Parameters.AddWithValue("@UserId", userId);
                        cmd.Parameters.AddWithValue("@CommentText", commentText);

                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                }

                LoadLostItems();
            }
        }
        protected void FilterButton_Click(object sender, EventArgs e)
        {
        string selectedCategory = FilterCategoryDropDown.SelectedValue;
        LoadLostItems(selectedCategory);
        }
        public class LostItem
        {
            public int ReportId { get; set; }
            public string ItemName { get; set; }
            public string ImageUrl { get; set; }
            public string Username { get; set; }
            public DateTime DateLost { get; set; }
            public string LastLocation { get; set; }
            public string Description { get; set; }
            public string ItemType { get; set; }
            public bool IsDeletable { get; set; }
            public List<Comment> Comments { get; set; }
        }

        public class Comment
        {
            public string Username { get; set; }
            public string CommentText { get; set; }
            public DateTime CommentDate { get; set; }
        }
    }
}


 

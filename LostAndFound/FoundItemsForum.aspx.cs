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
    public partial class FoundItemsForum : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            LoadFoundItems();
        }
        protected void Page_Init(object sender, EventArgs e)
        {
            RegisterDynamicControls();
        }

        private void LoadFoundItems(string category = "All")
        {
            string connectionString = ConfigurationManager.ConnectionStrings["LostAndFoundDB"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT FoundReportIdNumber, found_item_name AS ItemName, found_Image AS ImageUrl, user_name, FoundDate AS DateFound, Location, found_item_description AS Description, found_item_type AS ItemType, " +
                               "CASE WHEN user_name = @Username THEN CAST(1 AS BIT) ELSE CAST(0 AS BIT) END AS IsDeletable FROM [dbo].[FoundReport] WHERE found_report_status = 'Pending'";

                if (category != "All")
                {
                    query += " AND found_item_type = @ItemType";
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
                    var foundItems = new List<FoundItem>();

                    while (reader.Read())
                    {
                        var foundItem = new FoundItem
                        {
                            ReportId = reader.GetInt32(0),
                            ItemName = reader.GetString(1),
                            ImageUrl = "data:image/png;base64," + Convert.ToBase64String((byte[])reader["ImageUrl"]),
                            Username = reader.GetString(3),
                            DateFound = reader.GetDateTime(4),
                            LocationFound = reader.GetString(5),
                            Description = reader.GetString(6),
                            ItemType = reader.GetString(7),
                            IsDeletable = reader.GetBoolean(8),
                            Comments = GetComments(reader.GetInt32(0))
                        };
                        foundItems.Add(foundItem);
                    }

                    FoundItemsRepeater.DataSource = foundItems;
                    FoundItemsRepeater.DataBind();
                }
            }
        }
        protected void RegisterDynamicControls()
        {
            foreach (RepeaterItem item in FoundItemsRepeater.Items)
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

        private List<Comment> GetComments(int foundReportId)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["LostAndFoundDB"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT UserId, CommentText, CommentDate FROM [dbo].[FoundComments] WHERE FoundReportId = @FoundReportId";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@FoundReportId", foundReportId);

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
                string query = "SELECT username FROM [dbo].[Users] WHERE UserIdNumber = @UserId";

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
                string query = "DELETE FROM [dbo].[FoundReport] WHERE FoundReportIdNumber = @ReportId AND user_name = @Username";
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

            LoadFoundItems();
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
                    string query = "INSERT INTO [dbo].[FoundComments] (FoundReportId, UserId, CommentText) VALUES (@FoundReportId, @UserId, @CommentText)";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@FoundReportId", reportId);
                        cmd.Parameters.AddWithValue("@UserId", userId);
                        cmd.Parameters.AddWithValue("@CommentText", commentText);

                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                }

                LoadFoundItems();
            }
        }

        public class FoundItem
        {
            public int ReportId { get; set; }
            public string ItemName { get; set; }
            public string ImageUrl { get; set; }
            public string Username { get; set; }
            public DateTime DateFound { get; set; }
            public string LocationFound { get; set; }
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

        protected void FilterButton_Click(object sender, EventArgs e)
        {
            string selectedCategory = FilterCategoryDropDown.SelectedValue;
            LoadFoundItems(selectedCategory);
        }
    }
}
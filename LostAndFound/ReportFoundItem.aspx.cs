using System;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Web.UI.WebControls;

namespace LostAndFound
{
    public partial class ReportFoundItem : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            FetchUsername();
            if (!IsPostBack)
            {
                FieldsNotFilledError.Visible = false;
                ImageTypeError.Visible = false;
            }
        }

        private void FetchUsername()
        {
            if (Session["Username"] != null)
            {
                string username = Session["Username"].ToString();
            }
            else
            {
                Response.Redirect("LoginPage.aspx");
            }
        }

        protected void SubmitFoundReport_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(foundItemName.Text) ||
                string.IsNullOrEmpty(foundItemLocation.Text) ||
                string.IsNullOrEmpty(foundItemDate.Text) ||
                foundItemCategory.SelectedIndex == 0)
            {
                FieldsNotFilledError.Text = "Please fill in all required fields(marked with a *)).";
                FieldsNotFilledError.Visible = true;
                return;
            }

            string fileExtension = Path.GetExtension(foundImageUpload.FileName).ToLower();
            string[] allowedExtensions = { ".jpg", ".jpeg", ".png", ".gif" };
            if (Array.IndexOf(allowedExtensions, fileExtension) < 0)
            {
                ImageTypeError.Text = "Please upload a valid image file.";
                ImageTypeError.Visible = true;
                return;
            }

            byte[] imageBytes;
            using (BinaryReader br = new BinaryReader(foundImageUpload.PostedFile.InputStream))
            {
                imageBytes = br.ReadBytes(foundImageUpload.PostedFile.ContentLength);
            }

            int userId = Convert.ToInt32(Session["UserId"]);
            string username = Session["Username"].ToString();

            string connectionString = ConfigurationManager.ConnectionStrings["LostAndFoundDB"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO [dbo].[FoundReport] (FoundDate, Location, User_Id, found_Image, found_item_name, found_item_description, found_report_status, found_item_type, user_name) " +
                               "VALUES (@FoundDate, @FoundLocation, @UserId, @Item_image, @item_name, @item_description, @report_status, @item_type, @user_name)";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@FoundDate", Convert.ToDateTime(foundItemDate.Text));
                    cmd.Parameters.AddWithValue("@FoundLocation", foundItemLocation.Text);
                    cmd.Parameters.AddWithValue("@UserId", userId); 
                    cmd.Parameters.AddWithValue("@Item_image", imageBytes);
                    cmd.Parameters.AddWithValue("@item_name", foundItemName.Text);
                    cmd.Parameters.AddWithValue("@item_description", foundItemDescription.Text);
                    cmd.Parameters.AddWithValue("@report_status", "Pending");
                    cmd.Parameters.AddWithValue("@item_type", foundItemCategory.SelectedItem.Text);
                    cmd.Parameters.AddWithValue("@user_name", username);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }

            Response.Write("<script>alert('Found item report submitted successfully.');</script>");
            foundItemName.Text = "";
            foundItemDescription.Text = "";
            foundItemLocation.Text = "";
            foundItemDate.Text = "";
            foundItemCategory.SelectedIndex = 0;
        }

       
    }
}
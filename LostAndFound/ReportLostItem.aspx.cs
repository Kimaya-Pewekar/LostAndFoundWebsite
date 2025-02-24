using System;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Web.UI.WebControls;

namespace LostAndFound
{
    public partial class ReportLostItem : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            FetchUsername();
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

        protected void submitLostReport_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(lostItemName.Text) ||
                string.IsNullOrEmpty(lostItemLocation.Text) ||
                string.IsNullOrEmpty(lostItemDate.Text) ||
                lostItemCategories.SelectedIndex == 0)
            {
                FieldsNotFilledError.Text="Please fill in all required fields(marked with a *).";
                FieldsNotFilledError.Visible = true;
                return;
            }

            string fileExtension = Path.GetExtension(lostImageUpload.FileName).ToLower();
            string[] allowedExtensions = {".png"};
            string imageUrl = "~/Images/placeholder.png";
            byte[] imageBytes=null;
            if (Array.IndexOf(allowedExtensions, fileExtension) < 0)
            {
                ImageError.Text="Please upload a valid image file.";
                ImageError.Visible = true;
                return;
            } else if (lostImageUpload.HasFile && Array.IndexOf(allowedExtensions, fileExtension) >= 0)
            {
                string fileName = Path.GetFileName(lostImageUpload.FileName);
                imageUrl = "~/Images/" + fileName;
                lostImageUpload.SaveAs(Server.MapPath(imageUrl));
                using (BinaryReader br = new BinaryReader(lostImageUpload.PostedFile.InputStream))
                {
                    imageBytes = br.ReadBytes(lostImageUpload.PostedFile.ContentLength);
                }
            }


            int userId = Convert.ToInt32(Session["UserId"]);
            string username = Session["Username"].ToString();

            string connectionString = ConfigurationManager.ConnectionStrings["LostAndFoundDB"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO [dbo].[LostReport] (LostDate, LastLocation, UserId, Item_image, item_name, item_description, report_status, item_type, username) " +
                               "VALUES (@LostDate, @LastLocation, @UserId, @Item_image, @item_name, @item_description, @report_status, @item_type, @Username)";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@LostDate", Convert.ToDateTime(lostItemDate.Text));
                    cmd.Parameters.AddWithValue("@LastLocation", lostItemLocation.Text);
                    cmd.Parameters.AddWithValue("@UserId", userId); 
                    cmd.Parameters.AddWithValue("@Item_image", imageBytes);
                    cmd.Parameters.AddWithValue("@item_name", lostItemName.Text);
                    cmd.Parameters.AddWithValue("@item_description", lostItemDescription.Text);
                    cmd.Parameters.AddWithValue("@report_status", "Pending");
                    cmd.Parameters.AddWithValue("@item_type", lostItemCategories.SelectedItem.Text);
                    cmd.Parameters.AddWithValue("@Username", username);


                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }

            Response.Write("<script>alert('Lost item report submitted successfully.');</script>");
            lostItemName.Text = "";
            lostItemDescription.Text = "";
            lostItemLocation.Text = "";
            lostItemDate.Text = "";
            lostItemCategories.SelectedIndex = 0;
        }
    }
}
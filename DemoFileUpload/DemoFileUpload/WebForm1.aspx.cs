using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace DemoFileUpload
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        SqlConnection conn;

        protected void Page_Load(object sender, EventArgs e)
        {
            string conf = ConfigurationManager.ConnectionStrings["dbconfig"].ConnectionString;
            conn = new SqlConnection(conf);
            conn.Open();
        }

        protected void TextBox1_TextChanged(object sender, EventArgs e)
        {
            // Handle the change in the product name textbox if needed
        }

        protected void TextBoxPrice_TextChanged(object sender, EventArgs e)
        {
            // Handle the change in the price textbox if needed
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string product_name, product_file;
            decimal product_price;

            product_name = TextBox1.Text;
            product_price = Convert.ToDecimal(TextBoxPrice.Text);

            FileUpload1.SaveAs(Server.MapPath("MyFiles/") + Path.GetFileName(FileUpload1.FileName));
            product_file = "MyFiles/" + Path.GetFileName(FileUpload1.FileName);

            string q = "INSERT INTO Table2(Name, Price, Photo) VALUES('" + product_name + "', " + product_price + ", '" + product_file + "')";

            SqlCommand sqlCommand = new SqlCommand(q, conn);
            sqlCommand.ExecuteNonQuery();

            // Reload the data after the insertion
            displayProduct();
        }

        public void displayProduct()
        {
            string q = "SELECT * FROM Table2";
            SqlDataAdapter adapter = new SqlDataAdapter(q, conn);

            DataSet dataSet = new DataSet();
            adapter.Fill(dataSet);

            DataList1.DataSource = dataSet;
            DataList1.DataBind();

            if (DataList1.EditItemIndex != -1)
            {
                // If in edit mode, find the image control and set its visibility
                foreach (DataListItem item in DataList1.Items)
                {
                    if (item.ItemType == ListItemType.EditItem)
                    {
                        HtmlImage imgProduct = (HtmlImage)item.FindControl("imgProduct");
                        imgProduct.Visible = true;
                    }
                }
            }
        }




        protected void Button2_Click(object sender, EventArgs e)
        {
            displayProduct();
        }

        protected void DataList1_ItemCommand(object source, DataListCommandEventArgs e)
        {
            if (e.CommandName == "EditItem")
            {
                DataList1.EditItemIndex = e.Item.ItemIndex;
                displayProduct();
            }
            else if (e.CommandName == "CancelEdit")
            {
                DataList1.EditItemIndex = -1;
                displayProduct();
            }
            else if (e.CommandName == "DeleteItem")
            {
                int productID = Convert.ToInt32(DataList1.DataKeys[e.Item.ItemIndex]);
                DeleteProduct(productID);
                displayProduct();
            }
            else if (e.CommandName == "UpdateItem")
            {
                int itemIndex = e.Item.ItemIndex;
                TextBox textBoxEditName = (TextBox)e.Item.FindControl("TextBoxEditName");
                TextBox textBoxEditPrice = (TextBox)e.Item.FindControl("TextBoxEditPrice");

                string newName = textBoxEditName.Text;
                decimal newPrice = Convert.ToDecimal(textBoxEditPrice.Text);

                UpdateProduct(newName, newPrice);

                // Set the DataList back to normal mode after the update
                DataList1.EditItemIndex = -1;
                displayProduct();
            }
        }

        private void UpdateProduct(string newName, decimal newPrice)
        {
            int itemIndex = DataList1.EditItemIndex;
            int productID = Convert.ToInt32(DataList1.DataKeys[itemIndex]);

            string updateQuery = "UPDATE Table2 SET Name = @NewName, Price = @NewPrice WHERE ID = @ProductID";

            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconfig"].ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(updateQuery, connection))
                {
                    command.Parameters.AddWithValue("@NewName", newName);
                    command.Parameters.AddWithValue("@NewPrice", newPrice);
                    command.Parameters.AddWithValue("@ProductID", productID);

                    try
                    {
                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        // Log or display the exception
                        throw;
                    }
                }
            }
        }









        private void DeleteProduct(int productID)
        {
            string deleteQuery = "DELETE FROM Table2 WHERE ID = @ProductID";
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconfig"].ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(deleteQuery, connection))
                {
                    command.Parameters.AddWithValue("@ProductID", productID);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }


        protected void Button3_Click(object sender, EventArgs e)
        {
            // Handle the "Edit" button click event
            DataList1.EditItemIndex = ((DataListItem)((Button)sender).NamingContainer).ItemIndex;
            displayProduct();
        }

        protected void ButtonDelete_Click(object sender, EventArgs e)
        {
            // Handle the "Delete" button click event
            int selectedIndex = ((DataListItem)((Button)sender).NamingContainer).ItemIndex;
            int productID = Convert.ToInt32(DataList1.DataKeys[selectedIndex]);
            DeleteProduct(productID);
            displayProduct();
        }

        protected void ButtonUpdate_Click(object sender, EventArgs e)
        {
            TextBox textBoxEditName = (TextBox)((Button)sender).Parent.FindControl("TextBoxEditName");
            TextBox textBoxEditPrice = (TextBox)((Button)sender).Parent.FindControl("TextBoxEditPrice");

            string newName = textBoxEditName.Text;
            decimal newPrice = Convert.ToDecimal(textBoxEditPrice.Text);

            UpdateProduct(newName, newPrice);

            // Set the DataList back to normal mode after the update
            DataList1.EditItemIndex = -1;
            displayProduct();
        }



        protected void ButtonCancel_Click(object sender, EventArgs e)
        {
            DataList1.EditItemIndex = -1;
            ViewState["SelectedIndex"] = null; // Clear the selected index in ViewState
            displayProduct();
        }
    }
}
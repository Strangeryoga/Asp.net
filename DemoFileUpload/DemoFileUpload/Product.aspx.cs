using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Web.Script.Serialization;
using System.Web;
using System.Web.Services;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.UI.HtmlControls;
using System.Configuration;

namespace DemoFileUpload
{
    public partial class Product : System.Web.UI.Page
    {
        SqlConnection conn;

        protected List<ProductItem> GetProductData()
        {
            // Your logic to retrieve product data from a database or other source
            // Make sure to set the ImagePath property with correct image paths
            // This is a simplified example, replace it with your actual data retrieval logic

            var products = new List<ProductItem>
        {
            new ProductItem { ProductName = "Paper Fashion Solid Men's Round Neck Cotton Blend Half Sleeve T-Shirts ", Price = 199.99, ProductID = 1, ImagePath = "images/product-image-1.jpg" },
            new ProductItem { ProductName = "Lymio Men Sweatshirts ", Price = 229.99, ProductID = 2, ImagePath = "images/product-image-2.jpg" },
            new ProductItem { ProductName = "Allen Solly Men's Cotton Crew Neck Sweatshirt ", Price = 699.99, ProductID = 3, ImagePath = "images/product-image-3.jpg" },
            new ProductItem { ProductName = "U-TURN Men's Cotton Solid Formal/Semi Formal Shirt ", Price = 199.99, ProductID = 4, ImagePath = "images/product-image-4.jpg" },
            new ProductItem { ProductName = "Textured Shirts ", Price = 599.99, ProductID = 5, ImagePath = "images/product-image-5.jpg" },


            // Add more products as needed
        };

            return products;
        }

        protected void Page_Load(object sender, EventArgs e)
        {


            string conf = ConfigurationManager.ConnectionStrings["dbconfig"].ConnectionString;
            conn = new SqlConnection(conf);
            conn.Open();



        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            displayProduct();
        }

        protected void DataList1_ItemCommand(object source, DataListCommandEventArgs e)
        {
                
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

    }
        public class ProductItem
        {
            public string ProductName { get; set; }
            public double Price { get; set; }
            public int ProductID { get; set; }
            public string ImagePath { get; set; }
        }



    }
 
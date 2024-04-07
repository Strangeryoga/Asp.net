using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace CRUD2
{
    public partial class InsertProduct : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        private string GetConnectionString()
        {
            return "Data Source=DESKTOP-FROJFGN\\SQLEXPRESS;Initial Catalog=demo;Integrated Security=True;";
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            string name = txtName.Text;
            string description = txtDescription.Text;
            decimal price = Convert.ToDecimal(txtPrice.Text);
            int quantity = Convert.ToInt32(ddlQuantity.SelectedValue);

            using (SqlConnection con = new SqlConnection(GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("CRUD_Products", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "INSERT");
                cmd.Parameters.AddWithValue("@Name", name);
                cmd.Parameters.AddWithValue("@Description", description);
                cmd.Parameters.AddWithValue("@Price", price);
                cmd.Parameters.AddWithValue("@Quantity", quantity);
                con.Open();
                cmd.ExecuteNonQuery();
            }

            Response.Redirect("ListProducts.aspx");
        }

    }
}

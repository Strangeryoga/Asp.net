using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace CRUD2
{
    public partial class ListProducts : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindProducts();
            }
        }


        private string GetConnectionString()
        {
            return "Data Source=DESKTOP-FROJFGN\\SQLEXPRESS;Initial Catalog=demo;Integrated Security=True;";
        }

        private void BindProducts()
        {
            using (SqlConnection con = new SqlConnection(GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("CRUD_Products", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "SELECT");
                con.Open();
                GridView1.DataSource = cmd.ExecuteReader();
                GridView1.DataBind();
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            Button btnDelete = (Button)sender;
            string productId = btnDelete.CommandArgument;

            using (SqlConnection con = new SqlConnection(GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("CRUD_Products", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", productId);
                cmd.Parameters.AddWithValue("@Action", "DELETE");
                con.Open();
                cmd.ExecuteNonQuery();
            }

            BindProducts();
        }

        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridView1.EditIndex = e.NewEditIndex;
            BindProducts();
        }

        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridView1.EditIndex = -1;
            BindProducts();
        }

        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = GridView1.Rows[e.RowIndex];
            int productId = Convert.ToInt32(((Label)row.FindControl("lblEditId")).Text);
            string name = ((TextBox)row.FindControl("txtName")).Text;
            string description = ((TextBox)row.FindControl("txtDescription")).Text;
            decimal price = Convert.ToDecimal(((TextBox)row.FindControl("txtPrice")).Text);

            using (SqlConnection con = new SqlConnection(GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("CRUD_Products", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Name", name);
                cmd.Parameters.AddWithValue("@Description", description);
                cmd.Parameters.AddWithValue("@Price", price);
                cmd.Parameters.AddWithValue("@Id", productId);
                cmd.Parameters.AddWithValue("@Action", "UPDATE");
                con.Open();
                cmd.ExecuteNonQuery();
            }

            GridView1.EditIndex = -1;
            BindProducts();
        }


    }
}

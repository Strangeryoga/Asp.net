using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CrudProject
{
    public partial class index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DisplayData();
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            string name = txtName.Text;
            string surname = txtSurname.Text;

            // Insert into database
            string connectionString = ConfigurationManager.ConnectionStrings["dbconfig"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO UserInfo (Name, Surname) VALUES (@Name, @Surname)";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Name", name);
                command.Parameters.AddWithValue("@Surname", surname);
                connection.Open();
                command.ExecuteNonQuery();
            }

            // Refresh gridview
            DisplayData();
        }

        protected void DisplayData()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["dbconfig"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM UserInfo";
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                GridView1.DataSource = dt;
                GridView1.DataBind();
            }
        }

        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridView1.EditIndex = e.NewEditIndex;
            DisplayData();
        }

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int id = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Values["ID"]);

            // Delete from database
            string connectionString = ConfigurationManager.ConnectionStrings["dbconfig"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM UserInfo WHERE ID = @ID";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@ID", id);
                connection.Open();
                command.ExecuteNonQuery();
            }

            // Refresh gridview
            DisplayData();
        }

        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridView1.EditIndex = -1;
            DisplayData();
        }

        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            // Ensure the GridView is in edit mode
            GridViewRow row = GridView1.Rows[e.RowIndex];
            if (row != null && e.RowIndex > -1 && e.RowIndex < GridView1.Rows.Count)
            {
                // Get the updated values
                TextBox txtEditName = row.FindControl("txtEditName") as TextBox;
                TextBox txtEditSurname = row.FindControl("txtEditSurname") as TextBox;

                if (txtEditName != null && txtEditSurname != null)
                {
                    int id = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Values["ID"]);
                    string name = txtEditName.Text;
                    string surname = txtEditSurname.Text;

                    // Update the record in the database
                    string connectionString = ConfigurationManager.ConnectionStrings["dbconfig"].ConnectionString;
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        string query = "UPDATE UserInfo SET Name = @Name, Surname = @Surname WHERE ID = @ID";
                        SqlCommand command = new SqlCommand(query, connection);
                        command.Parameters.AddWithValue("@Name", name);
                        command.Parameters.AddWithValue("@Surname", surname);
                        command.Parameters.AddWithValue("@ID", id);
                        connection.Open();
                        command.ExecuteNonQuery();
                    }

                    // Exit edit mode
                    GridView1.EditIndex = -1;

                    // Refresh gridview
                    DisplayData();
                }


            }
        }

      




    }
}
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DemoFileUpload
{
    public partial class Register : System.Web.UI.Page
    {
        SqlConnection conn = new SqlConnection("Data Source=DESKTOP-8HLB8I7\\SQLEXPRESS;Initial Catalog=demo;Integrated Security=True;Encrypt=False");

        protected void Page_Load(object sender, EventArgs e)
        {
            string conf = ConfigurationManager.ConnectionStrings["dbconfig"].ConnectionString;

            conn = new SqlConnection(conf);
            conn.Open();

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string NAME, EMAIL, PASS;

            NAME = TextBox1.Text;
            EMAIL = TextBox2.Text;
            PASS = TextBox3.Text;

            string q = "insert into Register(FullName,Email,Password) values('" + NAME + "','" + EMAIL + "','" + PASS + "')";

            SqlCommand cmd = new SqlCommand(q, conn);
            cmd.ExecuteNonQuery();
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            Response.Redirect("Login.aspx");
        }
    }   
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EmailOTP
{
    public partial class VerifyOTP : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string uotp = TextBox1.Text;
            string eotp = Session["GOT"].ToString();

            if(uotp.Equals(eotp))
            {
                Response.Redirect("Dashboard.aspx");
            } else
            {
                Response.Write("<script>alert('Invalid OTP')</script>");
            }
        }
    }
}
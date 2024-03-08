using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.Services.Description;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace ProjectWeMate
{
    public partial class chataspx : System.Web.UI.Page
    {
        SqlConnection sqlconn = new SqlConnection(ConfigurationManager.ConnectionStrings["RegistrationConnectionString"].ToString());

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!User.Identity.IsAuthenticated)
            {
                Response.Redirect("Default.aspx");
            }
            else
            {
                CurrentSender.Text = Server.HtmlEncode(User.Identity.Name);

                if (!IsPostBack)
                {
                    // Check if the receiver's name is present  // Check if the receiver's name is present in the query string
                    string receiver = Request.QueryString["receiver"];
                    if (string.IsNullOrEmpty(receiver) || receiver.Equals("Group Chat", StringComparison.OrdinalIgnoreCase))
                    {
                        // It's a group chat
                        SetChatContext("Group");
                        // It's a group chat, so add the refresh meta tag
                        AddMetaRefresh();
                    }
                    else
                    {

                        // It's a one-to-one chat
                        SetChatContext("OneToOne");
                        // It's a one-to-one chat, set the receiver's name
                        CurrentRecevier.Text = Server.HtmlEncode(receiver);

                        // Check if the page is refreshed during a one-to-one chat
                        if (string.IsNullOrEmpty(Request.QueryString["receiver"]))
                        {
                            // Page is refreshed during a one-to-one chat, keep the receiver's name
                            CurrentRecevier.Text = receiver;
                        }
                        else
                        {
                            // Page is loaded for the first time or redirected from group chat, clear the receiver's name
                            CurrentRecevier.Text = "";
                        }


                        RemoveMetaRefresh();
                    }

                    LoadChatList();
                    MSGTextBox.Focus();
                    LoadingImage.Attributes.CssStyle.Add("opacity", "0");

                }

                int x = checkNewMessage();
                UnreadMsgCountLabel.Text = x.ToString();
                if (x != 0)
                {
                    LoadChatList();
                    this.Title = x.ToString() + " new messages";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Javascript", "javascript:playSound()", true);
                }
                else
                {
                    this.Title = "chat";
                }


            }


        }

        private void SetChatContext(string context)
        {
            Session["ChatContext"] = context;
        }

        private string GetChatContext()
        {
            return Session["ChatContext"] as string;
        }

        private void AddMetaRefresh()
        {
            HtmlMeta meta = new HtmlMeta();
            meta.HttpEquiv = "refresh";
            meta.Content = "10"; // Refresh every 10 seconds
            meta.ID = "refreshMeta";
            Page.Header.Controls.Add(meta);
        }

        private void RemoveMetaRefresh()
        {
            HtmlMeta meta = Page.Header.FindControl("refreshMeta") as HtmlMeta;
            if (meta != null)
            {
                Page.Header.Controls.Remove(meta);
            }
        }
        protected void ViewGroupChatButton_Click(object sender, EventArgs e)
        {
            // Set the receiver's name to empty to indicate it's a group chat
            CurrentRecevier.Text = "";

            // Reload the chat list to display group chats
            LoadChatList();

            // Set focus or any additional logic you need
        }


        protected void UsernameLabel_Click(object sender, EventArgs e)
        {
            CurrentRecevier.Text = Server.HtmlEncode(((LinkButton)sender).Text);
            LoadChatList();
            MsgPanel.Visible = true;
        }

        int checkNewMessage()
        {
            sqlconn.Open();

            string StrCmd = "Select COUNT(*) from  MsgTable where RecevierSeen = 0 and MsgReceiver = @receiver";
            SqlCommand sqlcmd = new SqlCommand(StrCmd, sqlconn);
            sqlcmd.Parameters.AddWithValue("@receiver", Server.HtmlEncode(CurrentSender.Text));
            int x = 0;
            x = Convert.ToInt32(sqlcmd.ExecuteScalar());
            sqlconn.Close();

            return x;
        }

       void LoadChatList()
        {
            DataSet ds = new DataSet();
            string StrCmd;
            SqlCommand sqlcmd;
            SqlDataAdapter sqlDA;

            try
            {
                sqlconn.Open();

                if (string.IsNullOrEmpty(CurrentRecevier.Text) || CurrentRecevier.Text.Equals("Hi, lets start chatting..."))
                {
                    // If CurrentRecevier is empty or contains the specified text, it's a group chat
                    StrCmd = "SELECT TOP 13 SenderUsername, MessageText FROM GroupMessages ORDER BY MessageId DESC"; 
                    sqlcmd = new SqlCommand(StrCmd, sqlconn);
                }
                else // Otherwise, it's a one-to-one chat
                {
                    StrCmd = "SELECT MsgSender AS SenderUsername, ChatMsg AS MessageText FROM MsgTable WHERE (MsgSender = @sender AND MsgReceiver = @receiver) OR (MsgSender = @ViseSender AND MsgReceiver = @ViseReceiver) "; 
                    sqlcmd = new SqlCommand(StrCmd, sqlconn);
                    sqlcmd.Parameters.AddWithValue("@sender", Server.HtmlEncode(CurrentSender.Text));
                    sqlcmd.Parameters.AddWithValue("@receiver", Server.HtmlEncode(CurrentRecevier.Text));
                    sqlcmd.Parameters.AddWithValue("@ViseSender", Server.HtmlEncode(CurrentRecevier.Text));
                    sqlcmd.Parameters.AddWithValue("@ViseReceiver", Server.HtmlEncode(CurrentSender.Text));
                }

                sqlDA = new SqlDataAdapter(sqlcmd);
                sqlDA.Fill(ds);

                DataList2.DataSource = ds.Tables[0];
                DataList2.DataBind();

                // Update the hidden field value to indicate that messages are loaded
                ScrollPositionHiddenField.Value = "bottom";

            }
            catch (Exception ex)
            {
                // Handle any exceptions
            }
            finally
            {
                sqlconn.Close();
            }

            seenAllMsg();
        }

        void seenAllMsg()
        {
            sqlconn.Open();

            string StrCmd = "update MsgTable set RecevierSeen = 1 where MsgReceiver = @MsgRec and MsgSender = @MsgSen";
            SqlCommand sqlcmd = new SqlCommand(StrCmd, sqlconn);
            sqlcmd.Parameters.AddWithValue("@MsgRec", Server.HtmlEncode(CurrentSender.Text));
            sqlcmd.Parameters.AddWithValue("@MsgSen", Server.HtmlEncode(CurrentRecevier.Text));
            sqlcmd.ExecuteNonQuery();
            sqlconn.Close();
        }

        protected void sendBTN_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Server.HtmlEncode(MSGTextBox.Text)))
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Javascript", "alert('Enter Message'); ", true);
                return;
            }

            if (!string.IsNullOrWhiteSpace(Server.HtmlEncode(CurrentRecevier.Text)))
            {
                // One-to-one chat
                string StrCmd = "AddMsgToTable @msg , @SenderName, @ReciverName";
                SqlCommand sqlcmd = new SqlCommand(StrCmd, sqlconn);
                sqlcmd.Parameters.AddWithValue("@msg", Server.HtmlEncode(MSGTextBox.Text));
                sqlcmd.Parameters.AddWithValue("@SenderName", Server.HtmlEncode(CurrentSender.Text));
                sqlcmd.Parameters.AddWithValue("@ReciverName", Server.HtmlEncode(CurrentRecevier.Text));
                sqlconn.Open();
                sqlcmd.ExecuteNonQuery();
                sqlconn.Close();
            }
            else
            {
                // Group chat
                string StrCmd = "INSERT INTO GroupMessages (SenderUsername, MessageText) VALUES (@SenderName, @Message)";
                SqlCommand sqlcmd = new SqlCommand(StrCmd, sqlconn);
                sqlcmd.Parameters.AddWithValue("@SenderName", Server.HtmlEncode(User.Identity.Name));
                sqlcmd.Parameters.AddWithValue("@Message", Server.HtmlEncode(MSGTextBox.Text));
                sqlconn.Open();
                sqlcmd.ExecuteNonQuery();
                sqlconn.Close();
            }

            MSGTextBox.Text = "";
            MSGTextBox.Focus();

            LoadChatList(); // Assuming this method updates the chat UI
            LoadingImage.Attributes.CssStyle.Add("opacity", "0");

        }


        protected void logout_Click(object sender, EventArgs e)
        {
            string OnlineStatusStr = "update UserDatabase set OnlineStatus = 0 where Username = @user";

            SqlCommand comm1 = new SqlCommand(OnlineStatusStr, sqlconn);
            comm1.Parameters.AddWithValue("@user", Server.HtmlEncode(CurrentSender.Text));
            sqlconn.Open();
            comm1.ExecuteNonQuery();
            sqlconn.Close();
            FormsAuthentication.SignOut();
            Response.Redirect("Default.aspx");
        }

        protected void Timer1_Tick(object sender, EventArgs e)
        {
            DataList1.DataBind();
            DataList3.DataBind();
            DataList4.DataBind();

        }

        protected string GetStyleForMsgList(string str)
        {
            if (string.Equals(Server.HtmlEncode(str), Server.HtmlEncode(CurrentSender.Text), StringComparison.OrdinalIgnoreCase))
            {
                return "SenderClass";
            }
            return "ReceiverClass";
        }
        protected string GetPerfactName(string str)
        {
            if (string.Equals(Server.HtmlEncode(str), Server.HtmlEncode(CurrentSender.Text), StringComparison.OrdinalIgnoreCase))
            {
                return "<span style='color:#efdab5'>You :</sapn>";
            }
            return "<span style='color:#efdab5'>" + Server.HtmlEncode(str) + " : </span>";
        }

        protected string GetWelcomeBanner(string str)
        {
            if (String.IsNullOrWhiteSpace(Server.HtmlEncode(str)))
                return "Group Chat";

            return str;
        }


    }
}
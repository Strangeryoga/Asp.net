using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI.WebControls;

namespace Documents
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Check if the page is not being loaded in response to a postback
            if (!Page.IsPostBack)
            {
                // If not a postback, bind the documents to the GridView
                BindDocumentsToGridView();
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            // Check if a file has been uploaded
            if (fileUpload.HasFile)
            {
                // Get document details from the user input
                string documentType = ddlDocumentType.SelectedValue;
                string fileName = fileUpload.FileName;
                byte[] fileData = fileUpload.FileBytes;

                // Save the document details to the database
                SaveToDatabase(documentType, fileName, fileData);

                // Clear form fields and bind documents to GridView
                ddlDocumentType.SelectedIndex = 0;
                fileUpload.Dispose();
                BindDocumentsToGridView();
            }
        }

        private void SaveToDatabase(string documentType, string fileName, byte[] fileData)
        {
            // Database connection string
            string connectionString = ConfigurationManager.ConnectionStrings["dbconfig"].ConnectionString;

            // Using statement ensures proper resource disposal
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Open the database connection
                connection.Open();

                // SQL query to insert document details into the database
                string query = $"INSERT INTO Documents1 (DocumentType, FileName, FileData, UploadDate) VALUES ('{documentType}', '{fileName}', @FileData, GETDATE())";

                // Using statement ensures proper resource disposal
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Add the binary data as a parameter
                    command.Parameters.AddWithValue("@FileData", fileData);

                    // Execute the SQL command
                    command.ExecuteNonQuery();
                }
            }
        }

        protected void btnShowDocuments_Click(object sender, EventArgs e)
        {
            // Bind documents to GridView when the button is clicked
            BindDocumentsToGridView();
        }

        private void BindDocumentsToGridView()
        {
            // Database connection string
            string connectionString = ConfigurationManager.ConnectionStrings["dbconfig"].ConnectionString;

            // Using statement ensures proper resource disposal
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Open the database connection
                connection.Open();

                // SQL query to select document details from the database
                string query = "SELECT DocumentID, DocumentType, FileName, UploadDate FROM Documents1";

                // Using statement ensures proper resource disposal
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Using SqlDataAdapter to fill a DataTable with the result of the SQL command
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        DataTable dtDocuments = new DataTable();
                        adapter.Fill(dtDocuments);
                        // Bind the DataTable to the GridView
                        gvDocuments.DataSource = dtDocuments;
                        gvDocuments.DataBind();
                    }
                }
            }
        }

        private byte[] GetDocumentBinaryData(int documentID)
        {
            byte[] fileData = null;

            // Database connection string
            string connectionString = ConfigurationManager.ConnectionStrings["dbconfig"].ConnectionString;

            // Using statement ensures proper resource disposal
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Open the database connection
                connection.Open();

                // SQL query to select file data from the database based on DocumentID
                string query = $"SELECT FileData FROM Documents1 WHERE DocumentID = {documentID}";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Using SqlDataReader to read the file data from the result of the SQL command
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        // Check if there is data and read it into the fileData array
                        if (reader.Read())
                        {
                            fileData = (byte[])reader["FileData"];
                        }
                    }
                }
            }

            return fileData;
        }

        protected void ViewDocument(object sender, EventArgs e)
        {
            // Event handler for viewing a document
            LinkButton lnkView = (LinkButton)sender;
            int documentID = Convert.ToInt32(lnkView.CommandArgument);

            // Get file data based on DocumentID    
            byte[] fileData = GetDocumentBinaryData(documentID);

            if (fileData != null)
            {
                // Store the file data in a session variable
                Session["DocumentData"] = fileData;

                // Redirect to a new page that will handle the display of the document
                Response.Redirect("ViewDocument.aspx", false);
            }
        }

        protected void DownloadFile(object sender, EventArgs e)
        {
            // Event handler for downloading a document
            int id = int.Parse((sender as LinkButton).CommandArgument);
            byte[] bytes;
            string FileName, DocumentType;

            // Database connection string
            string constr = "Data Source=DESKTOP-FROJFGN\\SQLEXPRESS;Initial Catalog=UserDB1;Integrated Security=True;Encrypt=False";

            // Using statement ensures proper resource disposal
            using (SqlConnection con = new SqlConnection(constr))
            {
                // Using SqlCommand to execute a query
                using (SqlCommand cmd = new SqlCommand())
                {
                    // SQL query to select file data, file name, and document type from the database based on DocumentID
                    cmd.CommandText = $"SELECT FileName, FileData, DocumentType FROM Documents1 WHERE DocumentId={id}";
                    cmd.Connection = con;
                    con.Open();

                    // Using SqlDataReader to read the file data, file name, and document type from the result of the SQL command
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        sdr.Read();
                        bytes = (byte[])sdr["FileData"];
                        DocumentType = sdr["DocumentType"].ToString();
                        FileName = sdr["FileName"].ToString();
                    }

                    // Close the database connection
                    con.Close();
                }
            }

            // Prepare and send the file for download
            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = DocumentType;
            Response.AppendHeader("Content-Disposition", "attachment; Filename=" + FileName);
            Response.BinaryWrite(bytes);
            Response.Flush();
            Response.End();
        }
    }
}

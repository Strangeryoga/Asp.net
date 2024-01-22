using System;

namespace Documents
{
    public partial class ViewDocument : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            byte[] fileData = Session["DocumentData"] as byte[];

            if (fileData != null)
            {
                // Clear any previous output from the buffer
                Response.Clear();

                // Set the content type to PDF (change it according to your document type)
                Response.ContentType = "application/pdf";

                // Set the content disposition to inline and specify the filename
                Response.AppendHeader("Content-Disposition", "inline; filename=document.pdf");

                // Write the file data to the response stream
                Response.BinaryWrite(fileData);

                // End the response
                Response.End();
            }
        }
    }
}

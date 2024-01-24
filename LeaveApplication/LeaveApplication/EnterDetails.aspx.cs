using System;
using System.IO;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;

namespace LeaveApplication
{
    public partial class EnterDetails : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["absent"] != null)
                {
                    // Retrieve the value from the session and set it to the txtAbsentDays TextBox
                    txtAbsentDays.Text = Session["absent"].ToString();

                    // Set the visibility of the txtAbsentDays TextBox based on the session value
                    txtAbsentDays.Visible = !string.IsNullOrEmpty(txtAbsentDays.Text);
                }
                txtAbsentDays.Visible = true;
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                string name = txtName.Text;

                double salary;
                if (!double.TryParse(txtSalary.Text, out salary))
                {
                    throw new Exception("Salary must be a valid numeric value.");
                }

                int absentDays;
                if (!int.TryParse(txtAbsentDays.Text, out absentDays))
                {
                    throw new Exception("Absent Days must be a valid integer value.");
                }

                double adjustedSalary = CalculateAdjustedSalary(salary, absentDays);

                Session["Name"] = name;
                Session["AdjustedSalary"] = adjustedSalary;

                lblMessage.Text = "Details submitted successfully!";
                lblMessage.ForeColor = System.Drawing.Color.Green;
                lblMessage.Visible = true;
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Error: " + ex.Message;
                lblMessage.ForeColor = System.Drawing.Color.Red;
                lblMessage.Visible = true;
            }
        }

        private double CalculateAdjustedSalary(double salary, int absentDays)
        {
            // Perform your salary calculation based on the number of absent days
            // For example, deduct 2% of salary for each absent day
            double deductionPercentage = 0.02;
            double adjustedSalary = salary - (salary * deductionPercentage * absentDays);

            return adjustedSalary;
        }

        protected void btnDownloadPDF_Click(object sender, EventArgs e)
        {
            // Generate PDF with details
            using (MemoryStream stream = new MemoryStream())
            {
                PdfWriter writer = new PdfWriter(stream);
                using (PdfDocument pdf = new PdfDocument(writer))
                {
                    Document document = new Document(pdf);

                    // Add content to the PDF (example)
                    document.Add(new Paragraph("Salary Slip"));
                    document.Add(new Paragraph("Name: " + txtName.Text));
                    document.Add(new Paragraph("Salary: " + txtSalary.Text));
                    document.Add(new Paragraph("Absent Days: " + txtAbsentDays.Text));
                    document.Add(new Paragraph("Adjusted Salary: " + Session["AdjustedSalary"]));

                    // Save the PDF
                    document.Close();
                }

                // Download the PDF
                Response.Clear();
                Response.ContentType = "application/pdf";
                Response.AddHeader("Content-Disposition", "attachment; filename=Details.pdf");
                Response.BinaryWrite(stream.ToArray());
                Response.End();
            }
        }
    }
}

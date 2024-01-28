﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Calendar
{
    public partial class Default : Page
    {
        // Connection string to the database
        string connectionString = "Data Source=DESKTOP-FROJFGN\\SQLEXPRESS;Initial Catalog=UserDB1;Integrated Security=True;Encrypt=False;Pooling=True";


        protected void Page_Load(object sender, EventArgs e)
        {
            // Ensure that async is supported during postback
            Page.RegisterAsyncTask(new PageAsyncTask(LoadDataAsync));
        }

        private async Task LoadDataAsync()
        {
            // Simulate an asynchronous delay
            await Task.Delay(1000);
        }


        protected void calEvents_DayRender(object sender, DayRenderEventArgs e)
        {
            // Highlight dates on the calendar that have events associated with them
            DateTime currentDate = e.Day.Date;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Check if there are events for the current date
                string checkQuery = "SELECT COUNT(*) FROM EventData WHERE EventDate = @EventDate";
                using (SqlCommand checkCmd = new SqlCommand(checkQuery, connection))
                {
                    checkCmd.CommandTimeout = 60; // Set a timeout for the command
                    checkCmd.Parameters.AddWithValue("@EventDate", currentDate);
                    int eventCount = (int)checkCmd.ExecuteScalar();

                    if (eventCount > 0)
                    {
                        e.Cell.BackColor = System.Drawing.Color.Yellow;

                        // Retrieve and display event text for the current date in the cell
                        string eventText = GetEventTextForDate(currentDate);

                        // Add a LiteralControl to the cell containing the date and event text
                        LiteralControl literalControl = new LiteralControl($"<br/>{eventText}");
                        e.Cell.Controls.Add(literalControl);
                    }
                }
            }
        }


        // Helper method to get event text for a given date
        private string GetEventTextForDate(DateTime date)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string selectQuery = "SELECT EventText FROM EventData WHERE EventDate = @EventDate";
                using (SqlCommand selectCmd = new SqlCommand(selectQuery, connection))
                {
                    selectCmd.Parameters.AddWithValue("@EventDate", date);

                    string eventText = selectCmd.ExecuteScalar()?.ToString();
                    return eventText ?? "No events for this date";
                }
            }
        }


        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            // Handle the submission of a new event
            DateTime selectedDate;
            if (DateTime.TryParseExact(txtDate.Text, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out selectedDate))
            {
                string eventText = txtEvent.Text;

                // Store the event data in the database
                StoreEventDataInDatabase(selectedDate, eventText);

                // Show an alert confirming the submission
                string alertMessage = $"Event on {selectedDate:yyyy-MM-dd}: {eventText}";
                ClientScript.RegisterStartupScript(this.GetType(), "alert", $"alert('{alertMessage}');", true);

            }
            else
            {
                // Handle invalid date format
            }
        }


        private void StoreEventDataInDatabase(DateTime selectedDate, string eventText)
        {
            // Store event data in the database if the date doesn't already have an associated event
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Check if there are existing events for the selected date
                string checkQuery = "SELECT COUNT(*) FROM EventData WHERE EventDate = @EventDate";
                using (SqlCommand checkCmd = new SqlCommand(checkQuery, connection))
                {
                    checkCmd.CommandTimeout = 60; // Set a timeout for the command
                    checkCmd.Parameters.AddWithValue("@EventDate", selectedDate);
                    int existingEventCount = (int)checkCmd.ExecuteScalar();

                    if (existingEventCount == 0)
                    {
                        // Insert the new event into the database
                        string insertQuery = "INSERT INTO EventData (EventDate, EventText) VALUES (@EventDate, @EventText)";
                        using (SqlCommand cmd = new SqlCommand(insertQuery, connection))
                        {
                            cmd.Parameters.AddWithValue("@EventDate", selectedDate);
                            cmd.Parameters.AddWithValue("@EventText", eventText);
                            cmd.ExecuteNonQuery();
                        }
                    }

                }
            }
        }


        protected async void calEvents_SelectionChanged(object sender, EventArgs e)
        {
            // Handle the selection change in the calendar and display events for the selected date
            DateTime selectedDate = calEvents.SelectedDate;

            // Retrieve events for the selected date asynchronously and display them in an alert
            List<string> eventsForSelectedDate = await GetEventsForSelectedDateAsync(selectedDate);

            string eventsText = eventsForSelectedDate.Any()
                ? string.Join("<br>", eventsForSelectedDate)
                : "No events for selected date";

            ScriptManager.RegisterStartupScript(this, GetType(), "alert", $"alert('{eventsText}');", true);
        }


        protected void btnShowEvents_Click(object sender, EventArgs e)
        {
            // Display all events in a GridView when the button is clicked
            DataTable allEvents = GetAllEventsDataTable();
            gvEvents.DataSource = allEvents;
            gvEvents.DataBind();
        }


        private DataTable GetAllEventsDataTable()
        {
            // Retrieve all events from the database and return them as a DataTable
            DataTable dataTable = new DataTable();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string selectQuery = "SELECT EventDate, EventText, ID FROM EventData";
                using (SqlCommand selectCmd = new SqlCommand(selectQuery, connection))
                {
                    selectCmd.CommandTimeout = 60; // Set a timeout for the command

                    using (SqlDataAdapter adapter = new SqlDataAdapter(selectCmd))
                    {
                        adapter.Fill(dataTable);
                    }
                }
            }
            return dataTable;
        }


        private async Task<List<string>> GetEventsForSelectedDateAsync(DateTime selectedDate)
        {
            // Retrieve events for the selected date and return them as a list of strings asynchronously
            List<string> eventsForSelectedDate = new List<string>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                string selectQuery = "SELECT EventText FROM EventData WHERE EventDate = @EventDate";
                using (SqlCommand selectCmd = new SqlCommand(selectQuery, connection))
                {
                    selectCmd.Parameters.AddWithValue("@EventDate", selectedDate);

                    using (SqlDataReader reader = await selectCmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            string eventText = reader["EventText"].ToString();
                            eventsForSelectedDate.Add(eventText);
                        }
                    }
                }
            }
            return eventsForSelectedDate;
        }


        protected void gvEvents_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvEvents.EditIndex = e.NewEditIndex;
            btnShowEvents_Click(sender, e); // Reload the GridView to enter edit mode
        }


        protected void gvEvents_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = gvEvents.Rows[e.RowIndex];
            string eventDate = ((TextBox)row.Cells[2].Controls[0]).Text;
            string eventText = ((TextBox)row.Cells[3].Controls[0]).Text;

            // Perform the update operation using the event date and event text
             UpdateEventDataInDatabase(DateTime.Parse(eventDate), eventText);

            gvEvents.EditIndex = -1; // Exit edit mode
            btnShowEvents_Click(sender, e); // Reload the GridView after the update
        }


        protected void gvEvents_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvEvents.EditIndex = -1; // Exit edit mode
            btnShowEvents_Click(sender, e); // Reload the GridView without updating
        }


        private void UpdateEventDataInDatabase(DateTime selectedDate, string eventText)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Update the event text in the database for the selected date
                string updateQuery = "UPDATE EventData SET EventText = @EventText WHERE EventDate = @EventDate";
                using (SqlCommand cmd = new SqlCommand(updateQuery, connection))
                {
                    cmd.Parameters.AddWithValue("@EventDate", selectedDate);
                    cmd.Parameters.AddWithValue("@EventText", eventText);
                    cmd.ExecuteNonQuery();
                }
            }
        }


        protected void gvEvents_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            GridViewRow row = gvEvents.Rows[e.RowIndex];

            // Ensure the cell contains a valid date string before attempting to parse
            string dateString = row.Cells[2].Text;
            DateTime eventDate;
            if (DateTime.TryParse(dateString, out eventDate))
            {
                // Perform the delete operation using the event date
                DeleteEventDataFromDatabase(eventDate);

                btnShowEvents_Click(sender, e); // Reload the GridView after the delete
            }
            else
            {
                // Handle the case where the date string is not valid
                // You may want to log an error or display a user-friendly message
            }
        }


        private void DeleteEventDataFromDatabase(DateTime eventDate)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Delete the event from the database for the specified date
                string deleteQuery = "DELETE FROM EventData WHERE EventDate = @EventDate";
                using (SqlCommand cmd = new SqlCommand(deleteQuery, connection))
                {
                    cmd.Parameters.AddWithValue("@EventDate", eventDate);
                    cmd.ExecuteNonQuery();
                }
            }
        }



    }
}
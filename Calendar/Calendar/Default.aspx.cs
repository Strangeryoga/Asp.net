using System;
using System.Collections.Generic;
using System.Configuration;
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
            DateTime currentDate = e.Day.Date;
            List<EventData> eventsForDate = GetEventsForDate(currentDate);
            if (eventsForDate.Any())
            {
                foreach (EventData eventData in eventsForDate)
                {
                    LiteralControl literalControl = new LiteralControl($"<div style=\"display:block;  margin-bottom: 5px;\"><span style=\"background-color:{GetEventColor(eventData.EventType)}; color: white; padding: 2px;\">{eventData.EventText}</span></div>");
                    e.Cell.Controls.Add(literalControl);
                }
            }
        }

        // Helper method to get the background color based on event type
        private string GetEventColor(string eventType)
        {
            switch (eventType)
            {
                case "Holiday":
                    return "red";
                case "Birthday":
                    return "blue";
                default:
                    return "yellow"; // Default color
            }
        }

        // Helper method to get event text for a given date
        private List<EventData> GetEventsForDate(DateTime date)
        {
            List<EventData> events = new List<EventData>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string selectQuery = "SELECT EventText, EventType FROM EventData WHERE EventDate = @EventDate";
                using (SqlCommand selectCmd = new SqlCommand(selectQuery, connection))
                {
                    selectCmd.Parameters.AddWithValue("@EventDate", date);
                    using (SqlDataReader reader = selectCmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string eventText = reader["EventText"].ToString();
                            string eventType = reader["EventType"].ToString();
                            events.Add(new EventData(date, eventText, eventType));
                        }
                    }
                }
            }
            return events;
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            DateTime selectedDate;
            if (DateTime.TryParseExact(txtDate.Text, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out selectedDate))
            {
                string eventText = txtEvent.Text;
                string eventType = ddlEventType.SelectedValue;
                StoreEventDataInDatabase(selectedDate, eventText, eventType);
                string alertMessage = $"Event on {selectedDate:yyyy-MM-dd}: {eventText}";
                ClientScript.RegisterStartupScript(this.GetType(), "alert", $"alert('{alertMessage}');", true);
            }
            else
            {
                // Handle invalid date format
            }
        }

        private void StoreEventDataInDatabase(DateTime selectedDate, string eventText, string eventType)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string insertQuery = "INSERT INTO EventData (EventDate, EventText, EventType) VALUES (@EventDate, @EventText, @EventType)";
                using (SqlCommand cmd = new SqlCommand(insertQuery, connection))
                {
                    cmd.Parameters.AddWithValue("@EventDate", selectedDate);
                    cmd.Parameters.AddWithValue("@EventText", eventText);
                    cmd.Parameters.AddWithValue("@EventType", eventType);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        protected async void calEvents_SelectionChanged(object sender, EventArgs e)
        {
            DateTime selectedDate = calEvents.SelectedDate;
            List<string> eventsForSelectedDate = await GetEventsForSelectedDateAsync(selectedDate);
            string eventsText = eventsForSelectedDate.Any() ? string.Join("<br>", eventsForSelectedDate) : "No events for selected date";
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", $"alert('{eventsText}');", true);
        }

        protected void btnShowEvents_Click(object sender, EventArgs e)
        {
            DataTable allEvents = GetAllEventsDataTable();
            gvEvents.DataSource = allEvents;
            gvEvents.DataBind();
        }

        private DataTable GetAllEventsDataTable()
        {
            DataTable dataTable = new DataTable();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string selectQuery = "SELECT EventDate, EventText, ID FROM EventData";
                using (SqlCommand selectCmd = new SqlCommand(selectQuery, connection))
                {
                    selectCmd.CommandTimeout = 60;
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
            BindGridView();
        }

        protected void gvEvents_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = gvEvents.Rows[e.RowIndex];
            int eventId = Convert.ToInt32(gvEvents.DataKeys[e.RowIndex].Values["ID"]);
            string eventDate = ((TextBox)row.Cells[2].Controls[0]).Text;
            string eventText = ((TextBox)row.Cells[3].Controls[0]).Text;
            UpdateEventDataInDatabase(eventId, DateTime.Parse(eventDate), eventText);
            gvEvents.EditIndex = -1;
            BindGridView();
        }

        protected void gvEvents_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvEvents.EditIndex = -1;
            BindGridView();
        }

        private void UpdateEventDataInDatabase(int eventId, DateTime selectedDate, string eventText)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string updateQuery = "UPDATE EventData SET EventText = @EventText, EventDate = @EventDate WHERE ID = @EventId";
                using (SqlCommand cmd = new SqlCommand(updateQuery, connection))
                {
                    cmd.Parameters.AddWithValue("@EventId", eventId);
                    cmd.Parameters.AddWithValue("@EventText", eventText);
                    cmd.Parameters.AddWithValue("@EventDate", selectedDate);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        protected void gvEvents_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int eventId = Convert.ToInt32(gvEvents.DataKeys[e.RowIndex].Values["ID"]);
            DeleteEventDataFromDatabase(eventId);
            BindGridView();
        }

        private void BindGridView()
        {
            DataTable allEvents = GetAllEventsDataTable();
            gvEvents.DataSource = allEvents;
            gvEvents.DataBind();
        }

        private void DeleteEventDataFromDatabase(int eventId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string deleteQuery = "DELETE FROM EventData WHERE ID = @EventId";
                using (SqlCommand cmd = new SqlCommand(deleteQuery, connection))
                {
                    cmd.Parameters.AddWithValue("@EventId", eventId);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}

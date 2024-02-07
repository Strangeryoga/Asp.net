using System;

public class EventData
{
    public DateTime EventDate { get; set; }
    public string EventText { get; set; }
    public string EventType { get; set; }

    public EventData(DateTime eventDate, string eventText, string eventType)
    {
        EventDate = eventDate;
        EventText = eventText;
        EventType = eventType;
    }
}

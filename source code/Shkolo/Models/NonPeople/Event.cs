using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shkolo.Models
{
    public class Event
    {
        public Event(long organiserID, string title, string date, string onlineOffline, string description, int duration)
        {
            OrganiserID = organiserID;
            Title = title;
            Date = date;
            OnlineOffline = onlineOffline;
            Description = description;
            Duration = duration;
        }

        public long ID { get; set; }
        public long OrganiserID { get; set; }
        public string Title { get; set; }
        public string Date { get; set; }
        public int Duration { get; set; }
        public string OnlineOffline { get; set; }
        public string Description { get; set; }
    }
}

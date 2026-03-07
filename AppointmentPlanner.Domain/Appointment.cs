using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace AppointmentPlanner.Domain
{
    public class Appointment
    {
        public string Title { get; set; }
        public DateTime Starttime { get; set; }
        public DateTime Endtime { get; set; }
        public int ParticipantsCount { get; set; }
        public Room Room { get; set; }
        public bool IsCancelled { get; set; }
        public DateTime CreatedAt { get; set; }

        public TimeSpan Duartion { get;}

        public Appointment(string title, DateTime starttime, DateTime endtime, int participantsCount, DateTime createdAt)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentException("Title mag niet leeg zijn.");

            if (endtime > starttime)
                throw new AggregateException("EndTime moet later zijn dan StartTime.");

            if (participantsCount < 1)
                throw new AggregateException("ParticipantsCount moet >= 1 zijn.");

            // if (createdAt < DateTime())
            //  throw new AggregateException("ParticipantsCount moet >= 1 zijn.");

            title = Title;
            starttime = Starttime;
            endtime = Endtime;
            participantsCount = ParticipantsCount;
        }
    }
}
